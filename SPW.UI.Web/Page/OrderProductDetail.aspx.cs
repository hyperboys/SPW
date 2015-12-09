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
    public partial class OrderProductDetail : System.Web.UI.Page
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

        public int INDEX
        {
            get
            {
                if (ViewState["index"] == null)
                {
                    ViewState["index"] = 0;
                }

                var list = (int)ViewState["index"];
                return list;
            }
            set
            {
                ViewState["index"] = value;
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
                order.ORDER_CODE = "";
                order.ORDER_DATE = System.DateTime.Now;
                order.ORDER_STEP = "10";
                //order.ORDER_TOTAL = Convert.ToDecimal(lblPrice.Text);
                //order.ORDER_APPROVE = "2";
                order.STORE_ID = store.STORE_ID;
                //order.SALE_ID = cmdZoneDetailService.Select(store.ZONE_DETAIL_ID.Value).EMPLOYEE_ID;
                order.SYE_DEL = false;
                order.CREATE_DATE = System.DateTime.Now;
                order.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                order.UPDATE_DATE = System.DateTime.Now;
                order.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                order.ORDER_DETAIL = lstOrderDetail;
                cmdOrderService.Add(order);
                order.ORDER_CODE = "IV" + order.ORDER_ID;
                cmdOrderService.Edit(order);
                alert.Visible = true;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnNew.Visible = false;
                gridProduct.Enabled = false;
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lstOrderDetail[INDEX].PRODUCT_QTY = Convert.ToInt32(txtQty.Text);
            lstOrderDetail[INDEX].COLOR_TYPE_ID = Convert.ToInt32(ddlColorType.SelectedValue);
            lstOrderDetail[INDEX].COLOR_ID = Convert.ToInt32(ddlColor.SelectedValue);
            lstOrderDetail[INDEX].PRODUCT_PRICE_TOTAL = Convert.ToDecimal(lstOrderDetail[INDEX].PRODUCT_PRICE * lstOrderDetail[INDEX].PRODUCT_QTY);
            Response.RedirectPermanent("OrderProductDetail.aspx");
        }

        protected void gridProduct_EditCommand(object sender, GridViewEditEventArgs e)
        {
            INDEX = e.NewEditIndex;
            OrderDetail = lstOrderDetail[INDEX];
            InitialDataPopup();
            this.txtQty.Focus();
            this.popup.Show();
        }

        protected void btnCancelD_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("OrderProductDetail.aspx");
        }

        protected void gridProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtQty = (TextBox)e.Row.FindControl("Qty");
                txtQty.Text = lstOrderDetail[e.Row.RowIndex].PRODUCT_QTY.Value.ToString();
                TextBox txtQtyFree = (TextBox)e.Row.FindControl("QtyFree");
                txtQtyFree.Text = lstOrderDetail[e.Row.RowIndex].QTYFree.ToString();
                lstOrderDetail[e.Row.RowIndex].PRODUCT_PRICE_TOTAL = Convert.ToDecimal(lstOrderDetail[e.Row.RowIndex].PRODUCT_PRICE) * Convert.ToDecimal(txtQty.Text);

                foreach (LinkButton button in e.Row.Cells[7].Controls.OfType<LinkButton>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('ต้องการจะลบข้อมูลใช่หรือไม่')){ return false; };";
                    }
                }
            }
        }

        protected void Qty_TextChanged(object sender, EventArgs e)
        {
            int i = 0;
            int sumQty = 0;
            decimal sumPrice = 0;
            foreach (ORDER_DETAIL item in lstOrderDetail)
            {
                item.PRODUCT_QTY = Convert.ToInt32(((TextBox)(gridProduct.Rows[i].FindControl("Qty"))).Text == "" ? "0" : ((TextBox)(gridProduct.Rows[i].FindControl("Qty"))).Text);
                item.PRODUCT_QTY = item.PRODUCT_QTY < 0 ? 0 : item.PRODUCT_QTY;
                item.PRODUCT_SEND_REMAIN = item.PRODUCT_QTY;
                item.PRODUCT_PRICE_TOTAL = Convert.ToDecimal(item.PRODUCT_PRICE * item.PRODUCT_QTY);
                sumQty += item.PRODUCT_QTY.Value;
                sumPrice += item.PRODUCT_PRICE_TOTAL.Value;
                i++;
            }
            this.sumQty.Text = sumQty.ToString();
            this.sumPrice.Text = sumPrice.ToString();

            gridProduct.DataSource = lstOrderDetail;
            gridProduct.DataBind();
        }

        protected void QtyFree_TextChanged(object sender, EventArgs e)
        {
            int i = 0;
            int sumQtyFree = 0;
            foreach (ORDER_DETAIL item in lstOrderDetail)
            {
                item.QTYFree = Convert.ToInt32(((TextBox)(gridProduct.Rows[i].FindControl("QtyFree"))).Text == "" ? "0" : ((TextBox)(gridProduct.Rows[i].FindControl("QtyFree"))).Text);
                item.QTYFree = item.QTYFree < 0 ? 0 : item.QTYFree;
                sumQtyFree += item.QTYFree;
                i++;
            }
            this.sumQtyFree.Text = sumQtyFree.ToString();
            gridProduct.DataSource = lstOrderDetail;
            gridProduct.DataBind();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("OrderProductDetail.aspx");
        }

        protected void gridProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lstOrderDetail.RemoveAt(e.RowIndex);
            if (lstOrderDetail.Count == 0)
            {
                Response.RedirectPermanent("OrderProduct.aspx");
            }
            else
            {
                Response.RedirectPermanent("OrderProductDetail.aspx");
            }
        }
    }
}