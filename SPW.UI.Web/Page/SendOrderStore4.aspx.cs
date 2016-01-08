using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.Model;
using SPW.UI.Web.Reports;
using System.Data;
using SPW.DataService;

namespace SPW.UI.Web.Page
{
    public partial class SendOrderStore4 : System.Web.UI.Page
    {

        private DataServiceEngine _dataServiceEngine;
        private VehicleService cmdVehicle;
        private StoreService cmdStore;
        private OrderService cmdOrder;
        private OrderDetailService cmdOrderDetails;
        private DeliveryOrderService cmdDelivery;
        private DeliveryIndexService cmdDeliveryIndex;
        public List<ORDER> DataSource
        {
            get
            {
                var list = (List<ORDER>)ViewState["listStore"];
                return list;
            }
            set
            {
                ViewState["listStore"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CreatePageEngine();
                PrepareSearchScreen();
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
            cmdVehicle = (VehicleService)_dataServiceEngine.GetDataService(typeof(VehicleService));
            cmdStore = (StoreService)_dataServiceEngine.GetDataService(typeof(StoreService));
            cmdOrderDetails = (OrderDetailService)_dataServiceEngine.GetDataService(typeof(OrderDetailService));
            cmdDelivery = (DeliveryOrderService)_dataServiceEngine.GetDataService(typeof(DeliveryOrderService));
            cmdDeliveryIndex = (DeliveryIndexService)_dataServiceEngine.GetDataService(typeof(DeliveryIndexService));
            cmdOrder = (OrderService)_dataServiceEngine.GetDataService(typeof(OrderService));
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void PrepareSearchScreen()
        {
            if (Session["VehicleSelected"] != null)
            {
                int VehicleSelected = (int)Session["VehicleSelected"];
                VEHICLE objVehicle = cmdVehicle.GetCurrentByID(VehicleSelected);
                if (objVehicle != null)
                {
                    txtVehicle.Text = objVehicle.VEHICLE_REGNO.ToString();
                }
            }
        }

        private void PrepareDefaultScreen()
        {
            List<ORDER> OrderSelected;
            if (Session["OrderSelected"] != null)
            {
                OrderSelected = (List<ORDER>)Session["OrderSelected"];
                List<ORDER_DETAIL> DetailsItems = ManageOrderDetailsItems(OrderSelected);
                if (DetailsItems.Count == 0)
                {
                    grideInOrder.EmptyDataText = "ไม่พบข้อมูลการเลือกรายการ";
                }
                grideInOrder.DataSource = DetailsItems;
                grideInOrder.DataBind();
            }
            else
            {
                grideInOrder.EmptyDataText = "ไม่พบข้อมูลการเลือกรายการ";
            }
            CalculateSummaryScreen();
        }

        private void CalculateSummaryScreen()
        {
            List<ORDER> OrderSelected;
            if (Session["OrderSelected"] != null)
            {
                OrderSelected = (List<ORDER>)Session["OrderSelected"];

                lb_TotalAmount.Text = String.Format("ราคารวม  : {0:C} บาท", ((decimal)OrderSelected.Sum(x => (decimal)x.ORDER_DETAIL.Where(z => z.IS_FREE == "N").Sum(z => z.PRODUCT_PRICE * z.PRODUCT_SEND_REMAIN))).ToString("n2"));
                lb_TotalWeight.Text = String.Format("น้ำหนักรวม : {0:C} กก.", ((decimal)OrderSelected.Sum(x => (decimal)x.ORDER_DETAIL.Sum(z => z.PRODUCT_WEIGHT * z.PRODUCT_SEND_REMAIN))).ToString("n2"));
                lb_TotalAmount.Visible = true;
                lb_TotalWeight.Visible = true;
                if (OrderSelected.Count == 0)
                {
                    lb_TotalAmount.Visible = false;
                    lb_TotalWeight.Visible = false;
                }
            }
            else
            {
                lb_TotalAmount.Visible = false;
                lb_TotalWeight.Visible = false;
            }
        }


        private List<ORDER_DETAIL> ManageOrderDetailsItems(List<ORDER> OrderSelected)
        {
            List<ORDER_DETAIL> OrderDetailsItems = new List<ORDER_DETAIL>();
            foreach (var Order in OrderSelected)
            {
                List<ORDER_DETAIL> OrderDetails = cmdOrderDetails.GetAllIncludeByOrder(Order.ORDER_ID);
                foreach (var Details in OrderDetails)
                {
                    ORDER_DETAIL objOrderDetial = OrderDetailsItems.FirstOrDefault(x => x.PRODUCT_ID == Details.PRODUCT_ID && x.COLOR_ID == Details.COLOR_ID && x.COLOR_TYPE_ID == Details.COLOR_TYPE_ID && x.IS_FREE == Details.IS_FREE);
                    if (objOrderDetial != null)
                    {
                        objOrderDetial.PRODUCT_SEND_REMAIN += Details.PRODUCT_SEND_REMAIN;
                        objOrderDetial.PRODUCT_WEIGHT_TOTAL += Details.PRODUCT_SEND_REMAIN * Details.PRODUCT_WEIGHT;
                        if (objOrderDetial.IS_FREE == "N")
                        {
                            objOrderDetial.PRODUCT_PRICE_TOTAL += Details.PRODUCT_SEND_REMAIN * Details.PRODUCT_PRICE;
                        }
                        else
                        {
                            objOrderDetial.PRODUCT_PRICE_TOTAL = 0;
                        }
                    }
                    else
                    {
                        ORDER_DETAIL objtemp = new ORDER_DETAIL();
                        objtemp.PRODUCT_ID = Details.PRODUCT_ID;
                        objtemp.PRODUCT = Details.PRODUCT;
                        objtemp.IS_FREE = Details.IS_FREE;
                        objtemp.COLOR_TYPE_ID = Details.COLOR_TYPE_ID;
                        objtemp.COLOR_ID = Details.COLOR_ID;
                        objtemp.PRODUCT_SEND_REMAIN = Details.PRODUCT_SEND_REMAIN;
                        objtemp.PRODUCT_PRICE = Details.PRODUCT_PRICE;
                        objtemp.PRODUCT_WEIGHT = Details.PRODUCT_WEIGHT;
                        objtemp.PRODUCT_WEIGHT_TOTAL = Details.PRODUCT_SEND_REMAIN * Details.PRODUCT_WEIGHT;
                        if (Details.IS_FREE == "N")
                        {
                            objtemp.PRODUCT_PRICE_TOTAL = Details.PRODUCT_SEND_REMAIN * Details.PRODUCT_PRICE;
                        }
                        else
                        {
                            objtemp.PRODUCT_PRICE_TOTAL = 0;
                        }
                        OrderDetailsItems.Add(objtemp);
                    }
                }
            }


            return OrderDetailsItems.Where(x => x.PRODUCT_SEND_REMAIN > 0).ToList();
        }

        private List<DELIVERY_INDEX_DETAIL> ManageOrderIndexDetailsItems(List<ORDER> OrderSelected)
        {
            List<DELIVERY_INDEX_DETAIL> OrderDetailsItems = new List<DELIVERY_INDEX_DETAIL>();
            int Seq = 0;
            foreach (var Order in OrderSelected)
            {
                List<ORDER_DETAIL> OrderDetails = cmdOrderDetails.GetAllIncludeByOrder(Order.ORDER_ID);
                foreach (var Details in OrderDetails)
                {
                    DELIVERY_INDEX_DETAIL objOrderIndexDetial = OrderDetailsItems.FirstOrDefault(x => x.PRODUCT_ID == Details.PRODUCT_ID && x.COLOR_ID == Details.COLOR_ID && x.COLOR_TYPE_ID == Details.COLOR_TYPE_ID && x.IS_FREE == Details.IS_FREE);
                    if (objOrderIndexDetial != null)
                    {
                        if (objOrderIndexDetial.IS_FREE == "N")
                        {
                            objOrderIndexDetial.PRODUCT_PRICE_TOTAL += Details.PRODUCT_SEND_REMAIN * Details.PRODUCT_PRICE;
                        }
                        else
                        {
                            objOrderIndexDetial.PRODUCT_PRICE_TOTAL = 0;
                        }
                        objOrderIndexDetial.PRODUCT_WEIGHT_TOTAL += Details.PRODUCT_SEND_REMAIN * Details.PRODUCT_WEIGHT;
                        objOrderIndexDetial.PRODUCT_SENT_QTY += Details.PRODUCT_SEND_REMAIN;
                    }
                    else
                    {
                        Seq+=1;
                        objOrderIndexDetial = new DELIVERY_INDEX_DETAIL();
                        objOrderIndexDetial.COLOR_ID = Details.COLOR_ID;
                        objOrderIndexDetial.COLOR_TYPE_ID = Details.COLOR_TYPE_ID;
                        objOrderIndexDetial.PRODUCT_ID = Details.PRODUCT_ID;
                        objOrderIndexDetial.PRODUCT_PRICE = Details.PRODUCT_PRICE;
                        objOrderIndexDetial.PRODUCT_SENT_QTY = Details.PRODUCT_SEND_REMAIN;
                        if (Details.IS_FREE == "N")
                        {
                            objOrderIndexDetial.PRODUCT_PRICE_TOTAL = Details.PRODUCT_SEND_REMAIN * Details.PRODUCT_PRICE;
                        }
                        else
                        {
                            objOrderIndexDetial.PRODUCT_PRICE_TOTAL = 0;
                        }
                        objOrderIndexDetial.PRODUCT_WEIGHT_TOTAL = Details.PRODUCT_SEND_REMAIN * Details.PRODUCT_WEIGHT;
                        objOrderIndexDetial.PRODUCT_WEIGHT = Details.PRODUCT_WEIGHT;
                        objOrderIndexDetial.CREATE_DATE = DateTime.Now;
                        objOrderIndexDetial.IS_FREE = Details.IS_FREE;
                        objOrderIndexDetial.SYE_DEL = false;
                        objOrderIndexDetial.UPDATE_DATE = DateTime.Now;
                        objOrderIndexDetial.DELIND_SEQ_NO = Seq;
                        OrderDetailsItems.Add(objOrderIndexDetial);
                    }
                }
            }
            return OrderDetailsItems.Where(x => x.PRODUCT_SENT_QTY > 0).ToList();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            List<ORDER> OrderSelected;
            bool isValid = false;
            if (Session["OrderSelected"] != null)
            {
                isValid = ((List<ORDER>)Session["OrderSelected"]).Count > 0;
            }
            if (isValid)
            {
                //[ex] UserCreate
                USER objUser = Session["user"] != null ? (USER)Session["user"] : null;
                OrderSelected = (List<ORDER>)Session["OrderSelected"];
                DELIVERY_INDEX objIndex = new DELIVERY_INDEX();
                List<DELIVERY_ORDER> DelOrderItems = new List<DELIVERY_ORDER>();
                int DEL_SEQ = 1;
                foreach (var order in OrderSelected)
                {
                    List<ORDER_DETAIL> DetailItems = cmdOrderDetails.GetAllIncludeByOrder(order.ORDER_ID);
                    DELIVERY_ORDER objDelivery = DelOrderItems.FirstOrDefault(x => x.STORE_ID == order.STORE_ID);
                    if (objDelivery != null)
                    {
                        int Seq_Prod = 1;
                        foreach (var item in DetailItems.Where(x=> x.PRODUCT_SEND_REMAIN > 0))
                        {
                            DELIVERY_ORDER_DETAIL objDeliveryDetail = new DELIVERY_ORDER_DETAIL();
                            objDeliveryDetail.CREATE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                            objDeliveryDetail.UPDATE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                            //objDeliveryDetail.ORDER_ID = item.ORDER_ID;
                            objDeliveryDetail.ORDER_DETAIL_ID = item.ORDER_DETAIL_ID;
                            objDeliveryDetail.PRODUCT_ID = item.PRODUCT_ID;
                            objDeliveryDetail.PRODUCT_QTY = item.PRODUCT_SEND_REMAIN;
                            objDeliveryDetail.PRODUCT_PRICE = item.PRODUCT_PRICE;
                            objDeliveryDetail.PRODUCT_SENT_QTY = item.PRODUCT_SEND_REMAIN;
                            objDeliveryDetail.PRODUCT_SENT_REMAIN = 0;
                            objDeliveryDetail.PRODUCT_SENT_ROUND = item.PRODUCT_SEND_ROUND + 1;
                            objDeliveryDetail.PRODUCT_WEIGHT = item.PRODUCT_WEIGHT;
                            objDeliveryDetail.PRODUCT_SENT_WEIGHT_TOTAL = objDeliveryDetail.PRODUCT_SENT_QTY * objDeliveryDetail.PRODUCT_WEIGHT;
                            if (item.IS_FREE == "N")
                            {
                                objDeliveryDetail.PRODUCT_SENT_PRICE_TOTAL = objDeliveryDetail.PRODUCT_SENT_QTY * objDeliveryDetail.PRODUCT_PRICE;
                            }
                            else
                            {
                                objDeliveryDetail.PRODUCT_SENT_PRICE_TOTAL = 0;
                            }
                            objDeliveryDetail.COLOR_ID = item.COLOR_ID;
                            objDeliveryDetail.COLOR_TYPE_ID = item.COLOR_TYPE_ID;
                            objDeliveryDetail.ColorDesc = item.ColorDesc;
                            objDeliveryDetail.CREATE_DATE = DateTime.Now;
                            objDeliveryDetail.PRODUCT_SEQ = Seq_Prod;
                            objDeliveryDetail.DELORDER_SEQ_NO = objDelivery.DELORDER_SEQ_NO;
                            objDeliveryDetail.IS_FREE = item.IS_FREE;
                            objDeliveryDetail.DELORDER_CODE = objDelivery.DELORDER_CODE;
                            objDeliveryDetail.SYE_DEL = false;
                            objDelivery.DELIVERY_ORDER_DETAIL.Add(objDeliveryDetail);
                            Seq_Prod = +1;
                        }
                        objDelivery.DELORDER_PRICE_TOTAL = objDelivery.DELIVERY_ORDER_DETAIL.Where(x=> x.IS_FREE == "N").Sum(x => x.PRODUCT_SENT_PRICE_TOTAL);
                        objDelivery.DELORDER_WEIGHT_TOTAL = objDelivery.DELIVERY_ORDER_DETAIL.Sum(x => x.PRODUCT_SENT_WEIGHT_TOTAL);
                    }
                    else
                    {
                        objDelivery = new DELIVERY_ORDER();
                        objDelivery.CREATE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                        objDelivery.UPDATE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                        objDelivery.VEHICLE_ID = (int)Session["VehicleSelected"];
                        objDelivery.DELORDER_DATE = DateTime.Now;
                        objDelivery.CREATE_DATE = DateTime.Now;
                        objDelivery.STORE_ID = order.STORE_ID;
                        objDelivery.DELORDER_CODE = "DOR"+DateTime.Now.ToString("yyMMdd" + ((cmdDelivery.GetID(DateTime.Now) + objIndex.DELIVERY_ORDER.Count).ToString().PadLeft(3, '0')));
                        objDelivery.DELORDER_STEP = "30";
                        objDelivery.SYE_DEL = false;
                        objDelivery.DELORDER_SEQ_NO = DEL_SEQ;
                        int Seq_Prod = 1;
                        DEL_SEQ += 1;
                        foreach (var item in DetailItems.Where(x => x.PRODUCT_SEND_REMAIN > 0))
                        {
                            DELIVERY_ORDER_DETAIL objDeliveryDetail = new DELIVERY_ORDER_DETAIL();
                            objDeliveryDetail.CREATE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                            objDeliveryDetail.UPDATE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                            //objDeliveryDetail.ORDER_ID = item.ORDER_ID;
                            objDeliveryDetail.ORDER_DETAIL_ID = item.ORDER_DETAIL_ID;
                            objDeliveryDetail.PRODUCT_ID = item.PRODUCT_ID;
                            objDeliveryDetail.PRODUCT_QTY = item.PRODUCT_SEND_REMAIN;
                            objDeliveryDetail.PRODUCT_PRICE = item.PRODUCT_PRICE;
                            objDeliveryDetail.PRODUCT_SENT_QTY = item.PRODUCT_SEND_REMAIN;
                            objDeliveryDetail.PRODUCT_SENT_REMAIN = 0;
                            objDeliveryDetail.PRODUCT_SENT_ROUND = item.PRODUCT_SEND_ROUND + 1;
                            objDeliveryDetail.PRODUCT_WEIGHT = item.PRODUCT_WEIGHT;
                            objDeliveryDetail.PRODUCT_SENT_WEIGHT_TOTAL = objDeliveryDetail.PRODUCT_SENT_QTY * objDeliveryDetail.PRODUCT_WEIGHT;
                            if (item.IS_FREE == "N")
                            {
                                objDeliveryDetail.PRODUCT_SENT_PRICE_TOTAL = objDeliveryDetail.PRODUCT_SENT_QTY * objDeliveryDetail.PRODUCT_PRICE;
                            }
                            else
                            {
                                objDeliveryDetail.PRODUCT_SENT_PRICE_TOTAL = 0;
                            }
                            objDeliveryDetail.COLOR_ID = item.COLOR_ID;
                            objDeliveryDetail.COLOR_TYPE_ID = item.COLOR_TYPE_ID;
                            objDeliveryDetail.ColorDesc = item.ColorDesc;
                            objDeliveryDetail.CREATE_DATE = DateTime.Now;
                            objDeliveryDetail.PRODUCT_SEQ = Seq_Prod;
                            objDeliveryDetail.DELORDER_SEQ_NO = objDelivery.DELORDER_SEQ_NO;
                            objDeliveryDetail.IS_FREE = item.IS_FREE;
                            objDeliveryDetail.DELORDER_CODE = objDelivery.DELORDER_CODE;
                            objDeliveryDetail.SYE_DEL = false;
                            objDelivery.DELIVERY_ORDER_DETAIL.Add(objDeliveryDetail);
                            Seq_Prod += 1;
                        }
                        objDelivery.DELORDER_PRICE_TOTAL = objDelivery.DELIVERY_ORDER_DETAIL.Where(x => x.IS_FREE == "N").Sum(x => x.PRODUCT_SENT_PRICE_TOTAL);
                        objDelivery.DELORDER_WEIGHT_TOTAL = objDelivery.DELIVERY_ORDER_DETAIL.Sum(x => x.PRODUCT_SENT_WEIGHT_TOTAL);
                    }
                    //}
                    objIndex.DELIVERY_ORDER.Add(objDelivery);
                }
                objIndex.CREATE_DATE = DateTime.Now;
                objIndex.DELIND_DATE = DateTime.Now;
                objIndex.SYE_DEL = false;
                objIndex.UPDATE_DATE = DateTime.Now;
                objIndex.CREATE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                objIndex.UPDATE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                objIndex.VEHICLE_ID = (int)Session["VehicleSelected"];
                objIndex.DELIND_CODE = "DIN" + DateTime.Now.ToString("yyMMdd" + (cmdDeliveryIndex.GetID(DateTime.Now).ToString().PadLeft(3, '0')));
                List<DELIVERY_INDEX_DETAIL> objIndexDetails = ManageOrderIndexDetailsItems(OrderSelected);
                foreach (var item in objIndexDetails)
                {
                    item.CREATE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                    item.UPDATE_EMPLOYEE_ID = objUser.EMPLOYEE_ID;
                    objIndex.DELIVERY_INDEX_DETAIL.Add(item);
                }
                try
                {
                    cmdDeliveryIndex.Add(objIndex);
                    FlagStatusOrder(OrderSelected,objUser.EMPLOYEE_ID);
                    Response.RedirectPermanent("~/Page/SendOrderStore5.aspx?delId=" + objIndex.DELIND_ID);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Warning Message", "<script>alert('ไม่สามารถทำรายการได้');</script>", true);
                }

            }


            //}            
            //Response.RedirectPermanent("~/Page/SendOrderStore5.aspx");
        }

        private void FlagStatusOrder(List<ORDER> OrderItems,int UpdateID)
        {
            cmdOrder.FlagStatus(OrderItems.Select(x => x.ORDER_ID).ToList(), "30", UpdateID);
        }

    }
}