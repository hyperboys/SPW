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
    public partial class OrderProduct : System.Web.UI.Page
    {
        public STORE _store
        {
            get
            {
                if (ViewState["store"] == null)
                {
                    ViewState["store"] = new STORE();
                }

                var list = (STORE)ViewState["store"];
                return list;
            }
            set
            {
                ViewState["store"] = value;
            }
        }

        public PRODUCT _product
        {
            get
            {
                if (ViewState["_product"] == null)
                {
                    ViewState["_product"] = new PRODUCT();
                }

                var list = (PRODUCT)ViewState["_product"];
                return list;
            }
            set
            {
                ViewState["_product"] = value;
            }
        }

        public List<PRODUCT> DataSouce
        {
            get
            {
                if (ViewState["listProduct"] == null)
                {
                    ViewState["listProduct"] = new List<PRODUCT>();
                }

                var list = (List<PRODUCT>)ViewState["listProduct"];
                return list;
            }
            set
            {
                ViewState["listProduct"] = value;
            }
        }

        public List<ShowProduct> DataSouceShowProduct
        {
            get
            {
                if (ViewState["ShowProduct"] == null)
                {
                    ViewState["ShowProduct"] = new List<ShowProduct>();
                }

                var list = (List<ShowProduct>)ViewState["ShowProduct"];
                return list;
            }
            set
            {
                ViewState["ShowProduct"] = value;
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
                InitialData();
            }
        }

        private void InitialData()
        {
            _store = (STORE)Session["store"];
            txtStoreCode.Text = _store.STORE_CODE;
            txtStoreName.Text = _store.STORE_NAME;

            var cmdCat = new CategoryService();
            var list = cmdCat.GetALL();
            foreach (var item in list)
            {
                ddlCategory.Items.Add(new ListItem(item.CATEGORY_NAME, item.CATEGORY_ID.ToString()));
            }

            var cmd = new ProductService();
            DataSouce = cmd.GetALLInclude().Where(x => x.PRODUCT_TYPE_CODE == 1).ToList();
            DataSouceShowProduct = new List<ShowProduct>();
            foreach (PRODUCT pro in DataSouce)
            {
                ShowProduct tmp = new ShowProduct();
                tmp.PRODUCT_ID = pro.PRODUCT_ID;
                tmp.PRODUCT_CODE = pro.PRODUCT_CODE;
                tmp.PRODUCT_NAME = pro.PRODUCT_NAME;
                tmp.PRICE = pro.PRODUCT_PRICELIST.Where(x => x.PRODUCT_ID == pro.PRODUCT_ID && x.ZONE_ID == _store.ZONE_ID).FirstOrDefault()
                    != null ? (decimal)pro.PRODUCT_PRICELIST.Where(x => x.PRODUCT_ID == pro.PRODUCT_ID && x.ZONE_ID == _store.ZONE_ID).FirstOrDefault().PRODUCT_PRICE : 0;
                tmp.CATEGORY_ID = pro.CATEGORY_ID;
                DataSouceShowProduct.Add(tmp);
            }
            gridProduct.DataSource = DataSouceShowProduct;
            gridProduct.DataBind();

            if (lstOrderDetail.Count > 0)
            {
                lblPrice.Text = lstOrderDetail.Select(x => x.PRODUCT_TOTAL).Sum().ToString();
                linkToOrder.Visible = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lstOrderDetail != null && lstOrderDetail.Count > 0)
            {
                ORDER order = new ORDER();
                var cmd = new OrderService(order);
                order.Action = ActionEnum.Create;
                order.ORDER_CODE = "";
                order.ORDER_DATE = System.DateTime.Now;
                order.ORDER_STEP = "1";
                order.ORDER_TOTAL = Convert.ToDecimal(lblPrice.Text);
                order.ORDER_APPROVE = "2";
                order.STORE_ID = ((STORE)ViewState["store"]).STORE_ID;
                order.SYE_DEL = true;
                order.CREATE_DATE = System.DateTime.Now;
                order.CREATE_EMPLOYEE_ID = 0;
                order.UPDATE_DATE = System.DateTime.Now;
                order.UPDATE_EMPLOYEE_ID = 0;
                int i = 1;
                foreach (ORDER_DETAIL od in lstOrderDetail)
                {
                    od.PRODUCT_SEQ = i++;
                }
                order.ORDER_DETAIL = lstOrderDetail;
                cmd.Add();
                order.ORDER_CODE = "IV" + order.ORDER_ID;
                cmd.Edit();
                Response.Redirect("MainAdmin.aspx");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (ddlCategory.SelectedValue != "0")
            {
                gridProduct.DataSource = DataSouceShowProduct.Where(x => x.CATEGORY_ID == Convert.ToInt32(ddlCategory.SelectedValue)
                    && x.PRODUCT_CODE.ToUpper().Contains(txtProductCode.Text.ToUpper()));
            }
            else
            {
                gridProduct.DataSource = DataSouceShowProduct.Where(x => x.PRODUCT_CODE.ToUpper().Contains(txtProductCode.Text.ToUpper()));
            }
            gridProduct.DataBind();
        }

        protected void gridProduct_EditCommand(object sender, GridViewEditEventArgs e)
        {
            var cmd = new ProductService();
            _product = cmd.Select(Convert.ToInt32(gridProduct.DataKeys[e.NewEditIndex].Values[0].ToString()));
            InitDataPopUp();
            this.popup.Show();
        }

        private void InitDataPopUp()
        {
            var cmd = new ColorService();
            var list = cmd.GetALL();
            foreach (var item in list)
            {
                ddlColor.Items.Add(new ListItem(item.COLOR_NAME, item.COLOR_ID.ToString()));
            }

            var cmdType = new TraceryService();
            var listType = cmdType.GetALL();
            foreach (var item in listType)
            {
                ddlColorType.Items.Add(new ListItem(item.COLOR_TYPE_NAME, item.COLOR_TYPE_ID.ToString()));
            }

            this.lblProduct.Text = _product.PRODUCT_NAME;
            this.lblTxtProductCode.Text = _product.PRODUCT_CODE;
            this.lblPriceProduct.Text = DataSouceShowProduct.Where(x => x.PRODUCT_ID == _product.PRODUCT_ID).FirstOrDefault().PRICE.ToString();
            this.lblPacking.Text = _product.PRODUCT_PACKING_DESC;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrderProduct.aspx");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ORDER_DETAIL obj = new ORDER_DETAIL();
            obj.PRODUCT_ID = _product.PRODUCT_ID;
            obj.PRODUCT_PRICE = Convert.ToDecimal(this.lblPriceProduct.Text);
            obj.PRODUCT_QTY = Convert.ToInt32(this.txtQty.Text);
            obj.PRODUCT_TOTAL = Convert.ToDecimal(this.lblPriceProduct.Text) * Convert.ToDecimal(this.txtQty.Text);
            obj.PRODUCT_SEND_QTY = 0;
            obj.CREATE_DATE = DateTime.Now;
            obj.CREATE_EMPLOYEE_ID = 0;
            obj.UPDATE_DATE = DateTime.Now;
            obj.UPDATE_EMPLOYEE_ID = 0;
            obj.SYE_DEL = true;
            obj.IS_FREE = false;
            lstOrderDetail.Add(obj);

            int zoneID = _store.ZONE_ID.Value;
            int productFree = 0;
            int proQty = obj.PRODUCT_QTY.Value;
            var cmd = new ProductPromotionService();
            PRODUCT_PROMOTION cond = cmd.SelectByProductZone(obj.PRODUCT_ID, zoneID);
            if (cond != null)
            {
                for (productFree = 0; (proQty - cond.PRODUCT_CONDITION_QTY) >= 0; productFree += cond.PRODUCT_FREE_QTY.Value)
                {
                    proQty -= cond.PRODUCT_CONDITION_QTY.Value;
                }

                if (productFree != 0)
                {
                    ORDER_DETAIL objFree = new ORDER_DETAIL();
                    objFree.PRODUCT_ID = _product.PRODUCT_ID;
                    objFree.PRODUCT_PRICE = 0;
                    objFree.PRODUCT_QTY = productFree;
                    objFree.PRODUCT_TOTAL = 0;
                    objFree.PRODUCT_SEND_QTY = 0;
                    objFree.CREATE_DATE = DateTime.Now;
                    objFree.CREATE_EMPLOYEE_ID = 0;
                    objFree.UPDATE_DATE = DateTime.Now;
                    objFree.UPDATE_EMPLOYEE_ID = 0;
                    objFree.SYE_DEL = true;
                    objFree.IS_FREE = true;
                    lstOrderDetail.Add(objFree);
                }
            }
            Response.Redirect("OrderProduct.aspx");
        }
    }
}