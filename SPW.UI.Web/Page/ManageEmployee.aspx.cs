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
using SPW.Common;

namespace SPW.UI.Web.Page
{
    public partial class ManageEmployee : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private DepartmentService cmdDepartmentService;
        private EmployeeService cmdEmployeeService;
        private EmpHistService cmdEmpHistService;
        private EmpPositionService cmdEmpPositionService;
        private EMPLOYEE _employee;
        private static bool isOpen = false;
        private static bool flagEdit = false;

        public List<EMPLOYEE_HIST> DataSouceEMPLOYEE_HIST
        {
            get
            {
                if (ViewState["DataSouceEMPLOYEE_HIST"] == null)
                {
                    ViewState["DataSouceEMPLOYEE_HIST"] = new List<EMPLOYEE_HIST>();
                }
                var list = (List<EMPLOYEE_HIST>)ViewState["DataSouceEMPLOYEE_HIST"];
                return list;
            }
            set
            {
                ViewState["DataSouceEMPLOYEE_HIST"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitialPage();
                isOpen = false;
                flagEdit = false;
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
            cmdDepartmentService = (DepartmentService)_dataServiceEngine.GetDataService(typeof(DepartmentService));
            cmdEmployeeService = (EmployeeService)_dataServiceEngine.GetDataService(typeof(EmployeeService));
            cmdEmpHistService = (EmpHistService)_dataServiceEngine.GetDataService(typeof(EmpHistService));
            cmdEmpPositionService = (EmpPositionService)_dataServiceEngine.GetDataService(typeof(EmpPositionService));
        }

        private void PrepareObjectScreen()
        {
            var list = cmdDepartmentService.GetAll();
            foreach (var item in list)
            {
                ddlDepart.Items.Add(new ListItem(item.DEPARTMENT_NAME, item.DEPARTMENT_ID.ToString()));
            }

            var list2 = cmdEmpPositionService.GetAll();
            foreach (var item in list2)
            {
                ddlEmpPos.Items.Add(new ListItem(item.POSITION_NAME, item.POSITION_ID.ToString()));
            }

            if (Request.QueryString["id"] != null)
            {
                _employee = cmdEmployeeService.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
                if (_employee != null)
                {
                    popTxtEmployeeCode.Text = _employee.EMPLOYEE_CODE;
                    txtName.Text = _employee.EMPLOYEE_NAME;
                    txtLastName.Text = _employee.EMPLOYEE_SURNAME;
                    lblName.Text = _employee.EMPLOYEE_CODE;
                    txtAddress1.Text = _employee.ADDRESS1;
                    txtGrade.Text = _employee.EDUCATION_GRADE;
                    txtMarry.Text = _employee.MARI_STT;
                    txtNationality.Text = _employee.NAT;
                    txtPhone1.Text = _employee.TEL1;
                    txtPhone2.Text = _employee.TEL2;
                    txtReligion.Text = _employee.RELI;
                    txtSoldier.Text = _employee.MILI_STT;
                    txtUniversity.Text = _employee.EDUCATION_NAME;
                    txtBDate.Text = _employee.BIR_DATE != null ? _employee.BIR_DATE.Value.ToString("dd/MM/yyyy") : "";
                    BindData();
                }
            }
            else
            {
                grdEmpPos.DataSource = DataSouceEMPLOYEE_HIST;
                grdEmpPos.DataBind();
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SearchEmployee.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                USER userItem = Session["user"] as USER;
                EMPLOYEE obj = new EMPLOYEE();
                obj.EMPLOYEE_CODE = popTxtEmployeeCode.Text;
                obj.EMPLOYEE_NAME = txtName.Text;
                obj.EMPLOYEE_SURNAME = txtLastName.Text;
                obj.ADDRESS1 = txtAddress1.Text;
                if (!String.IsNullOrEmpty(txtBDate.Text))
                {
                    obj.BIR_DATE = DateTime.ParseExact(txtBDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (obj.BIR_DATE.Value.Year > 2500)
                    {
                        obj.BIR_DATE = obj.BIR_DATE.Value.AddYears(-543);
                    }
                }
                obj.EDUCATION_GRADE = txtGrade.Text;
                obj.EDUCATION_NAME = txtUniversity.Text;
                obj.GEND = ddlSex.SelectedValue;
                obj.MARI_STT = txtMarry.Text;
                obj.MILI_STT = txtSoldier.Text;
                obj.NAT = txtNationality.Text;
                obj.RELI = txtReligion.Text;
                obj.TEL1 = txtPhone1.Text;
                obj.TEL2 = txtPhone2.Text;
                if (Request.QueryString["id"] != null)
                {
                    obj.Action = ActionEnum.Update;
                    obj.EMPLOYEE_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                    obj.UPDATE_DATE = DateTime.Now;
                    obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    obj.SYE_DEL = false;
                    cmdEmployeeService.Edit(obj);
                }
                else
                {
                    obj.Action = ActionEnum.Create;
                    obj.CREATE_DATE = DateTime.Now;
                    obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    obj.UPDATE_DATE = DateTime.Now;
                    obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    obj.SYE_DEL = false;
                    cmdEmployeeService.Add(obj);

                    if (DataSouceEMPLOYEE_HIST.Count() > 0)
                    {
                        foreach (EMPLOYEE_HIST item in DataSouceEMPLOYEE_HIST)
                        {
                            item.EMPLOYEE_ID = obj.EMPLOYEE_ID;
                            cmdEmpHistService.Add(item);
                        }
                    }
                }

                btnSave.Enabled = false;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                lblAlert.Text = "บันทึกข้อมูลสำเร็จ Save Success";
                alert.Visible = true;
                Response.AppendHeader("Refresh", "2; url=SearchEmployee.aspx");
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
                lblAlert.Text = "บันทึกข้อมูลไม่สำเร็จ กรุณาทำรายการใหม่";
                alert.Visible = true;
            }
        }

        private void BindData()
        {
            List<EMPLOYEE_HIST> lstItem = cmdEmpHistService.GetAll(Convert.ToInt32(Request.QueryString["id"]));
            foreach (EMPLOYEE_HIST item in lstItem)
            {
                if (item.POSITION_ID.Value != 0)
                {
                    item.POSITION_NAME = cmdEmpPositionService.Select(item.POSITION_ID.Value).POSITION_NAME;
                }
            }

            grdEmpPos.DataSource = lstItem;
            grdEmpPos.DataBind();
            grdEmpPos.Visible = true;
        }

        protected void btnAddEmpPos_Click(object sender, EventArgs e)
        {
            ValidateHideAddEmpPos();
        }

        private void ValidateHideAddEmpPos()
        {
            isOpen = !isOpen;
            HideAddEmpPos(isOpen);
            if (isOpen == false)
            {
                ClearEmpPos();
                btnAddEmpPos.Text = "+";
                grdEmpPos.Enabled = true;
            }
            else
            {
                grdEmpPos.Enabled = false;
                if (flagEdit == false)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        txtEmpPosSeq.Text = (cmdEmpHistService.GetSeqNo(Convert.ToInt32(Request.QueryString["id"].ToString())) + 1).ToString();
                    }
                    else
                    {
                        txtEmpPosSeq.Text = (DataSouceEMPLOYEE_HIST.Count() + 1).ToString();
                    }
                }
                btnAddEmpPos.Text = "-";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            USER userItem = Session["user"] as USER;
            EMPLOYEE_HIST item = new EMPLOYEE_HIST();
            item.CREATE_DATE = DateTime.Now;
            item.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
            item.DEPARTMENT_ID = Convert.ToInt32(ddlDepart.SelectedValue);
            if (!String.IsNullOrEmpty(txtEmpPosEff.Text))
            {
                item.EFF_DATE = DateTime.ParseExact(txtEmpPosEff.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (item.EFF_DATE.Value.Year > 2500)
                {
                    item.EFF_DATE = item.EFF_DATE.Value.AddYears(-543);
                }
            }
            if (!String.IsNullOrEmpty(txtEmpPosExp.Text))
            {
                item.EXP_DATE = DateTime.ParseExact(txtEmpPosExp.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (item.EXP_DATE.Value.Year > 2500)
                {
                    item.EXP_DATE = item.EXP_DATE.Value.AddYears(-543);
                }
            }
            item.EMPLOYEE_CODE = popTxtEmployeeCode.Text;
            item.POSITION_NAME = ddlEmpPos.SelectedItem.Text;
            if (Request.QueryString["id"] != null)
            {
                item.EMPLOYEE_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
            }
            else
            {
                item.EMPLOYEE_ID = 0;
            }

            item.JOBB_AMT = Convert.ToDecimal(txtEmpPosSalarySkill.Text == "" ? "0" : txtEmpPosSalarySkill.Text);
            item.POSI_AMT = Convert.ToDecimal(txtEmpPosSalaryPos.Text == "" ? "0" : txtEmpPosSalaryPos.Text);
            item.POSITION_ID = Convert.ToInt32(ddlEmpPos.SelectedValue);
            item.SAL_AMT = Convert.ToDecimal(txtEmpPosSalary.Text == "" ? "0" : txtEmpPosSalary.Text);
            item.SYE_DEL = false;
            item.UPDATE_DATE = DateTime.Now;
            item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;

            if (flagEdit)
            {
                item.HIST_SEQ_NO = Convert.ToInt32(txtEmpPosSeq.Text);
                cmdEmpHistService.Edit(item);
                BindData();
                flagEdit = false;
                ValidateHideAddEmpPos();
            }
            else
            {
                item.HIST_SEQ_NO = cmdEmpHistService.GetSeqNo(item.EMPLOYEE_ID) + 1;
                if (item.EMPLOYEE_ID != 0)
                {
                    cmdEmpHistService.Add(item);
                    BindData();
                    ValidateHideAddEmpPos();
                }
                else
                {
                    DataSouceEMPLOYEE_HIST.Add(item);
                    grdEmpPos.DataSource = DataSouceEMPLOYEE_HIST;
                    grdEmpPos.DataBind();
                    ValidateHideAddEmpPos();
                }
            }
        }

        private void ClearEmpPos()
        {
            txtEmpPosEff.Text = "";
            txtEmpPosExp.Text = "";
            txtEmpPosSalary.Text = "";
            txtEmpPosSalaryPos.Text = "";
            txtEmpPosSalarySkill.Text = "";
            txtEmpPosSeq.Text = "";
            ddlDepart.SelectedIndex = 0;
            ddlSex.SelectedIndex = 0;
        }

        private void HideAddEmpPos(bool visible)
        {
            this.addEmpPos1.Visible = visible;
            this.addEmpPos2.Visible = visible;
            this.addEmpPos3.Visible = visible;
            this.addEmpPos4.Visible = visible;
            this.addEmpPos5.Visible = visible;
        }

        protected void grdEmpPos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            EMPLOYEE_HIST item;
            flagEdit = true;
            if (Request.QueryString["id"] != null)
            {
                item = cmdEmpHistService.Select(Convert.ToInt32(grdEmpPos.DataKeys[e.NewEditIndex].Values[0].ToString()), Convert.ToInt32(Request.QueryString["id"].ToString()));
                txtEmpPosSeq.Text = item.HIST_SEQ_NO.ToString();
                txtEmpPosEff.Text = item.EFF_DATE.Value.ToString("dd/MM/yyyy");
                txtEmpPosExp.Text = item.EXP_DATE.Value.ToString("dd/MM/yyyy");
                txtEmpPosSalary.Text = item.SAL_AMT.ToString();
                txtEmpPosSalaryPos.Text = item.POSI_AMT.ToString();
                txtEmpPosSalarySkill.Text = item.JOBB_AMT.ToString();
                ddlDepart.SelectedValue = item.DEPARTMENT_ID.ToString();
                ddlEmpPos.SelectedValue = item.POSITION_ID.ToString();
            }
            else
            {
                txtEmpPosSeq.Text = DataSouceEMPLOYEE_HIST[e.NewEditIndex].HIST_SEQ_NO.ToString();
                txtEmpPosEff.Text = DataSouceEMPLOYEE_HIST[e.NewEditIndex].EFF_DATE.Value.ToString("dd/MM/yyyy");
                txtEmpPosExp.Text = DataSouceEMPLOYEE_HIST[e.NewEditIndex].EXP_DATE.Value.ToString("dd/MM/yyyy");
                txtEmpPosSalary.Text = DataSouceEMPLOYEE_HIST[e.NewEditIndex].SAL_AMT.ToString();
                txtEmpPosSalaryPos.Text = DataSouceEMPLOYEE_HIST[e.NewEditIndex].POSI_AMT.ToString();
                txtEmpPosSalarySkill.Text = DataSouceEMPLOYEE_HIST[e.NewEditIndex].JOBB_AMT.ToString();
                ddlDepart.SelectedValue = DataSouceEMPLOYEE_HIST[e.NewEditIndex].DEPARTMENT_ID.ToString();
                ddlEmpPos.SelectedValue = DataSouceEMPLOYEE_HIST[e.NewEditIndex].POSITION_ID.ToString();
            }
            //btnAdd.Visible = false;
            ValidateHideAddEmpPos();
        }

        protected void grdEmpPos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EMPLOYEE_HIST item;
            flagEdit = true;
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RowIndex = gvr.RowIndex;

            if (Request.QueryString["id"] != null)
            {
                item = cmdEmpHistService.Select(Convert.ToInt32(grdEmpPos.DataKeys[RowIndex].Values[0].ToString()), Convert.ToInt32(Request.QueryString["id"].ToString()));
                txtEmpPosSeq.Text = item.HIST_SEQ_NO.ToString();
                txtEmpPosEff.Text = item.EFF_DATE != null ? item.EFF_DATE.Value.ToString("dd/MM/yyyy") : "";
                txtEmpPosExp.Text = item.EXP_DATE != null ? item.EXP_DATE.Value.ToString("dd/MM/yyyy") : "";
                txtEmpPosSalary.Text = item.SAL_AMT.ToString();
                txtEmpPosSalaryPos.Text = item.POSI_AMT.ToString();
                txtEmpPosSalarySkill.Text = item.JOBB_AMT.ToString();
                ddlDepart.SelectedValue = item.DEPARTMENT_ID.ToString();
                ddlEmpPos.SelectedValue = item.POSITION_ID.ToString();
            }
            else
            {
                txtEmpPosSeq.Text = DataSouceEMPLOYEE_HIST[RowIndex].HIST_SEQ_NO.ToString();
                txtEmpPosEff.Text = DataSouceEMPLOYEE_HIST[RowIndex].EFF_DATE != null ? DataSouceEMPLOYEE_HIST[RowIndex].EFF_DATE.Value.ToString("dd/MM/yyyy") : "";
                txtEmpPosExp.Text = DataSouceEMPLOYEE_HIST[RowIndex].EXP_DATE != null ? DataSouceEMPLOYEE_HIST[RowIndex].EXP_DATE.Value.ToString("dd/MM/yyyy") : "";
                txtEmpPosSalary.Text = DataSouceEMPLOYEE_HIST[RowIndex].SAL_AMT.ToString();
                txtEmpPosSalaryPos.Text = DataSouceEMPLOYEE_HIST[RowIndex].POSI_AMT.ToString();
                txtEmpPosSalarySkill.Text = DataSouceEMPLOYEE_HIST[RowIndex].JOBB_AMT.ToString();
                ddlDepart.SelectedValue = DataSouceEMPLOYEE_HIST[RowIndex].DEPARTMENT_ID.ToString();
                ddlEmpPos.SelectedValue = DataSouceEMPLOYEE_HIST[RowIndex].POSITION_ID.ToString();
            }
            //btnAdd.Visible = false;
            ValidateHideAddEmpPos();
        }
    }
}