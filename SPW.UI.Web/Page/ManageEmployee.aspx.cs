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
        private ZoneDetailService cmdZoneDetailService;
        private ZoneService cmdZoneService;
        private EMPLOYEE _employee;
        private static bool isOpen = false;

        public List<ZONE_DETAIL> DataSouceRoleFunction
        {
            get
            {
                var list = (List<ZONE_DETAIL>)ViewState["DataSouceRoleFunction"];
                return list;
            }
            set
            {
                ViewState["DataSouceRoleFunction"] = value;
            }
        }

        public List<ZONE_DETAIL> DataSouceNewRoleFunction
        {
            get
            {
                if (ViewState["DataSouceNewRoleFunction"] == null)
                {
                    ViewState["DataSouceNewRoleFunction"] = new List<ZONE_DETAIL>();
                }
                var list = (List<ZONE_DETAIL>)ViewState["DataSouceNewRoleFunction"];
                return list;
            }
            set
            {
                ViewState["DataSouceNewRoleFunction"] = value;
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
            cmdDepartmentService = (DepartmentService)_dataServiceEngine.GetDataService(typeof(DepartmentService));
            cmdEmployeeService = (EmployeeService)_dataServiceEngine.GetDataService(typeof(EmployeeService));
            cmdZoneDetailService = (ZoneDetailService)_dataServiceEngine.GetDataService(typeof(ZoneDetailService));
            cmdZoneService = (ZoneService)_dataServiceEngine.GetDataService(typeof(ZoneService));
        }

        private void PrepareObjectScreen()
        {
            var list = cmdDepartmentService.GetAll();
            foreach (var item in list)
            {
                ddlDepart.Items.Add(new ListItem(item.DEPARTMENT_NAME, item.DEPARTMENT_ID.ToString()));
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SearchEmployee.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                USER userItem = Session["user"] as USER;
                var obj = new EMPLOYEE();
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

        protected void btnAddEmpPos_Click(object sender, EventArgs e)
        {
            isOpen = !isOpen;
            HideAddEmpPos(isOpen);
            if (isOpen == false)
            {
                ClearEmpPos();
                btnAddEmpPos.Text = "+";
            }
            else
            {
                btnAddEmpPos.Text = "-";
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

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

        protected void grdEmpPos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdEmpPos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //_empPositionService.Delete(Convert.ToInt32(grdEmpPos.DataKeys[e.RowIndex].Values[0].ToString()));
            }
            catch
            {
                lblAlert.Text = "ข้อมูลมีการใช้งานแล้ว ไม่สามารถลบได้";
                alert.Visible = true;
            }
            PrepareObjectScreen();
        }
    }
}