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
    public partial class OrderProductPreview : System.Web.UI.Page
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
            lstOrderDetail = null;
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
    }
}