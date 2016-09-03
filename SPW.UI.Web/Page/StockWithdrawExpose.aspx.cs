﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using SPW.DAL;
using SPW.Common;

namespace SPW.UI.Web.Page
{
    public partial class StockWithdrawExpose : System.Web.UI.Page
    {
        #region Declare variable
        private DataServiceEngine _dataServiceEngine;
        private RawProductService cmdRawProductService;
        private WrDtTransService cmdWrDtTransService;
        private RawPackService cmdRawPackService;
        private StockRawLotService cmdStockRawLotService;
        private StockRawTransService cmdStockRawTransService;
        private StockRawStockService cmdStockRawStockService;        

        public class DATAGRID
        {
            public RAW_PRODUCT RAW_PRODUCT { get; set; }
            public int RAW_ID { get; set; }
            public int WR_QTY { get; set; }
            public int WD_QTY { get; set; }
            public int RAW_PACK_ID { get; set; }
            public string LOT_NO { get; set; }
            public int RAW_REMAIN { get; set; }
            public int VENDOR_ID { get; set; }
            public string VENDOR_CODE { get; set; }
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
            cmdRawProductService = (RawProductService)_dataServiceEngine.GetDataService(typeof(RawProductService));
            cmdWrDtTransService = (WrDtTransService)_dataServiceEngine.GetDataService(typeof(WrDtTransService));
            cmdRawPackService = (RawPackService)_dataServiceEngine.GetDataService(typeof(RawPackService));
            cmdStockRawLotService = (StockRawLotService)_dataServiceEngine.GetDataService(typeof(StockRawLotService));
            cmdStockRawTransService = (StockRawTransService)_dataServiceEngine.GetDataService(typeof(StockRawTransService));
            cmdStockRawStockService = (StockRawStockService)_dataServiceEngine.GetDataService(typeof(StockRawStockService));
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }


        private void InitialData()
        {
            if (Request.QueryString["WR_BK_NO"] != null && Request.QueryString["WR_RN_NO"] != null && Request.QueryString["RAW_ID"] != null)
            {
                List<STOCK_RAW_LOT> lstSTOCK_RAW_LOT = cmdStockRawLotService.GetAll(int.Parse(Request.QueryString["RAW_ID"].ToString()));
                List<DATAGRID> listDataGrid = new List<DATAGRID>();
                WR_DT_TRANS _WR_DT_TRANS = cmdWrDtTransService.Select(Request.QueryString["WR_BK_NO"].ToString(), Request.QueryString["WR_RN_NO"].ToString(), int.Parse(Request.QueryString["RAW_ID"].ToString()));
                DATAGRID _datagrid = new DATAGRID();

                if (lstSTOCK_RAW_LOT != null)
                {
                    lstSTOCK_RAW_LOT.ForEach(e =>
                    {
                        _datagrid = new DATAGRID();
                        _datagrid.RAW_PRODUCT = cmdRawProductService.Select(e.RAW_ID);
                        _datagrid.RAW_ID = e.RAW_ID;
                        _datagrid.LOT_NO = e.LOT_NO;
                        _datagrid.RAW_REMAIN = e.RAW_REMAIN;
                        _datagrid.WR_QTY = _WR_DT_TRANS.WR_QTY;
                        _datagrid.WD_QTY = 0;
                        _datagrid.VENDOR_ID = e.VENDOR_ID;
                        _datagrid.VENDOR_CODE = e.VENDOR_CODE;
                        listDataGrid.Add(_datagrid);
                    });

                    Session["LISTDATAGRID"] = listDataGrid;
                    gdvWR.DataSource = listDataGrid;
                    gdvWR.DataBind();
                }
                //List<WR_DT_TRANS> listWrDtTrans = cmdWrDtTransService.GetAll(Request.QueryString["WR_BK_NO"].ToString(), Request.QueryString["WR_RN_NO"].ToString());
                //List<DATAGRID> listDataGrid = new List<DATAGRID>();
                //DATAGRID _datagrid = new DATAGRID();

                //flag.Text = "Edit";

                //listWrDtTrans.ForEach(e =>
                //{
                //    _datagrid = new DATAGRID();
                //    _datagrid.RAW_PRODUCT = cmdRawProductService.Select(e.RAW_ID);
                //    _datagrid.RAW_PACK_ID = e.RAW_PACK_ID;
                //    _datagrid.WR_QTY = e.WR_QTY;
                //    _datagrid.RAW_ID = e.RAW_ID;
                //    listDataGrid.Add(_datagrid);
                //});

                //gdvWR.DataSource = listDataGrid;
                //gdvWR.DataBind();
                //if (_PO_HD_TRANS.PO_HD_STATUS != "10")
                //{
                //    btnApprove.Visible = false;
                //}
            }
            ViewState["listRawPack"] = cmdRawPackService.GetAll();
            foreach (var item in (List<RAW_PACK_SIZE>)ViewState["listRawPack"])
            {
                ddlPack.Items.Add(new ListItem(item.RAW_PACK_SIZE1, item.RAW_PACK_ID.ToString()));
            }
            AutoCompleteTxtRawName();
            //ClearSession();
        }
        #endregion

