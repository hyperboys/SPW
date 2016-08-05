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
    public partial class SearchEmpSkillType : System.Web.UI.Page
    {

        #region Declare variable
        private DataServiceEngine _dataServiceEngine;
        #endregion

        #region Sevice control
        private EmpSkillTypeService _empSkillTypeService;
        public List<EMP_SKILL_TYPE> DataSouce
        {
            get
            {
                if (ViewState["EMP_SKILL_TYPE"] == null)
                {
                    ViewState["EMP_SKILL_TYPE"] = new List<EMP_SKILL_TYPE>();
                }
                var list = (List<EMP_SKILL_TYPE>)ViewState["EMP_SKILL_TYPE"];
                return list;
            }
            set
            {
                ViewState["EMP_SKILL_TYPE"] = value;
            }
        }

        private void InitialPage()
        {
            CreatePageEngine();
            InitialData();
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
            _empSkillTypeService = (EmpSkillTypeService)_dataServiceEngine.GetDataService(typeof(EmpSkillTypeService));
        }

        private void InitialData()
        {
            BindGridview();
        }

        private void BindGridview()
        {
            DataSouce = _empSkillTypeService.GetAll();
            if (DataSouce != null)
            {
                if (txtSkill.Text != "")
                {
                    DataSouce = DataSouce.Where(x => x.EMP_SKILL_TYPE_NA.ToString().Contains(txtSkill.Text)).ToList();
                }
            }

            grdEmpPos.DataSource = DataSouce;
            grdEmpPos.DataBind();
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
                lbtnDetail.PostBackUrl = "ManageEmpSkillType.aspx?id=" + grdEmpPos.DataKeys[e.Row.RowIndex].Values[0].ToString();
            }
        }
        #endregion

        protected void grdEmpPos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                _empSkillTypeService.Delete(Convert.ToInt32(grdEmpPos.DataKeys[e.RowIndex].Values[0].ToString()));
            }
            catch
            {
                string script = "alert(\"ข้อมูลมีการใช้งานแล้ว ไม่สามารถลบได้\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
            }
            InitialData();
        }
    }
}