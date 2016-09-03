﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using SPW.Common;
using SPW.DAL;

namespace SPW.UI.Web.Page
{
    public partial class SearchEmployeeAssessment : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private EmployeeService cmdEmp;
        private DepartmentService cmdDepartmentService;
        private EmpHistService cmdEmpHistService;
        public List<EMPLOYEE> DataSouce
        {
            get
            {
                var list = (List<EMPLOYEE>)ViewState["listEmployee"];
                return list;
            }
            set
            {
                ViewState["listEmployee"] = value;
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
            cmdEmp = (EmployeeService)_dataServiceEngine.GetDataService(typeof(EmployeeService));
            cmdDepartmentService = (DepartmentService)_dataServiceEngine.GetDataService(typeof(DepartmentService));
            cmdEmpHistService = (EmpHistService)_dataServiceEngine.GetDataService(typeof(EmpHistService));
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
            gridEmployee.DataSource = DataSouce;
            gridEmployee.DataBind();
        }

        private void InitialData()
        {
            var list = cmdDepartmentService.GetAll();
            foreach (var item in list)
            {
                ddlDepart.Items.Add(new ListItem(item.DEPARTMENT_NAME, item.DEPARTMENT_ID.ToString()));
            }
            DataSouce = cmdEmp.GetAllInclude();
            USER userItem = Session["user"] as USER;
            try
            {
                if (userItem.EMPLOYEE.EMPLOYEE_HIST.POSITION_NAME != "ผู้บริหาร")
                {
                    try
                    {
                        if (userItem.EMPLOYEE.EMPLOYEE_HIST.POSITION_NAME == "Supervisor")
                        {
                            DataSouce = DataSouce.Where(x =>x.EMPLOYEE_HIST != null && x.EMPLOYEE_HIST.DEPARTMENT_ID == userItem.EMPLOYEE.EMPLOYEE_HIST.DEPARTMENT_ID).ToList();
                        }
                        else
                        {
                            lblWarning.Text = "ผู้ใช้งานไม่ได้เป็น Supervisor ของแผนก";
                            warning.Visible = true;
                            DataSouce = null;
                            gridEmployee.DataSource = null;
                            gridEmployee.DataBind();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        DebugLog.WriteLog(ex.ToString());
                        lblWarning.Text = "ผู้ใช้งานไม่มีข้อมูลแผนก หรือไม่มีพนักงานให้ทำการประเมิน กรุณาตรวจสอบข้อมูล";
                        warning.Visible = true;
                        DataSouce = null;
                        gridEmployee.DataSource = null;
                        gridEmployee.DataBind();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
                lblWarning.Text = "ผู้ใช้งานไม่มีข้อมูลตำแหน่งหน้าที่ กรุณาตรวจสอบข้อมูล";
                warning.Visible = true;
                DataSouce = null;
                gridEmployee.DataSource = null;
                gridEmployee.DataBind();
                return;
            }
            gridEmployee.DataSource = DataSouce;
            gridEmployee.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            List<EMPLOYEE> tmpDataSouce = ObjectCopier.Clone<List<EMPLOYEE>>(DataSouce);
            if (!txtEmployeeCode.Text.Equals(""))
            {
                tmpDataSouce = tmpDataSouce.Where(x => x.EMPLOYEE_CODE.Contains(txtEmployeeCode.Text)).ToList();
            }

            if (!txtName.Text.Equals(""))
            {
                tmpDataSouce = tmpDataSouce.Where(x => x.EMPLOYEE_NAME.Contains(txtName.Text)).ToList();
            }

            if (ddlDepart.SelectedValue != "0")
            {
                tmpDataSouce = tmpDataSouce.Where(x => x.EMPLOYEE_HIST != null && x.EMPLOYEE_HIST.DEPARTMENT_ID == Convert.ToInt32(ddlDepart.SelectedValue)).ToList();
            }

            gridEmployee.DataSource = tmpDataSouce;
            gridEmployee.DataBind();
        }

        protected void gridEmployee_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            Response.RedirectPermanent("KeyInEmpAssessment.aspx?id=" + gridEmployee.DataKeys[e.NewEditIndex].Values[0].ToString());
        }

        protected void gridEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridEmployee.PageIndex = e.NewPageIndex;
            gridEmployee.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtEmployeeCode.Text = "";
            SearchGrid();
        }

        protected void gridEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton lbtnSelect = (LinkButton)e.Row.FindControl("lbtnSelect");
                if (cmdEmpHistService.GetAll(Convert.ToInt32(gridEmployee.DataKeys[e.Row.RowIndex][0].ToString())).Count() > 0)
                {
                    lbtnSelect.Attributes["href"] = "KeyInEmpAssessment.aspx?id=" + gridEmployee.DataKeys[e.Row.RowIndex][0].ToString();
                    lbtnSelect.Visible = true;
                }

                lbtnEdit.Attributes["href"] = "ManageEmployee.aspx?id=" + gridEmployee.DataKeys[e.Row.RowIndex][0].ToString();
            }
        }
    }
}