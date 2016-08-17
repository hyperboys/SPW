
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
    public partial class StockRawLot : System.Web.UI.Page
    {
        #region Declare variable
        private DataServiceEngine _dataServiceEngine;
        private StockRawLotService cmdStockRawLotService;
        private RawProductService cmdRawProductService;
        private UserService cmdUserService;

        private SupplierService cmdSupplierService;
        


        public class DATAGRID
        {
            public RAW_PRODUCT RAW_PRODUCT { get; set; }
            public int RAW_ID { get; set; }
            public int VENDOR_ID { get; set; }
            public string VENDOR_NAME { get; set; }
            public string LOT_NO { get; set; }
            public int RAW_REMAIN { get; set; }
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
            cmdStockRawLotService = (StockRawLotService)_dataServiceEngine.GetDataService(typeof(StockRawLotService));
            cmdRawProductService = (RawProductService)_dataServiceEngine.GetDataService(typeof(RawProductService));
            cmdUserService = (UserService)_dataServiceEngine.GetDataService(typeof(UserService));
            cmdSupplierService = (SupplierService)_dataServiceEngine.GetDataService(typeof(SupplierService)); 
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }


        private void InitialData()
        {
            if (Request.QueryString["RAW_ID"] != null)
            {
                List<STOCK_RAW_LOT> lstSTOCK_RAW_LOT = cmdStockRawLotService.GetAll(int.Parse(Request.QueryString["RAW_ID"].ToString()));
                List<DATAGRID> listDataGrid = new List<DATAGRID>();
                DATAGRID _datagrid = new DATAGRID();

                if (lstSTOCK_RAW_LOT != null)
                {
                    lstSTOCK_RAW_LOT.ForEach(e =>
                    {
                        _datagrid = new DATAGRID();
                        _datagrid.RAW_PRODUCT = cmdRawProductService.Select(e.RAW_ID);
                        _datagrid.RAW_ID = e.RAW_ID;
                        _datagrid.VENDOR_ID = e.VENDOR_ID;
                        _datagrid.VENDOR_NAME = cmdSupplierService.Select(e.VENDOR_ID).VENDOR_NAME; ;
                        _datagrid.LOT_NO = e.LOT_NO;
                        _datagrid.RAW_REMAIN = e.RAW_REMAIN;
                        listDataGrid.Add(_datagrid);
                    });

                    Session["LISTDATAGRID"] = listDataGrid;
                    gdvRawSetting.DataSource = listDataGrid;
                    gdvRawSetting.DataBind();
                }
            }
            AutoCompleteTxtVendorName();
        }
        #endregion

        #region Business

        private void AutoCompleteTxtVendorName()
        {
            List<string> nameList = SearchAutoCompleteDataService.Search("VENDOR", "VENDOR_NAME", "VENDOR_NAME", "");
            string str = "";
            for (int i = 0; i < nameList.Count; i++)
            {
                nameList[i] = nameList[i].Replace("\"", "นิ้ว");
                nameList[i] = nameList[i].Replace("'", "นิ้ว");
                str = str + '"' + nameList[i].ToString() + '"' + ',';
            }
            if (str != "")
            {
                str = str.Remove(str.Length - 1);
            }
            str = "[" + str + "]";
            txtVendorName.Attributes.Add("data-source", str);
        }
        private VENDOR GetVendorCode(string VendorName)
        {
            return cmdSupplierService.Select(VendorName);
        }
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
        protected void txtVendorName_TextChanged(object sender, EventArgs e)
        {
            VENDOR _vendor = GetVendorCode(txtVendorName.Text);
            if (_vendor != null)
            {
                txtVendorCode.Text = _vendor.VENDOR_CODE.ToString();
            }

        }
        #endregion

    }
}