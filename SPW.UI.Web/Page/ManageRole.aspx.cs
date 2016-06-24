﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using SPW.Common;

namespace SPW.UI.Web.Page
{
    public partial class ManageRole : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private RoleService cmdRoleService;
        private RoleFunctionService cmdRoleFunctionService;
        private FunctionService cmdFunctionService;

        public ROLE Role
        {
            get
            {
                var list = (ROLE)ViewState["Role"];
                return list;
            }
            set
            {
                ViewState["Role"] = value;
            }
        }

        //public List<ROLE_FUNCTION> DataSouceRoleFunction
        //{
        //    get
        //    {
        //        var list = (List<ROLE_FUNCTION>)ViewState["listRoleFunction"];
        //        return list;
        //    }
        //    set
        //    {
        //        ViewState["listRoleFunction"] = value;
        //    }
        //}

        //public List<ROLE_FUNCTION> DataSouceNewRoleFunction
        //{
        //    get
        //    {
        //        if (ViewState["listNewRoleFunction"] == null)
        //        {
        //            ViewState["listNewRoleFunction"] = new List<ROLE_FUNCTION>();
        //        }
        //        var list = (List<ROLE_FUNCTION>)ViewState["listNewRoleFunction"];
        //        return list;
        //    }
        //    set
        //    {
        //        ViewState["listNewRoleFunction"] = value;
        //    }
        //}

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
            cmdRoleService = (RoleService)_dataServiceEngine.GetDataService(typeof(RoleService));
            cmdRoleFunctionService = (RoleFunctionService)_dataServiceEngine.GetDataService(typeof(RoleFunctionService));
            cmdFunctionService = (FunctionService)_dataServiceEngine.GetDataService(typeof(FunctionService));
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

        private void PrepareObjectScreen()
        {
            if (Request.QueryString["id"] != null)
            {
                Role = cmdRoleService.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
                popTxtRoleCode.Text = Role.ROLE_CODE;
                popTxtRoleName.Text = Role.ROLE_NAME;
                lblName.Text = Role.ROLE_NAME;
            }
            fncSystemData.DataSource = cmdFunctionService.Select(2).SUB_FUNCTION.ToList();
            fncSystemData.DataBind();
            fncStandardData.DataSource = cmdFunctionService.Select(3).SUB_FUNCTION.ToList();
            fncStandardData.DataBind();
            fncOrderData.DataSource = cmdFunctionService.Select(4).SUB_FUNCTION.ToList();
            fncOrderData.DataBind();
            fncReport.DataSource = cmdFunctionService.Select(5).SUB_FUNCTION.ToList();
            fncReport.DataBind();
            fncStock.DataSource = cmdFunctionService.Select(6).SUB_FUNCTION.ToList();
            fncStock.DataBind();
            fncAsset.DataSource = cmdFunctionService.Select(7).SUB_FUNCTION.ToList();
            fncAsset.DataBind();
            fncPO.DataSource = cmdFunctionService.Select(8).SUB_FUNCTION.ToList();
            fncPO.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
            Response.AppendHeader("Refresh", "2; url=SearchRole.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SearchRole.aspx");
        }

        protected void btnCancelFunction_Click(object sender, EventArgs e)
        {
            PrepareObjectScreen();
        }

        protected void fncSystemData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            List<ROLE_FUNCTION> lstTmp;
            if (Request.QueryString["id"] != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    lstTmp = cmdRoleFunctionService.SelectByRole(Convert.ToInt32(Request.QueryString["id"].ToString()), 2);
                    foreach (ROLE_FUNCTION tmp in lstTmp)
                    {
                        if (e.Row.Cells[1].Text == tmp.SUB_FUNCTION.SUB_FUNCTION_NAME)
                        {
                            CheckBox t = (CheckBox)e.Row.FindControl("check");
                            t.Checked = true;
                        }
                    }
                }
            }
        }

        protected void fncStandardData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            List<ROLE_FUNCTION> lstTmp;
            if (Request.QueryString["id"] != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    lstTmp = cmdRoleFunctionService.SelectByRole(Convert.ToInt32(Request.QueryString["id"].ToString()), 3);
                    foreach (ROLE_FUNCTION tmp in lstTmp)
                    {
                        if (e.Row.Cells[1].Text == tmp.SUB_FUNCTION.SUB_FUNCTION_NAME)
                        {
                            CheckBox t = (CheckBox)e.Row.FindControl("check");
                            t.Checked = true;
                        }
                    }
                }
            }
        }

        protected void fncOrderData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            List<ROLE_FUNCTION> lstTmp;
            if (Request.QueryString["id"] != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    lstTmp = cmdRoleFunctionService.SelectByRole(Convert.ToInt32(Request.QueryString["id"].ToString()), 4);
                    foreach (ROLE_FUNCTION tmp in lstTmp)
                    {
                        if (e.Row.Cells[1].Text == tmp.SUB_FUNCTION.SUB_FUNCTION_NAME)
                        {
                            CheckBox t = (CheckBox)e.Row.FindControl("check");
                            t.Checked = true;
                        }
                    }
                }
            }
        }

        protected void fncReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            List<ROLE_FUNCTION> lstTmp;
            if (Request.QueryString["id"] != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    lstTmp = cmdRoleFunctionService.SelectByRole(Convert.ToInt32(Request.QueryString["id"].ToString()), 5);
                    foreach (ROLE_FUNCTION tmp in lstTmp)
                    {
                        if (e.Row.Cells[1].Text == tmp.SUB_FUNCTION.SUB_FUNCTION_NAME)
                        {
                            CheckBox t = (CheckBox)e.Row.FindControl("check");
                            t.Checked = true;
                        }
                    }
                }
            }
        }

        protected void fncStock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            List<ROLE_FUNCTION> lstTmp;
            if (Request.QueryString["id"] != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    lstTmp = cmdRoleFunctionService.SelectByRole(Convert.ToInt32(Request.QueryString["id"].ToString()), 6);
                    foreach (ROLE_FUNCTION tmp in lstTmp)
                    {
                        if (e.Row.Cells[1].Text == tmp.SUB_FUNCTION.SUB_FUNCTION_NAME)
                        {
                            CheckBox t = (CheckBox)e.Row.FindControl("check");
                            t.Checked = true;
                        }
                    }
                }
            }
        }



        protected void fncAsset_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            List<ROLE_FUNCTION> lstTmp;
            if (Request.QueryString["id"] != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    lstTmp = cmdRoleFunctionService.SelectByRole(Convert.ToInt32(Request.QueryString["id"].ToString()), 7);
                    foreach (ROLE_FUNCTION tmp in lstTmp)
                    {
                        if (e.Row.Cells[1].Text == tmp.SUB_FUNCTION.SUB_FUNCTION_NAME)
                        {
                            CheckBox t = (CheckBox)e.Row.FindControl("check");
                            t.Checked = true;
                        }
                    }
                }
            }
        }

        protected void fncPO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            List<ROLE_FUNCTION> lstTmp;
            if (Request.QueryString["id"] != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    lstTmp = cmdRoleFunctionService.SelectByRole(Convert.ToInt32(Request.QueryString["id"].ToString()), 8);
                    foreach (ROLE_FUNCTION tmp in lstTmp)
                    {
                        if (e.Row.Cells[1].Text == tmp.SUB_FUNCTION.SUB_FUNCTION_NAME)
                        {
                            CheckBox t = (CheckBox)e.Row.FindControl("check");
                            t.Checked = true;
                        }
                    }
                }
            }
        }
    }
}