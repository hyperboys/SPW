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
    public partial class SearchInvoice : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private APVehicleTransService cmdAPVehicleTransService;
        public List<AP_VEHICLE_TRANS> DataSouce
        {
            get
            {
                var list = (List<AP_VEHICLE_TRANS>)ViewState["listPO"];
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
            cmdAPVehicleTransService = (APVehicleTransService)_dataServiceEngine.GetDataService(typeof(APVehicleTransService));
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
            gdvInv.DataSource = DataSouce;
            gdvInv.DataBind();
        }

        private void InitialData()
        {
            DataSouce = cmdAPVehicleTransService.GetAll();
            gdvInv.DataSource = DataSouce;
            gdvInv.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtTransID.Text.Equals(""))
            {
                gdvInv.DataSource = DataSouce;
            }
            else
            {
                gdvInv.DataSource = DataSouce.Where(x => x.AP_VEHICLE_TRANS_ID == int.Parse(txtTransID.Text) && x.SYE_DEL == false).ToList();
            }
            gdvInv.DataBind();
        }

        protected void gdvInv_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            string prRnNo = gdvInv.Rows[e.NewEditIndex].Cells[2].Text;
            Response.RedirectPermanent("ManageInvoice.aspx?AP_VEHICLE_TRANS_ID=" + gdvInv.DataKeys[e.NewEditIndex].Values[0].ToString());
        }

        protected void gdvInv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvInv.PageIndex = e.NewPageIndex;
            gdvInv.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("ManageInvoice.aspx");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtTransID.Text = "";
            SearchGrid();
        }
    }
}