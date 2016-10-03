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
    public partial class SearchStockWithdrawExpose : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private WrDtTransService cmdWrDtTransService;
        public List<WR_DT_TRANS> DataSouce
        {
            get
            {
                var list = (List<WR_DT_TRANS>)ViewState["listWR"];
                return list;
            }
            set
            {
                ViewState["listWR"] = value;
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
            cmdWrDtTransService = (WrDtTransService)_dataServiceEngine.GetDataService(typeof(WrDtTransService));
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
            gridWR.DataSource = DataSouce;
            gridWR.DataBind();
        }

        private void InitialData()
        {
            DataSouce = cmdWrDtTransService.GetAll();
            gridWR.DataSource = DataSouce;
            gridWR.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtBkNo.Text.Equals(""))
            {
                gridWR.DataSource = DataSouce;
            }
            else
            {
                gridWR.DataSource = DataSouce.Where(x => x.WR_BK_NO.Contains(txtBkNo.Text) && x.WR_RN_NO.Contains(txtRnNo.Text) && x.SYE_DEL == false).ToList();
            }
            gridWR.DataBind();
        }

        protected void gridWR_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            string prRnNo = gridWR.Rows[e.NewEditIndex].Cells[2].Text;
            string rawID = gridWR.Rows[e.NewEditIndex].Cells[3].Text;
            Response.RedirectPermanent("StockWithdrawExpose.aspx?WR_BK_NO=" + gridWR.DataKeys[e.NewEditIndex].Values[0].ToString() + "&WR_RN_NO=" + prRnNo + "&RAW_ID=" + rawID);
        }

        protected void gridWR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridWR.PageIndex = e.NewPageIndex;
            gridWR.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("StockWithdrawIssue.aspx");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtBkNo.Text = "";
            SearchGrid();
        }
    }
}