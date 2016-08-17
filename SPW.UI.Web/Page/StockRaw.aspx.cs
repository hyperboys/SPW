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
    public partial class StockRaw : System.Web.UI.Page
    {
        #region Declare variable
        private DataServiceEngine _dataServiceEngine;
        public class DATAGRID
        {
            public DATAGRID()
            {

            }
            public int RAW_ID { get; set; }
            public string RAW_NAME1 { get; set; }
            public string RAW_NAME2 { get; set; }
            public Nullable<int> RAW_REMAIN { get; set; }
            public int RAW_MINIMUM { get; set; }
        }
        #endregion

        #region Sevice control
        private StockRawStockService _stockRawStockService;
        private StockTypeService _stockTypeService;
        private CategoryService _categoryService;
        private RawProductService _rawProductService;
        //private StockRawReceiveTransService _stockRawReceiveTransService;

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
            _stockRawStockService = (StockRawStockService)_dataServiceEngine.GetDataService(typeof(StockRawStockService));
            _stockTypeService = (StockTypeService)_dataServiceEngine.GetDataService(typeof(StockTypeService));
            _categoryService = (CategoryService)_dataServiceEngine.GetDataService(typeof(CategoryService));
            _rawProductService = (RawProductService)_dataServiceEngine.GetDataService(typeof(RawProductService));
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
            //ddlCategory.Items.Insert(0, new ListItem("เพิ่มคลัง", "0"));
            //ddlCategory.Items.Insert(1, new ListItem("ตั้งค่าคลัง", "1"));
            ddlCategory.Items.Insert(0, new ListItem("ตั้งค่าคลัง", "1"));
            
            //List<CATEGORY> listCategory = (List<CATEGORY>)ViewState["categorylist"];
            //listCategory.ForEach(item => ddlProductType.Items.Add(new ListItem(item.CATEGORY_NAME, item.CATEGORY_ID.ToString())));

            CheckRawStock();
        }
        #endregion

        #region Business
        private void BindgridStockRawSet()
        {
            PanelSet.Visible = true;
            PanelAdd.Visible = false;
            gridStockRawAdd.DataSource = null;
            var data1 = _stockRawStockService.GetAll();
            var data2 = _rawProductService.GetAll();
            List<DATAGRID> query = (from stock in data1
                                    join product in data2 on stock.RAW_ID equals product.RAW_ID into joined
                                    from j in joined.DefaultIfEmpty(new RAW_PRODUCT())
                                    where j.RAW_NAME1.Contains((txtRawName.Text == "" ? j.RAW_NAME1 : txtRawName.Text)) && (j.RAW_ID.ToString()).Contains((txtRawID.Text == "" ? j.RAW_ID.ToString() : txtRawID.Text))                                    
                                    select new DATAGRID
                                    {
                                        RAW_ID = stock.RAW_ID,
                                        RAW_NAME1 = j.RAW_NAME1,
                                        RAW_NAME2 = j.RAW_NAME2,
                                        RAW_REMAIN = stock.RAW_REMAIN,
                                        RAW_MINIMUM = stock.RAW_MINIMUM
                                    }).ToList();
            gridStockRawSet.DataSource = query;
            gridStockRawSet.DataBind();
        }
        private void BindgridStockRawAdd()
        {
            PanelSet.Visible = false;
            PanelAdd.Visible = true;
            gridStockRawSet.DataSource = null;
            var data1 = _stockRawStockService.GetAll();
            var data2 = _rawProductService.GetAll();
            List<DATAGRID> query = (from stock in data1
                                    join product in data2 on stock.RAW_ID equals product.RAW_ID into joined
                                    from j in joined.DefaultIfEmpty(new RAW_PRODUCT())
                                    where j.RAW_NAME1.Contains((txtRawName.Text == "" ? j.RAW_NAME1 : txtRawName.Text)) && (j.RAW_ID.ToString()).Contains((txtRawID.Text == "" ? j.RAW_ID.ToString() : txtRawID.Text))
                                    select new DATAGRID
                                    {
                                        RAW_ID = stock.RAW_ID,
                                        RAW_NAME1 = j.RAW_NAME1,
                                        RAW_NAME2 = j.RAW_NAME2,
                                        RAW_REMAIN = stock.RAW_REMAIN,
                                        RAW_MINIMUM = stock.RAW_MINIMUM
                                    }).ToList();
            gridStockRawAdd.DataSource = query;
            gridStockRawAdd.DataBind();
        }
        private List<STOCK_RAW_STOCK> GetUpdateItem(GridView gv)
        {
            List<STOCK_RAW_STOCK> listUpdate = new List<STOCK_RAW_STOCK>();

            foreach (GridViewRow row in gv.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hfRAW_REMAIN = (HiddenField)gv.Rows[row.RowIndex].FindControl("hfRAW_REMAIN");
                    TextBox txtRAW_REMAIN = (TextBox)gv.Rows[row.RowIndex].FindControl("txtRAW_REMAIN");
                    HiddenField hfRAW_MINIMUM = (HiddenField)gv.Rows[row.RowIndex].FindControl("hfRAW_MINIMUM");
                    TextBox txtRAW_MINIMUM = (TextBox)gv.Rows[row.RowIndex].FindControl("txtRAW_MINIMUM");
                    if ((hfRAW_REMAIN.Value != txtRAW_REMAIN.Text && !string.IsNullOrEmpty(txtRAW_REMAIN.Text)) ||
                        (hfRAW_MINIMUM.Value != txtRAW_MINIMUM.Text && !string.IsNullOrEmpty(txtRAW_MINIMUM.Text)))
                    {
                        STOCK_RAW_STOCK sps = new STOCK_RAW_STOCK();
                        sps.Action = ActionEnum.Update;
                        sps.RAW_ID = int.Parse(gv.DataKeys[row.RowIndex].Value.ToString());
                        sps.RAW_REMAIN = int.Parse(((TextBox)row.FindControl("txtRAW_REMAIN")).Text == "" ? "0" : ((TextBox)row.FindControl("txtRAW_REMAIN")).Text);
                        sps.STOCK_BEFORE = int.Parse(((HiddenField)row.FindControl("hfRAW_REMAIN")).Value);
                        sps.RAW_MINIMUM = int.Parse(((TextBox)row.FindControl("txtRAW_MINIMUM")).Text);
                        sps.UPDATE_DATE = DateTime.Now;
                        listUpdate.Add(sps);
                    }
                }
            }

            return listUpdate;
        }
        public void CheckRawStock()
        {
            try
            {
                List<STOCK_RAW_STOCK> lstSTOCK_RAW_STOCK = _stockRawStockService.GetAll();
                List<RAW_PRODUCT> lstRAW_PRODUCT = _rawProductService.GetAll(1);
                List<RAW_PRODUCT> lstNewRaw = new List<RAW_PRODUCT>();
                USER userItem = Session["user"] as USER;
                if (lstSTOCK_RAW_STOCK.Count != lstRAW_PRODUCT.Count)
                {
                    lstRAW_PRODUCT.ForEach(e =>
                        {
                            if (!lstSTOCK_RAW_STOCK.Exists(f => f.RAW_ID.Equals(e.RAW_ID)))
                            {
                                lstNewRaw.Add(e);
                                STOCK_RAW_STOCK item = new STOCK_RAW_STOCK();
                                item.RAW_ID = e.RAW_ID;
                                item.RAW_MINIMUM = 0;
                                item.RAW_REMAIN = 0;
                                item.CREATE_DATE = DateTime.Now;
                                item.UPDATE_DATE = DateTime.Now;
                                item.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                                item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                                item.SYE_DEL = false;
                                item.Action = ActionEnum.Create;
                                _stockRawStockService.Add(item);
                            }
                        });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //private List<STOCK_RAW_RECEIVE_TRANS> GetUpdateTrans(List<STOCK_RAW_STOCK> listStock, string trans_type)
        //{
        //    List<STOCK_RAW_RECEIVE_TRANS> listTrans = new List<STOCK_RAW_RECEIVE_TRANS>();

        //    foreach (STOCK_RAW_STOCK stock in listStock)
        //    {
        //        STOCK_RAW_RECEIVE_TRANS trans = new STOCK_RAW_RECEIVE_TRANS();
        //        trans.RAW_ID = stock.RAW_ID;
        //        trans.TRANS_DATE = DateTime.Now;
        //        trans.TRANS_TYPE = (trans_type == "ADJ" ? (stock.RAW_REMAIN - stock.STOCK_BEFORE > 0 ? "IN" : "OUT") : trans_type);
        //        trans.PO_BK_NO = "-";
        //        trans.PO_RN_NO = "-";
        //        trans.PO_YY = "-";
        //        trans.VENDOR_ID = -1;
        //        trans.STOCK_BEFORE = stock.STOCK_BEFORE;
        //        trans.TRANS_QTY = stock.RAW_REMAIN - stock.STOCK_BEFORE;
        //        trans.STOCK_AFTER = stock.RAW_REMAIN;
        //        trans.APPROVE_EMPLOYEE_ID = ((USER)Session["user"]).USER_ID;
        //        trans.SYS_TIME = DateTime.Now.TimeOfDay;
        //        trans.CREATE_DATE = DateTime.Now;
        //        trans.UPDATE_DATE = DateTime.Now;
        //        trans.CREATE_EMPLOYEE_ID = ((USER)Session["user"]).USER_ID;
        //        trans.UPDATE_EMPLOYEE_ID = ((USER)Session["user"]).USER_ID;
        //        trans.SYE_DEL = false;
        //        trans.Action = ActionEnum.Create;
        //        listTrans.Add(trans);
        //    }

        //    return listTrans;
        //}
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
                BindgridStockRawAdd();
            }
            else
            {
                BindgridStockRawSet();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<STOCK_RAW_STOCK> listUpdate = new List<STOCK_RAW_STOCK>();
            //List<STOCK_RAW_RECEIVE_TRANS> listSPT = new List<STOCK_RAW_RECEIVE_TRANS>();
            if (gridStockRawSet.Rows.Count > 0)
            {
                listUpdate = GetUpdateItem(gridStockRawSet);
                //listSPT = GetUpdateTrans(listUpdate, "SET");
            }
            else if (gridStockRawAdd.Rows.Count > 0)
            {
                listUpdate = GetUpdateItem(gridStockRawAdd);
                //listSPT = GetUpdateTrans(listUpdate, "ADJ");
            }
            if (listUpdate.Count > 0)
            {
                _stockRawStockService.EditList(listUpdate);
                //_stockRawReceiveTransService.AddList(listSPT);
                btnSearch_Click(null, null);
            }
        }
        protected void gridStockRawSet_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            Response.RedirectPermanent("StockRawLot.aspx?RAW_ID=" + gridStockRawSet.DataKeys[e.NewEditIndex].Value.ToString());
        }
        #endregion
    }
}