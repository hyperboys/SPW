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
    public partial class OrderProductDetailSummary : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private ProductService cmdProductService;
        private ColorService cmdColorService;
        private ColorTypeService cmdColorTypeService;
        private OrderService cmdOrderService;
        private ZoneDetailService cmdZoneDetailService;

        public STORE _store
        {
            get
            {
                if (Session["store"] == null)
                {
                    Session["store"] = new STORE();
                }

                var list = (STORE)Session["store"];
                return list;
            }
            set
            {
                ViewState["store"] = value;
            }
        }
        public ORDER_DETAIL OrderDetail
        {
            get
            {
                if (ViewState["_product"] == null)
                {
                    ViewState["_product"] = new ORDER_DETAIL();
                }

                var list = (ORDER_DETAIL)ViewState["_product"];
                return list;
            }
            set
            {
                ViewState["_product"] = value;
            }
        }

        public List<ORDER_DETAIL> lstOrderDetail
        {
            get
            {
                if (Session["lstOrderDetail"] == null)
                {
                    Session["lstOrderDetail"] = new List<ORDER_DETAIL>();
                }

                var list = (List<ORDER_DETAIL>)Session["lstOrderDetail"];
                return list;
            }
            set
            {
                Session["lstOrderDetail"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitialPage();
            }
            else
            {
                ReloadPageEngine();
            }
        }

        private void InitialPage()
        {
            CreatePageEngine();
            //ReloadDatasource();
            PrepareObjectScreen();
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void InitialDataService()
        {
            cmdProductService = (ProductService)_dataServiceEngine.GetDataService(typeof(ProductService));
            cmdColorService = (ColorService)_dataServiceEngine.GetDataService(typeof(ColorService));
            cmdColorTypeService = (ColorTypeService)_dataServiceEngine.GetDataService(typeof(ColorTypeService));
            cmdZoneDetailService = (ZoneDetailService)_dataServiceEngine.GetDataService(typeof(ZoneDetailService));
            cmdOrderService = (OrderService)_dataServiceEngine.GetDataService(typeof(OrderService));
        }

        private void ReloadDatasource()
        {
            //DataSouce = _categoryService.GetAll();
        }

        private void PrepareObjectScreen()
        {
            _store = (STORE)Session["store"];
            if (lstOrderDetail.Count > 0)
            {
                btnSave.Enabled = true;
            }

            txtStoreCode.Text = _store.STORE_CODE;
            txtStoreName.Text = _store.STORE_NAME;

            List<ORDER_DETAIL> tmp = new List<ORDER_DETAIL>();
            int sumQty = 0;
            int sumQtyFree = 0;
            decimal sumPrice = 0;
            foreach (ORDER_DETAIL item in lstOrderDetail)
            {
                PRODUCT prod = cmdProductService.SelectNotInclude(item.PRODUCT_ID);
                item.COLOR = cmdColorService.Select(item.COLOR_ID);
                item.COLOR_TYPE = cmdColorTypeService.Select(item.COLOR_TYPE_ID);
                item.PRODUCT = prod;
                item.ProductName = prod.PRODUCT_NAME + " " + item.COLOR.COLOR_SUBNAME + " " + item.COLOR_TYPE.COLOR_TYPE_SUBNAME;
                sumQty += item.PRODUCT_QTY.Value;
                sumPrice += item.PRODUCT_PRICE_TOTAL.Value;
                sumQtyFree += item.QTYFree;
            }

            this.sumQty.Text = sumQty.ToString();
            this.sumPrice.Text = sumPrice.ToString();
            this.sumQtyFree.Text = sumQtyFree.ToString();
            gridProduct.DataSource = lstOrderDetail;
            gridProduct.DataBind();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            USER userItem = Session["user"] as USER;
            if (lstOrderDetail != null && lstOrderDetail.Count > 0)
            {
                STORE store = ((STORE)Session["store"]);
                ORDER order = new ORDER();
                order.Action = ActionEnum.Create;
                
                int seq = cmdOrderService.GetOrderCode(String.Format("{0:yyyyMM}", DateTime.Now));
                order.ORDER_CODE = "ORD"+ String.Format("{0:yyyyMM}", DateTime.Now) + seq.ToString("D3"); 
                order.ORDER_DATE = System.DateTime.Now;
                order.ORDER_STEP = "10";
                order.ORDER_TOTAL = Convert.ToDecimal(this.sumPrice.Text);
                order.STORE_ID = store.STORE_ID;
                order.SYE_DEL = false;
                order.CREATE_DATE = System.DateTime.Now;
                order.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                order.UPDATE_DATE = System.DateTime.Now;
                order.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                int i = 1;
                List<ORDER_DETAIL> tmplstOrderDetail = new List<ORDER_DETAIL>();
                foreach (ORDER_DETAIL o in lstOrderDetail)
                {
                    o.PRODUCT_SEQ = i++;

                    ORDER_DETAIL x = new ORDER_DETAIL();
                    x.PRODUCT_ID = o.PRODUCT_ID;
                    x.COLOR_TYPE_ID = o.COLOR_TYPE_ID;
                    x.COLOR_ID = o.COLOR_ID;
                    x.PRODUCT_PRICE = o.PRODUCT_PRICE;
                    x.PRODUCT_QTY = o.QTYFree;
                    x.PRODUCT_SEND_REMAIN = o.QTYFree;
                    x.PRODUCT_SEND_QTY = 0;
                    x.PRODUCT_SEND_ROUND = 0;
                    x.PRODUCT_SEQ = o.PRODUCT_SEQ;
                    x.PRODUCT_SEND_COMPLETE = o.PRODUCT_SEND_COMPLETE;
                    x.PRODUCT_WEIGHT = o.PRODUCT_WEIGHT;
                    x.PRODUCT_WEIGHT_TOTAL = Convert.ToDecimal(o.PRODUCT_QTY * o.PRODUCT_WEIGHT);
                    x.PRODUCT_PRICE_TOTAL = 0;
                    x.IS_FREE = "F";
                    x.CREATE_DATE = o.CREATE_DATE;
                    x.CREATE_EMPLOYEE_ID = o.CREATE_EMPLOYEE_ID;
                    x.UPDATE_DATE = o.UPDATE_DATE;
                    x.UPDATE_EMPLOYEE_ID = o.UPDATE_EMPLOYEE_ID;
                    x.SYE_DEL = false;
                    tmplstOrderDetail.Add(x);
                }
                if (tmplstOrderDetail.Count > 0)
                {
                    tmplstOrderDetail.AddRange(lstOrderDetail);
                }

                tmplstOrderDetail = tmplstOrderDetail.OrderBy(x => x.PRODUCT_SEQ).ThenByDescending(y => y.IS_FREE).ToList();
                //tmplstOrderDetail = tmplstOrderDetail.OrderBy(x => x.PRODUCT_SEQ).ToList();
                order.ORDER_DETAIL = tmplstOrderDetail;
                cmdOrderService.Add(order);
                //order.ORDER_CODE = "IV" + order.ORDER_ID;
                //cmdOrderService.Edit(order);
                alert.Visible = true;
                btnSave.Visible = false;
                btnNew.Visible = false;
                gridProduct.Enabled = false;
                
                Response.RedirectPermanent("OrderProductPreview.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["lstOrderDetail"] = null;
            Response.RedirectPermanent("MainAdmin.aspx");
        }

        private void InitialDataPopup()
        {
            var list = cmdColorService.GetAll();
            foreach (var item in list)
            {
                ddlColor.Items.Add(new ListItem(item.COLOR_NAME, item.COLOR_ID.ToString()));
            }

            var listType = cmdColorTypeService.GetAll();
            foreach (var item in listType)
            {
                ddlColorType.Items.Add(new ListItem(item.COLOR_TYPE_NAME, item.COLOR_TYPE_ID.ToString()));
            }

            PRODUCT prod = cmdProductService.SelectNotInclude(OrderDetail.PRODUCT_ID);
            lblProduct.Text = prod.PRODUCT_NAME;
            lblTxtProductCode.Text = prod.PRODUCT_CODE;
            lblPriceProduct.Text = OrderDetail.PRODUCT_PRICE.ToString();
            ddlColorType.SelectedValue = OrderDetail.COLOR_TYPE_ID.ToString();
            ddlColor.SelectedValue = OrderDetail.COLOR_ID.ToString();
            txtQty.Text = OrderDetail.PRODUCT_QTY.ToString();
            lblPacking.Text = prod.PRODUCT_PACKING_DESC;
        }

        protected void gridProduct_EditCommand(object sender, GridViewEditEventArgs e)
        {
            OrderDetail = lstOrderDetail[e.NewEditIndex];
            InitialDataPopup();
            this.txtQty.Focus();
            this.popup.Show();
        }

        protected void btnCancelD_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("OrderProductDetailSummary.aspx");
        }
    }
}