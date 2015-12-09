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
        private DataServiceEngine _dataServiceEngine;
        private CategoryService cmdCategoryService;
        private ProductService cmdProductService;
        private ColorService cmdColorService;
        private ColorTypeService cmdColorTypeService;
        private StoreService cmdStoreService;

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
                Session["store"] = value;
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
            cmdCategoryService = (CategoryService)_dataServiceEngine.GetDataService(typeof(CategoryService));
            cmdProductService = (ProductService)_dataServiceEngine.GetDataService(typeof(ProductService));
            cmdColorService = (ColorService)_dataServiceEngine.GetDataService(typeof(ColorService));
            cmdColorTypeService = (ColorTypeService)_dataServiceEngine.GetDataService(typeof(ColorTypeService));
            cmdStoreService = (StoreService)_dataServiceEngine.GetDataService(typeof(StoreService));
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CreatePageEngine();
                InitialData();
            }
            else
            {
                ReloadPageEngine();
            }
        }

        private void InitialData()
        {
            if (Request.QueryString["id"] != null)
            {
                Session["storeId"] = Request.QueryString["id"].ToString();
                _store = cmdStoreService.Select(Convert.ToInt32(Request.QueryString["id"]));
            }

            if (_store != null)
            {
                txtStoreCode.Text = _store.STORE_CODE;
                txtStoreName.Text = _store.STORE_NAME;
                var list = cmdCategoryService.GetAll();
                foreach (var item in list)
                {
                    ddlCategory.Items.Add(new ListItem(item.CATEGORY_NAME, item.CATEGORY_ID.ToString()));
                }

                DataSouce = cmdProductService.GetAllInclude();
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
                    lblPrice.Text = lstOrderDetail.Select(x => x.PRODUCT_PRICE_TOTAL).Sum().ToString() + " ";
                    lblAmount.Text = lstOrderDetail.Count.ToString() + " ";
                    lbtnCart.Enabled = true;
                    //btnNext.Visible = true;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("OrderProductDetail.aspx");
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
            _product = cmdProductService.Select(Convert.ToInt32(gridProduct.DataKeys[e.NewEditIndex].Values[0].ToString()));
            InitDataPopUp();
            this.txtQty.Focus();
            this.popup.Show();
        }

        private void InitDataPopUp()
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

            this.lblProduct.Text = _product.PRODUCT_NAME;
            this.lblTxtProductCode.Text = _product.PRODUCT_CODE;
            this.lblPriceProduct.Text = DataSouceShowProduct.Where(x => x.PRODUCT_ID == _product.PRODUCT_ID).FirstOrDefault().PRICE.ToString();
            this.lblPacking.Text = _product.PRODUCT_PACKING_DESC;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("OrderProduct.aspx");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            bool validate = true;
            try
            {
                int x = Convert.ToInt32(txtQty.Text);
                if (x <= 0) 
                {
                    validate = false;
                    //this.lblError.Text = "กรุณากรอกตัวเลขมากกว่า 0";
                }
            }catch
            {
                validate = false;
                //this.lblError.Text = "กรุณากรอก (0 - 9)";
            }

            if (validate)
            {
                USER userItem = Session["user"] as USER;

                ORDER_DETAIL obj = lstOrderDetail.Where(x => x.PRODUCT_ID == _product.PRODUCT_ID && x.COLOR_ID == Convert.ToInt32(this.ddlColor.SelectedValue)
                   && x.COLOR_TYPE_ID == Convert.ToInt32(this.ddlColorType.SelectedValue)).FirstOrDefault();

                if (obj != null)
                {
                    obj.PRODUCT_QTY += Convert.ToInt32(this.txtQty.Text);
                    obj.PRODUCT_PRICE_TOTAL += Convert.ToDecimal(this.lblPriceProduct.Text) * Convert.ToDecimal(this.txtQty.Text);
                }
                else
                {
                    obj = new ORDER_DETAIL();
                    obj.PRODUCT_ID = _product.PRODUCT_ID;
                    obj.PRODUCT_PRICE = Convert.ToDecimal(this.lblPriceProduct.Text);
                    obj.PRODUCT_QTY = Convert.ToInt32(this.txtQty.Text);
                    obj.PRODUCT_PRICE_TOTAL = Convert.ToDecimal(this.lblPriceProduct.Text) * Convert.ToDecimal(this.txtQty.Text);
                    obj.PRODUCT_SEND_QTY = 0;
                    obj.PRODUCT_SEND_REMAIN = Convert.ToInt32(this.txtQty.Text);
                    obj.PRODUCT_SEND_ROUND = 0;
                    obj.PRODUCT_SEND_COMPLETE = "0";
                    obj.PRODUCT_WEIGHT = _product.PRODUCT_WEIGHT;
                    obj.PRODUCT_WEIGHT_TOTAL = Convert.ToDecimal(_product.PRODUCT_WEIGHT * obj.PRODUCT_QTY);
                    obj.COLOR_TYPE_ID = Convert.ToInt32(this.ddlColorType.SelectedValue);
                    obj.COLOR_ID = Convert.ToInt32(this.ddlColor.SelectedValue);
                    obj.CREATE_DATE = DateTime.Now;
                    obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    obj.UPDATE_DATE = DateTime.Now;
                    obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    obj.SYE_DEL = false;
                    obj.IS_FREE = "N";
                    lstOrderDetail.Add(obj);
                    lstOrderDetail = lstOrderDetail;
                }

                Response.RedirectPermanent("OrderProduct.aspx?id=" + Session["storeId"].ToString());
            }
            else 
            {
                this.popup.Show();
            }
        }

        #region FilterControl
        private void CreateFilterControl()
        {
            PlaceHolder1.Controls.Clear();
            UpdatePanel1.Triggers.Clear();
            //UpdatePanel1.Triggers.Add(new AsyncPostBackTrigger() { ControlID = "btnSearch" ,EventName ="Click"});

            Button objBtnPrevious = new Button();
            objBtnPrevious.ID = "btnPrevious";
            objBtnPrevious.Text = "Previous";
            objBtnPrevious.CssClass = "btn btn-primary";
            objBtnPrevious.Width = 100;
            objBtnPrevious.Click += new EventHandler(objBtnPrevious_Click);
            UpdatePanel1.Triggers.Add(new AsyncPostBackTrigger() { ControlID = "btnPrevious", EventName = "Click" });
            PlaceHolder1.Controls.Add(objBtnPrevious);
            //PlaceHolder1.Controls.Add(new LiteralControl("<br/>"));
            PlaceHolder1.Controls.Add(new LiteralControl(" "));

            DropDownList objddlPageSelect = new DropDownList();
            objddlPageSelect.ID = "ddlPageIndex";
            objddlPageSelect.CssClass = "text-center";
            objddlPageSelect.Width = 100;
            objddlPageSelect.Height = 32;
            objddlPageSelect.Style["text-align"] = "center";
            objddlPageSelect.AutoPostBack = true;
            objddlPageSelect.SelectedIndexChanged += new EventHandler(objddlPageSelect_SelectedIndexChanged);

            for (int i = 1; i <= 10; i++)
            {
                objddlPageSelect.Items.Add(i.ToString());
            }
            UpdatePanel1.Triggers.Add(new AsyncPostBackTrigger() { ControlID = "ddlPageIndex", EventName = "SelectedIndexChanged" });
            PlaceHolder1.Controls.Add(objddlPageSelect);
            PlaceHolder1.Controls.Add(new LiteralControl(" "));

            Button objBtnNext = new Button();
            objBtnNext.ID = "btnNext";
            objBtnNext.Text = "Next";
            objBtnNext.CssClass = "btn btn-primary";
            objBtnNext.Width = 100;
            objBtnNext.Height = 30;
            objBtnNext.Click += new EventHandler(objBtnNext_Click);
            UpdatePanel1.Triggers.Add(new AsyncPostBackTrigger() { ControlID = "btnNext", EventName = "Click" });
            PlaceHolder1.Controls.Add(objBtnNext);

            TextBox objPageIndex = new TextBox();
            objPageIndex.ID = "txtPageIndex";
            objPageIndex.Text = "40";
            objPageIndex.CssClass = "text-center";
            objPageIndex.Width = 100;
            objPageIndex.Height = 32;
            objPageIndex.Style["float"] = "right";
            objPageIndex.MaxLength = 3;
            PlaceHolder1.Controls.Add(objPageIndex);

            CompareValidator compval = new CompareValidator();
            compval.ID = "Compval";
            compval.ControlToValidate = "txtPageIndex";
            compval.ForeColor = System.Drawing.Color.Red;
            compval.Type = ValidationDataType.Integer;
            compval.Operator = ValidationCompareOperator.GreaterThanEqual;
            compval.ValueToCompare = "10";
            compval.Text = "Digit Only Accepted And Digit 10 - 999 ";
            compval.Style["float"] = "right";
            compval.CssClass = "text-center";
            compval.Width = 260;
            compval.Height = 30;
            compval.SetFocusOnError = true;
            compval.Style["margin-top"] = "6px";
            PlaceHolder1.Controls.Add(compval);
        }

        void objddlPageSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList objddlSelected = (DropDownList)sender;
            ViewState["PageIndex"] = Convert.ToInt32(objddlSelected.SelectedValue);
            //InitialData();
        }

        protected void objBtnNext_Click(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = (int)ViewState["PageIndex"] + 1;
            UpdatePageControl((int)ViewState["PageIndex"]);
            //InitialData();
        }

        protected void objBtnPrevious_Click(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = (int)ViewState["PageIndex"] - 1;
            UpdatePageControl((int)ViewState["PageIndex"]);
            //InitialData();
        }

        private void UpdatePageControl(int PageIndex)
        {
            DropDownList ddlPageIndex = (DropDownList)PlaceHolder1.FindControl("ddlPageIndex");
            ddlPageIndex.SelectedIndex = PageIndex - 1;
        }

        private void CreateFilterDataSource()
        {
            DropDownList ddlPageIndex = (DropDownList)PlaceHolder1.FindControl("ddlPageIndex");
            TextBox objtxtPageLimit = (TextBox)PlaceHolder1.FindControl("txtPageIndex");
            ViewState["PageIndex"] = 1;
            int PageLimit = Convert.ToInt32(objtxtPageLimit.Text);
            ViewState["PageLimit"] = PageLimit;
        }

        private void CreateFilterPageSelected(int SourceItems)
        {
            DropDownList ddlPageIndex = (DropDownList)PlaceHolder1.FindControl("ddlPageIndex");
            ddlPageIndex.Items.Clear();
            int PageLimit = (int)ViewState["PageLimit"];
            int AllPage = (int)Math.Ceiling((decimal)SourceItems / (decimal)PageLimit);
            ddlPageIndex.Items.Add("1");
            for (int i = 2; i <= AllPage; i++)
            {
                ddlPageIndex.Items.Add(i.ToString());
            }
        }

        private void PrepareButtonFilterDisplay()
        {
            Button btnPrevious = (Button)PlaceHolder1.FindControl("btnPrevious");
            Button btnNext = (Button)PlaceHolder1.FindControl("btnNext");
            DropDownList ddlPageIndex = (DropDownList)PlaceHolder1.FindControl("ddlPageIndex");

            if ((int)ViewState["PageIndex"] > 1)
            {
                btnPrevious.Enabled = true;
            }
            else
            {
                btnPrevious.Enabled = false;
            }

            int LastPageIndex = Convert.ToInt32(ddlPageIndex.Items[ddlPageIndex.Items.Count - 1].Text);
            if ((int)ViewState["PageIndex"] < LastPageIndex)
            {
                btnNext.Enabled = true;
            }
            else
            {
                btnNext.Enabled = false;
            }
        }
        #endregion
    }
}