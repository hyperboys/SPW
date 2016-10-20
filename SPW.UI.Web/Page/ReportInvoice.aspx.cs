using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer.DataDefModel;
using System.Data;
using System.Reflection;

namespace SPW.UI.Web.Page
{
    public partial class ReportInvoice : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private APVehicleTransService cmdAPVehicleTransService;
        private AssetTypeService _assetTypeService;
        private VehicleService _vehicleService;

        public List<AP_VEHICLE_TRANS> DataSouce
        {
            get
            {
                var list = (List<AP_VEHICLE_TRANS>)ViewState["listINV"];
                return list;
            }
            set
            {
                ViewState["listINV"] = value;
            }
        }
        public class INVDATATABLE
        {
            public int AP_VEHICLE_TRANS_ID { get; set; }
            public DateTime AP_VEHICLE_TRANS_DATE { get; set; }
            public int ASSET_TYPE_ID { get; set; }
            public int VEHICLE_ID { get; set; }
            public int VENDOR_ID { get; set; }
            public int MILE_NO { get; set; }
            public decimal MA_AMOUNT { get; set; }
            public string VEHICLE_REGNO { get; set; }
            public string ASSET_TYPE_NAME { get; set; }
            public string MA_DESC { get; set; }
            public System.DateTime MA_START_DATE { get; set; }
            public System.DateTime MA_FINISH_DATE { get; set; }
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
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        protected void gdvInv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvInv.PageIndex = e.NewPageIndex;
            gdvInv.DataBind();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            List<AP_VEHICLE_TRANS> lstAP_VEHICLE_TRANS = new List<AP_VEHICLE_TRANS>();
            List<INVDATATABLE> lstINVDATATABLE = new List<INVDATATABLE>();
            if (!txtStartDate.Text.Equals("") || !txtEndDate.Text.Equals("") || ddlAssetType.SelectedIndex != 0 || ddlVehicle.SelectedIndex != 0)
            {
                lstAP_VEHICLE_TRANS = DataSouce.Where(x => x.AP_VEHICLE_TRANS_DATE >= ((txtStartDate.Text == "") ? x.AP_VEHICLE_TRANS_DATE : DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)) &&
                    x.AP_VEHICLE_TRANS_DATE <= ((txtEndDate.Text == "") ? x.AP_VEHICLE_TRANS_DATE : DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)) &&
                    x.ASSET_TYPE_ID == ((ddlAssetType.SelectedIndex == 0) ? x.ASSET_TYPE_ID : int.Parse(ddlAssetType.SelectedValue)) &&
                    x.VEHICLE_ID == ((ddlVehicle.SelectedIndex == 0) ? x.VEHICLE_ID : int.Parse(ddlVehicle.SelectedValue)) &&
                    x.SYE_DEL == false).ToList();
            }
            else
            {
                lstAP_VEHICLE_TRANS = DataSouce;
            }

            if (lstAP_VEHICLE_TRANS.Count > 0)
            {
                lstAP_VEHICLE_TRANS.ForEach(f =>
                    {
                        INVDATATABLE _INVDATATABLE = new INVDATATABLE();
                        _INVDATATABLE.AP_VEHICLE_TRANS_ID = f.AP_VEHICLE_TRANS_ID;
                        _INVDATATABLE.AP_VEHICLE_TRANS_DATE = f.AP_VEHICLE_TRANS_DATE;
                        _INVDATATABLE.ASSET_TYPE_ID = f.ASSET_TYPE_ID;
                        _INVDATATABLE.VEHICLE_ID = f.VEHICLE_ID;
                        _INVDATATABLE.VENDOR_ID = f.VENDOR_ID;
                        _INVDATATABLE.MILE_NO = f.MILE_NO;
                        _INVDATATABLE.MA_AMOUNT = f.MA_AMOUNT;
                        _INVDATATABLE.VEHICLE_REGNO = f.VEHICLE.VEHICLE_REGNO;
                        _INVDATATABLE.ASSET_TYPE_NAME = f.ASSET_TYPE.ASSET_TYPE_NAME;
                        _INVDATATABLE.MA_DESC = f.MA_DESC;
                        _INVDATATABLE.MA_START_DATE = f.MA_START_DATE;
                        _INVDATATABLE.MA_FINISH_DATE = f.MA_FINISH_DATE;
                        lstINVDATATABLE.Add(_INVDATATABLE);
                    });
            }

            ReportDocument rpt = new ReportDocument();
            rpt.Load(Server.MapPath("../Reports/ReportInvoices.rpt"));
            System.Data.DataSet ds = new System.Data.DataSet();

            DataTable dt1 = new DataTable();
            dt1 = ToDataTable(lstINVDATATABLE);
            dt1.TableName = "INVDATATABLE";
            ds.Tables.Add(dt1);
            rpt.SetDataSource(ds);
            Session["DataToReportINV"] = rpt;
            Response.RedirectPermanent("../Reports/Report2.aspx");
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