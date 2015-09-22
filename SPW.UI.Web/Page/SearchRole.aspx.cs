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
    public partial class SearchRole : System.Web.UI.Page
    {
        private ROLE _item;
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

        public List<FUNCTION> DataSouceFunction
        {
            get
            {
                var list = (List<FUNCTION>)ViewState["listFunction"];
                return list;
            }
            set
            {
                ViewState["listFunction"] = value;
            }
        }

        public List<ROLE_FUNCTION> DataSouceRoleFunction
        {
            get
            {
                var list = (List<ROLE_FUNCTION>)ViewState["listRoleFunction"];
                return list;
            }
            set
            {
                ViewState["listRoleFunction"] = value;
            }
        }

        public List<ROLE_FUNCTION> DataSouceNewRoleFunction
        {
            get
            {
                var list = (List<ROLE_FUNCTION>)ViewState["listNewRoleFunction"];
                return list;
            }
            set
            {
                ViewState["listNewRoleFunction"] = value;
            }
        }

        private void BlindGrid()
        {
            gridRole.DataSource = DataSouce;
            gridRole.DataBind();
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
            var cmd = new RoleService();
            DataSouce = cmd.GetALL();
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
                gridRole.DataSource = DataSouce.Where(x => x.ROLE_CODE.Contains(txtRoleCode.Text)).ToList();
            }
            gridRole.DataBind();
        }

        protected void gridDepartment_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            ViewState["RoleId"] = gridRole.DataKeys[e.NewEditIndex].Values[0].ToString();
            InitialDataPopup();
            this.popup.Show();
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
            ViewState["RoleId"] = null;
            InitialDataPopup();
            this.popup.Show();
        }

        private void InitialDataPopup()
        {
            if (ViewState["RoleId"] != null)
            {
                var cmd = new RoleService();
                _item = cmd.Select(Convert.ToInt32(ViewState["RoleId"].ToString()));
                if (_item != null)
                {
                    popTxtRoleCode.Text = _item.ROLE_CODE;
                    popTxtRoleName.Text = _item.ROLE_NAME;
                    flag.Text = "Edit";
                }

                var cmdFunc = new RoleFunctionService();
                DataSouceRoleFunction = cmdFunc.GetALLIncludeFunction(_item.ROLE_ID);
            }
            else 
            {
                DataSouceRoleFunction = new List<ROLE_FUNCTION>();
            }
            DataSouceRoleFunction.AddRange(DataSouceNewRoleFunction);

            gridFunction.DataSource = DataSouceRoleFunction;
            gridFunction.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var obj = new ROLE();
            obj.ROLE_CODE = popTxtRoleCode.Text;
            obj.ROLE_NAME = popTxtRoleName.Text;
            var cmd = new RoleService(obj);
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
                obj.ROLE_ID = Convert.ToInt32(ViewState["RoleId"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = 0;
                obj.SYE_DEL = true;
                cmd.Edit();
            }
            if (DataSouceNewRoleFunction.Count > 0) 
            {
                foreach (ROLE_FUNCTION item in DataSouceNewRoleFunction) 
                {
                    item.ROLE_ID = obj.ROLE_ID;
                }
                var cmdRoleFunction = new RoleFunctionService(DataSouceNewRoleFunction);
                cmdRoleFunction.AddList();
            }
            ViewState["RoleId"] = null;
            Response.Redirect("SearchRole.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["RoleId"] = null;
            Response.Redirect("SearchRole.aspx");
        }

        private void InitialDataPopupFunction()
        {
            var cmd = new FunctionService();
            gridSelectFunction.DataSource = cmd.GetALL();
            gridSelectFunction.DataBind();
        }

        protected void AddFunction_Click(object sender, EventArgs e)
        {
            InitialDataPopupFunction();
            this.popup2.Show();
        }

        protected void gridFunction_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var cmd = new RoleFunctionService();
            cmd.Delete(Convert.ToInt32(gridFunction.DataKeys[e.RowIndex].Values[0].ToString()));
            InitialDataPopup();
            this.popup.Show();
        }

        protected void gridSelectFunction_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridSelectFunction.PageIndex = e.NewPageIndex;
            gridSelectFunction.DataBind();
        }

        protected void btnAddFunction_Click(object sender, EventArgs e)
        {
            var cmdFunc = new FunctionService();
            List<ROLE_FUNCTION> list = new List<ROLE_FUNCTION>();

            for (int i = 0; i < gridSelectFunction.Rows.Count; i++)
            {
                if (((CheckBox)gridSelectFunction.Rows[i].Cells[0].FindControl("check")).Checked)
                {
                    if (ViewState["RoleId"] != null && DataSouceRoleFunction.Where(x => x.FUNCTION_ID == Convert.ToInt32(gridSelectFunction.DataKeys[i].Value.ToString())).FirstOrDefault() == null)
                    {
                        ROLE_FUNCTION obj = new ROLE_FUNCTION();
                        obj.Action = ActionEnum.Create;
                        obj.ROLE_ID = Convert.ToInt32(ViewState["RoleId"].ToString());
                        obj.FUNCTION_ID = Convert.ToInt32(gridSelectFunction.DataKeys[i].Value.ToString());
                        obj.CREATE_DATE = DateTime.Now;
                        obj.CREATE_EMPLOYEE_ID = 0;
                        obj.UPDATE_DATE = DateTime.Now;
                        obj.UPDATE_EMPLOYEE_ID = 0;
                        obj.SYE_DEL = true;
                        list.Add(obj);
                    }
                    else if (DataSouceNewRoleFunction.Where(x => x.FUNCTION_ID == Convert.ToInt32(gridSelectFunction.DataKeys[i].Value.ToString())).FirstOrDefault() == null)
                    {
                        ROLE_FUNCTION obj = new ROLE_FUNCTION();
                        obj.Action = ActionEnum.Create;
                        obj.ROLE_ID = 0;
                        obj.FUNCTION_ID = Convert.ToInt32(gridSelectFunction.DataKeys[i].Value.ToString());
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
                var cmd = new RoleFunctionService(list);
                cmd.AddList();
            }

            InitialDataPopup();
            this.popup.Show();
        }

        protected void btnCancelFunction_Click(object sender, EventArgs e)
        {
            InitialDataPopup();
            this.popup.Show();
        }
    }
}