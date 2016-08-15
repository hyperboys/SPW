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
    public partial class KeyInEmpAssessment : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private EmpHdTemplateService cmdEmpHdTemplateService;
        private EmpDtTemplateService cmdEmpDtTemplateService;
        private DepartmentService cmdDepartmentService;
        private EmpSkillTypeService cmdEmpSkillTypeService;
        private EmpSkillService cmdEmpSkillService;
        private EmployeeService cmdEmployeeService;
        private EmpHistService cmdEmpHistService;
        private EmpPositionService cmdEmpPositionService;
        private UserService cmdUser;
        private RoleService cmdRole;
        private EmpMeasureWeightService cmdEmpMeasureWeightService;
        private EmpMeasureTransService cmdEmpMeasureTransService;

        public List<EMP_MEASURE_DT_TEMPLATE> DataSouceDetailCore
        {
            get
            {
                if (ViewState["DataSouceDetailCore"] == null)
                {
                    ViewState["DataSouceDetailCore"] = new List<EMP_MEASURE_DT_TEMPLATE>();
                }
                var list = (List<EMP_MEASURE_DT_TEMPLATE>)ViewState["DataSouceDetailCore"];
                return list;
            }
            set
            {
                ViewState["DataSouceDetailCore"] = value;
            }
        }

        public List<EMP_MEASURE_DT_TEMPLATE> DataSouceDetailJob
        {
            get
            {
                if (ViewState["DataSouceDetailJob"] == null)
                {
                    ViewState["DataSouceDetailJob"] = new List<EMP_MEASURE_DT_TEMPLATE>();
                }
                var list = (List<EMP_MEASURE_DT_TEMPLATE>)ViewState["DataSouceDetailJob"];
                return list;
            }
            set
            {
                ViewState["DataSouceDetailJob"] = value;
            }
        }

        public EMP_POSITION DataPosition
        {
            get
            {
                if (ViewState["EMP_POSITION"] == null)
                {
                    ViewState["EMP_POSITION"] = new EMP_POSITION();
                }
                var list = (EMP_POSITION)ViewState["EMP_POSITION"];
                return list;
            }
            set
            {
                ViewState["EMP_POSITION"] = value;
            }
        }

        public EMPLOYEE DataEmp
        {
            get
            {
                if (ViewState["EMPLOYEE"] == null)
                {
                    ViewState["EMPLOYEE"] = new EMPLOYEE();
                }
                var list = (EMPLOYEE)ViewState["EMPLOYEE"];
                return list;
            }
            set
            {
                ViewState["EMPLOYEE"] = value;
            }
        }

        public EMPLOYEE_HIST DataEmpHist
        {
            get
            {
                if (ViewState["EMPLOYEE_HIST"] == null)
                {
                    ViewState["EMPLOYEE_HIST"] = new EMPLOYEE_HIST();
                }
                var list = (EMPLOYEE_HIST)ViewState["EMPLOYEE_HIST"];
                return list;
            }
            set
            {
                ViewState["EMPLOYEE_HIST"] = value;
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

            cmdEmpHdTemplateService = (EmpHdTemplateService)_dataServiceEngine.GetDataService(typeof(EmpHdTemplateService));
            cmdDepartmentService = (DepartmentService)_dataServiceEngine.GetDataService(typeof(DepartmentService));
            cmdEmpSkillTypeService = (EmpSkillTypeService)_dataServiceEngine.GetDataService(typeof(EmpSkillTypeService));
            cmdEmpSkillService = (EmpSkillService)_dataServiceEngine.GetDataService(typeof(EmpSkillService));
            cmdEmpDtTemplateService = (EmpDtTemplateService)_dataServiceEngine.GetDataService(typeof(EmpDtTemplateService));
            cmdEmployeeService = (EmployeeService)_dataServiceEngine.GetDataService(typeof(EmployeeService));
            cmdEmpHistService = (EmpHistService)_dataServiceEngine.GetDataService(typeof(EmpHistService));
            cmdEmpPositionService = (EmpPositionService)_dataServiceEngine.GetDataService(typeof(EmpPositionService));
            cmdUser = (UserService)_dataServiceEngine.GetDataService(typeof(UserService));
            cmdRole = (RoleService)_dataServiceEngine.GetDataService(typeof(RoleService));
            cmdEmpMeasureWeightService = (EmpMeasureWeightService)_dataServiceEngine.GetDataService(typeof(EmpMeasureWeightService));
            cmdEmpMeasureTransService = (EmpMeasureTransService)_dataServiceEngine.GetDataService(typeof(EmpMeasureTransService));
        }

        private void ReloadDatasource()
        {

        }

        private void PrepareObjectScreen()
        {
            if (Request.QueryString["id"] != null)
            {
                DataEmp = cmdEmployeeService.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
                lblName.Text = DataEmp.EMPLOYEE_CODE;
                txtEmpCode.Text = DataEmp.EMPLOYEE_CODE;
                txtName.Text = DataEmp.EMPLOYEE_NAME + " " + DataEmp.EMPLOYEE_SURNAME;
                DataEmpHist = cmdEmpHistService.GetAll(DataEmp.EMPLOYEE_ID).OrderByDescending(x => x.EFF_DATE).FirstOrDefault();
                int PositionID = DataEmpHist.POSITION_ID.Value;
                DataPosition = cmdEmpPositionService.Select(PositionID);
                txtPosition.Text = DataPosition.POSITION_NAME;

                List<EMP_MEASURE_HD_TEMPLATE> itemCore = cmdEmpHdTemplateService.GetAll(DataEmpHist.DEPARTMENT_ID.Value, 1);
                foreach (EMP_MEASURE_HD_TEMPLATE core in itemCore)
                {
                    DataSouceDetailCore.AddRange(cmdEmpDtTemplateService.GetList(core.TEMPLATE_ID, 1));
                }

                List<EMP_MEASURE_HD_TEMPLATE> itemJob = cmdEmpHdTemplateService.GetAll(DataEmpHist.DEPARTMENT_ID.Value, 2);
                foreach (EMP_MEASURE_HD_TEMPLATE job in itemJob)
                {
                    DataSouceDetailJob.AddRange(cmdEmpDtTemplateService.GetList(job.TEMPLATE_ID, 2));
                }

                foreach (EMP_MEASURE_DT_TEMPLATE tmp in DataSouceDetailCore)
                {
                    tmp.SKILL_NAME = cmdEmpSkillService.Select(tmp.SKILL_ID).SKILL_NAME;
                    tmp.SKILL_TYPE_NAME = cmdEmpSkillTypeService.Select(tmp.EMP_SKILL_TYPE_ID).EMP_SKILL_TYPE_NA;
                }

                foreach (EMP_MEASURE_DT_TEMPLATE tmp in DataSouceDetailJob)
                {
                    tmp.SKILL_NAME = cmdEmpSkillService.Select(tmp.SKILL_ID).SKILL_NAME;
                    tmp.SKILL_TYPE_NAME = cmdEmpSkillTypeService.Select(tmp.EMP_SKILL_TYPE_ID).EMP_SKILL_TYPE_NA;
                }
            }
            BindData();
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

        private void BindData()
        {
            grdCore.DataSource = DataSouceDetailCore;
            grdCore.DataBind();
            grdJob.DataSource = DataSouceDetailJob;
            grdJob.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            USER userItem = Session["user"] as USER;
            EMP_MEASURE_HD_TEMPLATE itemCore = cmdEmpHdTemplateService.Select(DataEmpHist.DEPARTMENT_ID.Value, 1);
            EMP_MEASURE_HD_TEMPLATE itemJob = cmdEmpHdTemplateService.Select(DataEmpHist.DEPARTMENT_ID.Value, 2);
            EMP_MEASURE_TRANS item = new EMP_MEASURE_TRANS();
            EMPLOYEE_HIST userHist = cmdEmpHistService.GetAll(userItem.EMPLOYEE_ID).OrderByDescending(x => x.EFF_DATE).FirstOrDefault();
            userHist.POSITION_NAME = cmdEmpPositionService.Select(userHist.POSITION_ID.Value).POSITION_NAME;
            EMP_MEASURE_WEIGHT weightItem = cmdEmpMeasureWeightService.Select(userHist.POSITION_NAME);

            foreach (GridViewRow row in grdCore.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    item = cmdEmpMeasureTransService.Select(weightItem.WEIGHT_ID, grdCore.DataKeys[row.RowIndex][0].ToString(), DataEmp.EMPLOYEE_ID,
                         DateTime.Now.Year.ToString(), DateTime.Now.Month < 7 ? 1 : 2, 1);
                    if (item == null)
                    {
                        item = new EMP_MEASURE_TRANS();
                        item.CREATE_DATE = DateTime.Now;
                        item.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        item.EMP_SKILL_TYPE_ACTUAL = 0;
                        item.EMP_SKILL_TYPE_DEFAULT = itemCore.EMP_SKILL_TYPE_PERCENTAGE;
                        item.EMP_SKILL_TYPE_ID = 1;
                        item.EMPLOYEE_ID = DataEmp.EMPLOYEE_ID;
                        item.EMPLOYEE_CODE = DataEmp.EMPLOYEE_CODE;
                        item.MEASURE_DATE = DateTime.Now;
                        item.MEASURE_SEQ_NO = DateTime.Now.Month < 7 ? 1 : 2;
                        item.MEASURE_YY = DateTime.Now.Year.ToString();
                        item.SCORE_ACTUAL = Convert.ToDecimal(((TextBox)grdCore.Rows[row.RowIndex].Cells[4].FindControl("txtPoint")).Text);
                        item.SYE_DEL = false;
                        item.TEMPLATE_ID = grdCore.DataKeys[row.RowIndex][0].ToString();
                        item.UPDATE_DATE = DateTime.Now;
                        item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        item.WEIGHT_ID = weightItem.WEIGHT_ID;
                        item.WEIGHT_VALUE = weightItem.WEIGHT_VALUE;
                        item.SEQ_NO = cmdEmpMeasureTransService.GetCount(item.TEMPLATE_ID, item.EMPLOYEE_ID, item.MEASURE_YY, item.MEASURE_SEQ_NO, item.EMP_SKILL_TYPE_ID) + 1;
                        cmdEmpMeasureTransService.Add(item);
                    }
                    else
                    {
                        item.EMP_SKILL_TYPE_ACTUAL = 0;
                        item.EMP_SKILL_TYPE_DEFAULT = itemCore.EMP_SKILL_TYPE_PERCENTAGE;
                        item.SCORE_ACTUAL = Convert.ToDecimal(((TextBox)grdCore.Rows[row.RowIndex].Cells[4].FindControl("txtPoint")).Text);
                        item.UPDATE_DATE = DateTime.Now;
                        item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        item.MEASURE_DATE = DateTime.Now;
                        cmdEmpMeasureTransService.Edit(item);
                    }
                }
            }

            foreach (GridViewRow row in grdJob.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    item = cmdEmpMeasureTransService.Select(weightItem.WEIGHT_ID, grdJob.DataKeys[row.RowIndex][0].ToString(), DataEmp.EMPLOYEE_ID,
                        DateTime.Now.Year.ToString(), DateTime.Now.Month < 7 ? 1 : 2, 2);
                    if (item == null)
                    {
                        item = new EMP_MEASURE_TRANS();
                        item.CREATE_DATE = DateTime.Now;
                        item.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        item.EMP_SKILL_TYPE_ACTUAL = 0;
                        item.EMP_SKILL_TYPE_DEFAULT = itemJob.EMP_SKILL_TYPE_PERCENTAGE;
                        item.EMP_SKILL_TYPE_ID = 2;
                        item.EMPLOYEE_ID = DataEmp.EMPLOYEE_ID;
                        item.EMPLOYEE_CODE = DataEmp.EMPLOYEE_CODE;
                        item.MEASURE_DATE = DateTime.Now;
                        item.MEASURE_SEQ_NO = DateTime.Now.Month < 7 ? 1 : 2;
                        item.MEASURE_YY = DateTime.Now.Year.ToString();
                        item.SCORE_ACTUAL = Convert.ToDecimal(((TextBox)grdJob.Rows[row.RowIndex].Cells[4].FindControl("txtPoint")).Text);
                        item.SYE_DEL = false;
                        item.TEMPLATE_ID = grdJob.DataKeys[row.RowIndex][0].ToString();
                        item.UPDATE_DATE = DateTime.Now;
                        item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        item.WEIGHT_ID = weightItem.WEIGHT_ID;
                        item.WEIGHT_VALUE = weightItem.WEIGHT_VALUE;
                        item.SEQ_NO = cmdEmpMeasureTransService.GetCount(item.TEMPLATE_ID, item.EMPLOYEE_ID, item.MEASURE_YY, item.MEASURE_SEQ_NO, item.EMP_SKILL_TYPE_ID) + 1;
                        cmdEmpMeasureTransService.Add(item);
                    }
                    else
                    {
                        item.EMP_SKILL_TYPE_ACTUAL = 0;
                        item.EMP_SKILL_TYPE_DEFAULT = itemJob.EMP_SKILL_TYPE_PERCENTAGE;
                        item.SCORE_ACTUAL = Convert.ToDecimal(((TextBox)grdJob.Rows[row.RowIndex].Cells[4].FindControl("txtPoint")).Text);
                        item.UPDATE_DATE = DateTime.Now;
                        item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        item.MEASURE_DATE = DateTime.Now;
                        cmdEmpMeasureTransService.Edit(item);
                    }
                }
            }

            btnSave.Enabled = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            alert.Visible = true;
            Response.AppendHeader("Refresh", "2; url=SearchEmployeeAssessment.aspx");
        }
    }
}