using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using System.Globalization;

namespace SPW.UI.Web.Page
{
    public partial class SearchInvoice : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private APVehicleTransService cmdAPVehicleTransService;
        private AssetTypeService _assetTypeService;
        private VehicleService _vehicleService;
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
            _assetTypeService = (AssetTypeService)_dataServiceEngine.GetDataService(typeof(AssetTypeService));
            _vehicleService = (VehicleService)_dataServiceEngine.GetDataService(typeof(VehicleService));
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
            List<ASSET_TYPE> listAssetType = _assetTypeService.GetAll();
            listAssetType.ForEach(item => ddlAssetType.Items.Add(new ListItem(item.ASSET_TYPE_NAME, item.ASSET_TYPE_ID.ToString())));

            List<VEHICLE> listVehicle = _vehicleService.GetAll();
            listVehicle.ForEach(item => ddlVehicle.Items.Add(new ListItem(item.VEHICLE_REGNO, item.VEHICLE_ID.ToString())));

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
            if (!txtStartDate.Text.Equals("") || !txtEndDate.Text.Equals("") || ddlAssetType.SelectedIndex != 0 || ddlVehicle.SelectedIndex != 0)
            {
                gdvInv.DataSource = DataSouce.Where(x => x.AP_VEHICLE_TRANS_DATE >= ((txtStartDate.Text == "") ? x.AP_VEHICLE_TRANS_DATE : DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)) &&
                    x.AP_VEHICLE_TRANS_DATE <= ((txtEndDate.Text == "") ? x.AP_VEHICLE_TRANS_DATE : DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)) &&
                    x.ASSET_TYPE_ID == ((ddlAssetType.SelectedIndex == 0) ? x.ASSET_TYPE_ID : int.Parse(ddlAssetType.SelectedValue)) &&
                    x.VEHICLE_ID == ((ddlVehicle.SelectedIndex == 0) ? x.VEHICLE_ID : int.Parse(ddlVehicle.SelectedValue)) && 
                    x.SYE_DEL == false).ToList();
            }
            else
            {
                gdvInv.DataSource = DataSouce;
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
            ddlAssetType.SelectedIndex = 0;
            ddlVehicle.SelectedIndex = 0;
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            SearchGrid();
        }
    }
}