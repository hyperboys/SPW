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
    public partial class ManageUser : System.Web.UI.Page
    {
        private USER _item;
        private DataServiceEngine _dataServiceEngine;
        private UserService cmdUser;
        private EmployeeService cmdEmp;
        private RoleService cmdRole;

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
            cmdEmp = (EmployeeService)_dataServiceEngine.GetDataService(typeof(EmployeeService));
            cmdRole = (RoleService)_dataServiceEngine.GetDataService(typeof(RoleService));
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
            dllEmp.Items.Clear();
            dllRole.Items.Clear();
            foreach (var item in cmdEmp.GetAll())
            {
                dllEmp.Items.Add(new ListItem(item.EMPLOYEE_NAME + " " + item.EMPLOYEE_SURNAME, item.EMPLOYEE_ID.ToString()));
            }
            foreach (var item in cmdRole.GetAll())
            {
                dllRole.Items.Add(new ListItem(item.ROLE_NAME, item.ROLE_ID.ToString()));
            }

            if (Request.QueryString["id"] != null)
            {
                _item = cmdUser.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
                if (_item != null)
                {
                    popTxt1.Text = _item.USER_NAME;
                    popTxt2.TextMode = TextBoxMode.SingleLine;
                    popTxt2.Text = "*************";
                    popTxt2.Enabled = false;
                    dllEmp.SelectedValue = _item.EMPLOYEE_ID.ToString();
                    dllRole.SelectedValue = _item.ROLE_ID.ToString();
                    flag.Text = "Edit";
                    lblName.Text = popTxt1.Text;
                }
            }
            else 
            {
                popTxt1.Text = "";
                popTxt2.Text = "";
                popTxt2.TextMode = TextBoxMode.Password;
                popTxt2.Enabled = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            USER userItem = Session["user"] as USER;
            if (cmdUser.Select(popTxt1.Text) == null)
            {
                var obj = new USER();
                obj.USER_NAME = popTxt1.Text;


                obj.EMPLOYEE_ID = Convert.ToInt32(dllEmp.SelectedValue);
                obj.ROLE_ID = Convert.ToInt32(dllRole.SelectedValue);
                if (flag.Text.Equals("Add"))
                {
                    obj.PASSWORD = popTxt2.Text;
                    obj.Action = ActionEnum.Create;
                    obj.CREATE_DATE = DateTime.Now;
                    obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    obj.UPDATE_DATE = DateTime.Now;
                    obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    obj.SYE_DEL = false;
                    cmdUser.Add(obj);
                }
                else
                {
                    obj.Action = ActionEnum.Update;
                    obj.USER_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                    obj.UPDATE_DATE = DateTime.Now;
                    obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    obj.SYE_DEL = false;
                    cmdUser.Edit(obj);
                }
                alert.Visible = true;
                Response.AppendHeader("Refresh", "2; url=SearchUser.aspx");
            }
            else 
            {
            
            }
          
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SearchUser.aspx");
        }
    }
}