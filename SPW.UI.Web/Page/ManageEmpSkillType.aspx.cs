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
    public partial class ManageEmpSkillType : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
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
            cmdEmpSkillTypeService = (EmpSkillTypeService)_dataServiceEngine.GetDataService(typeof(EmpSkillTypeService));
        }

        private void ReloadDatasource()
        {
            //DataSouce = cmdColorService.GetAll();
        }

        private void PrepareObjectScreen()
        {
            if (Request.QueryString["id"] != null)
            {
                EMP_SKILL_TYPE item = cmdEmpSkillTypeService.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
                if (item != null)
                {
                    txtPosition.Text = item.EMP_SKILL_TYPE_NA;
                    lblName.Text = item.EMP_SKILL_TYPE_NA;
                    txtPercen.Text = item.EMP_SKILL_TYPE_DEFAULT.ToString();
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
            EMP_SKILL_TYPE item = new EMP_SKILL_TYPE();
            item.EMP_SKILL_TYPE_NA = txtPosition.Text;
            item.EMP_SKILL_TYPE_DEFAULT = Convert.ToDecimal(txtPercen.Text);
            if (Request.QueryString["id"] == null)
            {
                item.CREATE_DATE = DateTime.Now;
                item.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                item.UPDATE_DATE = DateTime.Now;
                item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                item.SYE_DEL = false;
                cmdEmpSkillTypeService.Add(item);
            }
            else
            {
                item.EMP_SKILL_TYPE_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                item.UPDATE_DATE = DateTime.Now;
                item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                item.SYE_DEL = false;
                cmdEmpSkillTypeService.Edit(item);
            }

            btnSave.Enabled = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            alert.Visible = true;
            Response.AppendHeader("Refresh", "2; url=SearchEmpSkillType.aspx");
        }
    }
}