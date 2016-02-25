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
using SPW.DAL;

namespace SPW.UI.Web.Page
{
    public partial class SendOrderStore5 : System.Web.UI.Page
    {

        private DataServiceEngine _dataServiceEngine;
        private VehicleService cmdVehicle;
        private StoreService cmdStore;
        private OrderDetailService cmdOrderDetails;
        private DeliveryIndexService cmdDeliveryOrderIndex;
        private DeliveryOrderDetailService cmdDeliveryOrderDetail;

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
            cmdDeliveryOrderIndex = (DeliveryIndexService)_dataServiceEngine.GetDataService(typeof(DeliveryIndexService));
            cmdDeliveryOrderDetail = (DeliveryOrderDetailService)_dataServiceEngine.GetDataService(typeof(DeliveryOrderDetailService));
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
            Session["OrderSelected"] = null;
            Session["VehicleSelected"] = null;
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

        protected void btnNext_Click1(object sender, EventArgs e)
        {
            Response.RedirectPermanent("~/Page/ManageSendOrder.aspx");
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            int delID = Convert.ToInt32(Request.QueryString["delId"].ToString());
            DELIVERY_INDEX objDEl = cmdDeliveryOrderIndex.Select(delID);
            SendOrderReportData ds = new SendOrderReportData();
            DataTable SendOrderHeader = ds.Tables["SendOrderHeader"];
            List<DELIVERY_ORDER> listItem = new List<DELIVERY_ORDER>();

            List<DELIVERY_ORDER_DETAIL> listOrder = new List<DELIVERY_ORDER_DETAIL>();
            objDEl.DELIVERY_ORDER = objDEl.DELIVERY_ORDER.OrderBy(x => x.STORE_ID).ToList();
            foreach (DELIVERY_ORDER item in objDEl.DELIVERY_ORDER)
            {
                item.DELIVERY_ORDER_DETAIL = cmdDeliveryOrderDetail.GetAllIncludeByDeliveryOrder(item.DELORDER_ID);
                DELIVERY_ORDER tmpStore = listItem.Where(x => x.STORE_ID == item.STORE_ID).FirstOrDefault();
                if (tmpStore == null)
                {
                    listItem.Add(item);
                }

                foreach (DELIVERY_ORDER_DETAIL od in item.DELIVERY_ORDER_DETAIL)
                {
                    DELIVERY_ORDER_DETAIL tmp = listOrder.Where(x => x.PRODUCT_ID == od.PRODUCT_ID && x.COLOR_TYPE_ID == od.COLOR_TYPE_ID && x.COLOR_ID == od.COLOR_ID).FirstOrDefault();
                    if (tmp != null)
                    {
                        tmp.PRODUCT_SENT_QTY += od.PRODUCT_SENT_QTY;
                    }
                    else
                    {
                        tmp = ObjectCopier.Clone<DELIVERY_ORDER_DETAIL>(od);
                        listOrder.Add(tmp);
                    }
                }
            }

            #region รายการร้านค้าทั้งหหมด
            foreach (DELIVERY_ORDER item in listItem)
            {
                foreach (DELIVERY_ORDER_DETAIL od in item.DELIVERY_ORDER_DETAIL)
                {
                    item.STORE = cmdStore.SelectInclude(item.STORE_ID);
                }
            }

            int seq = 1;
            foreach (DELIVERY_ORDER DOrder in listItem)
            {
                DataRow drSendOrderHeader = SendOrderHeader.NewRow();
                drSendOrderHeader["STORE_NAME"] = "";
                drSendOrderHeader["STORE_ADDR"] = "";
                drSendOrderHeader["STORE_TEL"] = "";
                drSendOrderHeader["STORE_CODE"] = "- รายการร้านค้าทั้งหมด";
                drSendOrderHeader["ORDER_DATE"] = DateTime.Now.ToShortDateString();
                drSendOrderHeader["SEND_DATE"] = DateTime.Now.ToShortDateString();
                drSendOrderHeader["ZONE_NAME"] = "";
                drSendOrderHeader["VEHICLE_REG"] = txtVehicle.Text;

                drSendOrderHeader["SEQ"] = seq.ToString();
                drSendOrderHeader["NAME"] = DOrder.STORE.STORE_NAME + " " + DOrder.STORE.STORE_CODE ;
                drSendOrderHeader["QTY"] = "";
                drSendOrderHeader["PACKAGE"] = " อ." + DOrder.STORE.STORE_DISTRICT;
                drSendOrderHeader["WEIGHT"] = "";
                drSendOrderHeader["SUM_WEIGHT"] = " จ." + DOrder.STORE.PROVINCE.PROVINCE_NAME;

                seq++;
                drSendOrderHeader["SUM_WEIGHT_TH"] = "";
                drSendOrderHeader["SUM_WEIGHT_NUMBER"] = "";
                SendOrderHeader.Rows.Add(drSendOrderHeader);
            }
            #endregion

            #region รายการสินค้าทั้งหมด
            List<DELIVERY_ORDER_DETAIL> listOrder2 = new List<DELIVERY_ORDER_DETAIL>();
            foreach (DELIVERY_ORDER_DETAIL od in listOrder)
            {
                DELIVERY_ORDER_DETAIL tmp = listOrder2.Where(x => x.PRODUCT_ID == od.PRODUCT_ID).FirstOrDefault();
                if (tmp != null)
                {
                    tmp.ColorDesc += ", " + od.COLOR_TYPE.COLOR_TYPE_SUBNAME + " " + od.COLOR.COLOR_SUBNAME + " " + od.PRODUCT_SENT_QTY;
                    tmp.PRODUCT_SENT_QTY += od.PRODUCT_SENT_QTY;
                }
                else
                {
                    tmp = ObjectCopier.Clone<DELIVERY_ORDER_DETAIL>(od);
                    tmp.ColorDesc = od.PRODUCT.PRODUCT_NAME + " " + od.COLOR_TYPE.COLOR_TYPE_SUBNAME + " " + od.COLOR.COLOR_SUBNAME + " " + od.PRODUCT_SENT_QTY;
                    listOrder2.Add(tmp);
                }
            }

            seq = 1;
            decimal sumWeight = 0;
            foreach (DELIVERY_ORDER_DETAIL od in listOrder2)
            {
                DataRow drSendOrderHeader = SendOrderHeader.NewRow();
                drSendOrderHeader["STORE_NAME"] = "";
                drSendOrderHeader["STORE_ADDR"] = "";
                drSendOrderHeader["STORE_TEL"] = "";
                drSendOrderHeader["STORE_CODE"] = "- รายการสินค้าทั้งหมด";
                drSendOrderHeader["ORDER_DATE"] = DateTime.Now.ToShortDateString();
                drSendOrderHeader["SEND_DATE"] = DateTime.Now.ToShortDateString();
                drSendOrderHeader["ZONE_NAME"] = "";
                drSendOrderHeader["VEHICLE_REG"] = txtVehicle.Text;

                drSendOrderHeader["SEQ"] = seq.ToString();
                drSendOrderHeader["NAME"] = od.ColorDesc;
                drSendOrderHeader["QTY"] = od.PRODUCT_SENT_QTY.ToString();
                drSendOrderHeader["PACKAGE"] = od.PRODUCT.PRODUCT_PACKING_DESC;
                drSendOrderHeader["WEIGHT"] = od.PRODUCT.PRODUCT_WEIGHT;
                drSendOrderHeader["SUM_WEIGHT"] = Convert.ToDecimal(od.PRODUCT.PRODUCT_WEIGHT * od.PRODUCT_SENT_QTY.Value);
                sumWeight += Convert.ToDecimal(od.PRODUCT.PRODUCT_WEIGHT * od.PRODUCT_SENT_QTY.Value);
                seq++;
                drSendOrderHeader["SUM_WEIGHT_TH"] = ThaiBaht(sumWeight.ToString());
                drSendOrderHeader["SUM_WEIGHT_NUMBER"] = sumWeight.ToString();
                SendOrderHeader.Rows.Add(drSendOrderHeader);
            }
            #endregion

            #region รายการตามร้านค้า
            listItem = new List<DELIVERY_ORDER>();
            foreach (DELIVERY_ORDER item in objDEl.DELIVERY_ORDER)
            {
                DELIVERY_ORDER tmpStore = listItem.Where(x => x.STORE_ID == item.STORE_ID).FirstOrDefault();
                item.DELIVERY_ORDER_DETAIL = cmdDeliveryOrderDetail.GetAllIncludeByDeliveryOrder(item.DELORDER_ID);
                if (tmpStore == null)
                {
                    tmpStore = ObjectCopier.Clone<DELIVERY_ORDER>(item);


                    listOrder = new List<DELIVERY_ORDER_DETAIL>();
                    foreach (DELIVERY_ORDER_DETAIL od in item.DELIVERY_ORDER_DETAIL)
                    {
                        DELIVERY_ORDER_DETAIL tmp = listOrder.Where(x => x.PRODUCT_ID == od.PRODUCT_ID && x.COLOR_TYPE_ID == od.COLOR_TYPE_ID
                            && x.COLOR_ID == od.COLOR_ID && x.IS_FREE == od.IS_FREE).FirstOrDefault();
                        if (tmp != null)
                        {
                            tmp.PRODUCT_SENT_QTY += od.PRODUCT_SENT_QTY;
                        }
                        else
                        {
                            tmp = ObjectCopier.Clone<DELIVERY_ORDER_DETAIL>(od);
                            listOrder.Add(tmp);
                        }
                    }

                    listOrder2 = new List<DELIVERY_ORDER_DETAIL>();
                    foreach (DELIVERY_ORDER_DETAIL od in listOrder)
                    {
                        DELIVERY_ORDER_DETAIL tmp = listOrder2.Where(x => x.PRODUCT_ID == od.PRODUCT_ID && x.IS_FREE == od.IS_FREE).FirstOrDefault();
                        if (tmp != null)
                        {
                            if (od.IS_FREE == "N")
                            {
                                tmp.ColorDesc += ", " + od.COLOR_TYPE.COLOR_TYPE_SUBNAME + " " + od.COLOR.COLOR_SUBNAME + " " + od.PRODUCT_SENT_QTY;
                            }
                            else
                            {
                                tmp.ColorDesc += ", แถม " + od.PRODUCT.PRODUCT_NAME + " " + od.COLOR.COLOR_SUBNAME + " " + od.PRODUCT_SENT_QTY;

                            }
                            tmp.PRODUCT_SENT_QTY += od.PRODUCT_SENT_QTY;
                        }
                        else
                        {
                            tmp = ObjectCopier.Clone<DELIVERY_ORDER_DETAIL>(od);
                            if (tmp.IS_FREE == "N")
                            {
                                tmp.ColorDesc = od.PRODUCT.PRODUCT_NAME + " " + od.COLOR_TYPE.COLOR_TYPE_SUBNAME + " " + od.COLOR.COLOR_SUBNAME + " " + od.PRODUCT_SENT_QTY;
                            }
                            else
                            {
                                tmp.ColorDesc = "แถม " + od.PRODUCT.PRODUCT_NAME + " " + od.COLOR_TYPE.COLOR_TYPE_SUBNAME + " " + od.COLOR.COLOR_SUBNAME + " " + od.PRODUCT_SENT_QTY;
                            }
                            listOrder2.Add(tmp);
                        }
                    }
                    tmpStore.DELIVERY_ORDER_DETAIL = listOrder2;

                    listItem.Add(tmpStore);
                }
                else
                {
                    foreach (DELIVERY_ORDER_DETAIL od in item.DELIVERY_ORDER_DETAIL)
                    {
                        DELIVERY_ORDER_DETAIL tmp = listOrder.Where(x => x.PRODUCT_ID == od.PRODUCT_ID && x.COLOR_TYPE_ID == od.COLOR_TYPE_ID
                            && x.COLOR_ID == od.COLOR_ID && x.IS_FREE == od.IS_FREE).FirstOrDefault();
                        if (tmp != null)
                        {
                            tmp.PRODUCT_SENT_QTY += od.PRODUCT_SENT_QTY;
                        }
                        else
                        {
                            tmp = ObjectCopier.Clone<DELIVERY_ORDER_DETAIL>(od);
                            listOrder.Add(tmp);
                        }
                    }

                    listOrder2 = new List<DELIVERY_ORDER_DETAIL>();
                    foreach (DELIVERY_ORDER_DETAIL od in listOrder)
                    {
                        DELIVERY_ORDER_DETAIL tmp = listOrder2.Where(x => x.PRODUCT_ID == od.PRODUCT_ID && x.IS_FREE == od.IS_FREE).FirstOrDefault();
                        if (tmp != null)
                        {

                            tmp.ColorDesc += ", " + od.COLOR_TYPE.COLOR_TYPE_SUBNAME + " " + od.COLOR.COLOR_SUBNAME + " " + od.PRODUCT_SENT_QTY;
                            tmp.PRODUCT_SENT_QTY += od.PRODUCT_SENT_QTY;
                        }
                        else
                        {

                            tmp = ObjectCopier.Clone<DELIVERY_ORDER_DETAIL>(od);
                            if (tmp.IS_FREE == "N")
                            {
                                tmp.ColorDesc = od.PRODUCT.PRODUCT_NAME + " " + od.COLOR_TYPE.COLOR_TYPE_SUBNAME + " " + od.COLOR.COLOR_SUBNAME + " " + od.PRODUCT_SENT_QTY;
                            }
                            else
                            {
                                tmp.ColorDesc = "แถม " + od.PRODUCT.PRODUCT_NAME + " " + od.COLOR_TYPE.COLOR_TYPE_SUBNAME + " " + od.COLOR.COLOR_SUBNAME + " " + od.PRODUCT_SENT_QTY;
                            }
                            listOrder2.Add(tmp);
                        }
                    }
                    tmpStore.DELIVERY_ORDER_DETAIL = listOrder2;
                }
            }

            foreach (DELIVERY_ORDER item in listItem)
            {
                foreach (DELIVERY_ORDER_DETAIL od in item.DELIVERY_ORDER_DETAIL)
                {
                    item.STORE = cmdStore.SelectInclude(item.STORE_ID);
                }
            }

            listItem = listItem.OrderBy(x => x.STORE.STORE_CODE).ThenBy(y => y.STORE.PROVINCE_ID).ToList();

            foreach (DELIVERY_ORDER item in listItem)
            {
                seq = 1;
                sumWeight = 0;
                foreach (DELIVERY_ORDER_DETAIL od in item.DELIVERY_ORDER_DETAIL)
                {
                    DataRow drSendOrderHeader = SendOrderHeader.NewRow();
                    drSendOrderHeader["STORE_NAME"] = item.STORE.STORE_NAME;
                    drSendOrderHeader["STORE_ADDR"] = item.STORE.STORE_ADDR1 + " ถ." + item.STORE.STORE_STREET + " ต." + item.STORE.STORE_SUBDISTRICT + " อ." + item.STORE.STORE_DISTRICT + " จ." + item.STORE.PROVINCE.PROVINCE_NAME + " " + item.STORE.STORE_POSTCODE;
                    drSendOrderHeader["STORE_TEL"] = item.STORE.STORE_TEL1;
                    if (item.STORE.STORE_TEL2 != "")
                    {
                        drSendOrderHeader["STORE_TEL"] += ("," + item.STORE.STORE_TEL2);
                    }
                    drSendOrderHeader["STORE_CODE"] = item.STORE.STORE_CODE;
                    drSendOrderHeader["ORDER_DATE"] = item.DELORDER_DATE.Value.ToShortDateString();
                    drSendOrderHeader["SEND_DATE"] = DateTime.Now.ToShortDateString();
                    drSendOrderHeader["ZONE_NAME"] = item.STORE.ZONE.ZONE_NAME;
                    drSendOrderHeader["VEHICLE_REG"] = txtVehicle.Text;

                    drSendOrderHeader["SEQ"] = seq.ToString();
                    drSendOrderHeader["NAME"] = od.ColorDesc;
                    drSendOrderHeader["QTY"] = od.PRODUCT_SENT_QTY.ToString();
                    drSendOrderHeader["PACKAGE"] = od.PRODUCT.PRODUCT_PACKING_DESC;
                    drSendOrderHeader["WEIGHT"] = od.PRODUCT.PRODUCT_WEIGHT;
                    drSendOrderHeader["SUM_WEIGHT"] = Convert.ToDecimal(od.PRODUCT.PRODUCT_WEIGHT * od.PRODUCT_SENT_QTY.Value);
                    sumWeight += Convert.ToDecimal(od.PRODUCT.PRODUCT_WEIGHT * od.PRODUCT_SENT_QTY.Value);
                    seq++;
                    drSendOrderHeader["SUM_WEIGHT_TH"] = ThaiBaht(sumWeight.ToString());
                    drSendOrderHeader["SUM_WEIGHT_NUMBER"] = sumWeight.ToString();

                    SendOrderHeader.Rows.Add(drSendOrderHeader);
                }
            }
            #endregion

            Session["SendOrderReportData"] = ds;
            Response.RedirectPermanent("../Reports/SendOrder.aspx");
        }