        #region Business

        private void AutoCompleteTxtRawName()
        {
            List<string> nameList = SearchAutoCompleteDataService.Search("RAW_PRODUCT", "RAW_NAME1", "RAW_NAME1", "");
            string str = "";
            for (int i = 0; i < nameList.Count; i++)
            {
                nameList[i] = nameList[i].Replace("\"", "นิ้ว");
                nameList[i] = nameList[i].Replace("'", "นิ้ว");
                str = str + '"' + nameList[i].ToString().Replace(",", " ") + '"' + ',';
            }
            if (str != "")
            {
                str = str.Remove(str.Length - 1);
            }
            str = "[" + str + "]";
            txtRawName.Attributes.Add("data-source", str);
        }

        private bool ValidateRawData()
        {
            bool returnValue = true;
            try
            {
                if (ddlPack.SelectedIndex == 0 || txtWrQty.Text == "")
                {
                    returnValue = false;
                    lblError.Text = "*กรุณากรอกข้อมูลให้ครบ";
                }
                else if (txtRawName.Text == "" || txtRawID.Text == "")
                {
                    returnValue = false;
                    lblError.Text = "*ไม่พบรหัสสินค้า";
                }
                else if (Session["LISTDATAGRID"] != null && ((List<DATAGRID>)Session["LISTDATAGRID"]).Any(e => e.RAW_ID == int.Parse(txtRawID.Text)))
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

        private bool SaveNewWrDtTrans()
        {
            try
            {
                USER userItem = Session["user"] as USER;
                List<WR_DT_TRANS> lstWR_DT_TRANS = new List<WR_DT_TRANS>();
                List<DATAGRID> listDataGrid = (List<DATAGRID>)Session["LISTDATAGRID"];
                if (listDataGrid.Count > 0)
                {
                    int seq = 1;
                    listDataGrid.ForEach(e =>
                    {
                        WR_DT_TRANS _WR_DT_TRANS = new WR_DT_TRANS();
                        string WR_BK_NO = GenerateBKNo();
                        string WR_RN_NO = GenerateRNNo();
                        _WR_DT_TRANS.WR_BK_NO = WR_BK_NO;
                        _WR_DT_TRANS.WR_RN_NO = WR_RN_NO;
                        _WR_DT_TRANS.WR_DATE = DateTime.Now;
                        _WR_DT_TRANS.WR_YY = DateTime.Now.ToString("yy");
                        _WR_DT_TRANS.WR_SEQ_NO = seq;
                        _WR_DT_TRANS.RAW_ID = e.RAW_ID;
                        _WR_DT_TRANS.RAW_PACK_ID = e.RAW_PACK_ID;
                        _WR_DT_TRANS.WR_QTY = e.WR_QTY;
                        _WR_DT_TRANS.REMARK1 = "WR";
                        _WR_DT_TRANS.REMARK2 = "1";
                        _WR_DT_TRANS.WR_DT_STATUS = "10";
                        _WR_DT_TRANS.REQUEST_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        _WR_DT_TRANS.APPROVE_EMPLOYEE_ID = null;
                        _WR_DT_TRANS.WITHDRAWER_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        _WR_DT_TRANS.APPROVE_DATE = DateTime.Now;
                        _WR_DT_TRANS.WITHDRAW_DATE = DateTime.Now;
                        _WR_DT_TRANS.CREATE_DATE = DateTime.Now;
                        _WR_DT_TRANS.UPDATE_DATE = DateTime.Now;
                        _WR_DT_TRANS.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        _WR_DT_TRANS.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        _WR_DT_TRANS.SYE_DEL = false;
                        seq++;
                        lstWR_DT_TRANS.Add(_WR_DT_TRANS);
                    });
                    cmdWrDtTransService.AddList(lstWR_DT_TRANS);
                }

                return true;
            }
            catch (Exception e)
            {
                lblerror2.Text = "*พบข้อผิดพลาดระหว่างบันทึกข้อมูล กรุณาติดต่อเจ้าหน้าที่";
                return false;
            }
        }
        private string GenerateBKNo()
        {
            string _maxBKNo = cmdWrDtTransService.GetMaxBKNo();
            string _maxRNNo = cmdWrDtTransService.GetMaxRNNo(_maxBKNo);

            if (_maxRNNo == null)
            {
                _maxBKNo = "BK-PR-" + DateTime.Now.ToString("yy") + "01";
            }
            else if (int.Parse(_maxRNNo) < 999)
            {
                _maxBKNo = cmdWrDtTransService.GetMaxBKNo();
            }
            else
            {
                int nextNo = int.Parse(_maxBKNo.Substring(8, 2)) + 1;
                _maxBKNo = "BK-PR-" + DateTime.Now.ToString("yy") + nextNo.ToString().PadLeft(2, '0');
            }
            return _maxBKNo;
        }
        private string GenerateRNNo()
        {
            string _maxBKNo = cmdWrDtTransService.GetMaxBKNo();
            string _maxRNNo = cmdWrDtTransService.GetMaxRNNo(_maxBKNo);

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
            txtRawID.Text = String.Empty;
            txtRawName.Text = String.Empty;
            txtWrQty.Text = String.Empty;
            ddlPack.SelectedIndex = 0;
            spRawCode.Visible = false;
            lblError.Text = String.Empty;
        }
        private void ClearSession()
        {
            Session["LISTDATAGRID"] = null;
            Session.Remove("LISTDATAGRID");
        }
        private RAW_PRODUCT GetProductCode(string RawName)
        {
            return cmdRawProductService.Select(RawName);
        }
        private bool ValidateAllScreenData()
        {
            bool returnValue = true;
            try
            {
                if (Session["LISTDATAGRID"] == null || ((List<DATAGRID>)Session["LISTDATAGRID"]).Count == 0)
                {
                    returnValue = false;
                    lblerror2.Text = "*ไม่พบรายการสินค้าในระบบ";
                }
                else
                    returnValue = true;
            }
            catch (Exception e)
            {
                DebugLog.WriteLog(e.ToString());
                lblerror2.Text = "*พบข้อผิดพลาดระหว่างการตรวจสอบ กรุณาติดต่อเจ้าหน้าที่";
                return false;
            }
            return returnValue;
        }
        private bool SaveStockRawTrans()
        {
            try
            {
                List<DATAGRID> listDataGrid = (List<DATAGRID>)Session["LISTDATAGRID"];
                if (listDataGrid.Count > 0)
                {
                    USER userItem = Session["user"] as USER;
                    WR_DT_TRANS _WR_DT_TRANS = cmdWrDtTransService.Select(Request.QueryString["WR_BK_NO"].ToString(), Request.QueryString["WR_RN_NO"].ToString(), int.Parse(Request.QueryString["RAW_ID"].ToString()));
                    listDataGrid.ForEach(e =>
                    {
                        STOCK_RAW_TRANS _STOCK_RAW_TRANS = new STOCK_RAW_TRANS();
                        _STOCK_RAW_TRANS.TRANS_ID = cmdStockRawTransService.GetNextTransID();
                        _STOCK_RAW_TRANS.RAW_ID = e.RAW_ID;
                        _STOCK_RAW_TRANS.TRANS_DATE = DateTime.Now;
                        _STOCK_RAW_TRANS.TRANS_TYPE = "WD";
                        _STOCK_RAW_TRANS.REF_DOC_TYPE = "WR";
                        _STOCK_RAW_TRANS.REF_DOC_BKNO = _WR_DT_TRANS.WR_BK_NO;
                        _STOCK_RAW_TRANS.REF_DOC_RNNO = _WR_DT_TRANS.WR_RN_NO;
                        _STOCK_RAW_TRANS.REF_DOC_YY = _WR_DT_TRANS.WR_YY;
                        _STOCK_RAW_TRANS.VENDOR_ID = e.VENDOR_ID;
                        _STOCK_RAW_TRANS.VENDOR_CODE = e.VENDOR_CODE;
                        _STOCK_RAW_TRANS.LOT_NO = e.LOT_NO;
                        _STOCK_RAW_TRANS.REF_REMARK1 = "";
                        _STOCK_RAW_TRANS.REF_REMARK2 = "";
                        _STOCK_RAW_TRANS.TRANS_QTY = e.WD_QTY;
                        _STOCK_RAW_TRANS.APPROVE_EMPLOYEE_ID = _WR_DT_TRANS.APPROVE_EMPLOYEE_ID;
                        _STOCK_RAW_TRANS.SYS_TIME = DateTime.Now.TimeOfDay;
                        _STOCK_RAW_TRANS.CREATE_DATE = DateTime.Now;
                        _STOCK_RAW_TRANS.UPDATE_DATE = DateTime.Now;
                        _STOCK_RAW_TRANS.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        _STOCK_RAW_TRANS.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        _STOCK_RAW_TRANS.SYE_DEL = false;
                        cmdStockRawTransService.Add(_STOCK_RAW_TRANS);
                    });
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
                List<DATAGRID> listDataGrid = (List<DATAGRID>)Session["LISTDATAGRID"];
                if (listDataGrid.Count > 0)
                {
                    USER userItem = Session["user"] as USER;
                    listDataGrid.ForEach(e =>
                    {
                        cmdStockRawLotService.Edit(e.RAW_ID, e.LOT_NO, cmdStockRawLotService.GetRemainQty(e.RAW_ID, e.LOT_NO) - e.WD_QTY, userItem.EMPLOYEE_ID);
                        cmdWrDtTransService.UpdateStatusWrDtByProduct(e.RAW_ID,Request.QueryString["WR_BK_NO"].ToString(), Request.QueryString["WR_RN_NO"].ToString(), userItem.EMPLOYEE_ID, "30");
                    });
                }
                if (SaveReceiveRawStock())
                    return true;
                else
                    return false;
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
                List<DATAGRID> listDataGrid = (List<DATAGRID>)Session["LISTDATAGRID"];
                USER userItem = Session["user"] as USER;
                if (listDataGrid.Count > 0)
                {
                    listDataGrid.ForEach(e =>
                    {
                        int _RAW_REMAIN = cmdStockRawStockService.GetRemainQty(e.RAW_ID);
                        cmdStockRawStockService.SetRawStockQty(e.RAW_ID, _RAW_REMAIN - e.WD_QTY, userItem.EMPLOYEE_ID);
                    });
                }
                ClearSession();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
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
        protected void txtRawName_TextChanged(object sender, EventArgs e)
        {
            ViewState["RAWPRODUCT"] = GetProductCode(txtRawName.Text);
            if (ViewState["RAWPRODUCT"] != null)
            {
                txtRawID.Text = ((RAW_PRODUCT)ViewState["RAWPRODUCT"]).RAW_ID.ToString();
                if (txtRawID.Text != "")
                {
                    spRawCode.Visible = true;
                }
                else
                {
                    spRawCode.Visible = false;
                }
            }
            else
            {
                spRawCode.Visible = false;
                txtRawID.Text = string.Empty;
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
                        _datagrid.RAW_ID = int.Parse(txtRawID.Text);
                        _datagrid.WR_QTY = int.Parse(txtWrQty.Text);
                        _datagrid.RAW_PACK_ID = int.Parse(ddlPack.SelectedValue);

                        listDataGrid.Add(_datagrid);
                        Session["LISTDATAGRID"] = listDataGrid;

                        ViewState["RAWPRODUCT"] = null;

                        gdvWR.DataSource = (List<DATAGRID>)Session["LISTDATAGRID"];
                        gdvWR.DataBind();
                        ClearDataScreen();

                        if (((List<DATAGRID>)Session["LISTDATAGRID"]).Count > 0)
                        {
                            btnSave.Visible = true;
                        }
                        else
                        {
                            btnSave.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
                lblError.Text = "*พบข้อผิดพลาดจากการตรวจสอบข้อมูล กรุณาติดต่อเจ้าหน้าที่";
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<DATAGRID> listDataGrid = (List<DATAGRID>)Session["LISTDATAGRID"];
                List<DATAGRID> listNewData = new List<DATAGRID>();
                if (listDataGrid != null)
                {
                    bool isNotExceedLimit = true;
                    int sum = 0;
                    for (int i = 0; i < gdvWR.Rows.Count; i++)
                    {
                        TextBox txtWRQty = (TextBox)gdvWR.Rows[i].FindControl("txtWRQty");
                        Label lblRawID = (Label)gdvWR.Rows[i].FindControl("lblRawID");
                        DATAGRID _DATAGRID = listDataGrid.Where(data => data.RAW_ID == int.Parse(lblRawID.Text)).FirstOrDefault();
                        _DATAGRID.WD_QTY = int.Parse(txtWRQty.Text);
                        sum = sum + int.Parse(txtWRQty.Text);
                        listNewData.Add(_DATAGRID);
                        isNotExceedLimit = (int.Parse(txtWRQty.Text) < 0 || int.Parse(txtWRQty.Text) > _DATAGRID.RAW_REMAIN || sum > _DATAGRID.WR_QTY) ? false : isNotExceedLimit;
                    }
                    if (isNotExceedLimit)
                    {
                        Session["LISTDATAGRID"] = listNewData;
                        if (SaveStockRawTrans())
                        {
                            if (SaveRawLot())
                            {
                                alert.Visible = true;
                                Response.AppendHeader("Refresh", "2; url=SearchStockWithdrawExpose.aspx");
                            }
                            else
                                lblerror2.Text = "*Fail to commit stock";
                        }
                        else
                            lblerror2.Text = "*Fail to commit transection";
                    }
                    else
                        lblerror2.Text = "*Limit Exceed";
                }
                else
                    lblerror2.Text = "*Data not found";
            }
            catch (Exception ex)
            {
                lblerror2.Text = "*Critical error";
            }
        }

        protected void gdvWR_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<DATAGRID> listDataGrid = (List<DATAGRID>)Session["LISTDATAGRID"];
            listDataGrid.RemoveAt(e.RowIndex);
            Session["LISTDATAGRID"] = listDataGrid;

            gdvWR.DataSource = (List<DATAGRID>)Session["LISTDATAGRID"];
            gdvWR.DataBind();
        }
        protected void gdvWR_RowEditing(object sender, GridViewEditEventArgs e)
        {
            lblError.Text = "555";
        }
        #endregion
    }
}