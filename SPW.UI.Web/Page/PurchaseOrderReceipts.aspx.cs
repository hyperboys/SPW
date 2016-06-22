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
    public partial class PurchaseOrderReceipts : System.Web.UI.Page
    {
        #region Declare variable
        private DataServiceEngine _dataServiceEngine;
        private SupplierService cmdSupplier;
        private RawPackService cmdRawPack;
        private RawProductService cmdRawProduct;
        private PoHdTransService cmdPoHdTrans;
        private PoDtTransService cmdPoDtTrans;
        private StockRawStockService cmdStockRawStockService;
        private StockRawTransService cmdStockRawTransService;
        private ReceiveRawTransService cmdReceiveRawTransService;


        public class DATAGRID
        {
            public RAW_PRODUCT RAW_PRODUCT { get; set; }
            public int RAW_ID { get; set; }
            public int RAW_PACK_ID { get; set; }
            public int STOCK_REMAIN { get; set; }
            public int PO_QTY { get; set; }
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
            cmdSupplier = (SupplierService)_dataServiceEngine.GetDataService(typeof(SupplierService));
            cmdRawPack = (RawPackService)_dataServiceEngine.GetDataService(typeof(RawPackService));
            cmdRawProduct = (RawProductService)_dataServiceEngine.GetDataService(typeof(RawProductService));
            cmdPoHdTrans = (PoHdTransService)_dataServiceEngine.GetDataService(typeof(PoHdTransService));
            cmdPoDtTrans = (PoDtTransService)_dataServiceEngine.GetDataService(typeof(PoDtTransService));
            cmdStockRawStockService = (StockRawStockService)_dataServiceEngine.GetDataService(typeof(StockRawStockService));
            cmdStockRawTransService = (StockRawTransService)_dataServiceEngine.GetDataService(typeof(StockRawTransService));
            cmdReceiveRawTransService = (ReceiveRawTransService)_dataServiceEngine.GetDataService(typeof(ReceiveRawTransService));
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
                PO_HD_TRANS _PO_HD_TRANS = cmdPoHdTrans.Select(Request.QueryString["PO_BK_NO"].ToString(), Request.QueryString["PO_RN_NO"].ToString());
                List<PO_DT_TRANS> listPoDtTrans = cmdPoDtTrans.Select(Request.QueryString["PO_BK_NO"].ToString(), Request.QueryString["PO_RN_NO"].ToString());
                List<DATAGRID> listDataGrid = new List<DATAGRID>();
                DATAGRID _datagrid = new DATAGRID();

                if (_PO_HD_TRANS != null)
                {
                    SetStatusBar(_PO_HD_TRANS.PO_HD_STATUS);
                    txtBKNo.Text = _PO_HD_TRANS.PO_BK_NO;
                    txtRNNo.Text = _PO_HD_TRANS.PO_RN_NO;
                    txtVendorName.Text = _PO_HD_TRANS.VENDOR_NAME;
                    txtVendorCode.Text = _PO_HD_TRANS.VENDOR_CODE;
                    flag.Text = "Edit";

                    listPoDtTrans.ForEach(e =>
                    {
                        _datagrid = new DATAGRID();
                        _datagrid.RAW_PRODUCT = cmdRawProduct.Select(e.RAW_ID);
                        _datagrid.RAW_PACK_ID = e.RAW_PACK_ID;
                        _datagrid.STOCK_REMAIN = cmdStockRawStockService.GetRemainQty(e.RAW_ID);
                        _datagrid.PO_QTY = e.PO_QTY;
                        _datagrid.RAW_ID = e.RAW_ID;
                        listDataGrid.Add(_datagrid);
                    });

                    Session["LISTDATAGRID"] = listDataGrid;
                    gdvREC.DataSource = listDataGrid;
                    gdvREC.DataBind();
                }
            }
            else
            {
                ClearSession();
            }
        }
        #endregion

        #region Business
        private RAW_PRODUCT GetProductCode(string RawName)
        {
            return cmdRawProduct.Select(RawName);
        }

        private VENDOR GetVendorCode(string VendorName)
        {
            return cmdSupplier.Select(VendorName);
        }
        
        private bool SaveStockRawTrans()
        {
            try
            {
                List<DATAGRID> listDataGrid = (List<DATAGRID>)Session["LISTDATAGRID"];
                if (listDataGrid.Count > 0)
                {
                    PO_HD_TRANS _PO_HD_TRANS = new PO_HD_TRANS();
                    USER userItem = Session["user"] as USER;
                    _PO_HD_TRANS = cmdPoHdTrans.Select(txtBKNo.Text, txtRNNo.Text);
                    listDataGrid.ForEach(e =>
                    {
                        STOCK_RAW_TRANS _STOCK_RAW_TRANS = new STOCK_RAW_TRANS();
                        _STOCK_RAW_TRANS.TRANS_ID = cmdStockRawTransService.GetNextTransID();
                        _STOCK_RAW_TRANS.RAW_ID = e.RAW_ID;
                        _STOCK_RAW_TRANS.TRANS_DATE = DateTime.Now;
                        _STOCK_RAW_TRANS.TRANS_TYPE = "REC";
                        _STOCK_RAW_TRANS.REF_DOC_TYPE = "PO";
                        _STOCK_RAW_TRANS.REF_DOC_BKNO = _PO_HD_TRANS.PO_BK_NO;
                        _STOCK_RAW_TRANS.REF_DOC_RNNO = _PO_HD_TRANS.PO_RN_NO;
                        _STOCK_RAW_TRANS.REF_DOC_YY = int.Parse(_PO_HD_TRANS.PO_YY);
                        _STOCK_RAW_TRANS.REF_NO1 = 0;
                        _STOCK_RAW_TRANS.REF_NO2 = 0;
                        _STOCK_RAW_TRANS.REF_REMARK1 = "";
                        _STOCK_RAW_TRANS.REF_REMARK2 = "";
                        _STOCK_RAW_TRANS.STOCK_BEFORE = cmdStockRawStockService.GetRemainQty(e.RAW_ID);
                        _STOCK_RAW_TRANS.TRANS_QTY = e.PO_QTY;
                        _STOCK_RAW_TRANS.STOCK_AFTER = cmdStockRawStockService.GetRemainQty(e.RAW_ID) + e.PO_QTY;
                        _STOCK_RAW_TRANS.APPROVE_EMPLOYEE_ID = _PO_HD_TRANS.CREATE_EMPLOYEE_ID;
                        _STOCK_RAW_TRANS.SYS_TIME = DateTime.Now.TimeOfDay;
                        _STOCK_RAW_TRANS.CREATE_DATE = DateTime.Now;
                        _STOCK_RAW_TRANS.UPDATE_DATE = DateTime.Now;
                        _STOCK_RAW_TRANS.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        _STOCK_RAW_TRANS.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        _STOCK_RAW_TRANS.SYE_DEL = false;
                        cmdStockRawTransService.Add(_STOCK_RAW_TRANS);
                    });
                }
                if (SaveReceiveRawTrans())
                {
                    ClearSession();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                lblerror2.Text = "*พบข้อผิดพลาดระหว่างบันทึกข้อมูล กรุณาติดต่อเจ้าหน้าที่";
                return false;
            }
        }
        private bool SaveReceiveRawTrans()
        {
            try
            {
                List<DATAGRID> listDataGrid = (List<DATAGRID>)Session["LISTDATAGRID"];
                if (listDataGrid.Count > 0)
                {
                    PO_HD_TRANS _PO_HD_TRANS = cmdPoHdTrans.Select(txtBKNo.Text, txtRNNo.Text);
                    List<PO_DT_TRANS> LstPO_DT_TRANS = cmdPoDtTrans.Select(txtBKNo.Text, txtRNNo.Text);
                    USER userItem = Session["user"] as USER;
                    int count = 1;
                    listDataGrid.ForEach(e =>
                    {
                        RECEIVE_RAW_TRANS _RECEIVE_RAW_TRANS = new RECEIVE_RAW_TRANS();
                        _RECEIVE_RAW_TRANS.RECEIVE_NO = GenerateReceiveNo();
                        _RECEIVE_RAW_TRANS.RECEIVE_YY = DateTime.Now.ToString("yy");
                        _RECEIVE_RAW_TRANS.RECEIVE_DATE = DateTime.Now;
                        _RECEIVE_RAW_TRANS.PO_BK_NO = _PO_HD_TRANS.PO_BK_NO;
                        _RECEIVE_RAW_TRANS.PO_RN_NO = _PO_HD_TRANS.PO_RN_NO;
                        _RECEIVE_RAW_TRANS.PO_YY = _PO_HD_TRANS.PO_YY;
                        _RECEIVE_RAW_TRANS.PO_SEQ_NO = LstPO_DT_TRANS.Where(f => f.RAW_ID == e.RAW_ID).FirstOrDefault().PO_SEQ_NO;
                        _RECEIVE_RAW_TRANS.RAW_ID = e.RAW_ID;
                        _RECEIVE_RAW_TRANS.RECEIVE_QTY = e.PO_QTY;
                        _RECEIVE_RAW_TRANS.RECEIVE_COMPLETE = (LstPO_DT_TRANS.Where(f => f.RAW_ID == e.RAW_ID).FirstOrDefault().PO_QTY == e.PO_QTY) ? "YES" : "NO";
                        _RECEIVE_RAW_TRANS.APPROVE_EMPLOYEE_ID = LstPO_DT_TRANS.Where(f => f.RAW_ID == e.RAW_ID).FirstOrDefault().CREATE_EMPLOYEE_ID;
                        _RECEIVE_RAW_TRANS.SYS_TIME = DateTime.Now.TimeOfDay;
                        _RECEIVE_RAW_TRANS.CREATE_DATE = DateTime.Now;
                        _RECEIVE_RAW_TRANS.UPDATE_DATE = DateTime.Now;
                        _RECEIVE_RAW_TRANS.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        _RECEIVE_RAW_TRANS.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        _RECEIVE_RAW_TRANS.SYE_DEL = false;
                        cmdReceiveRawTransService.Add(_RECEIVE_RAW_TRANS);
                        count++;
                    });
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }
        private string GenerateReceiveNo()
        {
            string _maxBKNo = cmdReceiveRawTransService.GetMaxBKNo();

            if (_maxBKNo == null)
            {
                _maxBKNo = "REC-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + "-0001";
            }
            else
            {
                int nextNo = int.Parse(_maxBKNo.Substring(8, 4)) + 1;
                _maxBKNo = "REC-" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + "-" + nextNo.ToString().PadLeft(4, '0');
            }
            return _maxBKNo;
        }
        private void ClearSession()
        {
            Session["LISTDATAGRID"] = null;
            Session.Remove("LISTDATAGRID");
        }
        private void SetStatusBar(string status)
        {
            switch (status)
            {
                case "10":
                    btnActive.CssClass = "btn btn-success";
                    break;
                case "20":
                    btnApprove.CssClass = "btn btn-success";
                    break;
                case "30":
                    btnFinish.CssClass = "btn btn-success";
                    break;
                case "40":
                    btnCancel.CssClass = "btn btn-danger";
                    break;
            }  
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
            List<DATAGRID> listDataGrid = (List<DATAGRID>)Session["LISTDATAGRID"];
            List<DATAGRID> listNewData = new List<DATAGRID>();
            if (listDataGrid != null)
            {
                for (int i = 0; i < gdvREC.Rows.Count; i++)
                {
                    TextBox txtQtyReceive = (TextBox)gdvREC.Rows[i].FindControl("txtQtyReceive");
                    Label lblRawID = (Label)gdvREC.Rows[i].FindControl("lblRawID");
                    DATAGRID _DATAGRID = listDataGrid.Where(data => data.RAW_ID == int.Parse(lblRawID.Text)).FirstOrDefault();
                    _DATAGRID.PO_QTY = int.Parse(txtQtyReceive.Text);
                    listNewData.Add(_DATAGRID);
                }
                Session["LISTDATAGRID"] = listNewData;
                if (SaveStockRawTrans())
                {
                    alert.Visible = true;
                    Response.AppendHeader("Refresh", "2; url=SearchPurchaseRequisitionOrder.aspx");
                }
                else
                    lblerror2.Text = "*Fail";
            }
            else
                lblerror2.Text = "*Data not found";

        }
        #endregion

    }
}