        private string ThaiBaht(string txt)
        {
            string bahtTxt, n, bahtTH = "";
            double amount;
            try { amount = Convert.ToDouble(txt); }
            catch { amount = 0; }
            bahtTxt = amount.ToString("####.00");
            string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
            string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
            string[] temp = bahtTxt.Split('.');
            string intVal = temp[0];
            string decVal = temp[1];
            if (Convert.ToDouble(bahtTxt) == 0)
                bahtTH = "ศูนย์กิโลกรัม";
            else
            {
                for (int i = 0; i < intVal.Length; i++)
                {
                    n = intVal.Substring(i, 1);
                    if (n != "0")
                    {
                        if ((i == (intVal.Length - 1)) && (n == "1"))
                            bahtTH += "เอ็ด";
                        else if ((i == (intVal.Length - 2)) && (n == "2"))
                            bahtTH += "ยี่";
                        else if ((i == (intVal.Length - 2)) && (n == "1"))
                            bahtTH += "";
                        else
                            bahtTH += num[Convert.ToInt32(n)];
                        bahtTH += rank[(intVal.Length - i) - 1];
                    }
                }
                if (decVal == "00")
                    bahtTH += "กิโลกรัม";
                else
                {
                    bahtTH += "จุด";
                    for (int i = 0; i < decVal.Length; i++)
                    {
                        n = decVal.Substring(i, 1);
                        if (n != "0")
                        {
                            if ((i == decVal.Length - 1) && (n == "1"))
                                bahtTH += "เอ็ด";
                            else if ((i == (decVal.Length - 2)) && (n == "2"))
                                bahtTH += "ยี่";
                            else if ((i == (decVal.Length - 2)) && (n == "1"))
                                bahtTH += "";
                            else
                                bahtTH += num[Convert.ToInt32(n)];
                            bahtTH += rank[(decVal.Length - i) - 1];
                        }
                    }
                    bahtTH += "กิโลกรัม";
                }
            }
            return bahtTH;
        }
    }
}