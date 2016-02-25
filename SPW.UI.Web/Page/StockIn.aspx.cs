﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;

namespace SPW.UI.Web.Page
{
    public partial class StockIn : System.Web.UI.Page
    {
        #region Declare variable
        private DataServiceEngine _dataServiceEngine;
        public class DATAGRID
        {
            public DATAGRID()
            {

            }
            public int PRODUCT_ID { get; set; }
            public string PRODUCT_CODE { get; set; }
            public string PRODUCT_NAME { get; set; }
            public int COLOR_ID { get; set; }
            public string COLOR_NAME { get; set; }
            public int COLOR_TYPE_ID { get; set; }
            public string COLOR_TYPE_NAME { get; set; }
            public Nullable<int> STOCK_REMAIN { get; set; }
            public int STOCK_MINIMUM { get; set; }
        }
        #endregion

        #region Sevice control
        private StockProductService _stockProductService;
        private StockTypeService _stockTypeService;
        private CategoryService _categoryService;
        private ProductService _productService;
        private StockTransService _stockTransService;

        private void InitialPage()
        {
            CreatePageEngine();
            ReloadDatasource();
            InitialData();
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

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void InitialDataService()
        {
            _stockProductService = (StockProductService)_dataServiceEngine.GetDataService(typeof(StockProductService));
            _stockTypeService = (StockTypeService)_dataServiceEngine.GetDataService(typeof(StockTypeService));
            _categoryService = (CategoryService)_dataServiceEngine.GetDataService(typeof(CategoryService));
            _productService = (ProductService)_dataServiceEngine.GetDataService(typeof(ProductService));
            _stockTransService = (StockTransService)_dataServiceEngine.GetDataService(typeof(StockTransService));
        }

        private void ReloadDatasource()
        {
            List<STOCK_TYPE> listStockType = _stockTypeService.GetAll();
            ViewState["stocktypelist"] = listStockType;

            List<CATEGORY> listCategory = _categoryService.GetAll();
            ViewState["categorylist"] = listCategory;
        }

        private void InitialData()
        {
            lblUser.Text = ((USER)Session["user"]).USER_NAME;

            ddlCategory.Items.Clear();
            ddlCategory.Items.Insert(0, new ListItem("เพิ่มคลัง", "0"));
            ddlCategory.Items.Insert(1, new ListItem("ตั้งค่าคลัง", "1"));

            List<STOCK_TYPE> listStockType = (List<STOCK_TYPE>)ViewState["stocktypelist"];
            listStockType.ForEach(item => ddlStock.Items.Add(new ListItem(item.STOCK_TYPE_NAME, item.STOCK_TYPE_ID.ToString())));

            List<CATEGORY> listCategory = (List<CATEGORY>)ViewState["categorylist"];
            listCategory.ForEach(item => ddlProductType.Items.Add(new ListItem(item.CATEGORY_NAME, item.CATEGORY_ID.ToString())));
        }
        #endregion

        #region Business
        private void BindGridStockInSet()
        {
            PanelSet.Visible = true;
            PanelAdd.Visible = false;
            gridStockInAdd.DataSource = null;
            var data1 = _stockProductService.GetAllColorDetail();
            var data2 = _productService.GetAll();
            List<DATAGRID> query = (from stock in data1
                                    join product in data2 on stock.PRODUCT_ID equals product.PRODUCT_ID into joined
                                    from j in joined.DefaultIfEmpty(new PRODUCT())
                                    where j.CATEGORY_ID.Equals((ddlProductType.SelectedValue == "0" ? j.CATEGORY_ID : int.Parse(ddlProductType.SelectedValue)))
                                    select new DATAGRID
                                    {
                                        PRODUCT_ID = stock.PRODUCT_ID,
                                        PRODUCT_CODE = stock.PRODUCT_CODE,
                                        PRODUCT_NAME = j.PRODUCT_NAME,
                                        COLOR_ID = stock.COLOR.COLOR_ID,
                                        COLOR_NAME = stock.COLOR.COLOR_NAME,
                                        COLOR_TYPE_ID = stock.COLOR_TYPE.COLOR_TYPE_ID,
                                        COLOR_TYPE_NAME = stock.COLOR_TYPE.COLOR_TYPE_NAME,
                                        STOCK_REMAIN = stock.STOCK_REMAIN,
                                        STOCK_MINIMUM = stock.STOCK_MINIMUM
                                    }).ToList();
            gridStockInSet.DataSource = query;
            gridStockInSet.DataBind();
        }
        private void BindGridStockInAdd()
        {
            PanelSet.Visible = false;
            PanelAdd.Visible = true;
            gridStockInSet.DataSource = null;
            var data1 = _stockProductService.GetAllColorDetail();
            var data2 = _productService.GetAll();
            List<DATAGRID> query = (from stock in data1
                                    join product in data2 on stock.PRODUCT_ID equals product.PRODUCT_ID into joined
                                    from j in joined.DefaultIfEmpty(new PRODUCT())
                                    where j.CATEGORY_ID.Equals((ddlProductType.SelectedValue == "0" ? j.CATEGORY_ID : int.Parse(ddlProductType.SelectedValue)))
                                    select new DATAGRID
                                    {
                                        PRODUCT_ID = stock.PRODUCT_ID,
                                        PRODUCT_CODE = stock.PRODUCT_CODE,
                                        PRODUCT_NAME = j.PRODUCT_NAME,
                                        COLOR_ID = stock.COLOR.COLOR_ID,
                                        COLOR_NAME = stock.COLOR.COLOR_NAME,
                                        COLOR_TYPE_ID = stock.COLOR_TYPE.COLOR_TYPE_ID,
                                        COLOR_TYPE_NAME = stock.COLOR_TYPE.COLOR_TYPE_NAME,
                                        STOCK_REMAIN = stock.STOCK_REMAIN,
                                        STOCK_MINIMUM = stock.STOCK_MINIMUM
                                    }).ToList();
            gridStockInAdd.DataSource = query;
            gridStockInAdd.DataBind();
        }
        private List<STOCK_PRODUCT_STOCK> GetUpdateItem(GridView gv)
        {
            List<STOCK_PRODUCT_STOCK> listUpdate = new List<STOCK_PRODUCT_STOCK>();

            foreach (GridViewRow row in gv.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hfSTOCK_REMAIN = (HiddenField)gv.Rows[row.RowIndex].FindControl("hfSTOCK_REMAIN");
                    TextBox txtSTOCK_REMAIN = (TextBox)gv.Rows[row.RowIndex].FindControl("txtSTOCK_REMAIN");
                    HiddenField hfSTOCK_MINIMUM = (HiddenField)gv.Rows[row.RowIndex].FindControl("hfSTOCK_MINIMUM");
                    TextBox txtSTOCK_MINIMUM = (TextBox)gv.Rows[row.RowIndex].FindControl("txtSTOCK_MINIMUM");
                    if ((hfSTOCK_REMAIN.Value != txtSTOCK_REMAIN.Text && !string.IsNullOrEmpty(txtSTOCK_REMAIN.Text)) ||
                        (hfSTOCK_MINIMUM.Value != txtSTOCK_MINIMUM.Text && !string.IsNullOrEmpty(txtSTOCK_MINIMUM.Text)))
                    {
                        STOCK_PRODUCT_STOCK sps = new STOCK_PRODUCT_STOCK();
                        sps.Action = ActionEnum.Update;
                        sps.PRODUCT_ID = int.Parse(gv.DataKeys[row.RowIndex].Value.ToString());
                        sps.PRODUCT_CODE = ((Label)row.FindControl("lblPRODUCT_CODE")).Text;
                        sps.COLOR_ID = int.Parse(((HiddenField)row.FindControl("hfCOLOR_ID")).Value);
                        sps.COLOR_TYPE_ID = int.Parse(((HiddenField)row.FindControl("hfCOLOR_TYPE_ID")).Value);
                        sps.STOCK_REMAIN = int.Parse(((TextBox)row.FindControl("txtSTOCK_REMAIN")).Text == "" ? "0" : ((TextBox)row.FindControl("txtSTOCK_REMAIN")).Text);
                        sps.STOCK_BEFORE = int.Parse(((HiddenField)row.FindControl("hfSTOCK_REMAIN")).Value);
                        sps.STOCK_MINIMUM = int.Parse(((TextBox)row.FindControl("txtSTOCK_MINIMUM")).Text);
                        sps.UPDATE_DATE = DateTime.Now;
                        listUpdate.Add(sps);
                    }
                }
            }

            return listUpdate;
        }
        private List<STOCK_PRODUCT_TRANS> GetUpdateTrans(List<STOCK_PRODUCT_STOCK> listStock, string trans_type)
        {
            List<STOCK_PRODUCT_TRANS> listTrans = new List<STOCK_PRODUCT_TRANS>();

            foreach (STOCK_PRODUCT_STOCK stock in listStock)
            {
                STOCK_PRODUCT_TRANS trans = new STOCK_PRODUCT_TRANS();
                trans.Action = ActionEnum.Create;
                trans.PRODUCT_ID = stock.PRODUCT_ID;
                trans.PRODUCT_CODE = stock.PRODUCT_CODE;
                trans.COLOR_ID = stock.COLOR_ID;
                trans.COLOR_TYPE_ID = stock.COLOR_TYPE_ID;
                trans.TRANS_DATE = DateTime.Now;
                trans.TRANS_TYPE = (trans_type == "ADJ" ? (stock.STOCK_REMAIN - stock.STOCK_BEFORE > 0 ? "IN" : "OUT") : trans_type);
                trans.STOCK_BEFORE = stock.STOCK_BEFORE;
                trans.TRANS_QTY = stock.STOCK_REMAIN - stock.STOCK_BEFORE;
                trans.STOCK_AFTER = stock.STOCK_REMAIN;
                trans.APPROVE_EMPLOYEE_ID = ((USER)Session["user"]).USER_ID;
                trans.SYS_TIME = DateTime.Now.TimeOfDay;
                trans.CREATE_DATE = DateTime.Now;
                trans.UPDATE_DATE = DateTime.Now;
                trans.CREATE_EMPLOYEE_ID = ((USER)Session["user"]).USER_ID;
                trans.UPDATE_EMPLOYEE_ID = ((USER)Session["user"]).USER_ID;
                trans.SYE_DEL = false;
                listTrans.Add(trans);
            }

            return listTrans;
        }
        #endregion

        #region ASP control
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedValue == "0")
            {
                BindGridStockInAdd();
            }
            else
            {
                BindGridStockInSet();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<STOCK_PRODUCT_STOCK> listUpdate = new List<STOCK_PRODUCT_STOCK>();
            List<STOCK_PRODUCT_TRANS> listSPT = new List<STOCK_PRODUCT_TRANS>();
            if (gridStockInSet.Rows.Count > 0)
            {
                listUpdate = GetUpdateItem(gridStockInSet);
                listSPT = GetUpdateTrans(listUpdate, "SET");
            }
            else if (gridStockInAdd.Rows.Count > 0)
            {
                listUpdate = GetUpdateItem(gridStockInAdd);
                listSPT = GetUpdateTrans(listUpdate, "ADJ");
            }
            if (listUpdate.Count > 0)
            {
                _stockProductService.EditList(listUpdate);
                _stockTransService.AddList(listSPT);
                btnSearch_Click(null, null);
            }
        }
        #endregion
    }
}