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
    public partial class IssuePurchaseOrders : System.Web.UI.Page
    {
        #region Declare variable
        private DataServiceEngine _dataServiceEngine;
        private SupplierService cmdSupplier;
        private RawPackService cmdRawPack;
        private RawProductService cmdRawProduct;
        private PoHdTransService cmdPoHdTrans;
        private PoDtTransService cmdPoDtTrans;


        public class DATAGRID
        {
            public RAW_PRODUCT RAW_PRODUCT { get; set; }
            public int RAW_ID { get; set; }
            public int RAW_PACK_ID { get; set; }
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
                    txtBKNo.Text = _PO_HD_TRANS.PO_BK_NO;
                    txtRNNo.Text = _PO_HD_TRANS.PO_RN_NO;
                    txtVendorName.Text = _PO_HD_TRANS.VENDOR_NAME;
                    txtVendorCode.Text = _PO_HD_TRANS.VENDOR_CODE;
                    flag.Text = "Edit";
                    lblName.Text = "Edit Purchase Order";

                    listPoDtTrans.ForEach(e =>
                    {
                        _datagrid = new DATAGRID();
                        _datagrid.RAW_PRODUCT = cmdRawProduct.Select(e.RAW_ID);
                        _datagrid.RAW_PACK_ID = e.RAW_PACK_ID;
                        _datagrid.PO_QTY = e.PO_QTY;
                        _datagrid.RAW_ID = e.RAW_ID;
                        listDataGrid.Add(_datagrid);
                    });

                    gdvPO.DataSource = listDataGrid;
                    gdvPO.DataBind();
                }
            }

            ViewState["listRawPack"] = cmdRawPack.GetAll();
            foreach (var item in (List<RAW_PACK_SIZE>)ViewState["listRawPack"])
            {
                ddlPack.Items.Add(new ListItem(item.RAW_PACK_SIZE1, item.RAW_PACK_ID.ToString()));
            }

            AutoCompleteTxtRawName();
            AutoCompleteTxtVendorName();

            ClearSession();
        }
        #endregion

        #region Business

        private void AutoCompleteTxtRawName()
        {
            List<string> nameList = SearchAutoCompleteDataService.Search("RAW_PRODUCT", "RAW_NAME1", "RAW_NAME1", "");
            string str = "";
            for (int i = 0; i < nameList.Count; i++)
            {
                str = str + '"' + nameList[i].ToString() + '"' + ',';
            }
            if (str != "")
            {
                str = str.Remove(str.Length - 1);
            }
            str = "[" + str + "]";
            txtRawName.Attributes.Add("data-source", str);
        }

        private void AutoCompleteTxtVendorName()
        {
            List<string> nameList = SearchAutoCompleteDataService.Search("VENDOR", "VENDOR_NAME", "VENDOR_NAME", "");
            string str = "";
            for (int i = 0; i < nameList.Count; i++)
            {
                str = str + '"' + nameList[i].ToString() + '"' + ',';
            }
            if (str != "")
            {
                str = str.Remove(str.Length - 1);
            }
            str = "[" + str + "]";
            txtVendorName.Attributes.Add("data-source", str);
        }

        private RAW_PRODUCT GetProductCode(string RawName)
        {
            return cmdRawProduct.Select(RawName);
        }

        private VENDOR GetVendorCode(string VendorName)
        {
            return cmdSupplier.Select(VendorName);
        }

        private bool ValidateAllScreenData()
        {
            bool returnValue = true;
            try
            {
                if (isFoundVendorCode.Value != "true")
                {
                    returnValue = false;
                    lblerror2.Text = "*ไม่พบรหัสผู้จำหน่าย";
                }
                else if (txtVendorName.Text == "" || txtVendorCode.Text == "")
                {
                    returnValue = false;
                    lblerror2.Text = "*กรุณาใส่ชื่อผู้จำหน่าย";
                }
                else if (((List<DATAGRID>)Session["LISTDATAGRID"]).Count == 0 || Session["LISTDATAGRID"] == null)
                {
                    returnValue = false;
                    lblerror2.Text = "*ไม่พบรายการสินค้าในระบบ";
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

        private bool ValidateRawData()
        {
            bool returnValue = true;
            try
            {
                if (txtRawName.Text == "" || txtRawCode.Text == "")
                {
                    returnValue = false;
                    lblError.Text = "*ไม่พบรหัสสินค้า";
                }
                else if (ddlPack.SelectedIndex == 0 || txtQty.Text == "")
                {
                    returnValue = false;
                    lblError.Text = "*กรุณากรอกข้อมูลให้ครบ";
                }
                else if (Session["LISTDATAGRID"] != null && ((List<DATAGRID>)Session["LISTDATAGRID"]).Any(e => e.RAW_ID == int.Parse(txtRawCode.Text)))
                {
                    returnValue = false;
                    lblError.Text = "*มีสินค้านี้อยู่ในรายการแล้ว";
                }
                else
                    returnValue = true;
            }
            catch (Exception e)
            {
                lblError.Text = "*พบข้อผิดพลาดระหว่างการตรวจสอบ กรุณาติดต่อเจ้าหน้าที่";
                return false;
            }
            return returnValue;
        }

        private bool SaveNewData()
        {
            try
            {
                USER userItem = Session["user"] as USER;
                PO_HD_TRANS _PO_HD_TRANS = new PO_HD_TRANS();
                VENDOR _VENDOR = cmdSupplier.SelectByVendorCode(txtVendorCode.Text);
                string PO_BK_NO = (txtBKNo.Text == "") ? GenerateBKNo() : txtBKNo.Text;
                string PO_RN_NO = (txtRNNo.Text == "") ? GenerateRNNo() : txtRNNo.Text;
                _PO_HD_TRANS.PO_BK_NO = PO_BK_NO;
                _PO_HD_TRANS.PO_RN_NO = PO_RN_NO;
                _PO_HD_TRANS.PO_YY = DateTime.Now.ToString("yy");
                _PO_HD_TRANS.PO_DATE = DateTime.Now;
                _PO_HD_TRANS.VENDOR_ID = _VENDOR.VENDOR_ID;
                _PO_HD_TRANS.VENDOR_CODE = _VENDOR.VENDOR_CODE;
                _PO_HD_TRANS.VENDOR_NAME = _VENDOR.VENDOR_NAME;
                _PO_HD_TRANS.VENDOR_TEL1 = _VENDOR.VENDOR_TEL1;
                _PO_HD_TRANS.VENDOR_TEL2 = _VENDOR.VENDOR_TEL2;
                _PO_HD_TRANS.VENDOR_MOBILE = _VENDOR.VENDOR_MOBILE;
                _PO_HD_TRANS.VENDOR_FAX = _VENDOR.VENDOR_FAX;
                _PO_HD_TRANS.VENDOR_EMAIL = _VENDOR.VENDOR_EMAIL;
                _PO_HD_TRANS.VENDOR_CONTACT_PERSON = _VENDOR.VENDOR_CONTACT_PERSON;
                _PO_HD_TRANS.PO_RECEIVE_PERSON = userItem.USER_NAME;
                _PO_HD_TRANS.VENDOR_CREDIT_INTERVAL = _VENDOR.VENDOR_CREDIT_INTERVAL;
                _PO_HD_TRANS.VENDOR_CREDIT_VALUE = _VENDOR.VENDOR_CREDIT_VALUE;
                _PO_HD_TRANS.PO_HD_STATUS = "10";
                _PO_HD_TRANS.CREATE_DATE = DateTime.Now;
                _PO_HD_TRANS.UPDATE_DATE = DateTime.Now;
                _PO_HD_TRANS.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                _PO_HD_TRANS.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                _PO_HD_TRANS.SYE_DEL = false;
                cmdPoHdTrans.Add(_PO_HD_TRANS);
                if (SaveNewDataGrid(PO_BK_NO, PO_RN_NO))
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
        private bool SaveNewDataGrid(string PO_BK_NO, string PO_RN_NO)
        {
            try
            {
                USER userItem = Session["user"] as USER;
                List<PO_DT_TRANS> listPoDtTrans = new List<PO_DT_TRANS>();
                List<DATAGRID> listDataGrid = (List<DATAGRID>)Session["LISTDATAGRID"];
                if (listDataGrid.Count > 0)
                {
                    int count = 1;
                    listDataGrid.ForEach(e =>
                    {
                        PO_DT_TRANS _PO_DT_TRANS = new PO_DT_TRANS();
                        _PO_DT_TRANS.PO_BK_NO = PO_BK_NO;
                        _PO_DT_TRANS.PO_RN_NO = PO_RN_NO;
                        _PO_DT_TRANS.PO_YY = DateTime.Now.ToString("yy");
                        _PO_DT_TRANS.PO_SEQ_NO = count;
                        _PO_DT_TRANS.RAW_ID = e.RAW_ID;
                        _PO_DT_TRANS.RAW_PACK_ID = e.RAW_PACK_ID;
                        _PO_DT_TRANS.PO_QTY = e.PO_QTY;
                        _PO_DT_TRANS.REMARK1 = "-";
                        _PO_DT_TRANS.REMARK2 = "-";
                        _PO_DT_TRANS.PO_DT_STATUS = "10";
                        _PO_DT_TRANS.CREATE_DATE = DateTime.Now;
                        _PO_DT_TRANS.UPDATE_DATE = DateTime.Now;
                        _PO_DT_TRANS.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        _PO_DT_TRANS.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        _PO_DT_TRANS.SYE_DEL = false;
                        _PO_DT_TRANS.Action = ActionEnum.Create;
                        listPoDtTrans.Add(_PO_DT_TRANS);
                        count++;
                    });
                }

                cmdPoDtTrans.AddList(listPoDtTrans);
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }
        private string GenerateBKNo()
        {
            string _maxBKNo = cmdPoHdTrans.GetMaxBKNo();
            string _maxRNNo = cmdPoHdTrans.GetMaxRNNo(_maxBKNo);

            if (_maxRNNo == null)
            {
                _maxBKNo = "BK-PO-" + DateTime.Now.ToString("yy") + "01";
            }
            else if (int.Parse(_maxRNNo) < 999)
            {
                _maxBKNo = cmdPoHdTrans.GetMaxBKNo();
            }
            else
            {
                int nextNo = int.Parse(_maxBKNo.Substring(8, 2)) + 1;
                _maxBKNo = "BK-PO-" + DateTime.Now.ToString("yy") + nextNo.ToString().PadLeft(2, '0');
            }
            return _maxBKNo;
        }
        private string GenerateRNNo()
        {
            string _maxBKNo = cmdPoHdTrans.GetMaxBKNo();
            string _maxRNNo = cmdPoHdTrans.GetMaxRNNo(_maxBKNo);

            if (_maxRNNo == null || _maxRNNo == "0999")
            {
                _maxRNNo = "0001";
            }
            else if (int.Parse(_maxRNNo) < 999)
            {
                int nextNo = int.Parse(_maxRNNo) + 1;
                _maxRNNo = nextNo.ToString().PadLeft(4, '0');
            }
            return _maxRNNo;
        }
        private void ClearDataScreen()
        {
            txtRawCode.Text = String.Empty;
            txtRawName.Text = String.Empty;
            ddlPack.SelectedIndex = 0;
            txtQty.Text = String.Empty;
            spRawCode.Visible = false;
            lblError.Text = String.Empty;
        }
        private void ClearSession()
        {
            Session["LISTDATAGRID"] = null;
            Session.Remove("LISTDATAGRID");
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

        protected void txtRawName_TextChanged(object sender, EventArgs e)
        {
            ViewState["RAWPRODUCT"] = GetProductCode(txtRawName.Text);
            if (ViewState["RAWPRODUCT"] != null)
            {
                txtRawCode.Text = ((RAW_PRODUCT)ViewState["RAWPRODUCT"]).RAW_ID.ToString();
                if (txtRawCode.Text != "")
                {
                    isFoundVendorCode.Value = "true";
                    spRawCode.Visible = true;
                }
                else
                {
                    isFoundVendorCode.Value = "false";
                    spRawCode.Visible = false;
                }
            }
            else
            {
                isFoundVendorCode.Value = "false";
                spRawCode.Visible = false;
            }

        }

        protected void txtVendorName_TextChanged(object sender, EventArgs e)
        {
            VENDOR _vendor = GetVendorCode(txtVendorName.Text);
            if (_vendor != null)
            {
                txtVendorCode.Text = _vendor.VENDOR_CODE.ToString();
                if (txtRawCode.Text != "")
                {
                    isFoundRawCode.Value = "true";
                    spVendorCode.Visible = true;
                }
                else
                {
                    isFoundRawCode.Value = "false";
                    spVendorCode.Visible = false;
                }

            }
            else
            {
                isFoundRawCode.Value = "false";
                spVendorCode.Visible = false;
            }

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateRawData())
                {
                    if (ViewState["RAWPRODUCT"] != null)
                    {
                        List<DATAGRID> listDataGrid = (Session["LISTDATAGRID"] == null) ? new List<DATAGRID>() : (List<DATAGRID>)Session["LISTDATAGRID"];
                        DATAGRID _datagrid = new DATAGRID();
                        RAW_PRODUCT _rawProduct = (RAW_PRODUCT)ViewState["RAWPRODUCT"];
                        _datagrid.RAW_PRODUCT = _rawProduct;
                        _datagrid.RAW_PACK_ID = int.Parse(ddlPack.SelectedValue);
                        _datagrid.PO_QTY = int.Parse(txtQty.Text);
                        _datagrid.RAW_ID = int.Parse(txtRawCode.Text);

                        listDataGrid.Add(_datagrid);
                        Session["LISTDATAGRID"] = listDataGrid;

                        ViewState["RAWPRODUCT"] = null;

                        gdvPO.DataSource = (List<DATAGRID>)Session["LISTDATAGRID"];
                        gdvPO.DataBind();
                        ClearDataScreen();

                        if (((List<DATAGRID>)Session["LISTDATAGRID"]).Count > 0)
                        {
                            btnApprove.Visible = true;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "*พบข้อผิดพลาดจากการตรวจสอบข้อมูล กรุณาติดต่อเจ้าหน้าที่";
            }
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            USER userItem = Session["user"] as USER;
            cmdPoHdTrans.UpdateStatusToApprove(txtBKNo.Text, txtRNNo.Text, userItem.EMPLOYEE_ID);
            cmdPoDtTrans.UpdateStatusToApprove(txtBKNo.Text, txtRNNo.Text, userItem.EMPLOYEE_ID);

            alert.Visible = true;
            Response.AppendHeader("Refresh", "2; url=SearchPurchaseOrder.aspx");
        }
        protected void gdvPO_RowEditing(object sender, GridViewEditEventArgs e)
        {
            lblError.Text = "555";
        }

        #endregion
    }
}