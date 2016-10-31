using SPW.Common;
using SPW.DAL;
using SPW.DataService;
using SPW.Model;
using SPW.UI.Web.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPW.UI.Web.Page
{
    public partial class PayInSlip : System.Web.UI.Page
    {
        private string ThaiBaht(string txt)
        {
            string bahtTxt, n, bahtTH = "";
            double amount;
            try { amount = Convert.ToDouble(txt); }
            catch { amount = 0; }
            bahtTxt = amount.ToString("####.00");
            string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
            string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน", "สิบ", "ร้อย", "พัน" };
            string[] temp = bahtTxt.Split('.');
            string intVal = temp[0];
            string decVal = temp[1];
            if (Convert.ToDouble(bahtTxt) == 0)
                bahtTH = "ศูนย์บาท";
            else
            {
                for (int i = 0; i < intVal.Length; i++)
                {
                    n = intVal.Substring(i, 1);
                    if (n != "0")
                    {
                        if ((i == (intVal.Length - 1)) && (n == "1"))
                            bahtTH += "เอ็ด";
                        else if ((i == (intVal.Length - 2)) && (n == "2"))
                            bahtTH += "ยี่";
                        else if ((i == (intVal.Length - 2)) && (n == "1"))
                            bahtTH += "";
                        else
                        {
                            if (((intVal.Length - i) - 1) > 6)
                            {
                                if (n == "1")
                                {
                                    bahtTH += "";
                                }
                                else if (n == "2")
                                {
                                    bahtTH += "ยี่";
                                }
                                else
                                {
                                    bahtTH += num[Convert.ToInt32(n)];
                                }
                            }
                            else if (((intVal.Length - i) - 1) > 5)
                            {
                                if (n == "1" && intVal.Length > 7)
                                {
                                    bahtTH += "เอ็ด";
                                }
                                else
                                {
                                    bahtTH += num[Convert.ToInt32(n)];
                                }
                            }
                            else if (((intVal.Length - i) - 1) > 6)
                            {
                                if (n == "2")
                                {
                                    bahtTH += "ยี่";
                                }
                                else
                                {
                                    bahtTH += num[Convert.ToInt32(n)];
                                }
                            }
                            else
                            {
                                bahtTH += num[Convert.ToInt32(n)];
                            }
                        }
                        bahtTH += rank[(intVal.Length - i) - 1];
                    }
                    else if ((intVal.Length - i) == 7)
                    {
                        bahtTH += "ล้าน";
                    }
                }
                if (decVal == "00")
                    bahtTH += "บาท";
                else
                {
                    bahtTH += "บาท";
                    for (int i = 0; i < decVal.Length; i++)
                    {
                        n = decVal.Substring(i, 1);
                        if (n != "0")
                        {
                            if ((i == decVal.Length - 1) && (n == "1"))
                                bahtTH += "เอ็ด";
                            else if ((i == (decVal.Length - 2)) && (n == "2"))
                                bahtTH += "ยี่";
                            else if ((i == (decVal.Length - 2)) && (n == "1"))
                                bahtTH += "";
                            else
                                bahtTH += num[Convert.ToInt32(n)];
                            bahtTH += rank[(decVal.Length - i) - 1];
                        }
                    }
                    bahtTH += "สตางค์";
                }
            }
            return bahtTH;
        }

        private AccountMastService _accountMastService;
        private PayInTranService _payInTranService;
        private StoreService _storeService;
        private DataServiceEngine _dataServiceEngine;

        private void ReloadPageEngine()
        {
            if (Session["DataServiceEngine"] != null)
            {
                _dataServiceEngine = (DataServiceEngine)Session["DataServiceEngine"];
                InitialDataService();
            }
            else
            {
                CreatePageEngine();
            }
        }

        private void InitialDataService()
        {
            _accountMastService = (AccountMastService)_dataServiceEngine.GetDataService(typeof(AccountMastService));
            _payInTranService = (PayInTranService)_dataServiceEngine.GetDataService(typeof(PayInTranService));
            _storeService = (StoreService)_dataServiceEngine.GetDataService(typeof(StoreService));
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                //txtStartDate.Text = DateTime.Now.ToShortDateString();
                string date = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                txtDatePayIn.Text = date;
                txtStartDate.Text = date;
                CreatePageEngine();
                InitialData();
                AutoCompleteStoreName();
                //AutoCompleteBranceName();
                AutoCompleteBankName();
            }
            else
            {
                ReloadPageEngine();
            }
        }

        private void InitialData()
        {
            if (Request.QueryString["id"] != null)
            {
                List<PAYIN_TRANS> lstPayIn = _payInTranService.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
                lstPayIn = lstPayIn.Where(x => x.PAYIN_DATE == Convert.ToDateTime(Request.QueryString["date"].ToString()) && x.ACCOUNT_ID == Request.QueryString["accid"].ToString()).ToList();
                lstPayIn = lstPayIn.OrderBy(x => x.PAYIN_DATE).ThenBy(x => x.PAYIN_SEQ_NO).ThenBy(x => x.CHQ_SEQ_NO).ToList();
                grdBank.DataSource = lstPayIn;
                grdBank.DataBind();
                Session["PAYIN_PRINT"] = lstPayIn;
                Session.Remove("PAYIN");
                //txtStartDate.Text = lstPayIn[0].PAYIN_DATE.ToShortDateString();
                txtDatePayIn.Text = lstPayIn[0].PAYIN_DATE.ToString("dd/MM/yyyy");
                txtDatePayIn.Enabled = false;
                txtStartDate.Enabled = false;
                txtAmount.Enabled = false;
                txtAccountName.Enabled = false;
                txtBankCheck.Enabled = false;
                txtBranceName.Enabled = false;
                txtCheck.Enabled = false;
                txtStoreName.Enabled = false;

                decimal tmpTotalAmt = 0;
                foreach (PAYIN_TRANS pt in lstPayIn)
                {
                    tmpTotalAmt += pt.CHQ_AMOUNT;
                }
                if (lstPayIn[0].BANK_SH_NAME.Equals("TMB"))
                {
                    rbBankThai.Checked = true;
                    rbBankKrungThai.Checked = false;
                }
                else
                {
                    rbBankKrungThai.Checked = true;
                    rbBankThai.Checked = false;
                }

                ddlAccountMast.Items.Clear();
                ddlAccountMast.Items.Add(new ListItem("กรุณาเลือก", "0"));
                txtAccountName.Text = string.Empty;
                var list = _accountMastService.GetAllBank(rbBankThai.Checked ? 1 : 2);
                foreach (var item in list)
                {
                    ddlAccountMast.Items.Add(new ListItem(item.ACCOUNT_ID, item.ACCOUNT_ID.ToString()));
                }

                lblNumAmount.Text = tmpTotalAmt.ToString("#,#.00#") + " บาท";
                lblAmount.Text = ThaiBaht(tmpTotalAmt.ToString());
                ddlAccountMast.SelectedValue = lstPayIn[0].ACCOUNT_ID;
                btnAdd.Visible = false;
                //btnSave.Visible = false;
                btnPrint1Submit.Visible = true;
                btnPrintXSubmit.Visible = true;
                btnPrint2Submit.Visible = true;
                lbl1.Visible = true;
                lbl2.Visible = true;
                ddlAccountMast.Enabled = false;
                rbBankKrungThai.Enabled = false;
                rbBankThai.Enabled = false;
                grdBank.Columns[4].Visible = false;
                dropdown();
                txtPayInSeq.Enabled = false;
                txtPageSeq.Enabled = false;
            }
            else
            {
                SQLUtility sql = new SQLUtility();
                int count = sql.GetCount(@"SELECT TOP 1 PAYIN_SEQ_NO FROM PAYIN_TRANS WHERE PAYIN_DATE = CONVERT(char(10), GetDate(),126) GROUP BY PAYIN_SEQ_NO ORDER BY PAYIN_SEQ_NO DESC");
                txtPayInSeq.Text = (count + 1).ToString();
                txtPageSeq.Text = "1";
                ddlAccountMast.Items.Clear();
                ddlAccountMast.Items.Add(new ListItem("กรุณาเลือก", "0"));
                txtAccountName.Text = string.Empty;
                var list = _accountMastService.GetAllBank(1);
                foreach (var item in list)
                {
                    ddlAccountMast.Items.Add(new ListItem(item.ACCOUNT_ID, item.ACCOUNT_ID.ToString()));
                }
                Session.Remove("PAYIN");
                Session.Remove("PAYIN_PRINT");
                grdBank.DataSource = null;
                grdBank.DataBind();
            }
        }

        private void dropdown()
        {
            if (!ddlAccountMast.SelectedValue.Equals("0"))
            {
                ACCOUNT_MAST accountMast = _accountMastService.Select(ddlAccountMast.SelectedValue);
                txtAccountName.Text = accountMast.ACCOUNT_NAME;
                txtBranceName.Text = accountMast.BANK_BRH_NAME;
            }
            else
            {
                txtAccountName.Text = string.Empty;
                txtBranceName.Text = string.Empty;
            }
        }

        protected void ddlAccountMast_SelectedIndexChanged(object sender, EventArgs e)
        {
            dropdown();

            btnPrint1Submit.Visible = false;
            btnPrint2Submit.Visible = false;
            btnPrintXSubmit.Visible = false;
            lbl1.Visible = false;
            lbl2.Visible = false;
            if (grdBank.Enabled == false)
            {
                string date = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                txtDatePayIn.Text = date;
                txtStartDate.Text = date;
                grdBank.Enabled = true;
                grdBank.DataSource = null;
                grdBank.DataBind();
                if (Session["PAYIN"] == null)
                {
                    SQLUtility sql = new SQLUtility();
                    txtPageSeq.Text = "1";
                    txtPayInSeq.Text = (sql.GetCount(@"SELECT TOP 1 PAYIN_SEQ_NO FROM PAYIN_TRANS WHERE PAYIN_DATE = '" + convertToDateUSA(txtDatePayIn.Text) + "' GROUP BY PAYIN_SEQ_NO ORDER BY PAYIN_SEQ_NO DESC") + 1).ToString();

                }
                grdBank.Columns[4].Visible = true;
            }
        }

        protected void rbBankThai_CheckedChanged(object sender, EventArgs e)
        {
            ddlAccountMast.Items.Clear();
            ddlAccountMast.Items.Add(new ListItem("กรุณาเลือก", "0"));
            txtAccountName.Text = string.Empty;
            var list = _accountMastService.GetAllBank(1);
            foreach (var item in list)
            {
                ddlAccountMast.Items.Add(new ListItem(item.ACCOUNT_ID, item.ACCOUNT_ID.ToString()));
            }
        }

        protected void rbBankKrungThai_CheckedChanged(object sender, EventArgs e)
        {
            ddlAccountMast.Items.Clear();
            ddlAccountMast.Items.Add(new ListItem("กรุณาเลือก", "0"));
            txtAccountName.Text = string.Empty;
            var list = _accountMastService.GetAllBank(2);
            foreach (var item in list)
            {
                ddlAccountMast.Items.Add(new ListItem(item.ACCOUNT_ID, item.ACCOUNT_ID.ToString()));
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                STORE itemStore = txtStoreName.Text != "" ? _storeService.GetStoreID(txtStoreName.Text) : null;
                if (itemStore == null)
                {
                    itemStore = new STORE();
                    itemStore.STORE_ID = 0;
                }

                List<PAYIN_TRANS> lstPayIn = new List<PAYIN_TRANS>();
                if (Session["PAYIN"] == null)
                {
                    Session["PAYIN"] = lstPayIn;
                }
                else
                {
                    lstPayIn = Session["PAYIN"] as List<PAYIN_TRANS>;
                }

                PAYIN_TRANS tmpItem = new PAYIN_TRANS();
                tmpItem.CHQ_AMOUNT = Convert.ToDecimal(txtAmount.Text);
                tmpItem.CHQ_BANK = txtBankCheck.Text;
                tmpItem.CHQ_BR_BANK = "";
                tmpItem.STORE_ID_PAID = itemStore.STORE_ID;
                tmpItem.STORE_NAME_PAID = txtStoreName.Text;
                tmpItem.CHQ_NO = txtCheck.Text;
                tmpItem.CHQ_SEQ_NO = lstPayIn.Count() + 1;
                tmpItem.UPDATE_DATE = DateTime.Now;
                //tmpItem.PAYIN_DATE = Convert.ToDateTime(txtStartDate.Text.ToString());
                tmpItem.CHQ_DATE = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                lstPayIn.Add(tmpItem);
                grdBank.DataSource = lstPayIn;
                grdBank.DataBind();

                SumAmt();
                ClearScreen();
                btnPrint1Submit.Visible = true;
                btnPrint2Submit.Visible = true;
                btnPrintXSubmit.Visible = true;
                lbl1.Visible = true;
                lbl2.Visible = true;

                ddlAccountMast.Enabled = false;
                rbBankKrungThai.Enabled = false;
                rbBankThai.Enabled = false;
                rbPayin.Enabled = false;
                txtPageSeq.Enabled = false;
                txtPayInSeq.Enabled = false;
                txtDatePayIn.Enabled = false;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.ToString();
                danger.Visible = true;
                DebugLog.WriteLog(ex.ToString());
            }
        }

        private void SumAmt()
        {
            List<PAYIN_TRANS> lstPayIn = new List<PAYIN_TRANS>();
            if (Session["PAYIN"] != null)
            {
                lstPayIn = Session["PAYIN"] as List<PAYIN_TRANS>;
            }

            decimal tmpTotalAmt = 0;
            foreach (PAYIN_TRANS pt in lstPayIn)
            {
                tmpTotalAmt += pt.CHQ_AMOUNT;
            }

            lblNumAmount.Text = tmpTotalAmt.ToString("#,#.00#") + " บาท";
            lblAmount.Text = ThaiBaht(tmpTotalAmt.ToString());
        }

        private decimal GetSumAmt()
        {
            List<PAYIN_TRANS> lstPayIn = new List<PAYIN_TRANS>();
            if (Session["PAYIN_PRINT"] == null)
            {
                if (Session["PAYIN"] == null)
                {
                    Session["PAYIN"] = lstPayIn;
                }
                else
                {
                    lstPayIn = Session["PAYIN"] as List<PAYIN_TRANS>;
                }
            }
            else
            {
                lstPayIn = Session["PAYIN_PRINT"] as List<PAYIN_TRANS>;
            }

            decimal tmpTotalAmt = 0;
            foreach (PAYIN_TRANS pt in lstPayIn)
            {
                tmpTotalAmt += pt.CHQ_AMOUNT;
            }
            return tmpTotalAmt;
        }

        private void ClearScreen()
        {
            txtAmount.Text = "";
            txtBankCheck.Text = "";
            txtCheck.Text = "";
            txtStoreName.Text = "";
        }

        private bool Save()
        {
            try
            {
                lblError.Text = "";
                danger.Visible = false;
                USER userItem = Session["user"] as USER;
                List<PAYIN_TRANS> lstPayIn = new List<PAYIN_TRANS>();
                if (Session["PAYIN"] == null)
                {
                    Session["PAYIN"] = lstPayIn;
                }
                else
                {
                    lstPayIn = Session["PAYIN"] as List<PAYIN_TRANS>;
                }

                decimal tmpTotalAmt = 0;
                foreach (PAYIN_TRANS pt in lstPayIn)
                {
                    tmpTotalAmt += pt.CHQ_AMOUNT;
                }

                ACCOUNT_MAST accountMast = _accountMastService.Select(ddlAccountMast.SelectedValue);
                SQLUtility sql = new SQLUtility();
                int count = sql.GetCount(@"SELECT TOP 1 PAYIN_SEQ_NO FROM PAYIN_TRANS WHERE PAYIN_DATE = '" + convertToDateUSA(txtDatePayIn.Text) + "' GROUP BY PAYIN_SEQ_NO ORDER BY PAYIN_SEQ_NO DESC");
                int payInSeq = 0;
                if (rbPayin.Checked)
                {
                    payInSeq = Convert.ToInt32(txtPayInSeq.Text);
                }
                else
                {
                    payInSeq = txtPageSeq.Text == "1" ? count + 1 : Convert.ToInt32(txtPayInSeq.Text);
                }

                foreach (PAYIN_TRANS tmpItem in lstPayIn)
                {
                    tmpItem.ACCOUNT_ID = accountMast.ACCOUNT_ID;
                    tmpItem.ACCOUNT_NAME = accountMast.ACCOUNT_NAME;
                    tmpItem.BANK_NAME = accountMast.BANK_NAME;
                    tmpItem.BANK_SH_NAME = accountMast.BANK_SH_NAME;
                    tmpItem.CREATE_DATE = DateTime.Now;
                    tmpItem.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    tmpItem.PAYIN_APPROVE_ID = 1;
                    tmpItem.PAYIN_SEQ_NO = payInSeq;
                    tmpItem.PAYIN_TOTAL_AMOUNT = 0; //txtPageSeq.Text == "1" ? tmpTotalAmt : sql.GetAmount("SELECT SUM(CHQ_AMOUNT) FROM [SPW].[dbo].[PAYIN_TRANS] WHERE PAYIN_SEQ_NO = " + payInSeq + " AND PAYIN_DATE = '" + convertToDateUSA(lstPayIn[0].PAYIN_DATE.ToString("dd-MM-yyyy")) + "'") + tmpTotalAmt;
                    tmpItem.SYE_DEL = false;
                    tmpItem.UPDATE_DATE = DateTime.Now;
                    tmpItem.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    tmpItem.PAYIN_TYPE_PRINT = "";
                    tmpItem.PAYIN_DATE = DateTime.ParseExact(txtDatePayIn.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (txtPageSeq.Text != "1")
                    {
                        tmpItem.CHQ_SEQ_NO += ((Convert.ToInt32(txtPageSeq.Text) - 1) * 25);
                    }
                }

                _payInTranService.AddList(lstPayIn);

                //try
                //{
                //    sql.SumAmount("UPDATE PAYIN_TRANS SET PAYIN_TOTAL_AMOUNT = " + lstPayIn[0].PAYIN_TOTAL_AMOUNT + "  WHERE PAYIN_SEQ_NO = " + payInSeq + " AND PAYIN_DATE = '" + convertToDateUSA(lstPayIn[0].PAYIN_DATE.ToString("dd-MM-yyyy")) + "'");
                //}
                //catch (Exception ex)
                //{
                //    DebugLog.WriteLog("SQL UPDATE ERROR : " + "UPDATE PAYIN_TRANS SET PAYIN_TOTAL_AMOUNT = " + lstPayIn[0].PAYIN_TOTAL_AMOUNT + "  WHERE PAYIN_SEQ_NO = " + payInSeq);
                //    DebugLog.WriteLog(ex.ToString());
                //}

                //Session["PAYIN_PRINT"] = ObjectCopier.Clone<List<PAYIN_TRANS>>(lstPayIn);

                Session["PAYIN_PRINT"] = lstPayIn;
                lbl1.Visible = true;
                lbl2.Visible = true;
                Session.Remove("PAYIN");

                SumAmt();

                rbBankKrungThai.Enabled = true;
                rbBankThai.Enabled = true;
                rbPayin.Enabled = true;
                txtDatePayIn.Enabled = true;
                ddlAccountMast.Enabled = true;
                //ddlAccountMast.SelectedValue = "0";
                grdBank.Enabled = false;
                if (lstPayIn.Count() > 0)
                {
                    grdBank.Columns[4].Visible = false;
                }

                return true;
            }
            catch (SqlException sex)
            {
                if (sex.Number == 2601)
                {
                    lblError.Text = "บันทึกข้อมูลไม่สำเร็จ เนื่องจากเลขที่ PayIn ซ้ำ";
                    danger.Visible = true;
                    DebugLog.WriteLog("บันทึกข้อมูลไม่สำเร็จ เนื่องจากเลขที่ PayIn ซ้ำ");
                    List<PAYIN_TRANS> lstPayIn = Session["PAYIN"] as List<PAYIN_TRANS>;
                    foreach (var item in lstPayIn)
                    {
                        DebugLog.WriteLog("ACCOUNT_ID:" + item.ACCOUNT_ID + "|PAYIN_DATE:" + item.PAYIN_DATE + "|PAYIN_SEQ_NO:" + item.PAYIN_SEQ_NO + "|CHQ_SEQ_NO:" + item.CHQ_SEQ_NO);
                    }
                }
                DebugLog.WriteLog(sex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                lblError.Text = "บันทึกข้อมูลไม่สำเร็จ";
                danger.Visible = true;
                DebugLog.WriteLog(ex.ToString());

                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void AutoCompleteStoreName()
        {
            List<string> nameList = SearchAutoCompleteDataService.Search("STORE", "STORE_NAME", "STORE_NAME", "");
            string str = "";
            if (nameList != null)
            {
                for (int i = 0; i < nameList.Count; i++)
                {
                    str = str + '"' + nameList[i].ToString() + '"' + ',';
                }
                if (str != "")
                {
                    str = str.Remove(str.Length - 1);
                }
                str = "[" + str + "]";
                txtStoreName.Attributes.Add("data-source", str);
            }
        }

        private void AutoCompleteBankName()
        {
            List<string> nameList = SearchAutoCompleteDataService.SearchGroupBy("PAYIN_TRANS", "CHQ_BANK", "CHQ_BANK", "", "CHQ_BANK");
            string str = "";
            if (nameList != null)
            {
                for (int i = 0; i < nameList.Count; i++)
                {
                    str = str + '"' + nameList[i].ToString() + '"' + ',';
                }
                if (str != "")
                {
                    str = str.Remove(str.Length - 1);
                }
                str = "[" + str + "]";
                txtBankCheck.Attributes.Add("data-source", str);
            }
        }

        protected void btnPrint1Submit_Click(object sender, EventArgs e)
        {
            try
            {
                bool isSave = false;
                if (Session["PAYIN"] != null)
                {
                    isSave = Save();
                }
                else
                {
                    isSave = true;
                }
                if (isSave)
                {

                    List<PAYIN_TRANS> lstPayIn = new List<PAYIN_TRANS>();
                    if (Session["PAYIN_PRINT"] == null)
                    {
                        Session["PAYIN_PRINT"] = lstPayIn;
                    }
                    else
                    {
                        lstPayIn = Session["PAYIN_PRINT"] as List<PAYIN_TRANS>;
                    }

                    decimal tmpTotalAmt = 0;
                    foreach (PAYIN_TRANS pt in lstPayIn)
                    {
                        tmpTotalAmt += pt.CHQ_AMOUNT;
                    }

                    SPW.UI.Web.Reports.PayInSlip ds = new SPW.UI.Web.Reports.PayInSlip();
                    DataTable payInSlipMain = ds.Tables["MAIN"];
                    DataRow drPayInSlipMain = payInSlipMain.NewRow();

                    drPayInSlipMain["ACCOUNT_NAME"] = txtAccountName.Text;
                    drPayInSlipMain["TEL"] = "02-961-6686-7";
                    drPayInSlipMain["AMOUNT_NUM"] = GetSumAmt().ToString("#,#.00#");
                    drPayInSlipMain["AMOUNT_CHAR"] = "(" + ThaiBaht(GetSumAmt().ToString()) + "ถ้วน)";
                    drPayInSlipMain["DEPOSIT"] = "SPW";
                    drPayInSlipMain["DATE"] = convertToDateThai(txtDatePayIn.Text);
                    drPayInSlipMain["BANK"] = rbBankThai.Checked ? "ทหารไทย" : "กรุงศรีอยุธยา";
                    drPayInSlipMain["BR_BANK"] = txtBranceName.Text;
                    string[] tmpAccount = lstPayIn[0].ACCOUNT_ID.Split('-');
                    string account = "";
                    foreach (string item in tmpAccount)
                    {
                        account += item;
                    }

                    drPayInSlipMain["ACCOUNT_NO1"] = account[0];
                    drPayInSlipMain["ACCOUNT_NO2"] = account[1];
                    drPayInSlipMain["ACCOUNT_NO3"] = account[2];
                    drPayInSlipMain["ACCOUNT_NO4"] = account[3];
                    drPayInSlipMain["ACCOUNT_NO5"] = account[4];
                    drPayInSlipMain["ACCOUNT_NO6"] = account[5];
                    drPayInSlipMain["ACCOUNT_NO7"] = account[6];
                    drPayInSlipMain["ACCOUNT_NO8"] = account[7];
                    drPayInSlipMain["ACCOUNT_NO9"] = account[8];
                    drPayInSlipMain["ACCOUNT_NO10"] = account[9];
                    drPayInSlipMain["CHECK_COUNT"] = lstPayIn.Count().ToString();
                    payInSlipMain.Rows.Add(drPayInSlipMain);

                    Session["DataToReport"] = ds;
                    if (rbBankThai.Checked)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "window.open('../Reports/PayInSlipTMBReport.aspx');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "window.open('../Reports/PayInSlipKSBReport.aspx');", true);
                    }
                    Session["PAYIN_PRINT"] = lstPayIn;
                }
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        protected void btnPrintXSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool isSave = false;
                if (Session["PAYIN"] != null)
                {
                    isSave = Save();
                }
                else
                {
                    isSave = true;
                }
                if (isSave)
                {
                    List<PAYIN_TRANS> lstPayIn = new List<PAYIN_TRANS>();
                    if (Session["PAYIN_PRINT"] == null)
                    {
                        Session["PAYIN_PRINT"] = lstPayIn;
                    }
                    else
                    {
                        lstPayIn = Session["PAYIN_PRINT"] as List<PAYIN_TRANS>;
                    }

                    decimal tmpTotalAmt = 0;
                    foreach (PAYIN_TRANS pt in lstPayIn)
                    {
                        tmpTotalAmt += pt.CHQ_AMOUNT;
                    }

                    SPW.UI.Web.Reports.PayInSlip ds = new SPW.UI.Web.Reports.PayInSlip();
                    DataTable payInSlipMain = ds.Tables["MAIN"];
                    DataRow drPayInSlipMain = payInSlipMain.NewRow();

                    drPayInSlipMain["ACCOUNT_NAME"] = txtAccountName.Text;
                    drPayInSlipMain["TEL"] = "02-961-6686-7";
                    drPayInSlipMain["AMOUNT_NUM"] = GetSumAmt().ToString("#,#.00#");
                    drPayInSlipMain["AMOUNT_CHAR"] = "(" + ThaiBaht(GetSumAmt().ToString()) + "ถ้วน)";
                    drPayInSlipMain["DEPOSIT"] = "SPW";
                    drPayInSlipMain["DATE"] = convertToDateThai(txtDatePayIn.Text);
                    drPayInSlipMain["BANK"] = rbBankThai.Checked ? "ทหารไทย" : "กรุงศรีอยุธยา";
                    drPayInSlipMain["BR_BANK"] = txtBranceName.Text;
                    string[] tmpAccount = lstPayIn[0].ACCOUNT_ID.Split('-');
                    string account = "";
                    foreach (string item in tmpAccount)
                    {
                        account += item;
                    }

                    drPayInSlipMain["ACCOUNT_NO1"] = account[0];
                    drPayInSlipMain["ACCOUNT_NO2"] = account[1];
                    drPayInSlipMain["ACCOUNT_NO3"] = account[2];
                    drPayInSlipMain["ACCOUNT_NO4"] = account[3];
                    drPayInSlipMain["ACCOUNT_NO5"] = account[4];
                    drPayInSlipMain["ACCOUNT_NO6"] = account[5];
                    drPayInSlipMain["ACCOUNT_NO7"] = account[6];
                    drPayInSlipMain["ACCOUNT_NO8"] = account[7];
                    drPayInSlipMain["ACCOUNT_NO9"] = account[8];
                    drPayInSlipMain["ACCOUNT_NO10"] = account[9];
                    drPayInSlipMain["CHECK_COUNT"] = lstPayIn.Count().ToString();
                    payInSlipMain.Rows.Add(drPayInSlipMain);

                    DataTable payInSlipSub = ds.Tables["SUB"];
                    DataRow drPayInSlipSub = payInSlipSub.NewRow();
                    lstPayIn.OrderBy(x => x.PAYIN_DATE).ThenBy(x => x.PAYIN_SEQ_NO).ThenBy(x => x.CHQ_SEQ_NO).ToList();
                    if (lstPayIn.Count() <= 5)
                    {
                        if (lstPayIn.Count() >= 1)
                        {
                            drPayInSlipSub["CHECK_NO1"] = lstPayIn[0].CHQ_NO;
                            drPayInSlipSub["CHECK_BANK1"] = lstPayIn[0].CHQ_BANK;
                            drPayInSlipSub["AMOUNT1"] = lstPayIn[0].CHQ_AMOUNT.ToString("#,#.00#");
                        }
                        if (lstPayIn.Count() >= 2)
                        {
                            drPayInSlipSub["CHECK_NO2"] = lstPayIn[1].CHQ_NO;
                            drPayInSlipSub["CHECK_BANK2"] = lstPayIn[1].CHQ_BANK;
                            drPayInSlipSub["AMOUNT2"] = lstPayIn[1].CHQ_AMOUNT.ToString("#,#.00#");
                        }
                        if (lstPayIn.Count() >= 3)
                        {
                            drPayInSlipSub["CHECK_NO3"] = lstPayIn[2].CHQ_NO;
                            drPayInSlipSub["CHECK_BANK3"] = lstPayIn[2].CHQ_BANK;
                            drPayInSlipSub["AMOUNT3"] = lstPayIn[2].CHQ_AMOUNT.ToString("#,#.00#");
                        }
                        if (lstPayIn.Count() >= 4)
                        {
                            drPayInSlipSub["CHECK_NO4"] = lstPayIn[3].CHQ_NO;
                            drPayInSlipSub["CHECK_BANK4"] = lstPayIn[3].CHQ_BANK;
                            drPayInSlipSub["AMOUNT4"] = lstPayIn[3].CHQ_AMOUNT.ToString("#,#.00#");
                        }
                        if (lstPayIn.Count() >= 5)
                        {
                            drPayInSlipSub["CHECK_NO5"] = lstPayIn[4].CHQ_NO;
                            drPayInSlipSub["CHECK_BANK5"] = lstPayIn[4].CHQ_BANK;
                            drPayInSlipSub["AMOUNT5"] = lstPayIn[4].CHQ_AMOUNT.ToString("#,#.00#");
                        }
                    }
                    payInSlipSub.Rows.Add(drPayInSlipSub);
                    Session["DataToReport"] = ds;
                    if (rbBankThai.Checked)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "window.open('../Reports/PayInSlipTMBReport.aspx');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "window.open('../Reports/PayInSlipKSBReport.aspx');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        protected void btnPrint2Submit_Click(object sender, EventArgs e)
        {
            try
            {
                bool isSave = false;
                if (Session["PAYIN"] != null)
                {
                    isSave = Save();
                }
                else
                {
                    isSave = true;
                }
                if (isSave)
                {
                    List<PAYIN_TRANS> lstPayIn = new List<PAYIN_TRANS>();
                    if (Session["PAYIN_PRINT"] == null)
                    {
                        Session["PAYIN_PRINT"] = lstPayIn;
                    }
                    else
                    {
                        lstPayIn = Session["PAYIN_PRINT"] as List<PAYIN_TRANS>;
                    }

                    SPW.UI.Web.Reports.PayInSlip ds = new SPW.UI.Web.Reports.PayInSlip();
                    DataTable payInSlipMain = ds.Tables["MAIN"];

                    DataTable payInSlipPaper = ds.Tables["SUM_PAPER"];

                    decimal tmpTotalAmt = 0;
                    int i = 1;
                    lstPayIn = lstPayIn.OrderBy(x => x.PAYIN_DATE).ThenBy(x => x.PAYIN_SEQ_NO).ThenBy(x => x.CHQ_SEQ_NO).ToList();

                    foreach (PAYIN_TRANS pt in lstPayIn)
                    {
                        DataRow drpayInSlipPaper = payInSlipPaper.NewRow();
                        drpayInSlipPaper["SEQ"] = (i++).ToString();
                        drpayInSlipPaper["DATE"] = convertToDateThai(pt.CHQ_DATE.ToString("dd/MM/yyyy"));
                        drpayInSlipPaper["CHECK_NO"] = pt.CHQ_NO.ToString();
                        drpayInSlipPaper["CHECK_BANK"] = pt.CHQ_BANK.ToString();
                        drpayInSlipPaper["AMOUNT"] = pt.CHQ_AMOUNT.ToString("#,#.00#"); ;
                        tmpTotalAmt += pt.CHQ_AMOUNT;
                        drpayInSlipPaper["STORE_NAME"] = pt.STORE_NAME_PAID;
                        payInSlipPaper.Rows.Add(drpayInSlipPaper);
                    }

                    DataRow drPayInSlipMain = payInSlipMain.NewRow();
                    drPayInSlipMain["ACCOUNT_NO"] = ddlAccountMast.SelectedValue;
                    drPayInSlipMain["ACCOUNT_NAME"] = txtAccountName.Text;
                    drPayInSlipMain["AMOUNT_NUM"] = GetSumAmt().ToString("#,#.00#");
                    drPayInSlipMain["DATE"] = convertToDateThai(txtDatePayIn.Text);
                    drPayInSlipMain["BANK"] = rbBankThai.Checked ? "ทหารไทย" : "กรุงศรีอยุธยา";
                    drPayInSlipMain["BR_BANK"] = txtBranceName.Text;
                    drPayInSlipMain["CHECK_COUNT"] = lstPayIn.Count().ToString();
                    payInSlipMain.Rows.Add(drPayInSlipMain);

                    Session["DataToReport"] = ds;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "window.open('../Reports/PayInSlipPaper.aspx');", true);
                }
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        protected void grdBank_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                List<PAYIN_TRANS> lstPayIn = new List<PAYIN_TRANS>();
                if (Session["PAYIN"] == null)
                {
                    Session["PAYIN"] = lstPayIn;
                }
                else
                {
                    lstPayIn = Session["PAYIN"] as List<PAYIN_TRANS>;
                }

                lstPayIn.RemoveAt(e.RowIndex);
                Session["PAYIN"] = lstPayIn;
                SumAmt();
                grdBank.DataSource = lstPayIn;
                grdBank.DataBind();
                if (lstPayIn.Count() == 0)
                {
                    btnPrint1Submit.Visible = false;
                    btnPrint2Submit.Visible = false;
                    btnPrintXSubmit.Visible = false;
                    lbl1.Visible = false;
                    lbl2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        protected void grdBank_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (LinkButton button in e.Row.Cells[4].Controls.OfType<LinkButton>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('ต้องการจะลบข้อมูลใช่หรือไม่')){ return false; };";
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Remove("PAYIN");
                Response.RedirectPermanent("PayInSlip.aspx");
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        private string convertToDateThai(string date)
        {
            if (date != "")
            {
                string[] tmp = date.Split('/');
                if (Convert.ToInt32(tmp[2]) < 2500)
                {
                    return tmp[0] + "/" + tmp[1] + "/" + (Convert.ToInt32(tmp[2]) + 543);
                }
                else
                {
                    return date;
                }
            }
            else
            {
                return date;
            }

        }

        private string convertToDateUSA(string date)
        {
            if (date != "")
            {
                string[] tmp = date.Split('/');

                return tmp[2] + "-" + tmp[1] + "-" + tmp[0];
            }
            else
            {
                return date;
            }

        }

        protected void rbPayin_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPayin.Checked)
            {
                txtPayInSeq.Enabled = true;
                txtPageSeq.Enabled = true;
            }
            else
            {
                txtPayInSeq.Enabled = false;
                txtPageSeq.Enabled = false;

            }
        }
    }
}