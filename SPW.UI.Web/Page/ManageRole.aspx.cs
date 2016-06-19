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
    public partial class ManageRole : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private RoleService cmdRoleService;
        private RoleFunctionService cmdRoleFunctionService;
        private FunctionService cmdFunctionService;

        private ROLE _item;
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
            cmdRoleService = (RoleService)_dataServiceEngine.GetDataService(typeof(RoleService));
            cmdRoleFunctionService = (RoleFunctionService)_dataServiceEngine.GetDataService(typeof(RoleFunctionService));
            cmdFunctionService = (FunctionService)_dataServiceEngine.GetDataService(typeof(FunctionService));
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

        private void PrepareObjectScreen()
        {
            if (Request.QueryString["id"] != null)
            {
                _item = cmdRoleService.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
                if (_item != null)
                {
                    popTxtRoleCode.Text = _item.ROLE_CODE;
                    popTxtRoleName.Text = _item.ROLE_NAME;
                    flag.Text = "Edit";
                    lblName.Text = popTxtRoleName.Text;
                }
                DataSouceRoleFunction = cmdRoleFunctionService.GetAllIncludeFunction(_item.ROLE_ID);
            }
            else
            {
                DataSouceRoleFunction = new List<ROLE_FUNCTION>();
            }
            DataSouceRoleFunction.AddRange(DataSouceNewRoleFunction);

            //gridFunction.DataSource = DataSouceRoleFunction;
            //gridFunction.DataBind();

            InitialDataPopupFunction();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            USER userItem = Session["user"] as USER;
            var obj = new ROLE();
            obj.ROLE_CODE = popTxtRoleCode.Text;
            obj.ROLE_NAME = popTxtRoleName.Text;
            if (flag.Text.Equals("Add"))
            {
                obj.Action = ActionEnum.Create;
                obj.CREATE_DATE = DateTime.Now;
                obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                cmdRoleService.Add(obj);
            }
            else
            {
                obj.Action = ActionEnum.Update;
                obj.ROLE_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                cmdRoleService.Edit(obj);
            }
            if (DataSouceNewRoleFunction.Count > 0)
            {
                foreach (ROLE_FUNCTION item in DataSouceNewRoleFunction)
                {
                    item.ROLE_ID = obj.ROLE_ID;
                }
                cmdRoleFunctionService.AddList(DataSouceNewRoleFunction);
            }

            btnSave.Enabled = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            alert.Visible = true;
            Response.AppendHeader("Refresh", "2; url=SearchRole.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SearchRole.aspx");
        }

        private void InitialDataPopupFunction()
        {
            //gridSelectFunction.DataSource = cmdFunctionService.GetAll();
            //gridSelectFunction.DataBind();
        }

        protected void gridFunction_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //cmdRoleFunctionService.Delete(Convert.ToInt32(gridFunction.DataKeys[e.RowIndex].Values[0].ToString()));
            PrepareObjectScreen();
        }

        protected void gridSelectFunction_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gridSelectFunction.PageIndex = e.NewPageIndex;
            //gridSelectFunction.DataBind();
        }

        protected void btnAddFunction_Click(object sender, EventArgs e)
        {
            //USER userItem = Session["user"] as USER;
            //List<ROLE_FUNCTION> list = new List<ROLE_FUNCTION>();

            //for (int i = 0; i < gridSelectFunction.Rows.Count; i++)
            //{
            //    if (((CheckBox)gridSelectFunction.Rows[i].Cells[0].FindControl("check")).Checked)
            //    {
            //        if (Request.QueryString["id"] != null && DataSouceRoleFunction.Where(x => x.FUNCTION_ID == Convert.ToInt32(gridSelectFunction.DataKeys[i].Value.ToString())).FirstOrDefault() == null)
            //        {
            //            ROLE_FUNCTION obj = new ROLE_FUNCTION();
            //            obj.Action = ActionEnum.Create;
            //            obj.ROLE_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
            //            obj.FUNCTION_ID = Convert.ToInt32(gridSelectFunction.DataKeys[i].Value.ToString());
            //            obj.CREATE_DATE = DateTime.Now;
            //            obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
            //            obj.UPDATE_DATE = DateTime.Now;
            //            obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
            //            obj.SYE_DEL = false;
            //            list.Add(obj);
            //        }
            //        else if (DataSouceNewRoleFunction.Where(x => x.FUNCTION_ID == Convert.ToInt32(gridSelectFunction.DataKeys[i].Value.ToString())).FirstOrDefault() == null)
            //        {
            //            ROLE_FUNCTION obj = new ROLE_FUNCTION();
            //            obj.Action = ActionEnum.Create;
            //            obj.ROLE_ID = 0;
            //            obj.FUNCTION_ID = Convert.ToInt32(gridSelectFunction.DataKeys[i].Value.ToString());
            //            obj.CREATE_DATE = DateTime.Now;
            //            obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
            //            obj.UPDATE_DATE = DateTime.Now;
            //            obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
            //            obj.SYE_DEL = false;
            //            DataSouceNewRoleFunction.Add(obj);
            //        }
            //    }
            //}

            //if (list.Count > 0)
            //{
            //    cmdRoleFunctionService.AddList(list);
            //}

            //PrepareObjectScreen();
        }

        protected void btnCancelFunction_Click(object sender, EventArgs e)
        {
            PrepareObjectScreen();
        }

        protected void gridFunction_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (ImageButton button in e.Row.Cells[0].Controls.OfType<ImageButton>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('ต้องการจะลบข้อมูลใช่หรือไม่')){ return false; };";
                    }
                }
            }
        }
    }
}