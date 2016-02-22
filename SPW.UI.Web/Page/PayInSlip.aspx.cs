using SPW.Common;
using SPW.DataService;
using SPW.Model;
using System;
using System.Collections.Generic;
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
            string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
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
                            bahtTH += num[Convert.ToInt32(n)];
                        bahtTH += rank[(intVal.Length - i) - 1];
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
        private double totalAmount;
        private AccountMastService _accountMastService;
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
            lblAmount.Text = ThaiBaht(totalAmount.ToString());
            lblNumAmount.Text = totalAmount.ToString() + " บาท";
            if (!Page.IsPostBack)
            {
                CreatePageEngine();
                InitialData();
                grdBank.DataSource = null;
                grdBank.DataBind();
            }
            else
            {
                ReloadPageEngine();
            }
        }

        private void InitialData()
        {
            var list = _accountMastService.GetAll();
            foreach (var item in list)
            {
                ddlAccountMast.Items.Add(new ListItem(item.ACCOUNT_NAME + " " + item.BANK_SH_NAME + " " + item.ACCOUNT_ID, item.ACCOUNT_ID.ToString()));
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

                decimal tmpTotalAmt = 0;
                foreach (PAYIN_TRANS pt in lstPayIn) 
                {
                    tmpTotalAmt += pt.CHQ_AMOUNT;
                }

                lblNumAmount.Text = tmpTotalAmt.ToString("#,#.00#") +" บาท";
                lblAmount.Text = ThaiBaht(tmpTotalAmt.ToString());

                ClearScreen();
            }
            catch (Exception ex) 
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        private void ClearScreen() 
        {
            ddlAccountMast.SelectedIndex = 0;
            txtAccountName.Text = "";
            txtAmount.Text = "";
            txtBankCheck.Text = "";
            txtBranceCheck.Text = "";
            txtCheck.Text = "";
        }
    }
}