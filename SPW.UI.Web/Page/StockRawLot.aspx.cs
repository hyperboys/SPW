
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using SPW.DAL;
using System.Globalization;

namespace SPW.UI.Web.Page
{
    public partial class StockRawLot : System.Web.UI.Page
    {
        #region Declare variable
        private DataServiceEngine _dataServiceEngine;
        private StockRawLotService cmdStockRawLotService;
        private RawProductService cmdRawProductService;
        private UserService cmdUserService;
        private StockRawStockService cmdStockRawStockService;
        private PoHdTransService cmdPoHdTrans;
        private StockRawTransService cmdStockRawTransService;
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
            cmdStockRawStockService = (StockRawStockService)_dataServiceEngine.GetDataService(typeof(StockRawStockService));
            cmdPoHdTrans = (PoHdTransService)_dataServiceEngine.GetDataService(typeof(PoHdTransService));
            cmdStockRawTransService = (StockRawTransService)_dataServiceEngine.GetDataService(typeof(StockRawTransService));
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
        public string GetLotNo(string VENDOR_CODE)
        {
            return VENDOR_CODE + DateTime.Now.ToString("yyyy", CultureInfo.InvariantCulture) + DateTime.Now.ToString("MM", CultureInfo.InvariantCulture) + DateTime.Now.ToString("dd", CultureInfo.InvariantCulture);
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
        public void CheckRawStock()
        {
            try
            {
                List<STOCK_RAW_STOCK> lstSTOCK_RAW_STOCK = cmdStockRawStockService.GetAll();
                List<RAW_PRODUCT> lstRAW_PRODUCT = cmdRawProductService.GetAll(1);
                List<RAW_PRODUCT> lstNewRaw = new List<RAW_PRODUCT>();
                USER userItem = Session["user"] as USER;
                if (lstSTOCK_RAW_STOCK.Count != lstRAW_PRODUCT.Count)
                {
                    lstRAW_PRODUCT.ForEach(e =>
                    {
                        if (!lstSTOCK_RAW_STOCK.Exists(f => f.RAW_ID.Equals(e.RAW_ID)))
                        {
                            lstNewRaw.Add(e);
                            STOCK_RAW_STOCK item = new STOCK_RAW_STOCK();
                            item.RAW_ID = e.RAW_ID;
                            item.RAW_MINIMUM = 0;
                            item.RAW_REMAIN = 0;
                            item.CREATE_DATE = DateTime.Now;
                            item.UPDATE_DATE = DateTime.Now;
                            item.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                            item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                            item.SYE_DEL = false;
                            item.Action = ActionEnum.Create;
                            cmdStockRawStockService.Add(item);
                        }
                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private bool SaveStockRawTrans()
        {
            try
            {
                USER userItem = Session["user"] as USER;
                VENDOR _VENDOR = cmdSupplierService.SelectByVendorCode(txtVendorCode.Text);
                STOCK_RAW_TRANS _STOCK_RAW_TRANS = new STOCK_RAW_TRANS();
                _STOCK_RAW_TRANS.TRANS_ID = cmdStockRawTransService.GetNextTransID();
                _STOCK_RAW_TRANS.RAW_ID = int.Parse(Request.QueryString["RAW_ID"].ToString());
                _STOCK_RAW_TRANS.TRANS_DATE = DateTime.Now;
                _STOCK_RAW_TRANS.TRANS_TYPE = "SET";
                _STOCK_RAW_TRANS.REF_DOC_TYPE = "SET";
                _STOCK_RAW_TRANS.REF_DOC_BKNO = "SET";
                _STOCK_RAW_TRANS.REF_DOC_RNNO = "SET";
                _STOCK_RAW_TRANS.REF_DOC_YY = DateTime.Now.ToString("yy");
                _STOCK_RAW_TRANS.VENDOR_ID = _VENDOR.VENDOR_ID;
                _STOCK_RAW_TRANS.VENDOR_CODE = _VENDOR.VENDOR_CODE;
                _STOCK_RAW_TRANS.LOT_NO = GetLotNo(_VENDOR.VENDOR_CODE);
                _STOCK_RAW_TRANS.REF_REMARK1 = "";
                _STOCK_RAW_TRANS.REF_REMARK2 = "";
                _STOCK_RAW_TRANS.TRANS_QTY = int.Parse(txtStockQty.Text);
                _STOCK_RAW_TRANS.APPROVE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                _STOCK_RAW_TRANS.SYS_TIME = DateTime.Now.TimeOfDay;
                _STOCK_RAW_TRANS.CREATE_DATE = DateTime.Now;
                _STOCK_RAW_TRANS.UPDATE_DATE = DateTime.Now;
                _STOCK_RAW_TRANS.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                _STOCK_RAW_TRANS.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                _STOCK_RAW_TRANS.SYE_DEL = false;
                cmdStockRawTransService.Add(_STOCK_RAW_TRANS);
                if (SaveRawLot())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                lblerror2.Text = "*พบข้อผิดพลาดระหว่างบันทึกข้อมูล กรุณาติดต่อเจ้าหน้าที่";
                return false;
            }
        }
        private bool SaveRawLot()
        {
            try
            {
                int RAW_ID = int.Parse(Request.QueryString["RAW_ID"].ToString());
                USER userItem = Session["user"] as USER;
                VENDOR _VENDOR = cmdSupplierService.SelectByVendorCode(txtVendorCode.Text);
                if (!cmdStockRawLotService.isHasLot(RAW_ID, GetLotNo(_VENDOR.VENDOR_CODE)))
                {
                    STOCK_RAW_LOT _STOCK_RAW_LOT = new STOCK_RAW_LOT();
                    _STOCK_RAW_LOT.RAW_ID = RAW_ID;
                    _STOCK_RAW_LOT.VENDOR_ID = _VENDOR.VENDOR_ID;
                    _STOCK_RAW_LOT.VENDOR_CODE = _VENDOR.VENDOR_CODE;
                    _STOCK_RAW_LOT.LOT_NO = GetLotNo(_VENDOR.VENDOR_CODE);
                    _STOCK_RAW_LOT.RAW_REMAIN = int.Parse(txtStockQty.Text);
                    _STOCK_RAW_LOT.CREATE_DATE = DateTime.Now;
                    _STOCK_RAW_LOT.UPDATE_DATE = DateTime.Now;
                    _STOCK_RAW_LOT.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    _STOCK_RAW_LOT.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    _STOCK_RAW_LOT.SYE_DEL = false;
                    cmdStockRawLotService.Add(_STOCK_RAW_LOT);
                }
                else
                {
                    cmdStockRawLotService.Edit(RAW_ID, GetLotNo(_VENDOR.VENDOR_CODE), cmdStockRawLotService.GetRemainQty(RAW_ID, GetLotNo(_VENDOR.VENDOR_CODE)) + int.Parse(txtStockQty.Text), userItem.EMPLOYEE_ID);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }
        private bool SaveReceiveRawStock()
        {
            try
            {
                int RAW_ID = int.Parse(Request.QueryString["RAW_ID"].ToString());
                USER userItem = Session["user"] as USER;
                //int oPO_QTY = cmdStockRawStockService.GetRemainQty(RAW_ID);
                //cmdStockRawStockService.SetRawStockQty(RAW_ID, oPO_QTY + int.Parse(txtStockQty.Text), userItem.EMPLOYEE_ID);
                int sumRemain = cmdStockRawLotService.GetSumRemainQty(RAW_ID);
                cmdStockRawStockService.SetRawStockQty(RAW_ID, sumRemain, userItem.EMPLOYEE_ID);
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }
        private bool UpdateStockRawStock(int rawID,int qty)
        {
            try
            {
                USER userItem = Session["user"] as USER;
                //int oPO_QTY = cmdStockRawStockService.GetRemainQty(rawID);
                //cmdStockRawStockService.SetRawStockQty(rawID, oPO_QTY + qty, userItem.EMPLOYEE_ID);
                int sumRemain = cmdStockRawLotService.GetSumRemainQty(rawID);
                cmdStockRawStockService.SetRawStockQty(rawID, sumRemain, userItem.EMPLOYEE_ID);
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }
        private bool UpdateStockRawLot(int rawID,string lotNo, int qty)
        {
            try
            {
                int RAW_ID = rawID;
                USER userItem = Session["user"] as USER;
                cmdStockRawLotService.SetRawLotQty(RAW_ID, qty, userItem.EMPLOYEE_ID, lotNo);
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }
        private bool UpdateStockRawTrans(string lot,int qty,int vendorID)
        {
            try
            {
                USER userItem = Session["user"] as USER;
                VENDOR _VENDOR = cmdSupplierService.Select(vendorID);
                STOCK_RAW_TRANS _STOCK_RAW_TRANS = new STOCK_RAW_TRANS();
                _STOCK_RAW_TRANS.TRANS_ID = cmdStockRawTransService.GetNextTransID();
                _STOCK_RAW_TRANS.RAW_ID = int.Parse(Request.QueryString["RAW_ID"].ToString());
                _STOCK_RAW_TRANS.TRANS_DATE = DateTime.Now;
                _STOCK_RAW_TRANS.TRANS_TYPE = "SET";
                _STOCK_RAW_TRANS.REF_DOC_TYPE = "SET";
                _STOCK_RAW_TRANS.REF_DOC_BKNO = "SET";
                _STOCK_RAW_TRANS.REF_DOC_RNNO = "SET";
                _STOCK_RAW_TRANS.REF_DOC_YY = DateTime.Now.ToString("yy");
                _STOCK_RAW_TRANS.VENDOR_ID = _VENDOR.VENDOR_ID;
                _STOCK_RAW_TRANS.VENDOR_CODE = _VENDOR.VENDOR_CODE;
                _STOCK_RAW_TRANS.LOT_NO = lot;
                _STOCK_RAW_TRANS.REF_REMARK1 = "";
                _STOCK_RAW_TRANS.REF_REMARK2 = "";
                _STOCK_RAW_TRANS.TRANS_QTY = qty;
                _STOCK_RAW_TRANS.APPROVE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                _STOCK_RAW_TRANS.SYS_TIME = DateTime.Now.TimeOfDay;
                _STOCK_RAW_TRANS.CREATE_DATE = DateTime.Now;
                _STOCK_RAW_TRANS.UPDATE_DATE = DateTime.Now;
                _STOCK_RAW_TRANS.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                _STOCK_RAW_TRANS.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                _STOCK_RAW_TRANS.SYE_DEL = false;
                cmdStockRawTransService.Add(_STOCK_RAW_TRANS);
                return true;
            }
            catch (Exception e)
            {
                lblerror2.Text = "*พบข้อผิดพลาดระหว่างบันทึกข้อมูล กรุณาติดต่อเจ้าหน้าที่";
                return false;
            }
        }
        private bool ValidateAllScreenData()
        {
            bool returnValue = true;
            try
            {
                int result = 0;
                if (txtVendorName.Text == "" || txtVendorCode.Text == "")
                {
                    returnValue = false;
                    lblerror2.Text = "*กรุณาใส่ชื่อผู้จำหน่าย";
                }
                else if (txtStockQty.Text == "" || !int.TryParse(txtStockQty.Text, out result))
                {
                    returnValue = false;
                    lblerror2.Text = "*กรุณาใส่จำนวนให้ถูกต้อง";
                }
                else
                    returnValue = true;
            }
            catch (Exception e)
            {
                lblerror2.Text = "*พบข้อผิดพลาดระหว่างการตรวจสอบ กรุณาติดต่อเจ้าหน้าที่";
                return false;
            }
            return returnValue;
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
            try
            {
                if (ValidateAllScreenData())
                {
                    CheckRawStock();
                    if (SaveStockRawTrans())
                    {
                        if (SaveReceiveRawStock())
                        {
                            alert.Visible = true;
                            Response.AppendHeader("Refresh", "2; url=StockRaw.aspx");
                        }
                        else
                            lblerror2.Text = "*Fail to commit stock";
                    }
                    else
                        lblerror2.Text = "*Fail to commit transection";
                }
            }
            catch (Exception ex)
            {
                lblerror2.Text = "*Critical error";
            }
        }
        protected void txtVendorName_TextChanged(object sender, EventArgs e)
        {
            VENDOR _vendor = GetVendorCode(txtVendorName.Text);
            if (_vendor != null)
            {
                txtVendorCode.Text = _vendor.VENDOR_CODE.ToString();
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                bool isError = false;
                foreach (GridViewRow row in gdvRawSetting.Rows)
                {
                    int rawID = int.Parse(((Label)row.FindControl("lblRawID")).Text);
                    string lotNo = ((Label)row.FindControl("lblLotNo")).Text;
                    int vendorID = int.Parse(((Label)row.FindControl("lblVendorID")).Text);
                    int oldRawRemain = int.Parse(((HiddenField)row.FindControl("hfOldRawRemain")).Value);
                    int newRawRemain = int.Parse(((TextBox)row.FindControl("txtRawRemain")).Text);
                    if (oldRawRemain != newRawRemain)
                    {
                        if (UpdateStockRawLot(rawID, lotNo, newRawRemain))
                        {
                            if (UpdateStockRawStock(rawID, newRawRemain - oldRawRemain))
                            {
                                if (!UpdateStockRawTrans(lotNo, newRawRemain - oldRawRemain, vendorID))
                                {
                                    lblerror2.Text = "*Fail to update stock trans";
                                    isError = true;
                                }
                            }
                            else
                            {
                                lblerror2.Text = "*Fail to update stock";
                                isError = true;
                            }
                        }
                        else
                        {
                            lblerror2.Text = "*Fail to update stock lot";
                            isError = true;
                        }
                    }
                }

                if (!isError)
                {
                    alert.Visible = true;
                    Response.AppendHeader("Refresh", "2; url=StockRaw.aspx");
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                lblerror2.Text = "*Critical error";
            }
        }
        #endregion

    }
}