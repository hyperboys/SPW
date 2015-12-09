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
    public partial class SearchUser : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private UserService cmdUser;

        public List<USER> DataSouce
        {
            get
            {
                var list = (List<USER>)ViewState["listUser"];
                return list;
            }
            set
            {
                ViewState["listUser"] = value;
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
            cmdUser = (UserService)_dataServiceEngine.GetDataService(typeof(UserService));
        }


        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void BlindGrid()
        {
            gridUsername.DataSource = DataSouce;
            gridUsername.DataBind();
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
            DataSouce = cmdUser.GetAllInclude();
            gridUsername.DataSource = DataSouce;
            gridUsername.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtUsername.Text.Equals(""))
            {
                gridUsername.DataSource = DataSouce;
            }
            else
            {
                gridUsername.DataSource = DataSouce.Where(x => x.USER_NAME.Contains(txtUsername.Text)).ToList();
            }
            gridUsername.DataBind();
        }

        protected void gridDepartment_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            Response.RedirectPermanent("ManageUser.aspx?id=" + gridUsername.DataKeys[e.NewEditIndex].Values[0].ToString());
        }

        protected void gridDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridUsername.PageIndex = e.NewPageIndex;
            gridUsername.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            SearchGrid();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("ManageUser.aspx");
        }
    }
}