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
    public partial class SearchDepartment : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private DepartmentService cmdDepartmant;
        public List<DEPARTMENT> DataSouce
        {
            get
            {
                var list = (List<DEPARTMENT>)ViewState["listDepartment"];
                return list;
            }
            set
            {
                ViewState["listDepartment"] = value;
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
            cmdDepartmant = (DepartmentService)_dataServiceEngine.GetDataService(typeof(DepartmentService));
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
            DataSouce = cmdDepartmant.GetAll();
            gridDepartment.DataSource = DataSouce;
            gridDepartment.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtDepartmentCode.Text.Equals(""))
            {
                gridDepartment.DataSource = DataSouce;
            }
            else
            {
                gridDepartment.DataSource = DataSouce.Where(x => x.DEPARTMENT_CODE.Contains(txtDepartmentCode.Text)).ToList();
            }
            gridDepartment.DataBind();
        }

        protected void gridDepartment_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            Response.RedirectPermanent("ManageDepartment.aspx?id=" + gridDepartment.DataKeys[e.NewEditIndex].Values[0].ToString());
        }

        protected void gridDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridDepartment.PageIndex = e.NewPageIndex;
            gridDepartment.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtDepartmentCode.Text = "";
            SearchGrid();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("ManageDepartment.aspx");
        }
    }
}