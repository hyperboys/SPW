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
    public partial class ManageCategory : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private CategoryService _categoryService;
        private CATEGORY _category = new CATEGORY();

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

        private void PrepareObjectScreen()
        {
            _categoryService = (CategoryService)_dataServiceEngine.GetDataService(typeof(CategoryService));

            if (Request.QueryString["id"] != null)
            {
                _category = _categoryService.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
                if (_category != null)
                {
                    popTxtCategoryCode.Text = _category.CATEGORY_CODE;
                    txtCategoryName.Text = _category.CATEGORY_NAME;
                    lblName.Text = _category.CATEGORY_NAME;
                    flag.Text = "Edit";
                }
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            USER userItem = Session["user"] as USER;
            var obj = new CATEGORY();
            obj.CATEGORY_CODE = popTxtCategoryCode.Text;
            obj.CATEGORY_NAME = txtCategoryName.Text;
            if (flag.Text.Equals("Add"))
            {
                obj.Action = ActionEnum.Create;
                obj.CREATE_DATE = DateTime.Now;
                obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                _categoryService.Add(obj);
            }
            else
            {
                obj.Action = ActionEnum.Update;
                obj.CATEGORY_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                _categoryService.Edit(obj);
            }
            alert.Visible = true;
            Response.AppendHeader("Refresh", "2; url=SearchCategory.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SearchCategory.aspx");
        }
    }
}