using SPW.Common;
using SPW.DAL;
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
    public partial class ManageAccountMast : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private AccountMastService _accountMastService;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CreatePageEngine();
                Init();
            }
            else
            {
                ReloadPageEngine();
            }
        }

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

        private void Init()
        {
            if (Request.QueryString["id"] != null)
            {
                txtAccountNo.Enabled = false;
                ACCOUNT_MAST item = _accountMastService.Select(Request.QueryString["id"].ToString());
                txtAccountNo.Text = item.ACCOUNT_ID;
                txtAccountName.Text = item.ACCOUNT_NAME;
                txtBrhName.Text = item.BANK_BRH_NAME;
                ddlType.SelectedValue = item.PAYIN_FORMAT.ToString();
            }
            else
            {
                txtAccountNo.Enabled = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string bankName = "";
                string bankShName = "";
                if (ddlType.SelectedValue.Equals("1"))
                {
                    bankName = "ธ.ทหารไทย จำกัด(มหาชน)";
                    bankShName = "TMB";
                }
                else
                {
                    bankName = "ธนาคารกรุงศรีอยุธยา จำกัด (มหาชน) ";
                    bankShName = "BAY";
                }

                USER userItem = Session["user"] as USER;
                ACCOUNT_MAST item = new ACCOUNT_MAST();
                if (Request.QueryString["id"] != null)
                {
                    item = _accountMastService.Select(Request.QueryString["id"].ToString());
                }
                else
                {
                    item.ACCOUNT_ID = txtAccountNo.Text;
                    item.CREATE_DATE = DateTime.Now;
                    item.CREATE_EMPLOYEE_ID = userItem.USER_ID;
                }

                item.ACCOUNT_NAME = txtAccountName.Text;
                item.BANK_BRH_NAME = txtBrhName.Text;
                item.PAYIN_FORMAT = Convert.ToInt32(ddlType.SelectedValue);
                item.BANK_NAME = bankName;
                item.BANK_SH_NAME = bankShName;
                item.UPDATE_DATE = DateTime.Now;
                item.UPDATE_EMPLOYEE_ID = userItem.USER_ID;
                item.SYE_DEL = false;
                _accountMastService.Update(item);
                alert.Visible = true;
                Response.AppendHeader("Refresh", "2; url=SearchAccountMast.aspx");
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }
    }
}