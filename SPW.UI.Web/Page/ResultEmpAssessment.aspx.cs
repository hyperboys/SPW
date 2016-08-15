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
    public partial class ResultEmpAssessment : System.Web.UI.Page
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
        private EmpGradeService cmdEmpGradeService;

        public List<EMP_MEASURE_TRANS> DataSouce
        {
            get
            {
                if (ViewState["DataSouce"] == null)
                {
                    ViewState["DataSouce"] = new List<EMP_MEASURE_TRANS>();
                }
                var list = (List<EMP_MEASURE_TRANS>)ViewState["DataSouce"];
                return list;
            }
            set
            {
                ViewState["DataSouce"] = value;
            }
        }

        public List<EMP_MEASURE_TRANS> DataSouceResult
        {
            get
            {
                if (ViewState["DataSouceResult"] == null)
                {
                    ViewState["DataSouceResult"] = new List<EMP_MEASURE_TRANS>();
                }
                var list = (List<EMP_MEASURE_TRANS>)ViewState["DataSouceResult"];
                return list;
            }
            set
            {
                ViewState["DataSouceResult"] = value;
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
            cmdEmpGradeService = (EmpGradeService)_dataServiceEngine.GetDataService(typeof(EmpGradeService));
        }

        private void ReloadDatasource()
        {

        }

        private void PrepareObjectScreen()
        {
            List<EMP_MEASURE_TRANS> lstTmp = new List<EMP_MEASURE_TRANS>();
            List<string> lstYear = new List<string>();
            if (Request.QueryString["id"] != null)
            {
                DataEmp = cmdEmployeeService.SelectIncludeTrans(Convert.ToInt32(Request.QueryString["id"].ToString()));
                lblName.Text = DataEmp.EMPLOYEE_CODE;
                txtEmpCode.Text = DataEmp.EMPLOYEE_CODE;
                txtName.Text = DataEmp.EMPLOYEE_NAME + " " + DataEmp.EMPLOYEE_SURNAME;
                DataEmpHist = cmdEmpHistService.GetAll(DataEmp.EMPLOYEE_ID).OrderByDescending(x => x.EFF_DATE).FirstOrDefault();
                if (DataEmpHist.POSITION_ID != null)
                {
                    int PositionID = DataEmpHist.POSITION_ID.Value;
                    DataPosition = cmdEmpPositionService.Select(PositionID);
                    txtPosition.Text = DataPosition.POSITION_NAME;
                }
                DataSouce = DataEmp.EMP_MEASURE_TRANS != null ? DataEmp.EMP_MEASURE_TRANS.ToList() : null;
                if (DataSouce != null)
                {
                    EMP_MEASURE_TRANS tmp = new EMP_MEASURE_TRANS();
                    DataSouce = DataSouce.OrderBy(x => x.MEASURE_YY).ThenBy(y => y.MEASURE_SEQ_NO).ThenBy(z => z.EMP_SKILL_TYPE_ID).ToList();
                    string year = "";
                    int seq = 0;
                    decimal typePercen = 0;
                    foreach (EMP_MEASURE_TRANS item in DataSouce)
                    {
                        if (item.MEASURE_YY != year)
                        {
                            year = item.MEASURE_YY;
                            lstYear.Add(year);
                            seq = item.SEQ_NO;
                            typePercen = cmdEmpHdTemplateService.Select(item.TEMPLATE_ID).EMP_SKILL_TYPE_PERCENTAGE;
                            tmp = new EMP_MEASURE_TRANS();
                            tmp.MEASURE_YY = year;
                            tmp.YEAR_NAME = item.MEASURE_SEQ_NO == 1 ? "ต้นปี" : "ปลายปี";
                            tmp.SUM_TARGET += cmdEmpDtTemplateService.Select(item.TEMPLATE_ID, item.SEQ_NO).SKILL_TARGET_SCORE;
                            tmp.SUM_SCORE += item.SCORE_ACTUAL;
                            tmp.TYPE_PERCEN = typePercen;
                            tmp.WEIGHT_ID = item.WEIGHT_ID;
                            lstTmp.Add(tmp);
                            continue;
                        }

                        if (item.MEASURE_YY == year && item.TYPE_PERCEN == typePercen && item.SEQ_NO == seq)
                        {
                            tmp.SUM_TARGET += cmdEmpDtTemplateService.Select(item.TEMPLATE_ID, item.SEQ_NO).SKILL_TARGET_SCORE;
                            tmp.SUM_SCORE += item.SCORE_ACTUAL;
                            continue;
                        }

                        if (item.SEQ_NO != seq)
                        {
                            year = item.MEASURE_YY;
                            seq = item.SEQ_NO;
                            typePercen = cmdEmpHdTemplateService.Select(item.TEMPLATE_ID).EMP_SKILL_TYPE_PERCENTAGE;
                            tmp = new EMP_MEASURE_TRANS();
                            tmp.MEASURE_YY = year;
                            tmp.YEAR_NAME = item.MEASURE_SEQ_NO == 1 ? "ต้นปี" : "ปลายปี";
                            tmp.SUM_TARGET += cmdEmpDtTemplateService.Select(item.TEMPLATE_ID, item.SEQ_NO).SKILL_TARGET_SCORE;
                            tmp.SUM_SCORE += item.SCORE_ACTUAL;
                            tmp.TYPE_PERCEN = typePercen;
                            tmp.WEIGHT_ID = item.WEIGHT_ID;
                            lstTmp.Add(tmp);
                            continue;
                        }

                        if (item.TYPE_PERCEN != typePercen)
                        {
                            year = item.MEASURE_YY;
                            seq = item.SEQ_NO;
                            typePercen = cmdEmpHdTemplateService.Select(item.TEMPLATE_ID).EMP_SKILL_TYPE_PERCENTAGE;
                            tmp = new EMP_MEASURE_TRANS();
                            tmp.MEASURE_YY = year;
                            tmp.YEAR_NAME = item.MEASURE_SEQ_NO == 1 ? "ต้นปี" : "ปลายปี";
                            tmp.SUM_TARGET += cmdEmpDtTemplateService.Select(item.TEMPLATE_ID, item.SEQ_NO).SKILL_TARGET_SCORE;
                            tmp.SUM_SCORE += item.SCORE_ACTUAL;
                            tmp.TYPE_PERCEN = typePercen;
                            tmp.WEIGHT_ID = item.WEIGHT_ID;
                            lstTmp.Add(tmp);
                            continue;
                        }
                    }

                    EMP_MEASURE_TRANS tmpResult = new EMP_MEASURE_TRANS();

                    foreach (EMP_MEASURE_TRANS tmpItem in lstTmp)
                    {
                        tmpItem.SCORE_ACTUAL = (tmpItem.SUM_SCORE * tmpItem.TYPE_PERCEN) / tmpItem.SUM_TARGET;
                    }

                    foreach (string tmpYear in lstYear)
                    {
                        tmpResult = new EMP_MEASURE_TRANS();
                        tmpResult.MEASURE_YY = tmpYear;
                        decimal Score = 0;
                        foreach (EMP_MEASURE_TRANS item in lstTmp.Where(x => x.MEASURE_YY == tmpYear && x.WEIGHT_ID == 1).ToList())
                        {
                            Score += item.SCORE_ACTUAL;
                            tmpResult.YEAR_NAME = item.MEASURE_SEQ_NO == 1 ? "ต้นปี" : "ปลายปี";

                        }
                        EMP_MEASURE_WEIGHT weightItem = cmdEmpMeasureWeightService.Select(1);
                        tmpResult.POINT += (Score * weightItem.WEIGHT_VALUE) / 100;
                        Score = 0;
                        foreach (EMP_MEASURE_TRANS item in lstTmp.Where(x => x.MEASURE_YY == tmpYear && x.WEIGHT_ID == 2).ToList())
                        {
                            Score += item.SCORE_ACTUAL;
                            tmpResult.YEAR_NAME = item.MEASURE_SEQ_NO == 1 ? "ต้นปี" : "ปลายปี";
                        }
                        weightItem = cmdEmpMeasureWeightService.Select(2);
                        tmpResult.POINT += (Score * weightItem.WEIGHT_VALUE) / 100;
                        DataSouceResult.Add(tmpResult);
                    }
                }
            }
            foreach (EMP_MEASURE_TRANS itemResult in DataSouceResult)
            {
                List<EMP_GRADE_SET> items = cmdEmpGradeService.GetAllTop5();
                if (items != null && items.Count() >= 5)
                {
                    if (itemResult.POINT >= items[0].GRADE_SET_PERCENTAGE)
                    {
                        itemResult.GRADE = "A";
                    }
                    else if (itemResult.POINT >= items[1].GRADE_SET_PERCENTAGE)
                    {
                        itemResult.GRADE = "B";
                    }
                    else if (itemResult.POINT >= items[2].GRADE_SET_PERCENTAGE)
                    {
                        itemResult.GRADE = "C";
                    }
                    else if (itemResult.POINT >= items[3].GRADE_SET_PERCENTAGE)
                    {
                        itemResult.GRADE = "D";
                    }
                    else
                    {
                        itemResult.GRADE = "E";
                    }
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
            grdResult.DataSource = DataSouceResult;
            grdResult.DataBind();
        }
    }
}