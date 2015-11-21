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
    public partial class ManageDepartment : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private DepartmentService cmdDepartmentService;
        private DEPARTMENT _department;

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
        }

        private void ReloadDatasource()
        {
            //DataSouce = cmdDepartmentService.GetAll();
        }

        private void PrepareObjectScreen()
        {
            if (Request.QueryString["id"] != null)
            {
                _department = cmdDepartmentService.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
                if (_department != null)
                {
                    popTxtDepartmentCode.Text = _department.DEPARTMENT_CODE;
                    txtDepartmentName.Text = _department.DEPARTMENT_NAME;
                    flag.Text = "Edit";
                    lblName.Text = _department.DEPARTMENT_NAME;
                }
            }
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            USER userItem = Session["user"] as USER;
            var obj = new DEPARTMENT();
            obj.DEPARTMENT_CODE = popTxtDepartmentCode.Text;
            obj.DEPARTMENT_NAME = txtDepartmentName.Text;
            if (flag.Text.Equals("Add"))
            {
                obj.Action = ActionEnum.Create;
                obj.CREATE_DATE = DateTime.Now;
                obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                cmdDepartmentService.Add(obj);
            }
            else
            {
                obj.Action = ActionEnum.Update;
                obj.DEPARTMENT_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                cmdDepartmentService.Edit(obj);
            }
            alert.Visible = true;
            Response.AppendHeader("Refresh", "2; url=SearchDepartment.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SearchDepartment.aspx");
        }
    }
}