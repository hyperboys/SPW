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
        private EmpDtTemplateService cmdEmpDtTemplateService;
        private DepartmentService cmdDepartmentService;
        private EmpSkillTypeService cmdEmpSkillTypeService;
        private EmpSkillService cmdEmpSkillService;

        public List<EMP_MEASURE_DT_TEMPLATE> DataSouceDetail
        {
            get
            {
                if (ViewState["EMP_MEASURE_DT_TEMPLATE"] == null)
                {
                    ViewState["EMP_MEASURE_DT_TEMPLATE"] = new List<EMP_MEASURE_DT_TEMPLATE>();
                }
                var list = (List<EMP_MEASURE_DT_TEMPLATE>)ViewState["EMP_MEASURE_DT_TEMPLATE"];
                return list;
            }
            set
            {
                ViewState["EMP_MEASURE_DT_TEMPLATE"] = value;
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

            var listSkillType = cmdEmpSkillTypeService.GetAll();
            foreach (var item in listSkillType)
            {
                ddlSkillType.Items.Add(new ListItem(item.EMP_SKILL_TYPE_NA, item.EMP_SKILL_TYPE_ID.ToString()));
            }

            var listSkill = cmdEmpSkillService.GetAll();
            foreach (var item in listSkill)
            {
                ddlSkill.Items.Add(new ListItem(item.SKILL_NAME, item.SKILL_ID.ToString()));
            }

            if (Request.QueryString["id"] != null)
            {
                EMP_MEASURE_HD_TEMPLATE item = cmdEmpHdTemplateService.Select(Request.QueryString["id"].ToString(), Convert.ToInt32(Request.QueryString["typeId"].ToString()));
                if (item != null)
                {
                    ddlDepartment.SelectedValue = item.DEPARTMENT_ID.ToString();
                    ddlDepartment.Enabled = false;
                    lblName.Text = item.TEMPLATE_ID;
                    txtPercen.Text = item.EMP_SKILL_TYPE_PERCENTAGE.ToString();
                    ddlSkillType.SelectedValue = item.EMP_SKILL_TYPE_ID.ToString();
                    ddlSkillType.Enabled = false;
                    DataSouceDetail = cmdEmpDtTemplateService.GetList(item.TEMPLATE_ID, Convert.ToInt32(Request.QueryString["typeId"].ToString()));
                    foreach (EMP_MEASURE_DT_TEMPLATE tmp in DataSouceDetail)
                    {
                        tmp.SKILL_NAME = cmdEmpSkillService.Select(tmp.SKILL_ID).SKILL_NAME;
                        tmp.SKILL_TYPE_NAME = cmdEmpSkillTypeService.Select(tmp.EMP_SKILL_TYPE_ID).EMP_SKILL_TYPE_NA;
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
            grdEmpPos.DataSource = DataSouceDetail;
            grdEmpPos.DataBind();
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
                if (cmdEmpHdTemplateService.Select(Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlSkillType.SelectedValue)) == null)
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
                    lblError.Text = "มีข้อมูล Template ดังกล่าวแล้วไม่สามารถเพิ่มได้ กรุณาแก้ไข Template เดิม";
                    alert.Visible = false;
                    danger.Visible = true;
                    return;
                }
            }
            else
            {
                item.TEMPLATE_ID = Request.QueryString["id"].ToString();
                item.UPDATE_DATE = DateTime.Now;
                item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                item.SYE_DEL = false;
                cmdEmpHdTemplateService.Edit(item);
            }

            foreach (EMP_MEASURE_DT_TEMPLATE tmp in DataSouceDetail)
            {
                if (tmp.Action == ActionEnum.Create)
                {
                    tmp.TEMPLATE_ID = item.TEMPLATE_ID;
                    cmdEmpDtTemplateService.Add(tmp);
                }
            }

            foreach (EMP_MEASURE_DT_TEMPLATE tmp in DataSouceDetail)
            {
                tmp.EMP_SKILL_TYPE_ID = item.EMP_SKILL_TYPE_ID;
                cmdEmpDtTemplateService.Edit(tmp);
            }

            btnSave.Enabled = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            alert.Visible = true;
            Response.AppendHeader("Refresh", "2; url=SearchEmpHdTemplate.aspx");
        }

        protected void btnAddDetail_Click(object sender, EventArgs e)
        {
            ValidateHideAddEmpPos();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            USER userItem = Session["user"] as USER;
            EMP_MEASURE_DT_TEMPLATE item;
            if (Request.QueryString["id"] != null)
            {
                if (flagEdit)
                {
                    item = DataSouceDetail.Where(x => x.SEQ_NO == Convert.ToInt32(txtSeq.Text)).FirstOrDefault();
                    item.SKILL_ID = Convert.ToInt32(ddlSkill.SelectedValue);
                    item.SKILL_TARGET_SCORE = Convert.ToDecimal(txtAddPercen.Text);
                    item.UPDATE_DATE = DateTime.Now;
                    item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    item.SKILL_NAME = ddlSkill.SelectedItem.Text;
                    item.SKILL_TYPE_NAME = ddlSkillType.SelectedItem.Text;
                    cmdEmpDtTemplateService.Edit(item);
                }
                else
                {
                    item = new EMP_MEASURE_DT_TEMPLATE();
                    item.CREATE_DATE = DateTime.Now;
                    item.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    item.EMP_SKILL_TYPE_ID = Convert.ToInt32(ddlSkillType.SelectedValue);
                    item.SEQ_NO = DataSouceDetail.Count() + 1;
                    item.SKILL_ID = Convert.ToInt32(ddlSkill.SelectedValue);
                    item.SKILL_TARGET_SCORE = Convert.ToDecimal(txtAddPercen.Text);
                    item.SYE_DEL = false;
                    item.UPDATE_DATE = DateTime.Now;
                    item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    item.Action = ActionEnum.Create;
                    item.SKILL_NAME = ddlSkill.SelectedItem.Text;
                    item.SKILL_TYPE_NAME = ddlSkillType.SelectedItem.Text;
                    DataSouceDetail.Add(item);
                }
            }
            else
            {
                item = DataSouceDetail.Where(x => x.SEQ_NO == Convert.ToInt32(txtSeq.Text)).FirstOrDefault();
                if (item != null)
                {
                    item.SKILL_ID = Convert.ToInt32(ddlSkill.SelectedValue);
                    item.SKILL_TARGET_SCORE = Convert.ToDecimal(txtAddPercen.Text);
                    item.SKILL_NAME = ddlSkill.SelectedItem.Text;
                    item.SKILL_TYPE_NAME = ddlSkillType.SelectedItem.Text;
                }
                else
                {
                    item = new EMP_MEASURE_DT_TEMPLATE();
                    item.CREATE_DATE = DateTime.Now;
                    item.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    item.EMP_SKILL_TYPE_ID = Convert.ToInt32(ddlSkillType.SelectedValue);
                    item.SEQ_NO = DataSouceDetail.Count() + 1;
                    item.SKILL_ID = Convert.ToInt32(ddlSkill.SelectedValue);
                    item.SKILL_TARGET_SCORE = Convert.ToDecimal(txtAddPercen.Text);
                    item.SYE_DEL = false;
                    item.UPDATE_DATE = DateTime.Now;
                    item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    item.Action = ActionEnum.Create;
                    item.SKILL_NAME = ddlSkill.SelectedItem.Text;
                    item.SKILL_TYPE_NAME = ddlSkillType.SelectedItem.Text;
                    DataSouceDetail.Add(item);
                }
            }
            grdEmpPos.DataSource = DataSouceDetail;
            grdEmpPos.DataBind();
            flagEdit = false;
            ValidateHideAddEmpPos();
        }

        private static bool isOpen = false;
        private static bool flagEdit = false;
        private void ClearEmpPos()
        {
            txtAddPercen.Text = "";
            ddlSkill.SelectedIndex = 0;
        }

        private void HideAddEmpPos(bool visible)
        {
            this.rowAdd1.Visible = visible;
            this.rowAdd2.Visible = visible;
            this.rowAdd3.Visible = visible;
            this.rowAdd4.Visible = visible;
        }

        private void ValidateHideAddEmpPos()
        {
            isOpen = !isOpen;
            HideAddEmpPos(isOpen);
            if (isOpen == false)
            {
                ClearEmpPos();
                btnAddDetail.Text = "+";
                grdEmpPos.Enabled = true;
            }
            else
            {
                grdEmpPos.Enabled = false;
                if (flagEdit == false)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        txtSeq.Text = (cmdEmpDtTemplateService.GetSeqNo(Request.QueryString["id"].ToString()) + 1).ToString();
                    }
                    else
                    {
                        txtSeq.Text = (DataSouceDetail.Count() + 1).ToString();
                    }
                }
                btnAddDetail.Text = "-";
            }
        }

        protected void grdEmpPos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EMP_MEASURE_DT_TEMPLATE item;
            flagEdit = true;

            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RowIndex = gvr.RowIndex;

            if (Request.QueryString["id"] != null)
            {
                item = cmdEmpDtTemplateService.Select(grdEmpPos.DataKeys[RowIndex].Values[0].ToString(), Convert.ToInt32(grdEmpPos.DataKeys[RowIndex].Values[2].ToString()));
                if (item != null)
                {
                    ddlSkill.SelectedValue = item.SKILL_ID.ToString();
                    txtAddPercen.Text = item.SKILL_TARGET_SCORE.ToString();
                    txtSeq.Text = item.SEQ_NO.ToString();
                }
            }
            else
            {
                if (DataSouceDetail.Count() > 0)
                {
                    item = DataSouceDetail[RowIndex];
                    ddlSkill.SelectedValue = item.SKILL_ID.ToString();
                    txtAddPercen.Text = item.SKILL_TARGET_SCORE.ToString();
                    txtSeq.Text = item.SEQ_NO.ToString();
                }
            }
            ValidateHideAddEmpPos();
        }
    }
}