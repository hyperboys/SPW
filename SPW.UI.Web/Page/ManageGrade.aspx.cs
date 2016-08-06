using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using SPW.DAL;

namespace SPW.UI.Web.Page
{
    public partial class ManageGrade : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private EmpGradeService cmdEmpGradeService;

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
            cmdEmpGradeService = (EmpGradeService)_dataServiceEngine.GetDataService(typeof(EmpGradeService));
        }

        private void ReloadDatasource()
        {

        }

        private void PrepareObjectScreen()
        {
            List<EMP_GRADE_SET> items = cmdEmpGradeService.GetAllTop5();
            if (items != null && items.Count() >= 5)
            {
                txtPercenA.Text = items[0].GRADE_SET_PERCENTAGE.ToString();
                txtPercenB.Text = items[1].GRADE_SET_PERCENTAGE.ToString();
                txtPercenC.Text = items[2].GRADE_SET_PERCENTAGE.ToString();
                txtPercenD.Text = items[3].GRADE_SET_PERCENTAGE.ToString();
                txtPercenE.Text = items[4].GRADE_SET_PERCENTAGE.ToString();

                lblDate1.Text = items[0].EFFECTIVE_DATE.ToString("dd/MM/yyyy");
                lblDate2.Text = items[1].EFFECTIVE_DATE.ToString("dd/MM/yyyy");
                lblDate3.Text = items[2].EFFECTIVE_DATE.ToString("dd/MM/yyyy");
                lblDate4.Text = items[3].EFFECTIVE_DATE.ToString("dd/MM/yyyy");
                lblDate5.Text = items[4].EFFECTIVE_DATE.ToString("dd/MM/yyyy");
            }
            else
            {
                DateTime date = DateTime.Now;
                lblDate1.Text = date.ToString("dd/MM/yyyy");
                lblDate2.Text = date.ToString("dd/MM/yyyy");
                lblDate3.Text = date.ToString("dd/MM/yyyy");
                lblDate4.Text = date.ToString("dd/MM/yyyy");
                lblDate5.Text = date.ToString("dd/MM/yyyy");
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
            List<EMP_GRADE_SET> items = cmdEmpGradeService.GetAllTop5();
            items = items.Count() >= 5 ? items.Take(5).ToList() : items;
            foreach (EMP_GRADE_SET tmp in items)
            {
                tmp.SYE_DEL = true;
                tmp.GRADE_SET_ACTIVE = "I";
                tmp.EXPIRE_DATE = DateTime.Now;
                tmp.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                tmp.UPDATE_DATE = DateTime.Now;
                cmdEmpGradeService.Edit(tmp);
            }
            SQLUtility sql = new SQLUtility();
            int GRADE_SET_SEQ_NO = sql.GetCount(@"SELECT TOP 1 GRADE_SET_SEQ_NO FROM EMP_GRADE_SET ORDER BY GRADE_SET_SEQ_NO DESC");
            GRADE_SET_SEQ_NO += 1;
            
            // GRADE A
            EMP_GRADE_SET item = new EMP_GRADE_SET();
            item.CREATE_DATE = DateTime.Now;
            item.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
            item.EFFECTIVE_DATE = DateTime.Now;
            item.GRADE_SET = "A";
            item.GRADE_SET_ACTIVE = "A";
            item.GRADE_SET_PERCENTAGE = Convert.ToDecimal(txtPercenA.Text);
            item.GRADE_SET_SEQ_NO = GRADE_SET_SEQ_NO;
            item.SYE_DEL = false;
            item.UPDATE_DATE = DateTime.Now;
            item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
            cmdEmpGradeService.Add(item);

            // GRADE B
            item = new EMP_GRADE_SET();
            item.CREATE_DATE = DateTime.Now;
            item.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
            item.EFFECTIVE_DATE = DateTime.Now;
            item.GRADE_SET = "B";
            item.GRADE_SET_ACTIVE = "A";
            item.GRADE_SET_PERCENTAGE = Convert.ToDecimal(txtPercenB.Text);
            item.GRADE_SET_SEQ_NO = GRADE_SET_SEQ_NO;
            item.SYE_DEL = false;
            item.UPDATE_DATE = DateTime.Now;
            item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
            cmdEmpGradeService.Add(item);

            // GRADE C
            item = new EMP_GRADE_SET();
            item.CREATE_DATE = DateTime.Now;
            item.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
            item.EFFECTIVE_DATE = DateTime.Now;
            item.GRADE_SET = "C";
            item.GRADE_SET_ACTIVE = "A";
            item.GRADE_SET_PERCENTAGE = Convert.ToDecimal(txtPercenC.Text);
            item.GRADE_SET_SEQ_NO = GRADE_SET_SEQ_NO;
            item.SYE_DEL = false;
            item.UPDATE_DATE = DateTime.Now;
            item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
            cmdEmpGradeService.Add(item);

            // GRADE D
            item = new EMP_GRADE_SET();
            item.CREATE_DATE = DateTime.Now;
            item.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
            item.EFFECTIVE_DATE = DateTime.Now;
            item.GRADE_SET = "D";
            item.GRADE_SET_ACTIVE = "A";
            item.GRADE_SET_PERCENTAGE = Convert.ToDecimal(txtPercenD.Text);
            item.GRADE_SET_SEQ_NO = GRADE_SET_SEQ_NO;
            item.SYE_DEL = false;
            item.UPDATE_DATE = DateTime.Now;
            item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
            cmdEmpGradeService.Add(item);

            // GRADE E
            item = new EMP_GRADE_SET();
            item.CREATE_DATE = DateTime.Now;
            item.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
            item.EFFECTIVE_DATE = DateTime.Now;
            item.GRADE_SET = "E";
            item.GRADE_SET_ACTIVE = "A";
            item.GRADE_SET_PERCENTAGE = Convert.ToDecimal(txtPercenE.Text);
            item.GRADE_SET_SEQ_NO = GRADE_SET_SEQ_NO;
            item.SYE_DEL = false;
            item.UPDATE_DATE = DateTime.Now;
            item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
            cmdEmpGradeService.Add(item);

            btnSave.Enabled = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            alert.Visible = true;
            Response.AppendHeader("Refresh", "2; url=MainAdmin.aspx");
        }
    }
}