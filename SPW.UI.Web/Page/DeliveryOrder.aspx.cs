using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;

namespace SPW.UI.Web.Page
{
    public partial class DeliveryOrder : System.Web.UI.Page
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
            DataSouceDetail = cmd.GetALLInclude().Where(x => x.ORDER_ID == orderId).ToList();

            foreach (var item in cmdDetail.GetALLInclude().Where(y => y.DELIVERY_ORDER.ORDER_ID == orderId))
            {
                DataSouceDetail.RemoveAll(x => x.PRODUCT_ID == item.PRODUCT_ID);
            }
            gridProductDetail.DataSource = DataSouceDetail;
            gridProductDetail.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //var cmdPro = new ProductDetailService();
            //List<DELIVERY_ORDER> listDO = new List<DELIVERY_ORDER>();
            //DELIVERY_ORDER tmp = new DELIVERY_ORDER();
            //tmp.ORDER_ID = Convert.ToInt32(ViewState["orderId"].ToString());
            //tmp.DELORDER_DATE = DateTime.Now;
            //tmp.DELORDER_STEP = "0";
            //tmp.CREATE_DATE = DateTime.Now;
            //tmp.CREATE_EMPLOYEE_ID = 0;
            //tmp.SYE_DEL = true;
            //tmp.UPDATE_DATE = DateTime.Now;
            //tmp.UPDATE_EMPLOYEE_ID = 0;
            //tmp.DELIVERY_ORDER_DETAIL = new List<DELIVERY_ORDER_DETAIL>();
            //DELIVERY_ORDER_DETAIL dod;
            //for (int i = 0; i < gridProductDetail.Rows.Count; i++)
            //{
            //    CheckBox ch = (CheckBox)gridProductDetail.Rows[i].FindControl("checkBox");
            //    if (ch.Checked)
            //    {
            //        dod = new DELIVERY_ORDER_DETAIL();
            //        dod.PRODUCT_ID = Convert.ToInt32(gridProductDetail.DataKeys[i].Values[0].ToString());
            //        dod.CREATE_DATE = DateTime.Now;
            //        dod.CREATE_EMPLOYEE_ID = 0;
            //        dod.SYE_DEL = true;
            //        dod.UPDATE_DATE = DateTime.Now;
            //        dod.UPDATE_EMPLOYEE_ID = 0;
            //        dod.PRODUCT_PRICE = cmdPro.Select(dod.PRODUCT_ID).Where(x => x.ZONE_ID == DataSouce.Where(y => y.ORDER_ID == Convert.ToInt32(ViewState["orderId"].ToString())).FirstOrDefault().STORE.ZONE_ID).FirstOrDefault().PRODUCT_PRICE;
            //        ORDER_DETAIL od = DataSouceDetail.Where(x => x.PRODUCT_ID == dod.PRODUCT_ID && x.ORDER_ID == Convert.ToInt32(ViewState["orderId"].ToString())).FirstOrDefault();
            //        dod.PRODUCT_QTY = od.PRODUCT_QTY;
            //        dod.PRODUCT_SEQ = od.PRODUCT_SEQ;
            //        dod.PRODUCT_TOTAL = od.PRODUCT_TOTAL;
            //        tmp.DELIVERY_ORDER_DETAIL.Add(dod);
            //    }
            //}
            //var cmd = new DeliveryOrderService(tmp);
            //cmd.Add();

            //tmp.DELORDER_CODE = "IY" + tmp.DELORDER_ID;
            //cmd.Edit();

            //Response.Redirect("DeliveryOrder.aspx");

            DataTable dt = new DataTable(); 
            dt.Clear();
            dt.Columns.Add("STORE_NAME");
            dt.Columns.Add("STORE_ADDR");
            dt.Columns.Add("STORE_TEL");
            dt.Columns.Add("ORDER_DATE");
            dt.Columns.Add("SEND_DATE");
            dt.Columns.Add("ZONE_NAME");


            DataRow _ravi = dt.NewRow();
            _ravi["STORE_NAME"] = "aaaaaaaaaaaaaaaaaaaaaaaa";
            _ravi["STORE_ADDR"] = "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb";
            _ravi["STORE_TEL"] = "ccccccccccccc";
            _ravi["ORDER_DATE"] = DateTime.Now.ToShortDateString();
            _ravi["SEND_DATE"] = DateTime.Now.ToShortDateString();
            _ravi["ZONE_NAME"] = "500";
            dt.Rows.Add(_ravi);


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeliveryOrder.aspx");
        }
    }
}