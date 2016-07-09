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
        private DataServiceEngine _dataServiceEngine;
        private UserService cmdUser;
        private EmployeeService cmdEmp;

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

            USER userItem = Session["user"] as USER;

            if (userItem != null)
            {

                if (userItem != null)
                {
                    txtUsername.Text = userItem.USER_NAME;
                    txtUsername.Enabled = false;
                    txtUsername.TextMode = TextBoxMode.SingleLine;
                    lblName.Text = txtUsername.Text;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPassword1.Text != "" && txtPassword2.Text != "")
            {
                if (txtPassword1.Text == txtPassword2.Text)
                {
                    USER userItem = Session["user"] as USER;
                    userItem.PASSWORD = txtPassword1.Text;
                    userItem.UPDATE_DATE = DateTime.Now;
                    userItem.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    cmdUser.Edit(userItem);
                    btnSave.Enabled = false;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                    warning.Visible = false;
                    alert.Visible = true;
                    Response.AppendHeader("Refresh", "2; url=MainAdmin.aspx");
                }
                else 
                {
                    lblWarning.Text = "กรุณรกรอกข้อมูลรหัสผ่านทั้ง 2 ช่องให้ตรงกัน";
                    warning.Visible = true;
                    alert.Visible = false;
                }
            }
            else 
            {
                lblWarning.Text = "กรุณรกรอกข้อมูลรหัสผ่านทั้ง 2 ช่อง";
                warning.Visible = true;
                alert.Visible = false;
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("MainAdmin.aspx");
        }
    }
}