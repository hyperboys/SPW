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
        private USER _item;
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

        private void BlindGrid()
        {
            gridUsername.DataSource = DataSouce;
            gridUsername.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitialData();
            }
        }

        private void InitialData()
        {
            var cmd = new UserService();
            DataSouce = cmd.GetALLInclude();
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
            ViewState["userId"] = gridUsername.DataKeys[e.NewEditIndex].Values[0].ToString();
            InitialDataPopup();
            this.popup.Show();
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
            ViewState["userId"] = null;
            InitialDataPopup();
            this.popup.Show();
        }

        private void InitialDataPopup()
        {
            dllEmp.Items.Clear();
            dllRole.Items.Clear();
            var emp = new EmployeeService();
            foreach (var item in emp.GetALL())
            {
                dllEmp.Items.Add(new ListItem(item.EMPLOYEE_NAME + " " + item.EMPLOYEE_SURNAME, item.EMPLOYEE_ID.ToString()));
            }

            var role = new RoleService();
            foreach (var item in role.GetALL())
            {
                dllRole.Items.Add(new ListItem(item.ROLE_NAME, item.ROLE_ID.ToString()));
            }

            if (ViewState["userId"] != null)
            {
                var cmd = new UserService();
                _item = cmd.Select(Convert.ToInt32(ViewState["userId"].ToString()));
                if (_item != null)
                {
                    popTxtUsername.Text = _item.USER_NAME;
                    popTxtPassword.TextMode = TextBoxMode.SingleLine;
                    popTxtPassword.Text = "*************";
                    popTxtPassword.Enabled = false;
                    dllEmp.SelectedValue = _item.EMPLOYEE_ID.ToString();
                    dllRole.SelectedValue = _item.ROLE_ID.ToString();
                    flag.Text = "Edit";
                }
            }
            else 
            {
                popTxtPassword.TextMode = TextBoxMode.Password;
                popTxtPassword.Enabled = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var obj = new USER();
            obj.USER_NAME = popTxtUsername.Text;
            obj.PASSWORD = popTxtPassword.Text;
            obj.EMPLOYEE_ID = Convert.ToInt32(dllEmp.SelectedValue);
            obj.ROLE_ID = Convert.ToInt32(dllRole.SelectedValue);
            var cmd = new UserService(obj);
            if (flag.Text.Equals("Add"))
            {
                obj.Action = ActionEnum.Create;
                obj.CREATE_DATE = DateTime.Now;
                obj.CREATE_EMPLOYEE_ID = 0;
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = 0;
                obj.SYE_DEL = true;
                cmd.Add();
            }
            else
            {
                obj.Action = ActionEnum.Update;
                obj.USER_ID = Convert.ToInt32(ViewState["userId"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = 0;
                obj.SYE_DEL = true;
                cmd.Edit();
            }
            ViewState["userId"] = null;
            Response.Redirect("SearchUser.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["userId"] = null;
            Response.Redirect("SearchUser.aspx");
        }
    }
}