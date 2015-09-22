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
    public partial class SearchEmployee : System.Web.UI.Page
    {
        private EMPLOYEE _employee;
        public List<EMPLOYEE> DataSouce
        {
            get
            {
                var list = (List<EMPLOYEE>)ViewState["listEmployee"];
                return list;
            }
            set
            {
                ViewState["listEmployee"] = value;
            }
        }

        public List<ZONE> DataSouceFunction
        {
            get
            {
                var list = (List<ZONE>)ViewState["listZone"];
                return list;
            }
            set
            {
                ViewState["listZone"] = value;
            }
        }

        public List<ZONE_DETAIL> DataSouceRoleFunction
        {
            get
            {
                var list = (List<ZONE_DETAIL>)ViewState["DataSouceRoleFunction"];
                return list;
            }
            set
            {
                ViewState["DataSouceRoleFunction"] = value;
            }
        }

        public List<ZONE_DETAIL> DataSouceNewRoleFunction
        {
            get
            {
                var list = (List<ZONE_DETAIL>)ViewState["DataSouceNewRoleFunction"];
                return list;
            }
            set
            {
                ViewState["DataSouceNewRoleFunction"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitialData();
            }
        }

        private void BlindGrid()
        {
            gridEmployee.DataSource = DataSouce;
            gridEmployee.DataBind();
        }

        private void InitialData()
        {
            var cmd = new EmployeeService();
            DataSouce = cmd.GetALL();
            gridEmployee.DataSource = DataSouce;
            gridEmployee.DataBind();
            DataSouceNewRoleFunction = new List<ZONE_DETAIL>();
        }

        private void InitialDataPopup()
        {
            var cmd = new DepartmentService();
            var list = cmd.GetALL();
            foreach (var item in list)
            {
                ddlDepartment.Items.Add(new ListItem(item.DEPARTMENT_NAME, item.DEPARTMENT_ID.ToString()));
            }

            if (ViewState["empId"] != null)
            {
                var cmdEmp = new EmployeeService();
                _employee = cmdEmp.Select(Convert.ToInt32(ViewState["empId"].ToString()));
                if (_employee != null)
                {
                    popTxtEmployeeCode.Text = _employee.EMPLOYEE_CODE;
                    txtName.Text = _employee.EMPLOYEE_NAME;
                    txtLastName.Text = _employee.EMPLOYEE_SURNAME;
                    ddlDepartment.SelectedValue = _employee.DEPARTMENT_ID.ToString();
                    flag.Text = "Edit";
                }

                var cmdFunc = new ZoneDetailService();
                DataSouceRoleFunction = cmdFunc.GetALLInclude(_employee.EMPLOYEE_ID);
            }
            else
            {
                DataSouceRoleFunction = new List<ZONE_DETAIL>();
            }
            DataSouceRoleFunction.AddRange(DataSouceNewRoleFunction);

            gridZone.DataSource = DataSouceRoleFunction;
            gridZone.DataBind();
        }

        private void InitialDataPopupZone()
        {
            var cmd = new ZoneService();
            gridSelectZone.DataSource = cmd.GetALL();
            gridSelectZone.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtEmployeeCode.Text.Equals(""))
            {
                gridEmployee.DataSource = DataSouce;
            }
            else
            {
                gridEmployee.DataSource = DataSouce.Where(x => x.EMPLOYEE_CODE.Contains(txtEmployeeCode.Text) && x.SYE_DEL == true).ToList();
            }
            gridEmployee.DataBind();
        }

        protected void gridEmployee_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            ViewState["empId"] = gridEmployee.DataKeys[e.NewEditIndex].Values[0].ToString();
            InitialDataPopup();
            this.popup.Show();
        }

        protected void gridEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridEmployee.PageIndex = e.NewPageIndex;
            gridEmployee.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            InitialDataPopup();
            this.popup.Show();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtEmployeeCode.Text = "";
            SearchGrid();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["empId"] = null;
            Response.Redirect("SearchEmployee.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var obj = new EMPLOYEE();
            obj.DEPARTMENT_ID = Convert.ToInt32(ddlDepartment.SelectedValue);
            obj.EMPLOYEE_CODE = popTxtEmployeeCode.Text;
            obj.EMPLOYEE_NAME = txtName.Text;
            obj.EMPLOYEE_SURNAME = txtLastName.Text;
            var cmd = new EmployeeService(obj);
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
                obj.EMPLOYEE_ID = Convert.ToInt32(ViewState["empId"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = 0;
                obj.SYE_DEL = true;
                cmd.Edit();
            }
            ViewState["empId"] = null;
            Response.Redirect("SearchEmployee.aspx");
        }

        protected void AddZone_Click(object sender, EventArgs e)
        {
            InitialDataPopupZone();
            this.popup2.Show();
        }

        protected void gridSelectZone_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridSelectZone.PageIndex = e.NewPageIndex;
            gridSelectZone.DataBind();
        }

        protected void btnAddZone_Click(object sender, EventArgs e)
        {
            var cmdZone = new ZoneService();
            List<ZONE_DETAIL> list = new List<ZONE_DETAIL>();

            for (int i = 0; i < gridSelectZone.Rows.Count; i++)
            {
                if (((CheckBox)gridSelectZone.Rows[i].Cells[0].FindControl("check")).Checked)
                {
                    if (ViewState["empId"] != null && DataSouceRoleFunction.Where(x => x.ZONE_ID == Convert.ToInt32(gridSelectZone.DataKeys[i].Value.ToString())).FirstOrDefault() == null)
                    {
                        ZONE_DETAIL obj = new ZONE_DETAIL();
                        obj.Action = ActionEnum.Create;
                        obj.EMPLOYEE_ID = Convert.ToInt32(ViewState["empId"].ToString());
                        obj.ZONE_ID = Convert.ToInt32(gridSelectZone.DataKeys[i].Value.ToString());
                        obj.CREATE_DATE = DateTime.Now;
                        obj.CREATE_EMPLOYEE_ID = 0;
                        obj.UPDATE_DATE = DateTime.Now;
                        obj.UPDATE_EMPLOYEE_ID = 0;
                        obj.SYE_DEL = true;
                        list.Add(obj);
                    }
                    else if (DataSouceNewRoleFunction.Where(x => x.ZONE_ID == Convert.ToInt32(gridSelectZone.DataKeys[i].Value.ToString())).FirstOrDefault() == null)
                    {
                        ZONE_DETAIL obj = new ZONE_DETAIL();
                        obj.Action = ActionEnum.Create;
                        obj.EMPLOYEE_ID = 0;
                        obj.ZONE_ID = Convert.ToInt32(gridSelectZone.DataKeys[i].Value.ToString());
                        obj.CREATE_DATE = DateTime.Now;
                        obj.CREATE_EMPLOYEE_ID = 0;
                        obj.UPDATE_DATE = DateTime.Now;
                        obj.UPDATE_EMPLOYEE_ID = 0;
                        obj.SYE_DEL = true;
                        DataSouceNewRoleFunction.Add(obj);
                    }
                }
            }

            if (list.Count > 0)
            {
                var cmd = new ZoneDetailService(list);
                cmd.AddList();
            }

            InitialDataPopup();
            this.popup.Show();
        }

        protected void btnCancelZone_Click(object sender, EventArgs e)
        {
            InitialDataPopup();
            this.popup.Show();
        }

        protected void gridZone_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var cmd = new ZoneDetailService();
            cmd.Delete(Convert.ToInt32(gridZone.DataKeys[e.RowIndex].Values[0].ToString()));
            InitialDataPopup();
            this.popup.Show();
        }
    }
}