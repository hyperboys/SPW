using SPW.Common;
using SPW.DataService;
using SPW.Model;
using SPW.UI.Web.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
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
            string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน","สิบ","ร้อย","พัน" };
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
                                if (n == "1")
                                {
                                    bahtTH += "เอ็ด";
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
                    else if((intVal.Length - i) == 7)
                    {
                        bahtTH += "ล้าน";
                    }
                }
                if (decVal == "00")
                    bahtTH += "บาท";
                else
                {
                    bahtTH += "จุด";
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
                    bahtTH += "บาท";
                }
            }
            return bahtTH;
        }

        private AccountMastService _accountMastService;
        private PayInTranService _payInTranService;
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
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            lblDateTime.Text = "วันที่ " + DateTime.Now.ToShortDateString();
            if (!Page.IsPostBack)
            {
                CreatePageEngine();
                InitialData();
                grdBank.DataSource = null;
                grdBank.DataBind();
                Session["PAYIN"] = null;
            }
            else
            {
                ReloadPageEngine();
            }
        }

        private void InitialData()
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

        protected void ddlAccountMast_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlAccountMast.SelectedValue.Equals("0"))
            {
                ACCOUNT_MAST accountMast = _accountMastService.Select(ddlAccountMast.SelectedValue);
                txtAccountName.Text = accountMast.ACCOUNT_NAME;
            }
            else 
            {
                txtAccountName.Text = string.Empty;
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
                List<PAYIN_TRANS> lstPayIn = new List<PAYIN_TRANS>();
                if (Session["PAYIN"] == null)
                {
                    Session["PAYIN"] = lstPayIn;
                }
                else 
                {
                    lstPayIn =  Session["PAYIN"] as List<PAYIN_TRANS>;
                }

                PAYIN_TRANS tmpItem = new PAYIN_TRANS();
                tmpItem.CHQ_AMOUNT = Convert.ToDecimal(txtAmount.Text);
                tmpItem.CHQ_BANK = txtBankCheck.Text;
                tmpItem.CHQ_NO = txtCheck.Text;
                tmpItem.CHQ_SEQ_NO = lstPayIn.Count() + 1;
                lstPayIn.Add(tmpItem);
                grdBank.DataSource = lstPayIn;
                grdBank.DataBind();

                SumAmt();
                ClearScreen();
                btnSave.Visible = true;
                ddlAccountMast.Enabled = false;
                rbBankKrungThai.Enabled = false;
                rbBankThai.Enabled = false;

                if (rbBankThai.Checked)
                {
                    if (lstPayIn.Count() == 3)
                    {
                        btnAdd.Enabled = false;
                    }
                    else 
                    {
                        btnAdd.Enabled = true;
                    }
                }
                else if(rbBankKrungThai.Checked) 
                {
                    if (lstPayIn.Count() == 5)
                    {
                        btnAdd.Enabled = false;
                    }
                    else
                    {
                        btnAdd.Enabled = true;
                    }
                }
            }
            catch (Exception ex) 
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        private void SumAmt() 
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
            return tmpTotalAmt;
        }

        private void ClearScreen() 
        {
            txtAmount.Text = "";
            txtBankCheck.Text = "";
            txtBranceCheck.Text = "";
            txtCheck.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
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
                foreach (PAYIN_TRANS tmpItem in lstPayIn) 
                {
                    tmpItem.ACCOUNT_ID = accountMast.ACCOUNT_ID;
                    tmpItem.ACCOUNT_NAME = accountMast.ACCOUNT_NAME;
                    tmpItem.BANK_NAME = accountMast.BANK_NAME;
                    tmpItem.BANK_SH_NAME = accountMast.BANK_SH_NAME;
                    tmpItem.CREATE_DATE = DateTime.Now;
                    tmpItem.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    tmpItem.PAYIN_APPROVE_ID = 1;
                    tmpItem.PAYIN_SEQ_NO = _payInTranService.GetCount() + 1;
                    tmpItem.PAYIN_DATE = DateTime.Now;
                    tmpItem.PAYIN_TOTAL_AMOUNT = tmpTotalAmt;
                    tmpItem.SYE_DEL = false;
                    tmpItem.UPDATE_DATE = DateTime.Now;
                    tmpItem.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                }

                Reports.PayInSlip ds = new Reports.PayInSlip();
                DataTable payInSlipMain = ds.Tables["MAIN"];
                DataRow drPayInSlipMain = payInSlipMain.NewRow();

                drPayInSlipMain["ACCOUNT_NAME"] = txtAccountName.Text;
                drPayInSlipMain["TEL"] = "02-961-6686-7";
                drPayInSlipMain["AMOUNT_NUM"] = GetSumAmt().ToString();
                drPayInSlipMain["AMOUNT_CHAR"] = lblAmount.Text.ToString();
                drPayInSlipMain["DEPOSIT"] = "SPW";
                drPayInSlipMain["ACCOUNT_NO1"] = ddlAccountMast.SelectedValue;
                payInSlipMain.Rows.Add(drPayInSlipMain);

                DataTable payInSlipSub = ds.Tables["SUB"];
                DataRow drPayInSlipSub = payInSlipSub.NewRow();
                if (lstPayIn.Count() >= 1)
                {
                    drPayInSlipSub["CHECK_NO1"] = lstPayIn[0].CHQ_NO;
                    drPayInSlipSub["CHECK_BANK1"] = lstPayIn[0].CHQ_BANK;
                    drPayInSlipSub["AMOUNT1"] = lstPayIn[0].CHQ_AMOUNT;
                }
                if (lstPayIn.Count() >= 2)
                {
                    drPayInSlipSub["CHECK_NO2"] = lstPayIn[1].CHQ_NO;
                    drPayInSlipSub["CHECK_BANK2"] = lstPayIn[1].CHQ_BANK;
                    drPayInSlipSub["AMOUNT2"] = lstPayIn[1].CHQ_AMOUNT;
                }
                if (lstPayIn.Count() >= 3)
                {
                    drPayInSlipSub["CHECK_NO3"] = lstPayIn[2].CHQ_NO;
                    drPayInSlipSub["CHECK_BANK3"] = lstPayIn[2].CHQ_BANK;
                    drPayInSlipSub["AMOUNT3"] = lstPayIn[2].CHQ_AMOUNT;
                }
                if (lstPayIn.Count() >= 4)
                {
                    drPayInSlipSub["CHECK_NO4"] = lstPayIn[3].CHQ_NO;
                    drPayInSlipSub["CHECK_BANK4"] = lstPayIn[3].CHQ_BANK;
                    drPayInSlipSub["AMOUNT4"] = lstPayIn[3].CHQ_AMOUNT;
                }
                if (lstPayIn.Count() >= 5)
                {
                    drPayInSlipSub["CHECK_NO5"] = lstPayIn[4].CHQ_NO;
                    drPayInSlipSub["CHECK_BANK5"] = lstPayIn[4].CHQ_BANK;
                    drPayInSlipSub["AMOUNT5"] = lstPayIn[4].CHQ_AMOUNT;
                }

                payInSlipSub.Rows.Add(drPayInSlipSub);

                _payInTranService.AddList(lstPayIn);
                Session["PAYIN"] = null;

                Session["DataToReport"] = ds;
                if (rbBankThai.Checked)
                {
                    Response.RedirectPermanent("../Reports/PayInSlipTMBReport.aspx");
                }
                else 
                {
                    Response.RedirectPermanent("../Reports/PayInSlipKSBReport.aspx");
                }
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }
    }
}