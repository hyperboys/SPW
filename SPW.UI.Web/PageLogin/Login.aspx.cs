using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using SPW.UI.Web.Page;
using System.Web.SessionState;

namespace SPW.UI.Web.PageLogin
{
    public partial class Login : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private UserService cmdUserService;
        private RoleService cmdRoleService;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session.Clear();
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
            cmdUserService = (UserService)_dataServiceEngine.GetDataService(typeof(UserService));
            cmdRoleService = (RoleService)_dataServiceEngine.GetDataService(typeof(RoleService));
        }

        private void ReloadDatasource()
        {

        }

        private void PrepareObjectScreen()
        {
           
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

        protected void btnSingon_Click(object sender, EventArgs e)
        {
            USER tmpUser = cmdUserService.SelectInclude(txtUsername.Text, txtPassword.Text);
            if (tmpUser != null)
            {
                tmpUser.ROLE = cmdRoleService.SelectIncludeEmployee(tmpUser.ROLE.ROLE_ID);
                Session["user"] = tmpUser;
                Response.RedirectPermanent("../Page/MainAdmin.aspx");
            }
            else
            {
                this.alert.Visible = true;
            }
        }
    }
}