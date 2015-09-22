using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;

namespace SPW.UI.Web.PageLogin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.RemoveAll();
            }
        }

        protected void btnSingon_Click(object sender, EventArgs e)
        {
            var cmd = new UserService();
            USER tmpUser = cmd.SelectInclude(txtUsername.Text, txtPassword.Text);
            if (tmpUser != null)
            {
                var cmdRole = new RoleService();
                tmpUser.ROLE = cmdRole.SelectIncludeEmployee(tmpUser.ROLE.ROLE_ID);
                Session["user"] = tmpUser;
                Response.Redirect("../Page/MainAdmin.aspx");
            }
            else 
            {
                this.alert.Visible = true;
            }
        }
    }
}