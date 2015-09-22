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
        private DEPARTMENT _department;
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

        private void BlindGrid()
        {
            gridDepartment.DataSource = DataSouce;
            gridDepartment.DataBind();
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
            var cmd = new DepartmentService();
            DataSouce = cmd.GetALL();
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
            ViewState["depId"] = gridDepartment.DataKeys[e.NewEditIndex].Values[0].ToString();
            InitialDataPopup();
            this.popup.Show();
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
            ViewState["depId"] = null;
            InitialDataPopup();
            this.popup.Show();
        }

        private void InitialDataPopup()
        {
            if (ViewState["depId"] != null)
            {
                var cmd = new DepartmentService();
                _department = cmd.Select(Convert.ToInt32(ViewState["depId"].ToString()));
                if (_department != null)
                {
                    popTxtDepartmentCode.Text = _department.DEPARTMENT_CODE;
                    txtDepartmentName.Text = _department.DEPARTMENT_NAME;
                    flag.Text = "Edit";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var obj = new DEPARTMENT();
            obj.DEPARTMENT_CODE = popTxtDepartmentCode.Text;
            obj.DEPARTMENT_NAME = txtDepartmentName.Text;
            var cmd = new DepartmentService(obj);
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
                obj.DEPARTMENT_ID = Convert.ToInt32(ViewState["depId"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = 0;
                obj.SYE_DEL = true;
                cmd.Edit();
            }
            ViewState["depId"] = null;
            Response.Redirect("SearchDepartment.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["depId"] = null;
            Response.Redirect("SearchDepartment.aspx");
        }
    }
}