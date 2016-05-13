using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;

namespace SPW.UI.Web.Page
{
    public partial class SearchPurchaseRequisitionOrder : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private PrHdTransService cmdPrHdTrans;
        public List<PR_HD_TRANS> DataSouce
        {
            get
            {
                var list = (List<PR_HD_TRANS>)ViewState["listPR"];
                return list;
            }
            set
            {
                ViewState["listPR"] = value;
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
            cmdPrHdTrans = (PrHdTransService)_dataServiceEngine.GetDataService(typeof(PrHdTransService));
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
                CreatePageEngine();
                InitialData();
            }
            else
            {
                ReloadPageEngine();
            }
        }

        private void BlindGrid()
        {
            gridPR.DataSource = DataSouce;
            gridPR.DataBind();
        }

        private void InitialData()
        {
            DataSouce = cmdPrHdTrans.GetAll();
            gridPR.DataSource = DataSouce;
            gridPR.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtBkNo.Text.Equals(""))
            {
                gridPR.DataSource = DataSouce;
            }
            else
            {
                gridPR.DataSource = DataSouce.Where(x => x.PR_BK_NO.Contains(txtBkNo.Text) && x.PR_RN_NO.Contains(txtRnNo.Text) && x.SYE_DEL == false).ToList();
            }
            gridPR.DataBind();
        }

        protected void gridPR_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            string prRnNo = gridPR.Rows[e.NewEditIndex].Cells[2].Text;
            Response.RedirectPermanent("IssuePurchaseRequisitionOrder.aspx?PR_BK_NO=" + gridPR.DataKeys[e.NewEditIndex].Values[0].ToString() + "&PR_RN_NO=" + prRnNo);
        }

        protected void gridPR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridPR.PageIndex = e.NewPageIndex;
            gridPR.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("IssuePurchaseRequisitionOrder.aspx");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtBkNo.Text = "";
            SearchGrid();
        }
    }
}