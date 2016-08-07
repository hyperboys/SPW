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
    public partial class SearchRoleFunc : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private RoleService cmdRole;
        public List<ROLE> DataSouce
        {
            get
            {
                var list = (List<ROLE>)ViewState["listROLE"];
                return list;
            }
            set
            {
                ViewState["listROLE"] = value;
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
            cmdRole = (RoleService)_dataServiceEngine.GetDataService(typeof(RoleService));
        }


        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        public List<ROLE_FUNCTION> DataSouceNewRoleFunction
        {
            get
            {
                if (ViewState["listNewRoleFunction"] == null)
                {
                    ViewState["listNewRoleFunction"] = new List<ROLE_FUNCTION>();
                }
                var list = (List<ROLE_FUNCTION>)ViewState["listNewRoleFunction"];
                return list;
            }
            set
            {
                ViewState["listNewRoleFunction"] = value;
            }
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
            DataSouce = cmdRole.GetAll();
            gridRole.DataSource = DataSouce;
            gridRole.DataBind();
            DataSouceNewRoleFunction = new List<ROLE_FUNCTION>();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtRoleCode.Text.Equals(""))
            {
                gridRole.DataSource = DataSouce;
            }
            else
            {
                gridRole.DataSource = DataSouce.Where(x => x.ROLE_CODE.ToUpper().Contains(txtRoleCode.Text.ToUpper())).ToList();
            }
            gridRole.DataBind();
        }

        protected void gridDepartment_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            Response.RedirectPermanent("ManageRole.aspx?id=" + gridRole.DataKeys[e.NewEditIndex].Values[0].ToString());
        }

        protected void gridDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridRole.PageIndex = e.NewPageIndex;
            gridRole.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtRoleCode.Text = "";
            SearchGrid();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("ManageRole.aspx");
        }
    }
}