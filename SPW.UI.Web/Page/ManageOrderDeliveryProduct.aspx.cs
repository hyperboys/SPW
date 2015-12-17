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
    public partial class ManageOrderDeliveryProduct : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private DeliveryIndexService cmdDeliveryOrderIndex;
        private OrderService cmdOrder;
        private DeliveryOrderDetailService cmdDelOrderDetails;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CreatePageEngine();
                PrepareDefaultScreen();
                lb_Exception.Visible = false;
            }
            else
            {
                ReloadPageEngine();
                lb_Exception.Visible = false;
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
            cmdOrder = (OrderService)_dataServiceEngine.GetDataService(typeof(OrderService));
            cmdDelOrderDetails = (DeliveryOrderDetailService)_dataServiceEngine.GetDataService(typeof(DeliveryOrderDetailService));
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void PrepareDefaultScreen()
        {
            if (Session["DelOrderSelected"] != null)
            {
                DELIVERY_ORDER objDelOrder = (DELIVERY_ORDER)Session["DelOrderSelected"];
                List<DELIVERY_ORDER_DETAIL> DetailItems = UpdateDefaultScreen(objDelOrder.DELORDER_ID);
                if (DetailItems.Count == 0)
                {
                    DetailItems = cmdDelOrderDetails.GetAllIncludeByDeliveryOrder(objDelOrder.DELORDER_ID);
                    List<ORDER> OrderItems = GetOrderFromDeliveryOrder(objDelOrder.DELORDER_ID);
                    foreach (var item in DetailItems)
                    {
                        ORDER objOrder = OrderItems.FirstOrDefault(x => x.ORDER_ID == item.ORDER_DETAIL.ORDER_ID);
                        if (objOrder != null)
                        {
                            item.ORDER_CODE = objOrder.ORDER_CODE;
                            item.ORDER_DETAIL.ColorDesc = item.PRODUCT.PRODUCT_NAME + " " + item.ORDER_DETAIL.COLOR_TYPE.COLOR_TYPE_SUBNAME + " " + item.ORDER_DETAIL.COLOR.COLOR_SUBNAME;
                        }
                    }
                }
                else
                {
                    List<DELIVERY_ORDER_DETAIL> DetailItemsUpdate = cmdDelOrderDetails.GetAllIncludeByDeliveryOrder(objDelOrder.DELORDER_ID);
                    foreach (var item in DetailItems)
                    {
                        DELIVERY_ORDER_DETAIL objDelOrderDetail = DetailItemsUpdate.FirstOrDefault(x => x.DELORDER_DETAIL_ID == item.DELORDER_DETAIL_ID);
                        if (objDelOrderDetail != null)
                        {
                            item.PRODUCT = objDelOrderDetail.PRODUCT;
                            item.ORDER_DETAIL = objDelOrderDetail.ORDER_DETAIL;
                            item.ORDER_DETAIL.ColorDesc = item.PRODUCT.PRODUCT_NAME + " " + item.ORDER_DETAIL.COLOR_TYPE.COLOR_TYPE_SUBNAME + " " + item.ORDER_DETAIL.COLOR.COLOR_SUBNAME;
                        }
                    }                  
                    List<ORDER> OrderItems = GetOrderFromDeliveryOrder(objDelOrder.DELORDER_ID);
                    foreach (var item in DetailItems)
                    {
                        ORDER objOrder = OrderItems.FirstOrDefault(x => x.ORDER_ID == item.ORDER_DETAIL.ORDER_ID);
                        if (objOrder != null)
                        {
                            item.ORDER_CODE = objOrder.ORDER_CODE;
                            item.ORDER_DETAIL.ColorDesc = item.PRODUCT.PRODUCT_NAME + " " + item.ORDER_DETAIL.COLOR_TYPE.COLOR_TYPE_SUBNAME + " " + item.ORDER_DETAIL.COLOR.COLOR_SUBNAME;
                        }
                    }
                }
                if (DetailItems.Count == 0)
                {
                    gdvManageOrderHQ.EmptyDataText = "ไม่พบข้อมูลใบขึ้นของ";
                }
                gdvManageOrderHQ.DataSource = DetailItems;
                gdvManageOrderHQ.DataBind();
                lb_Store.Text = objDelOrder.STORE.STORE_NAME;
                lb_Vehicle.Text = objDelOrder.VEHICLE.VEHICLE_REGNO;
                btnConfirmDelOrder.Visible = (bool)Session["DelEdit"];
                btnConfirmZeroDelOrder.Visible = (bool)Session["DelEdit"];
            }
            else
            {
                gdvManageOrderHQ.EmptyDataText = "ไม่พบข้อมูลการเลือกรายการ";
            }
        }

        private List<DELIVERY_ORDER_DETAIL> UpdateDefaultScreen(int DelOrderID)
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

            DELIVERY_ORDER objDelOrderUpdate = DelOrderValidateItems.FirstOrDefault(x => x.DELORDER_ID == DelOrderID);
            if (objDelOrderUpdate != null)
            {
                return objDelOrderUpdate.DELIVERY_ORDER_DETAIL.ToList();
            }
            else
                return new List<DELIVERY_ORDER_DETAIL>();

        }

        private List<ORDER> GetOrderFromDeliveryOrder(int DelOrderID)
        {
            List<int> OrderItems = new List<int>();
            foreach (var delOrderDetails in cmdDelOrderDetails.GetAllIncludeOrderByDeliveryOrder(DelOrderID))
            {
                if (!OrderItems.Any(x => x == delOrderDetails.ORDER_DETAIL.ORDER_ID))
                {
                    OrderItems.Add(delOrderDetails.ORDER_DETAIL.ORDER_ID);
                }
            }            
            return cmdOrder.GetAllByID(OrderItems);
        }

        protected void btnConfirmDelOrder_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save() 
        {
            if (Session["DelOrderSelected"] != null && Session["DelOrderIndexSelected"] != null)
            {
                DELIVERY_INDEX objDelIndex = (DELIVERY_INDEX)Session["DelOrderIndexSelected"];
                DELIVERY_ORDER objDelOrder = (DELIVERY_ORDER)Session["DelOrderSelected"];
                UpdateObject(objDelOrder);
                if (IsValid(objDelOrder.DELIVERY_ORDER_DETAIL.ToList()))
                {
                    objDelOrder.Status = true;
                    List<DELIVERY_ORDER> DelOrderValidateItems;
                    if (Session["DelOrderSelectedValidate"] != null)
                    {
                        DelOrderValidateItems = (List<DELIVERY_ORDER>)Session["DelOrderSelectedValidate"];
                    }
                    else
                    {
                        DelOrderValidateItems = new List<DELIVERY_ORDER>();
                    }
                    DelOrderValidateItems.RemoveAll(x => x.DELORDER_ID == objDelOrder.DELORDER_ID);
                    DelOrderValidateItems.Add(objDelOrder);
                    Session["DelOrderSelectedValidate"] = DelOrderValidateItems;
                    Response.RedirectPermanent("~/Page/ManageOrderDeliveryDetail.aspx");
                }
                else
                {
                    lb_Exception.Visible = true;
                    lb_Exception.Text = "ไม่สามารถทำรายการได้ กรุณาตรวจสอบรายการ   ";
                }
            }        
        }

        private bool IsValid(List<DELIVERY_ORDER_DETAIL> objDelOrder)
        {
            bool isValid = objDelOrder.All(x => x.PRODUCT_SENT_QTY >= 0);
            int SendQtyTotal = (int)objDelOrder.Sum(x => x.PRODUCT_SENT_QTY);
            return (SendQtyTotal >= 0 && isValid);
        }

        private void UpdateObject(DELIVERY_ORDER objDelOrder)
        {
            foreach (GridViewRow item in gdvManageOrderHQ.Rows)
            {
                int DelKeyId = (int)gdvManageOrderHQ.DataKeys[item.RowIndex].Values[0];
                DELIVERY_ORDER_DETAIL objDelDetails = objDelOrder.DELIVERY_ORDER_DETAIL.FirstOrDefault(x => x.DELORDER_DETAIL_ID == DelKeyId);
                if (objDelDetails != null)
                {
                    TextBox objControl = (TextBox)item.FindControl("txtQty");
                    if (objControl != null)
                    {
                        int QtyBeforeSend = (int)objDelDetails.PRODUCT_SENT_REMAIN + (int)objDelDetails.PRODUCT_SENT_QTY;
                        string RowValue = objControl.Text;
                        objDelDetails.PRODUCT_SENT_QTY = RowValue == "" ? 0 : Convert.ToInt32(RowValue);
                        objDelDetails.PRODUCT_SENT_REMAIN = QtyBeforeSend - objDelDetails.PRODUCT_SENT_QTY;
                        objDelDetails.PRODUCT_SENT_WEIGHT_TOTAL = objDelDetails.PRODUCT_WEIGHT * objDelDetails.PRODUCT_SENT_QTY;
                        if (objDelDetails.IS_FREE == "N")
                        {
                            objDelDetails.PRODUCT_SENT_PRICE_TOTAL = objDelDetails.PRODUCT_PRICE * objDelDetails.PRODUCT_SENT_QTY;
                        }
                    }
                }
            }
        }

        private void UpdateZeroDataRow()
        {
            foreach (GridViewRow item in gdvManageOrderHQ.Rows)
            {
                TextBox objControl = (TextBox)item.FindControl("txtQty");
                if (objControl != null)
                {
                    objControl.Text = "0";
                }
            }
        }

        public bool ISEnabled() 
        {
            return !(bool)Session["DelEdit"];
        }

        protected void btnConfirmZeroDelOrder_Click(object sender, EventArgs e)
        {
            UpdateZeroDataRow();
            Save();
        }
    }
}