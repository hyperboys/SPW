using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using SPW.UI.Web.Reports;

namespace SPW.UI.Web.Page
{
    public partial class SurveyOrder : System.Web.UI.Page
    {
        public List<ORDER> DataSouce
        {
            get
            {
                if (ViewState["listOrder"] == null)
                {
                    ViewState["listOrder"] = new List<ORDER>();
                }
                var list = (List<ORDER>)ViewState["listOrder"];
                return list;
            }
            set
            {
                ViewState["listOrder"] = value;
            }
        }

        public List<ORDER_DETAIL> DataSouceDetail
        {
            get
            {
                var list = (List<ORDER_DETAIL>)ViewState["listOrderDetail"];
                return list;
            }
            set
            {
                ViewState["listOrderDetail"] = value;
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
            if (txtStoreCode.Text.Equals("") && txtStoreName.Text.Equals("") && txtOrderCode.Text.Equals(""))
            {
                gridStore.DataSource = DataSouce;
            }
            else
            {
                gridStore.DataSource = DataSouce.Where(x => x.ORDER_CODE.ToUpper().Contains(txtOrderCode.Text.ToUpper()) && x.STORE.STORE_CODE.ToUpper().Contains(txtStoreCode.Text.ToUpper()) && x.STORE.STORE_NAME.ToUpper().Contains(txtStoreName.Text.ToUpper())).ToList();
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
            txtStoreCode.Text = "";
            txtStoreName.Text = "";
            txtOrderCode.Text = "";
            SearchGrid();
        }

        private void InitialDataPopup()
        {
            int orderId = Convert.ToInt32(ViewState["orderId"]);
            var cmd = new OrderDetailService();
            var cmdDetail = new DeliveryOrderDetailService();
            var cmdDO = new DeliveryOrderService();
            DataSouceDetail = cmd.GetALLInclude().Where(x => x.ORDER_ID == orderId && x.PRODUCT_QTY != x.PRODUCT_SEND_QTY).ToList();
            foreach (ORDER_DETAIL item in DataSouceDetail)
            {
                item.PRODUCT_QTY -= item.PRODUCT_SEND_QTY;
            }
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
            SendOrderReportData ds = new SendOrderReportData();
            DataTable SendOrderHeader = ds.Tables["SendOrderHeader"];
            DataTable SendOrderDetail = ds.Tables["SendOrderDetail"];
            DataTable SendOrderFooter = ds.Tables["SendOrderFooter"];

            var cmd = new OrderService();
            ORDER item = cmd.Select(Convert.ToInt32(ViewState["orderId"]));
            var cmdStore = new StoreService();
            item.STORE = cmdStore.Select(item.STORE_ID);
            var cmdOrderdetail = new OrderDetailService();
            item.ORDER_DETAIL = cmdOrderdetail.GetALLInclude(item.ORDER_ID);
            DataRow drSendOrderHeader = SendOrderHeader.NewRow();
            drSendOrderHeader["STORE_NAME"] = item.STORE.STORE_NAME;
            drSendOrderHeader["STORE_ADDR"] = item.STORE.STORE_ADDR1;
            drSendOrderHeader["STORE_TEL"] = item.STORE.STORE_TEL1;
            if (item.STORE.STORE_TEL2 != "")
            {
                drSendOrderHeader["STORE_TEL"] += ("," + item.STORE.STORE_TEL2);
            }
            drSendOrderHeader["STORE_CODE"] = item.STORE.STORE_CODE;
            drSendOrderHeader["ORDER_DATE"] = item.ORDER_DATE.Value.ToShortDateString();
            drSendOrderHeader["SEND_DATE"] = DateTime.Now.ToShortDateString();
            drSendOrderHeader["ZONE_NAME"] = item.STORE.ZONE.ZONE_NAME;
            drSendOrderHeader["VEHICLE_REG"] = ddlVehicle.SelectedItem.Text;
            SendOrderHeader.Rows.Add(drSendOrderHeader);
            int seq = 1;
            decimal sumWeight = 0;
            foreach (ORDER_DETAIL od in item.ORDER_DETAIL)
            {
                DataRow drSendOrderDetail = SendOrderDetail.NewRow();
                drSendOrderDetail["SEQ"] = seq.ToString();
                drSendOrderDetail["NAME"] = od.PRODUCT.PRODUCT_NAME;
                drSendOrderDetail["QTY"] = (od.PRODUCT_QTY - od.PRODUCT_SEND_QTY).ToString();
                drSendOrderDetail["PACKAGE"] = od.PRODUCT.PRODUCT_PACKING_DESC;
                drSendOrderDetail["WEIGHT"] = od.PRODUCT.PRODUCT_WEIGHT;
                drSendOrderDetail["SUM_WEIGHT"] = (od.PRODUCT.PRODUCT_WEIGHT * (od.PRODUCT_QTY - od.PRODUCT_SEND_QTY)).ToString();
                sumWeight += (od.PRODUCT.PRODUCT_WEIGHT * (od.PRODUCT_QTY - od.PRODUCT_SEND_QTY)).Value;
                SendOrderDetail.Rows.Add(drSendOrderDetail);
                seq++;
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
            Response.Redirect("SurveyOrder.aspx");
        }

        protected void gridProductDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[3].Text.ToString().Equals("True"))
                    e.Row.Cells[3].Text = "แถม";
                else if (e.Row.Cells[3].Text.ToString().Equals("False"))
                    e.Row.Cells[3].Text = "ซื้อ";
            }
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