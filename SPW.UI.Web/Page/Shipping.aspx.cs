using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using SPW.UI.Web.Reports;
using System.Data;

namespace SPW.UI.Web.Page
{
    public partial class Shipping : System.Web.UI.Page
    {
        public List<ORDER> DataSouce
        {
            get
            {
                var list = (List<ORDER>)ViewState["listDeliveryOrder"];
                return list;
            }
            set
            {
                ViewState["listDeliveryOrder"] = value;
            }
        }

        public List<ORDER_DETAIL> DataSouceDetail
        {
            get
            {
                var list = (List<ORDER_DETAIL>)ViewState["listDeliveryOrderDetail"];
                return list;
            }
            set
            {
                ViewState["listDeliveryOrderDetail"] = value;
            }
        }

        private void BlindGrid()
        {
            gridStore.DataSource = DataSouce;
            gridStore.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["listDeliveryOrder"] = null;
                ViewState["listDeliveryOrderDetail"] = null;
                InitialData();
            }
        }

        private void InitialData()
        {
            var cmd = new OrderService();
            DataSouce = cmd.GetALLIncludeStore();
            gridStore.DataSource = DataSouce;
            gridStore.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtDeliveryCode.Text.Equals(""))
            {
                gridStore.DataSource = DataSouce;
            }
            else
            {
                gridStore.DataSource = DataSouce.Where(x => x.ORDER_CODE.Contains(txtDeliveryCode.Text)).ToList();
            }
            gridStore.DataBind();
        }

        protected void gridProduct_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            ViewState["orderId"] = gridStore.DataKeys[e.NewEditIndex].Values[0].ToString();
            InitialDataPopup();
            this.popup.Show();
        }

        protected void gridProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridStore.PageIndex = e.NewPageIndex;
            gridStore.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtDeliveryCode.Text = "";
            SearchGrid();
        }

        private void InitialDataPopup()
        {
            int orderId = Convert.ToInt32(ViewState["orderId"]);
            var cmd = new OrderDetailService();
            var cmdDO = new DeliveryOrderService();
            DataSouceDetail = cmd.GetALLInclude().Where(x => x.ORDER_ID == orderId && x.PRODUCT_QTY != x.PRODUCT_SEND_QTY).ToList();
            gridProductDetail.DataSource = DataSouceDetail;
            gridProductDetail.DataBind();

            var cmdVehicle = new VehicleService();
            var list = cmdVehicle.GetALL();
            foreach (var item in list)
            {
                ddlVehicle.Items.Add(new ListItem(item.VEHICLE_REGNO, item.VEHICLE_ID.ToString()));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int orderId = Convert.ToInt32(ViewState["orderId"]);
            var cmd = new OrderDetailService();
            DataSouceDetail = cmd.GetALLInclude().Where(x => x.ORDER_ID == orderId && x.PRODUCT_QTY != x.PRODUCT_SEND_QTY).ToList();
            int i = 0;
            int index = 0;
            DELIVERY_ORDER od = new DELIVERY_ORDER();
            od.DELIVERY_ORDER_DETAIL = new List<DELIVERY_ORDER_DETAIL>();
            var cmdDO = new DeliveryOrderService(od);
            foreach (ORDER_DETAIL item in DataSouceDetail)
            {
                if (Convert.ToInt32(((TextBox)gridProductDetail.Rows[i].Cells[4].FindControl("txtQty")).Text) > 0)
                {
                    item.PRODUCT_SEND_QTY += Convert.ToInt32(((TextBox)gridProductDetail.Rows[i].Cells[4].FindControl("txtQty")).Text);
                    DELIVERY_ORDER_DETAIL dod = new DELIVERY_ORDER_DETAIL();
                    dod.PRODUCT_ID = item.PRODUCT_ID;
                    dod.PRODUCT_PRICE = item.PRODUCT_PRICE;
                    dod.PRODUCT_QTY = item.PRODUCT_QTY;
                    dod.PRODUCT_SEND_QTY = Convert.ToInt32(((TextBox)gridProductDetail.Rows[i].Cells[4].FindControl("txtQty")).Text);
                    dod.PRODUCT_SEQ = (++index);
                    dod.PRODUCT_TOTAL = item.PRODUCT_TOTAL;
                    dod.CREATE_DATE = DateTime.Now;
                    dod.CREATE_EMPLOYEE_ID = 0;
                    dod.UPDATE_DATE = DateTime.Now;
                    dod.UPDATE_EMPLOYEE_ID = 0;
                    dod.SYE_DEL = true;
                    od.DELIVERY_ORDER_DETAIL.Add(dod);
                }
                i++;
            }
            od.DELORDER_DATE = DateTime.Now;
            od.DELORDER_STEP = "1";
            od.ORDER_ID = orderId;
            od.VEHICLE_ID = Convert.ToInt32(ddlVehicle.SelectedValue);
            od.CREATE_DATE = DateTime.Now;
            od.CREATE_EMPLOYEE_ID = 0;
            od.UPDATE_DATE = DateTime.Now;
            od.UPDATE_EMPLOYEE_ID = 0;
            od.SYE_DEL = true;
            cmdDO.Add();
            od.DELORDER_CODE = "IV" + od.DELORDER_ID;
            cmdDO.Edit();
            cmd = new OrderDetailService(DataSouceDetail);
            cmd.UpdateList();

            SendOrderReportData ds = new SendOrderReportData();
            DataTable SendOrderHeader = ds.Tables["SendOrderHeader"];
            DataTable SendOrderDetail = ds.Tables["SendOrderDetail"];
            DataTable SendOrderFooter = ds.Tables["SendOrderFooter"];

            var cmdOrder = new OrderService();
            ORDER Order = cmdOrder.Select(orderId);
            var cmdStore = new StoreService();
            od.STORE = cmdStore.Select(Order.STORE_ID);
            var cmdOrderdetail = new DeliveryOrderDetailService();
            od.DELIVERY_ORDER_DETAIL = cmdOrderdetail.GetALLInclude(od.DELORDER_ID);
            DataRow drSendOrderHeader = SendOrderHeader.NewRow();
            drSendOrderHeader["STORE_NAME"] = od.STORE.STORE_NAME;
            drSendOrderHeader["STORE_ADDR"] = od.STORE.STORE_ADDR1;
            drSendOrderHeader["STORE_TEL"] = od.STORE.STORE_TEL1;
            if (od.STORE.STORE_TEL2 != "")
            {
                drSendOrderHeader["STORE_TEL"] += ("," + od.STORE.STORE_TEL2);
            }
            drSendOrderHeader["STORE_CODE"] = od.STORE.STORE_CODE;
            drSendOrderHeader["ORDER_DATE"] = od.DELORDER_DATE.Value.ToShortDateString();
            drSendOrderHeader["SEND_DATE"] = DateTime.Now.ToShortDateString();
            drSendOrderHeader["ZONE_NAME"] = od.STORE.ZONE.ZONE_NAME;
            drSendOrderHeader["VEHICLE_REG"] = ddlVehicle.SelectedItem.Text;
            SendOrderHeader.Rows.Add(drSendOrderHeader);
            int seq = 1;
            decimal sumWeight = 0;
            foreach (DELIVERY_ORDER_DETAIL dodItem in od.DELIVERY_ORDER_DETAIL)
            {
                if (dodItem.PRODUCT_SEND_QTY > 0)
                {
                    DataRow drSendOrderDetail = SendOrderDetail.NewRow();
                    drSendOrderDetail["SEQ"] = seq.ToString();
                    drSendOrderDetail["NAME"] = dodItem.PRODUCT.PRODUCT_NAME;
                    drSendOrderDetail["QTY"] = dodItem.PRODUCT_SEND_QTY.ToString();
                    drSendOrderDetail["PACKAGE"] = dodItem.PRODUCT.PRODUCT_PACKING_DESC;
                    drSendOrderDetail["WEIGHT"] = dodItem.PRODUCT.PRODUCT_WEIGHT;
                    drSendOrderDetail["SUM_WEIGHT"] = (dodItem.PRODUCT.PRODUCT_WEIGHT * dodItem.PRODUCT_SEND_QTY).ToString();
                    sumWeight += (dodItem.PRODUCT.PRODUCT_WEIGHT * dodItem.PRODUCT_SEND_QTY).Value;
                    SendOrderDetail.Rows.Add(drSendOrderDetail);
                    seq++;
                }
            }

            DataRow drSendOrderFooter = SendOrderFooter.NewRow();
            drSendOrderFooter["SUM_WEIGHT_TH"] = ThaiBaht(sumWeight.ToString());
            drSendOrderFooter["SUM_WEIGHT_NUMBER"] = sumWeight.ToString();
            SendOrderFooter.Rows.Add(drSendOrderFooter);

            Session["SendOrderReportData"] = ds;
            Response.Redirect("../Reports/SendOrder.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            InitialData();
            Response.Redirect("Shipping.aspx");
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
                bahtTH = "ศูนย์บาทถ้วน";
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