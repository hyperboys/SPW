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
    public partial class ManageEmployee : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private DepartmentService cmdDepartmentService;
        private EmployeeService cmdEmployeeService;
        private ZoneDetailService cmdZoneDetailService;
        private ZoneService cmdZoneService;

        private EMPLOYEE _employee;

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
                if (ViewState["DataSouceNewRoleFunction"] == null)
                {
                    ViewState["DataSouceNewRoleFunction"] = new List<ZONE_DETAIL>();
                }
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
            //ReloadDatasource();
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
            cmdDepartmentService = (DepartmentService)_dataServiceEngine.GetDataService(typeof(DepartmentService));
            cmdEmployeeService = (EmployeeService)_dataServiceEngine.GetDataService(typeof(EmployeeService));
            cmdZoneDetailService = (ZoneDetailService)_dataServiceEngine.GetDataService(typeof(ZoneDetailService));
            cmdZoneService = (ZoneService)_dataServiceEngine.GetDataService(typeof(ZoneService));
        }

        private void ReloadDatasource()
        {
            //DataSouce = _categoryService.GetAll();
        }

        private void PrepareObjectScreen()
        {
            var list = cmdDepartmentService.GetAll();
            foreach (var item in list)
            {
                ddlDepartment.Items.Add(new ListItem(item.DEPARTMENT_NAME, item.DEPARTMENT_ID.ToString()));
            }

            //if (ddlDepartment.SelectedItem.Text == "Sale")
            //{
            //    AddZone.Enabled = true;
            //    gridZone.Visible = true;
            //}
            //else
            //{
            //    AddZone.Enabled = false;
            //    gridZone.Visible = false;
            //}

            if (Request.QueryString["id"] != null)
            {
                _employee = cmdEmployeeService.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
                if (_employee != null)
                {
                    popTxtEmployeeCode.Text = _employee.EMPLOYEE_CODE;
                    txtName.Text = _employee.EMPLOYEE_NAME;
                    txtLastName.Text = _employee.EMPLOYEE_SURNAME;
                    ddlDepartment.SelectedValue = _employee.DEPARTMENT_ID.ToString();
                    lblName.Text = _employee.EMPLOYEE_CODE;
                    flag.Text = "Edit";
                }

                //DataSouceRoleFunction = cmdZoneDetailService.GetAllInclude(_employee.EMPLOYEE_ID);
            }
            else
            {
                //DataSouceRoleFunction = new List<ZONE_DETAIL>();
            }

            //if (DataSouceNewRoleFunction.Count > 0) 
            //{
            //    DataSouceRoleFunction.AddRange(DataSouceNewRoleFunction);
            //}

            //gridZone.DataSource = DataSouceRoleFunction;
            //gridZone.DataBind();

            //InitialDataPopupZone();
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

        //private void InitialDataPopupZone()
        //{
        //    gridSelectZone.DataSource = cmdZoneService.GetAll();
        //    gridSelectZone.DataBind();
        //}

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SearchEmployee.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            USER userItem = Session["user"] as USER;
            var obj = new EMPLOYEE();
            obj.DEPARTMENT_ID = Convert.ToInt32(ddlDepartment.SelectedValue);
            obj.EMPLOYEE_CODE = popTxtEmployeeCode.Text;
            obj.EMPLOYEE_NAME = txtName.Text;
            obj.EMPLOYEE_SURNAME = txtLastName.Text;
            if (flag.Text.Equals("Add"))
            {
                obj.Action = ActionEnum.Create;
                obj.CREATE_DATE = DateTime.Now;
                obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                cmdEmployeeService.Add(obj);
            }
            else
            {
                obj.Action = ActionEnum.Update;
                obj.EMPLOYEE_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                cmdEmployeeService.Edit(obj);
            }
            alert.Visible = true;
            Response.AppendHeader("Refresh", "2; url=SearchEmployee.aspx");
        }

        //protected void gridSelectZone_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gridSelectZone.PageIndex = e.NewPageIndex;
        //    gridSelectZone.DataBind();
        //}

        protected void btnAddZone_Click(object sender, EventArgs e)
        {
            USER userItem = Session["user"] as USER;
            var cmdZoneService = new ZoneService();
            List<ZONE_DETAIL> list = new List<ZONE_DETAIL>();

            //for (int i = 0; i < gridSelectZone.Rows.Count; i++)
            //{
            //    if (((CheckBox)gridSelectZone.Rows[i].Cells[0].FindControl("check")).Checked)
            //    {
            //        if (Request.QueryString["id"] != null && DataSouceRoleFunction.Where(x => x.ZONE_ID == Convert.ToInt32(gridSelectZone.DataKeys[i].Value.ToString())).FirstOrDefault() == null)
            //        {
            //            ZONE_DETAIL obj = new ZONE_DETAIL();
            //            obj.Action = ActionEnum.Create;
            //            obj.EMPLOYEE_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
            //            obj.ZONE_ID = Convert.ToInt32(gridSelectZone.DataKeys[i].Value.ToString());
            //            obj.CREATE_DATE = DateTime.Now;
            //            obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
            //            obj.UPDATE_DATE = DateTime.Now;
            //            obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
            //            obj.SYE_DEL = false;
            //            list.Add(obj);
            //        }
            //        else if (DataSouceNewRoleFunction.Where(x => x.ZONE_ID == Convert.ToInt32(gridSelectZone.DataKeys[i].Value.ToString())).FirstOrDefault() == null)
            //        {
            //            ZONE_DETAIL obj = new ZONE_DETAIL();
            //            obj.Action = ActionEnum.Create;
            //            obj.EMPLOYEE_ID = 0;
            //            obj.ZONE_ID = Convert.ToInt32(gridSelectZone.DataKeys[i].Value.ToString());
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
            //    cmdZoneDetailService.AddList(list);
            //}

            PrepareObjectScreen();
        }

        //protected void gridZone_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    cmdZoneDetailService.Delete(Convert.ToInt32(gridZone.DataKeys[e.RowIndex].Values[0].ToString()));
        //    PrepareObjectScreen();
        //}

        //protected void gridZone_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        foreach (ImageButton button in e.Row.Cells[0].Controls.OfType<ImageButton>())
        //        {
        //            if (button.CommandName == "Delete")
        //            {
        //                button.Attributes["onclick"] = "if(!confirm('ต้องการจะลบข้อมูลใช่หรือไม่')){ return false; };";
        //            }
        //        }
        //    }
        //}

        //protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlDepartment.SelectedItem.Text == "Sale")
        //    {
        //        AddZone.Enabled = true;
        //        gridZone.Visible = true;
        //    }
        //    else 
        //    {
        //        AddZone.Enabled = false;
        //        gridZone.Visible = false;
        //    }
        //}
    }
}