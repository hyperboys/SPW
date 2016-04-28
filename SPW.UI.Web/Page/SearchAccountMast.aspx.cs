using SPW.DAL;
using SPW.DataService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.Common;
using SPW.Model;

namespace SPW.UI.Web.Page
{
    public partial class SearchAccountMast : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private AccountMastService _accountMastService;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitialPage();
            }
            else
            {
                ReloadPageEngine();
            }
        }

        private void InitialPage()
        {
            CreatePageEngine();
            InitialData();
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

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void InitialDataService()
        {
            _accountMastService = (AccountMastService)_dataServiceEngine.GetDataService(typeof(AccountMastService));
        }

        private void InitialData()
        {
            SearchTranspot();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchTranspot();
        }

        private void SearchTranspot()
        {
            try
            {
                grdTrans.DataSource = _accountMastService.GetAllCondition(txtAccountNo.Text, txtAccountName.Text, Convert.ToInt32(ddlType.SelectedValue));
                grdTrans.DataBind();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        protected void grdTrans_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ACCOUNT_ID = grdTrans.DataKeys[e.Row.RowIndex][0].ToString();
                LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                lbtnEdit.Attributes["href"] = "ManageAccountMast.aspx?id=" + ACCOUNT_ID;
            }
        }
    }
}