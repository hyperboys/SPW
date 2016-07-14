using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using SPW.DAL;

namespace SPW.UI.Web.Page
{
    public partial class PurchaseOrderReceiptsHistory : System.Web.UI.Page
    {
        #region Declare variable
        private DataServiceEngine _dataServiceEngine;
        private PoHdTransService cmdPoHdTransService;
        private StockRawStockService cmdStockRawStockService;
        private StockRawTransService cmdStockRawTransService;
        private ReceiveRawTransService cmdReceiveRawTransService;
        private StockRawStockService cmdRawStockService;
        private RawProductService cmdRawProductService;
        private UserService cmdUserService;


        public class DATAGRID
        {
            public RAW_PRODUCT RAW_PRODUCT { get; set; }
            public string RECEIVE_NO { get; set; }
            public System.DateTime RECEIVE_DATE { get; set; }
            public string PO_BK_NO { get; set; }
            public string PO_RN_NO { get; set; }
            public int PO_SEQ_NO { get; set; }
            public int RAW_ID { get; set; }
            public int RECEIVE_QTY { get; set; }
            public Nullable<System.DateTime> CREATE_DATE { get; set; }
            public Nullable<System.DateTime> UPDATE_DATE { get; set; }
            public Nullable<int> CREATE_EMPLOYEE_ID { get; set; }
            public Nullable<int> UPDATE_EMPLOYEE_ID { get; set; }
            public string USER_NAME { get; set; }
        }
        #endregion

        #region Sevice control
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
            cmdPoHdTransService = (PoHdTransService)_dataServiceEngine.GetDataService(typeof(PoHdTransService));
            cmdStockRawStockService = (StockRawStockService)_dataServiceEngine.GetDataService(typeof(StockRawStockService));
            cmdStockRawTransService = (StockRawTransService)_dataServiceEngine.GetDataService(typeof(StockRawTransService));
            cmdReceiveRawTransService = (ReceiveRawTransService)_dataServiceEngine.GetDataService(typeof(ReceiveRawTransService));
            cmdRawStockService = (StockRawStockService)_dataServiceEngine.GetDataService(typeof(StockRawStockService));
            cmdRawProductService = (RawProductService)_dataServiceEngine.GetDataService(typeof(RawProductService));
            cmdUserService = (UserService)_dataServiceEngine.GetDataService(typeof(UserService));
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }


        private void InitialData()
        {
            if (Request.QueryString["PO_BK_NO"] != null && Request.QueryString["PO_RN_NO"] != null)
            {
                PO_HD_TRANS _PO_HD_TRANS = cmdPoHdTransService.Select(Request.QueryString["PO_BK_NO"].ToString(), Request.QueryString["PO_RN_NO"].ToString());
                List<RECEIVE_RAW_TRANS> lstRECEIVE_RAW_TRANS = cmdReceiveRawTransService.GetAll(Request.QueryString["PO_BK_NO"].ToString(), Request.QueryString["PO_RN_NO"].ToString());
                List<DATAGRID> listDataGrid = new List<DATAGRID>();
                DATAGRID _datagrid = new DATAGRID();

                if (lstRECEIVE_RAW_TRANS != null && lstRECEIVE_RAW_TRANS.Count > 0)
                {
                    txtBKNo.Text = _PO_HD_TRANS.PO_BK_NO;
                    txtRNNo.Text = _PO_HD_TRANS.PO_RN_NO;
                    txtVendorName.Text = _PO_HD_TRANS.VENDOR_NAME;
                    txtVendorCode.Text = _PO_HD_TRANS.VENDOR_CODE;
                    flag.Text = "Edit";

                    lstRECEIVE_RAW_TRANS.ForEach(e =>
                    {
                        _datagrid = new DATAGRID();
                        _datagrid.RAW_PRODUCT = cmdRawProductService.Select(e.RAW_ID);
                        _datagrid.RECEIVE_NO = e.RECEIVE_NO;
                        _datagrid.PO_BK_NO = e.PO_BK_NO;
                        _datagrid.PO_RN_NO = e.PO_RN_NO;
                        _datagrid.PO_SEQ_NO = e.PO_SEQ_NO;
                        _datagrid.RAW_ID = e.RAW_ID;
                        _datagrid.RECEIVE_QTY = e.RECEIVE_QTY;
                        _datagrid.CREATE_DATE = e.CREATE_DATE;
                        _datagrid.UPDATE_DATE = e.UPDATE_DATE;
                        _datagrid.CREATE_EMPLOYEE_ID = e.CREATE_EMPLOYEE_ID;
                        _datagrid.UPDATE_EMPLOYEE_ID = e.UPDATE_EMPLOYEE_ID;
                        _datagrid.USER_NAME = cmdUserService.SelectFromEmpID((int)e.CREATE_EMPLOYEE_ID).USER_NAME;
                        listDataGrid.Add(_datagrid);
                    });

                    Session["LISTDATAGRID"] = listDataGrid;
                    gdvRECHis.DataSource = listDataGrid;
                    gdvRECHis.DataBind();
                }
            }
        }
        #endregion

        #region Business
        
        #endregion

        #region ASP control
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
        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
        #endregion

    }
}