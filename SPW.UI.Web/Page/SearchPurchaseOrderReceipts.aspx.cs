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
    public partial class SearchPurchaseOrderReceipts : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private PoHdTransService cmdPoHdTrans;
        public List<PO_HD_TRANS> DataSouce
        {
            get
            {
                var list = (List<PO_HD_TRANS>)ViewState["listPO"];
                return list;
            }
            set
            {
                ViewState["listPO"] = value;
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
            cmdPoHdTrans = (PoHdTransService)_dataServiceEngine.GetDataService(typeof(PoHdTransService));
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
            gridPO.DataSource = DataSouce;
            gridPO.DataBind();
        }

        private void InitialData()
        {
            DataSouce = cmdPoHdTrans.GetAll();
            gridPO.DataSource = DataSouce;
            gridPO.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtBkNo.Text.Equals(""))
            {
                gridPO.DataSource = DataSouce;
            }
            else
            {
                gridPO.DataSource = DataSouce.Where(x => x.PO_BK_NO.Contains(txtBkNo.Text) && x.PO_RN_NO.Contains(txtRnNo.Text) && x.SYE_DEL == false).ToList();
            }
            gridPO.DataBind();
        }

        protected void gridPO_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            string prRnNo = gridPO.Rows[e.NewEditIndex].Cells[2].Text;
            Response.RedirectPermanent("PurchaseOrderReceipts.aspx?PO_BK_NO=" + gridPO.DataKeys[e.NewEditIndex].Values[0].ToString() + "&PO_RN_NO=" + prRnNo);
        }

        protected void gridPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridPO.PageIndex = e.NewPageIndex;
            gridPO.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("IssuePurchaseOrders.aspx");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtBkNo.Text = "";
            txtRnNo.Text = "";
            SearchGrid();
        }
    }
}