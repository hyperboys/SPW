﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using SPW.Common;
using System.Transactions;

namespace SPW.UI.Web.Page
{
    public partial class ManageOrderDeliveryDetail : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private DeliveryIndexService cmdDeliveryOrderIndex;
        private DeliveryOrderDetailService cmdDelOrderDetails;
        private DeliveryOrderService cmdDelOrder;
        private OrderService cmdOrder;
        private OrderDetailService cmdOrderDetails;
        private StockProductService cmdStockProductService;
        private StockTransService cmdStockTransService;
        private ProductService cmdProductService;
        private StockProductWithdrawService cmdStockProductWithdrawService;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CreatePageEngine();
                PrepareDefaultScreen();
            }
            else
            {
                ReloadPageEngine();
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
            cmdDeliveryOrderIndex = (DeliveryIndexService)_dataServiceEngine.GetDataService(typeof(DeliveryIndexService));
            cmdDelOrder = (DeliveryOrderService)_dataServiceEngine.GetDataService(typeof(DeliveryOrderService));
            cmdDelOrderDetails = (DeliveryOrderDetailService)_dataServiceEngine.GetDataService(typeof(DeliveryOrderDetailService));
            cmdOrderDetails = (OrderDetailService)_dataServiceEngine.GetDataService(typeof(OrderDetailService));
            cmdOrder = (OrderService)_dataServiceEngine.GetDataService(typeof(OrderService));
            cmdStockProductService = (StockProductService)_dataServiceEngine.GetDataService(typeof(StockProductService));
            cmdStockTransService = (StockTransService)_dataServiceEngine.GetDataService(typeof(StockTransService));
            cmdProductService = (ProductService)_dataServiceEngine.GetDataService(typeof(ProductService));
            cmdStockProductWithdrawService = (StockProductWithdrawService)_dataServiceEngine.GetDataService(typeof(StockProductWithdrawService));
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void PrepareDefaultScreen()
        {
            if (Session["DelOrderIndexSelected"] != null)
            {
                DELIVERY_INDEX objDEl = (DELIVERY_INDEX)Session["DelOrderIndexSelected"];
                List<DELIVERY_ORDER> DetailItems = cmdDelOrder.GetAllByDelIndexID(objDEl.DELIND_ID);
                if (DetailItems.Count == 0)
                {
                    gdvManageOrderHQ.EmptyDataText = "ไม่พบข้อมูลใบขึ้นของ";
                }
                UpdateDefaultScreen(ref DetailItems);
                gdvManageOrderHQ.DataSource = DetailItems;
                gdvManageOrderHQ.DataBind();
                lb_Vehicle.Text = "ทะเบียนรถยนต์ : " + objDEl.VEHICLE.VEHICLE_REGNO.ToString();
                PrepreConfirmStatusDisplay();
            }
            else
            {
                gdvManageOrderHQ.EmptyDataText = "ไม่พบข้อมูลใบแปะหน้า";
            }
        }

        private void UpdateDefaultScreen(ref List<DELIVERY_ORDER> DelOrderItems)
        {
            List<DELIVERY_ORDER> DelOrderValidateItems;
            if (Session["DelOrderSelectedValidate"] != null)
            {
                DelOrderValidateItems = (List<DELIVERY_ORDER>)Session["DelOrderSelectedValidate"];
            }
            else
            {
                DelOrderValidateItems = new List<DELIVERY_ORDER>();
            }
            foreach (var item in DelOrderValidateItems)
            {
                DELIVERY_ORDER objDelOrderUpdate = DelOrderItems.FirstOrDefault(x => x.DELORDER_ID == item.DELORDER_ID);
                if (objDelOrderUpdate != null)
                {
                    foreach (var Details in item.DELIVERY_ORDER_DETAIL)
                    {
                        DELIVERY_ORDER_DETAIL objDetailsUpdate = objDelOrderUpdate.DELIVERY_ORDER_DETAIL.FirstOrDefault(x => x.DELORDER_DETAIL_ID == Details.DELORDER_DETAIL_ID);
                        if (objDetailsUpdate != null)
                        {
                            objDetailsUpdate.PRODUCT_SENT_QTY = Details.PRODUCT_SENT_QTY;
                            objDetailsUpdate.PRODUCT_SENT_REMAIN = Details.PRODUCT_SENT_REMAIN;
                            objDetailsUpdate.PRODUCT_SENT_WEIGHT_TOTAL = Details.PRODUCT_SENT_WEIGHT_TOTAL;
                            objDetailsUpdate.PRODUCT_SENT_PRICE_TOTAL = Details.PRODUCT_SENT_PRICE_TOTAL;
                        }
                    }
                    objDelOrderUpdate.Status = true;
                    objDelOrderUpdate.DELORDER_PRICE_TOTAL = objDelOrderUpdate.DELIVERY_ORDER_DETAIL.Where(x => x.IS_FREE == "N").Sum(x => x.PRODUCT_SENT_PRICE_TOTAL);
                    objDelOrderUpdate.DELORDER_WEIGHT_TOTAL = objDelOrderUpdate.DELIVERY_ORDER_DETAIL.Sum(x => x.PRODUCT_SENT_WEIGHT_TOTAL);
                }
            }
            btnConfirmDelOrderIndex.Visible = (bool)Session["DelEdit"];
            btnConfirmDelOrderIndex.Enabled = DelOrderItems.All(x => x.Status != null && (bool)x.Status);
        }

        private void PrepreConfirmStatusDisplay()
        {
            if (Session["DelOrderIndexSelected"] != null && Session["DelOrderSelected"] != null)
            {

                List<DELIVERY_ORDER> DelOrderValidateItems;
                if (Session["DelOrderSelectedValidate"] != null)
                {
                    DelOrderValidateItems = (List<DELIVERY_ORDER>)Session["DelOrderSelectedValidate"];
                }
                else
                {
                    DelOrderValidateItems = new List<DELIVERY_ORDER>();
                }
                foreach (GridViewRow item in gdvManageOrderHQ.Rows)
                {
                    int DelID = (int)gdvManageOrderHQ.DataKeys[item.RowIndex].Value;
                    DELIVERY_ORDER objtempRow = DelOrderValidateItems.FirstOrDefault(x => x.DELORDER_ID == DelID);
                    if (objtempRow != null && objtempRow.Status != null)
                    {
                        if ((bool)objtempRow.Status)
                        {
                            item.BackColor = Color.LightGreen;
                        }
                    }
                }
            }
        }

        protected void gdvManageOrderHQ_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
            int DelOrderID = Convert.ToInt32(gdvManageOrderHQ.DataKeys[RowIndex].Values[0].ToString());
            switch (e.CommandName)
            {
                case "ViewDeliveryOrder":
                    {
                        DELIVERY_ORDER objDelOrder = cmdDelOrder.Select(DelOrderID);
                        Session["DelOrderSelected"] = objDelOrder;
                        Response.RedirectPermanent("~/Page/ManageOrderDeliveryProduct.aspx");
                    }
                    break;
                default:
                    break;

            }
        }

        private void UpdateOrderCompleteStatus(List<DELIVERY_ORDER> DelOrderItems)
        {
            List<int> OrderItems = new List<int>();
            foreach (var item in DelOrderItems)
            {
                foreach (var delOrderDetails in cmdDelOrderDetails.GetAllIncludeOrderByDeliveryOrder(item.DELORDER_ID))
                {
                    if (!OrderItems.Any(x => x == delOrderDetails.ORDER_DETAIL.ORDER_ID))
                    {
                        OrderItems.Add(delOrderDetails.ORDER_DETAIL.ORDER_ID);
                    }
                }
            }
            List<int> OrderCompleteItems = new List<int>();
            List<int> OrderNotCompleteItems = new List<int>();
            foreach (var item in cmdOrder.GetAllByID(OrderItems))
            {
                if (item.ORDER_DETAIL.All(x => x.PRODUCT_SEND_REMAIN == 0))
                {
                    OrderCompleteItems.Add(item.ORDER_ID);
                }
                else
                {
                    OrderNotCompleteItems.Add(item.ORDER_ID);
                }
            }
            USER objUser = Session["user"] != null ? (USER)Session["user"] : null;
            if (OrderCompleteItems.Count > 0)
            {
                cmdOrder.FlagStatus(OrderCompleteItems, "50", objUser.EMPLOYEE_ID);
            }
            if (OrderNotCompleteItems.Count > 0)
            {
                cmdOrder.FlagStatus(OrderNotCompleteItems, "20", objUser.EMPLOYEE_ID);
            }
        }

        protected void btnConfirmDelOrder_Click(object sender, EventArgs e)
        {

            if (Session["DelOrderIndexSelected"] != null)
            {
                //using (TransactionScope txScope = new TransactionScope())
                //{
                    DELIVERY_INDEX objDEl = (DELIVERY_INDEX)Session["DelOrderIndexSelected"];
                    List<DELIVERY_ORDER> DetailItems = cmdDelOrder.GetAllByDelIndexID(objDEl.DELIND_ID);
                    UpdateDefaultScreen(ref DetailItems);

                    try
                    {

                        USER objUser = Session["user"] != null ? (USER)Session["user"] : null;
                        //Update DeliveryOrderIndex
                        cmdDeliveryOrderIndex.ConfirmDelOrderIndex(DetailItems, objDEl.DELIND_ID, objUser.EMPLOYEE_ID);
                        //Update DeliveryOrder
                        //cmdDelOrder.ConfirmDelOrder(DetailItems);
                        //Update Order
                        //cmdOrderDetails.ConfirmOrder(DetailItems);
                        //Update OrderStatus
                        UpdateOrderCompleteStatus(DetailItems);

                        try
                        {
                            //ตัด Stock
                            foreach (DELIVERY_ORDER item in DetailItems)
                            {
                                foreach (DELIVERY_ORDER_DETAIL item2 in item.DELIVERY_ORDER_DETAIL)
                                {
                                    int stockAfter = 0;
                                    STOCK_PRODUCT_STOCK tmp = cmdStockProductService.SelectForCutStock(item2.PRODUCT_ID);
                                    if (tmp != null)
                                    {
                                        stockAfter = tmp.STOCK_REMAIN.Value;
                                        tmp.PRODUCT_ID = item2.PRODUCT_ID;
                                        tmp.STOCK_REMAIN = item2.PRODUCT_SENT_QTY;
                                        tmp.UPDATE_DATE = DateTime.Now;
                                        tmp.UPDATE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                                        cmdStockProductService.CutStock(tmp);
                                    }
                                    else
                                    {
                                        tmp = new STOCK_PRODUCT_STOCK();
                                        tmp.CREATE_DATE = DateTime.Now;
                                        tmp.CREATE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                                        tmp.PRODUCT_ID = item2.PRODUCT_ID;
                                        tmp.PRODUCT_CODE = cmdProductService.SelectNotInclude(item2.PRODUCT_ID).PRODUCT_CODE;
                                        tmp.STOCK_BEFORE = 0;
                                        tmp.STOCK_MINIMUM = 0;
                                        tmp.SYE_DEL = false;
                                        tmp.STOCK_REMAIN = 0 - item2.PRODUCT_SENT_QTY;
                                        tmp.UPDATE_DATE = DateTime.Now;
                                        tmp.UPDATE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                                        cmdStockProductService.Add(tmp);
                                    }

                                    STOCK_PRODUCT_TRANS tmpTrans = new STOCK_PRODUCT_TRANS();
                                    tmpTrans.APPROVE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                                    tmpTrans.COLOR_ID = item2.COLOR_ID;
                                    tmpTrans.COLOR_TYPE_ID = item2.COLOR_TYPE_ID;
                                    tmpTrans.CREATE_DATE = DateTime.Now;
                                    tmpTrans.CREATE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                                    tmpTrans.PRODUCT_ID = item2.PRODUCT_ID;
                                    tmpTrans.PRODUCT_CODE = cmdProductService.SelectNotInclude(item2.PRODUCT_ID).PRODUCT_CODE;
                                    tmpTrans.STOCK_AFTER = stockAfter;
                                    tmpTrans.STOCK_BEFORE = cmdStockProductService.SelectForCutStock(item2.PRODUCT_ID).STOCK_REMAIN.Value;
                                    tmpTrans.SYE_DEL = false;
                                    tmpTrans.SYS_TIME = DateTime.Now.TimeOfDay;
                                    tmpTrans.TRANS_DATE = DateTime.Now;
                                    tmpTrans.TRANS_QTY = item2.PRODUCT_SENT_QTY;
                                    tmpTrans.TRANS_TYPE = "OUT";
                                    tmpTrans.UPDATE_DATE = DateTime.Now;
                                    tmpTrans.UPDATE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                                    cmdStockTransService.Add(tmpTrans);

                                    STOCK_PRODUCT_WITHDRAW_TRANS tmpWithdraw = new STOCK_PRODUCT_WITHDRAW_TRANS();
                                    tmpWithdraw.APPROVE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                                    tmpWithdraw.COLOR_ID = item2.COLOR_ID;
                                    tmpWithdraw.COLOR_TYPE_ID = item2.COLOR_TYPE_ID;
                                    tmpWithdraw.CREATE_DATE = DateTime.Now;
                                    tmpWithdraw.CREATE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                                    tmpWithdraw.ORDER_CODE = item2.ORDER_CODE;
                                    tmpWithdraw.ORDER_DATE = item2.ORDER_DETAIL.CREATE_DATE;
                                    tmpWithdraw.ORDER_DETAIL_ID = item2.ORDER_DETAIL_ID;
                                    tmpWithdraw.ORDER_ID = item2.ORDER_DETAIL.ORDER_ID;
                                    tmpWithdraw.PRODUCT_CODE = item2.PRODUCT.PRODUCT_CODE;
                                    tmpWithdraw.PRODUCT_ID = item2.PRODUCT_ID;
                                    tmpWithdraw.PRODUCT_QTY = item2.PRODUCT_SENT_QTY;
                                    tmpWithdraw.PRODUCT_SEQ = item2.PRODUCT_SEQ;
                                    tmpWithdraw.STOCK_AFTER = stockAfter;
                                    tmpWithdraw.STOCK_BEFORE = tmpTrans.STOCK_BEFORE;
                                    tmpWithdraw.STORE_ID = item.STORE_ID;
                                    tmpWithdraw.SYE_DEL = false;
                                    tmpWithdraw.SYS_TIME = DateTime.Now.TimeOfDay;
                                    tmpWithdraw.TRANS_DATE = DateTime.Now;
                                    tmpWithdraw.UPDATE_DATE = DateTime.Now;
                                    tmpWithdraw.UPDATE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                                    tmpWithdraw.VEHICLE_ID = item.VEHICLE_ID;
                                    tmpWithdraw.WITHDRAW_QTY = item2.PRODUCT_SENT_QTY;
                                    tmpWithdraw.WITHDRAW_REMAIN_QTY = tmpTrans.STOCK_BEFORE;
                                    tmpWithdraw.WITHDRAW_COMPLETE_FLG = 0;
                                    cmdStockProductWithdrawService.Add(tmpWithdraw);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            DebugLog.WriteLog(ex.ToString());
                        }

                        //Clear Session
                        Session["DelOrderSelected"] = null;
                        Session["DelOrderSelectedValidate"] = null;
                        Session["DelEdit"] = null;

                        Response.Redirect("~/Page/ManageOrderDeliveryPreview.aspx?delID=" + objDEl.DELIND_ID.ToString(),false);
                    }
                    catch (Exception ex)
                    {
                        //txScope.Dispose();
                        DebugLog.WriteLog(ex.ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Warning Message", "<script>alert('ไม่สามารถทำรายการได้');</script>", true);
                    }
                //    txScope.Complete();
                //}
            }
        }
    }
}