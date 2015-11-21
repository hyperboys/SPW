using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.Model;
using SPW.DataService;

namespace SPW.UI.Web.Page
{
    public partial class SearchCategory : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private CategoryService _categoryService;

        private List<CATEGORY> DataSouce 
        {
            get 
            {
                return ViewState["CategoryList"] as List<CATEGORY>;
            }
            set 
            {
                ViewState["CategoryList"] = value;
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
            ReloadDatasource();
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
            _categoryService = (CategoryService)_dataServiceEngine.GetDataService(typeof(CategoryService));
        }

        private void ReloadDatasource()
        {
            DataSouce = _categoryService.GetAll();
        }

        private void PrepareObjectScreen()
        {
            gridCategory.DataSource = DataSouce;
            gridCategory.DataBind();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtCategoryCode.Text.Equals(""))
            {
                gridCategory.DataSource = (List<CATEGORY>)ViewState["CategoryList"];
            }
            else
            {
                gridCategory.DataSource = DataSouce.Where(x => x.CATEGORY_CODE.ToUpper().Contains(txtCategoryCode.Text.ToUpper())).ToList();
            }
            gridCategory.DataBind();
        }

        protected void gridCategory_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            Response.RedirectPermanent("ManageCategory.aspx?id="+ gridCategory.DataKeys[e.NewEditIndex].Values[0].ToString()) ;
        }

        protected void gridCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridCategory.PageIndex = e.NewPageIndex;
            gridCategory.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtCategoryCode.Text = "";
            SearchGrid();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("ManageCategory.aspx");
        }
    }
}