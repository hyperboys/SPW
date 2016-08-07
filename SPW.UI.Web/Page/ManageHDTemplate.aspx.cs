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
    public partial class ManageHDTemplate : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private EmpHdTemplateService cmdEmpHdTemplateService;
        private DepartmentService cmdDepartmentService;
        private EmpSkillTypeService cmdEmpSkillTypeService;

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
            cmdEmpHdTemplateService = (EmpHdTemplateService)_dataServiceEngine.GetDataService(typeof(EmpHdTemplateService));
            cmdDepartmentService = (DepartmentService)_dataServiceEngine.GetDataService(typeof(DepartmentService));
            cmdEmpSkillTypeService = (EmpSkillTypeService)_dataServiceEngine.GetDataService(typeof(EmpSkillTypeService));
        }

        private void ReloadDatasource()
        {

        }

        private void PrepareObjectScreen()
        {
            var list = cmdDepartmentService.GetAll();
            foreach (var item in list)
            {
                ddlDepartment.Items.Add(new ListItem(item.DEPARTMENT_NAME, item.DEPARTMENT_ID.ToString()));
            }

            var listSkill = cmdEmpSkillTypeService.GetAll();
            foreach (var item in listSkill)
            {
                ddlSkillType.Items.Add(new ListItem(item.EMP_SKILL_TYPE_NA, item.EMP_SKILL_TYPE_ID.ToString()));
            }

            if (Request.QueryString["id"] != null)
            {
                EMP_MEASURE_HD_TEMPLATE item = cmdEmpHdTemplateService.Select(Request.QueryString["id"].ToString());
                if (item != null)
                {
                    ddlDepartment.SelectedValue = item.DEPARTMENT.DEPARTMENT_ID.ToString();
                    ddlDepartment.Enabled = false;
                    lblName.Text = item.TEMPLATE_ID;
                    txtPercen.Text = item.EMP_SKILL_TYPE_PERCENTAGE.ToString();
                    ddlSkillType.SelectedValue = item.EMP_SKILL_TYPE.EMP_SKILL_TYPE_ID.ToString();
                    ddlSkillType.Enabled = false;
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
            EMP_MEASURE_HD_TEMPLATE item = new EMP_MEASURE_HD_TEMPLATE();
            item.DEPARTMENT_ID = Convert.ToInt32(ddlDepartment.SelectedValue);
            item.EMP_SKILL_TYPE_ID = Convert.ToInt32(ddlSkillType.SelectedValue);
            item.EMP_SKILL_TYPE_PERCENTAGE = Convert.ToDecimal(txtPercen.Text);
            string tmpId = "TMP-" + item.DEPARTMENT_ID + "-";
            item.TEMPLATE_ID = tmpId + (cmdEmpHdTemplateService.GetCount(tmpId) + 1).ToString("D6");
            if (Request.QueryString["id"] == null)
            {
                item.CREATE_DATE = DateTime.Now;
                item.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                item.UPDATE_DATE = DateTime.Now;
                item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                item.SYE_DEL = false;
                cmdEmpHdTemplateService.Add(item);
            }
            else
            {
                item.TEMPLATE_ID = Request.QueryString["id"].ToString();
                item.UPDATE_DATE = DateTime.Now;
                item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                item.SYE_DEL = false;
                cmdEmpHdTemplateService.Edit(item);
            }

            btnSave.Enabled = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            alert.Visible = true;
            Response.AppendHeader("Refresh", "2; url=SearchEmpHdTemplate.aspx");
        }
    }
}