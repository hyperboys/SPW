using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using SPW.DAL;

namespace SPW.UI.Web.Page
{
    public partial class SearchEmpHdTemplate : System.Web.UI.Page
    {

        #region Declare variable
        private DataServiceEngine _dataServiceEngine;
        #endregion

        #region Sevice control
        private EmpHdTemplateService _empMeasureHdTemplate;
        public List<EMP_MEASURE_HD_TEMPLATE> DataSouce
        {
            get
            {
                if (ViewState["EMP_MEASURE_HD_TEMPLATE"] == null)
                {
                    ViewState["EMP_MEASURE_HD_TEMPLATE"] = new List<EMP_MEASURE_HD_TEMPLATE>();
                }
                var list = (List<EMP_MEASURE_HD_TEMPLATE>)ViewState["EMP_MEASURE_HD_TEMPLATE"];
                return list;
            }
            set
            {
                ViewState["EMP_MEASURE_HD_TEMPLATE"] = value;
            }
        }

        private void InitialPage()
        {
            CreatePageEngine();
            InitialData();
            AutoCompletetxtSkillTypeName();
            AutoCompletetxtDepartmentName();
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

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void InitialDataService()
        {
            _empMeasureHdTemplate = (EmpHdTemplateService)_dataServiceEngine.GetDataService(typeof(EmpHdTemplateService));
        }

        private void InitialData()
        {
            BindGridview();
        }

        private void BindGridview()
        {
            DataSouce = _empMeasureHdTemplate.GetAll();
            if (DataSouce != null)
            {
                if (txtSkill.Text != "")
                {
                    DataSouce = DataSouce.Where(x => x.EMP_SKILL_TYPE.EMP_SKILL_TYPE_NA.Contains(txtSkill.Text)).ToList();
                }

                if (txtDepartment.Text != "")
                {
                    DataSouce = DataSouce.Where(x => x.DEPARTMENT.DEPARTMENT_NAME.Contains(txtDepartment.Text)).ToList();
                }
            }

            grdEmpPos.DataSource = DataSouce;
            grdEmpPos.DataBind();
        }

        private void AutoCompletetxtDepartmentName()
        {
            List<string> nameList = SearchAutoCompleteDataService.Search("DEPARTMENT", "DEPARTMENT_NAME", "DEPARTMENT_NAME", "");
            string str = "";
            for (int i = 0; i < nameList.Count; i++)
            {
                str = str + '"' + nameList[i].ToString() + '"' + ',';
            }
            if (str != "")
            {
                str = str.Remove(str.Length - 1);
            }
            str = "[" + str + "]";
            txtDepartment.Attributes.Add("data-source", str);
        }


        private void AutoCompletetxtSkillTypeName()
        {
            List<string> nameList = SearchAutoCompleteDataService.Search("EMP_SKILL_TYPE", "EMP_SKILL_TYPE_NA", "EMP_SKILL_TYPE_NA", "");
            string str = "";
            for (int i = 0; i < nameList.Count; i++)
            {
                str = str + '"' + nameList[i].ToString() + '"' + ',';
            }
            if (str != "")
            {
                str = str.Remove(str.Length - 1);
            }
            str = "[" + str + "]";
            txtSkill.Attributes.Add("data-source", str);
        }


        #endregion

        #region ASP control
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridview();
        }

        protected void grdEmpPos_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtnDetail = (LinkButton)e.Row.FindControl("lbtnDetail");
                lbtnDetail.PostBackUrl = "ManageHDTemplate.aspx?id=" + grdEmpPos.DataKeys[e.Row.RowIndex].Values[0].ToString() + "&typeId=" + grdEmpPos.DataKeys[e.Row.RowIndex].Values[1].ToString();
            }
        }
        #endregion
    }
}