using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using SPW.Common;
using SPW.DAL;
using SPW.UI.Web.Reports;
using System.Data;

namespace SPW.UI.Web.Page
{
    public partial class ResultEmpListAssessment : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private DepartmentService cmdDepartmentService;
        private EmpHistService cmdEmpHistService;
        private EmpHdTemplateService cmdEmpHdTemplateService;
        private EmpDtTemplateService cmdEmpDtTemplateService;
        private EmpSkillTypeService cmdEmpSkillTypeService;
        private EmpSkillService cmdEmpSkillService;
        private EmployeeService cmdEmployeeService;
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

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CreatePageEngine();
                InitialData();
            }
            else
            {
                ReloadPageEngine();
            }
        }

        private void BlindGrid()
        {
            
        }

        private void InitialData()
        {
            var list = cmdDepartmentService.GetAll();
            foreach (var item in list)
            {
                ddlDepart.Items.Add(new ListItem(item.DEPARTMENT_NAME, item.DEPARTMENT_ID.ToString()));
            }
            ddlSeq.SelectedValue = DateTime.Now.Month < 7 ? "1" : "2";
            txtYear.Text = DateTime.Now.Year.ToString();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            Calculate();
            ResultEmpListAssessmentData ds = new ResultEmpListAssessmentData();
            DataTable RESULT = ds.Tables["RESULT"];
            for (int i = 0; i < DataSouceResult.Where(x => x.GRADE == "A").Count(); i++)
            {
                DataRow drRESULT_A = RESULT.NewRow();
                drRESULT_A["GRADE"] = "A";
                RESULT.Rows.Add(drRESULT_A);
            }

            for (int i = 0; i < DataSouceResult.Where(x => x.GRADE == "B").Count(); i++)
            {
                DataRow drRESULT_A = RESULT.NewRow();
                drRESULT_A["GRADE"] = "B";
                RESULT.Rows.Add(drRESULT_A);
            }

            for (int i = 0; i < DataSouceResult.Where(x => x.GRADE == "C").Count(); i++)
            {
                DataRow drRESULT_A = RESULT.NewRow();
                drRESULT_A["GRADE"] = "C";
                RESULT.Rows.Add(drRESULT_A);
            }

            for (int i = 0; i < DataSouceResult.Where(x => x.GRADE == "D").Count(); i++)
            {
                DataRow drRESULT_A = RESULT.NewRow();
                drRESULT_A["GRADE"] = "D";
                RESULT.Rows.Add(drRESULT_A);
            }

            for (int i = 0; i < DataSouceResult.Where(x => x.GRADE == "E").Count(); i++)
            {
                DataRow drRESULT_A = RESULT.NewRow();
                drRESULT_A["GRADE"] = "E";
                RESULT.Rows.Add(drRESULT_A);
            }

            Session["ResultEmpListAssessmentData"] = ds;
            Response.RedirectPermanent("../Reports/ResultEmpListAssessmentPageReport.aspx");
        }

        private void Calculate() 
        {
            List<EMP_MEASURE_TRANS> lstTmp = new List<EMP_MEASURE_TRANS>();
            List<string> lstYear = new List<string>();
            List<EMPLOYEE> listEmp = cmdEmployeeService.GetAll();
            foreach (EMPLOYEE empItem in listEmp)
            {
                DataEmp = cmdEmployeeService.SelectIncludeTrans(empItem.EMPLOYEE_ID);
                DataEmpHist = cmdEmpHistService.GetAll(DataEmp.EMPLOYEE_ID).OrderByDescending(x => x.EFF_DATE).FirstOrDefault();
                DataSouce = DataEmp.EMP_MEASURE_TRANS != null ? DataEmp.EMP_MEASURE_TRANS.ToList() : null;
                if (DataSouce != null)
                {
                    DebugLog.WriteLog("DataSouce != null");
                    EMP_MEASURE_TRANS tmp = new EMP_MEASURE_TRANS();
                    if (txtYear.Text != "")
                    {
                        DataSouce = DataSouce.Where(x => x.MEASURE_YY == txtYear.Text && x.MEASURE_SEQ_NO == Convert.ToInt32(ddlSeq.SelectedValue)).ToList();
                    }
                    else 
                    {
                        return;
                    }
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
                            //tmp.SUM_TARGET += cmdEmpDtTemplateService.Select(item.TEMPLATE_ID, item.SEQ_NO).SKILL_TARGET_SCORE;
                            tmp.SUM_TARGET = 100;
                            tmp.SUM_SCORE += item.SCORE_ACTUAL;
                            tmp.TYPE_PERCEN = typePercen;
                            tmp.WEIGHT_ID = item.WEIGHT_ID;
                            lstTmp.Add(tmp);
                            continue;
                        }

                        if (item.MEASURE_YY == year && item.TYPE_PERCEN == typePercen && item.SEQ_NO == seq)
                        {
                            //tmp.SUM_TARGET += cmdEmpDtTemplateService.Select(item.TEMPLATE_ID, item.SEQ_NO).SKILL_TARGET_SCORE;
                            tmp.SUM_TARGET = 100;
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
                            //tmp.SUM_TARGET += cmdEmpDtTemplateService.Select(item.TEMPLATE_ID, item.SEQ_NO).SKILL_TARGET_SCORE;
                            tmp.SUM_TARGET = 100;
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
                            //tmp.SUM_TARGET += cmdEmpDtTemplateService.Select(item.TEMPLATE_ID, item.SEQ_NO).SKILL_TARGET_SCORE;
                            tmp.SUM_TARGET = 100;
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
                        DebugLog.WriteLog("SCORE_ACTUAL = " + tmpItem.SUM_SCORE + "*" + tmpItem.TYPE_PERCEN + "//" + tmpItem.SUM_TARGET);
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
                        EMP_MEASURE_WEIGHT weightItem1 = cmdEmpMeasureWeightService.Select(1);
                        tmpResult.POINT += (Score * weightItem1.WEIGHT_VALUE) / 100;
                        DebugLog.WriteLog("POINT = " + Score + "*" + weightItem1.WEIGHT_VALUE + "//" + 100);
                        Score = 0;
                        foreach (EMP_MEASURE_TRANS item in lstTmp.Where(x => x.MEASURE_YY == tmpYear && x.WEIGHT_ID == 2).ToList())
                        {
                            Score += item.SCORE_ACTUAL;
                            tmpResult.YEAR_NAME = item.MEASURE_SEQ_NO == 1 ? "ต้นปี" : "ปลายปี";
                        }
                        EMP_MEASURE_WEIGHT weightItem2 = cmdEmpMeasureWeightService.Select(2);
                        tmpResult.POINT += (Score * weightItem2.WEIGHT_VALUE) / 100;
                        DebugLog.WriteLog("POINT = " + Score + "*" + weightItem2.WEIGHT_VALUE + "//" + 100);
                        if (tmpResult.POINT > 100)
                        {
                            tmpResult.POINT = 100;
                        }
                        DebugLog.WriteLog("tmpResult.POINT : " + tmpResult.POINT);
                        DataSouceResult.Add(tmpResult);
                    }
                }else
                {
                    DebugLog.WriteLog("DataSouce == null");
                }
            }
           
            foreach (EMP_MEASURE_TRANS itemResult in DataSouceResult)
            {
                List<EMP_GRADE_SET> items = cmdEmpGradeService.GetAllTop5();
                if (items != null && items.Count() >= 5)
                {
                    DebugLog.WriteLog("tmpResult.POINT : " + itemResult.POINT);
                    DebugLog.WriteLog("items[0].GRADE_SET_PERCENTAGE : " + items[0].GRADE_SET_PERCENTAGE);
                    DebugLog.WriteLog("items[1].GRADE_SET_PERCENTAGE : " + items[1].GRADE_SET_PERCENTAGE);
                    DebugLog.WriteLog("items[2].GRADE_SET_PERCENTAGE : " + items[2].GRADE_SET_PERCENTAGE);
                    DebugLog.WriteLog("items[3].GRADE_SET_PERCENTAGE : " + items[3].GRADE_SET_PERCENTAGE);
                    DebugLog.WriteLog("items[4].GRADE_SET_PERCENTAGE : " + items[4].GRADE_SET_PERCENTAGE);
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
        }
    }
}