﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;

namespace SPW.UI.Web.Page
{
    public partial class ManageOrderDelivery : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private DeliveryIndexService cmdDelIndex;
        private VehicleService cmdVehicle;
        private OrderService cmdOrder;
        private DeliveryOrderService cmdDelOrder;
        private DeliveryOrderDetailService cmdDelOrderDetails;
        public List<DELIVERY_INDEX> DataSource
        {
            get
            {
                var list = (List<DELIVERY_INDEX>)ViewState["listStore"];
                return list;
            }
            set
            {
                ViewState["listStore"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CreatePageEngine();
                CreateFilterControl();
                CreateFilterDataSource();
                CreateFilterPageSelected(cmdDelIndex.GetAllCount());
                ClearFilter();
                BindData();
                PrepareSearchScreen();
            }
            else
            {
                ReloadPageEngine();
                CreateFilterControl();
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
            cmdDelIndex = (DeliveryIndexService)_dataServiceEngine.GetDataService(typeof(DeliveryIndexService));
            cmdVehicle = (VehicleService)_dataServiceEngine.GetDataService(typeof(VehicleService));
            cmdOrder = (OrderService)_dataServiceEngine.GetDataService(typeof(OrderService));
            cmdDelOrder = (DeliveryOrderService)_dataServiceEngine.GetDataService(typeof(DeliveryOrderService));
            cmdDelOrderDetails = (DeliveryOrderDetailService)_dataServiceEngine.GetDataService(typeof(DeliveryOrderDetailService));
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void PrepareSearchScreen()
        {
            List<VEHICLE> vehicleItems = cmdVehicle.GetAll();
            vehicleItems.Insert(0, new VEHICLE() { VEHICLE_REGNO = "กรุณาเลือกรถ", VEHICLE_ID = 0 });
            ddlVehicle.DataSource = vehicleItems;
            ddlVehicle.DataTextField = "VEHICLE_REGNO";
            ddlVehicle.DataValueField = "VEHICLE_ID";
            ddlVehicle.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime? StartDate = null, EndDate = null;
            int VehicelSelected = Convert.ToInt32(ddlVehicle.SelectedValue);
            if (txtStartDate.Text.Trim() != "" && txtEndDate.Text.Trim() != "")
            {
                StartDate = Convert.ToDateTime(txtStartDate.Text).AddYears(543);
                EndDate = Convert.ToDateTime(txtEndDate.Text).AddYears(543);
            }
            List<object> ParamItems = new List<object>();
            ParamItems.Insert(0, txtVehicle.Text.Trim());
            ParamItems.Insert(1, VehicelSelected);
            ParamItems.Insert(2, StartDate);
            ParamItems.Insert(3, EndDate);
            Session[this.GetType().Name + "Filter"] = ParamItems;
            CreateFilterDataSource();
            BindData();
        }

        #region FilterControl
        private void ClearFilter()
        {
            Session[this.GetType().Name + "Filter"] = null;
            Session["DelOrderIndexSelected"] = null;
            Session["DelOrderSelected"] = null;
            Session["DelOrderSelectedValidate"] = null;
            Session["DelEdit"] = null;
        }
        private void BindData()
        {
            if (Session[this.GetType().Name + "Filter"] != null)
            {
                List<object> ParamItems = (List<object>)Session[this.GetType().Name + "Filter"];
                int SourceItemCount = 0;
                DataSource = cmdDelIndex.GetAllByFilterCondition((string)ParamItems[0], (int)ParamItems[1], (DateTime?)ParamItems[2], (DateTime?)ParamItems[3], (int)ViewState["PageIndex"], (int)ViewState["PageLimit"], ref SourceItemCount);
                CreateFilterPageSelected(SourceItemCount);
                UpdatePageControl((int)ViewState["PageIndex"]);
                gdvManageOrderHQ.DataSource = DataSource;
                gdvManageOrderHQ.DataBind();
                PrepareButtonFilterDisplay();
            }
            else
            {
                DataSource = cmdDelIndex.GetAllByFilter((int)ViewState["PageIndex"], (int)ViewState["PageLimit"]);
                gdvManageOrderHQ.DataSource = DataSource;
                gdvManageOrderHQ.DataBind();
                PrepareButtonFilterDisplay();
            }
        }

        private void CreateFilterControl()
        {
            PlaceHolder1.Controls.Clear();
            UpdatePanel1.Triggers.Clear();
            UpdatePanel1.Triggers.Add(new AsyncPostBackTrigger() { ControlID = "btnSearch", EventName = "Click" });

            Button objBtnPrevious = new Button();
            objBtnPrevious.ID = "btnPrevious";
            objBtnPrevious.Text = "Previous";
            objBtnPrevious.CssClass = "btn btn-primary";
            objBtnPrevious.Width = 100;
            objBtnPrevious.Click += new EventHandler(objBtnPrevious_Click);
            UpdatePanel1.Triggers.Add(new AsyncPostBackTrigger() { ControlID = "btnPrevious", EventName = "Click" });
            PlaceHolder1.Controls.Add(objBtnPrevious);
            //PlaceHolder1.Controls.Add(new LiteralControl("<br/>"));

            DropDownList objddlPageSelect = new DropDownList();
            objddlPageSelect.ID = "ddlPageIndex";
            objddlPageSelect.CssClass = "btn";
            objddlPageSelect.Width = 100;
            objddlPageSelect.Style["text-align"] = "center";
            objddlPageSelect.AutoPostBack = true;
            objddlPageSelect.SelectedIndexChanged += new EventHandler(objddlPageSelect_SelectedIndexChanged);
            UpdatePanel1.Triggers.Add(new AsyncPostBackTrigger() { ControlID = "ddlPageIndex", EventName = "SelectedIndexChanged" });
            PlaceHolder1.Controls.Add(objddlPageSelect);

            Button objBtnNext = new Button();
            objBtnNext.ID = "btnNext";
            objBtnNext.Text = "Next";
            objBtnNext.CssClass = "btn btn-primary";
            objBtnNext.Width = 100;
            objBtnNext.Click += new EventHandler(objBtnNext_Click);
            UpdatePanel1.Triggers.Add(new AsyncPostBackTrigger() { ControlID = "btnNext", EventName = "Click" });
            PlaceHolder1.Controls.Add(objBtnNext);

            TextBox objPageIndex = new TextBox();
            objPageIndex.ID = "txtPageIndex";
            objPageIndex.Text = "40";
            objPageIndex.CssClass = "text-center";
            objPageIndex.Width = 100;
            objPageIndex.Height = 32;
            objPageIndex.Style["float"] = "right";
            objPageIndex.MaxLength = 3;
            PlaceHolder1.Controls.Add(objPageIndex);

            CompareValidator compval = new CompareValidator();
            compval.ID = "Compval";
            compval.ControlToValidate = "txtPageIndex";
            compval.ForeColor = System.Drawing.Color.Red;
            compval.Type = ValidationDataType.Integer;
            compval.Operator = ValidationCompareOperator.GreaterThanEqual;
            compval.ValueToCompare = "10";
            compval.Text = "Digit Only Accepted And Digit 10 - 999 ";
            compval.Style["float"] = "right";
            compval.CssClass = "text-center";
            compval.Width = 260;
            compval.Height = 30;
            compval.SetFocusOnError = true;
            compval.Style["margin-top"] = "6px";
            PlaceHolder1.Controls.Add(compval);
        }

        void objddlPageSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList objddlSelected = (DropDownList)sender;
            ViewState["PageIndex"] = Convert.ToInt32(objddlSelected.SelectedValue);
            BindData();
        }

        protected void objBtnNext_Click(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = (int)ViewState["PageIndex"] + 1;
            UpdatePageControl((int)ViewState["PageIndex"]);
            BindData();
        }

        protected void objBtnPrevious_Click(object sender, EventArgs e)
        {
            ViewState["PageIndex"] = (int)ViewState["PageIndex"] - 1;
            UpdatePageControl((int)ViewState["PageIndex"]);
            BindData();
        }

        private void UpdatePageControl(int PageIndex)
        {
            DropDownList ddlPageIndex = (DropDownList)PlaceHolder1.FindControl("ddlPageIndex");
            ddlPageIndex.SelectedIndex = PageIndex - 1;
        }

        private void CreateFilterDataSource()
        {
            DropDownList ddlPageIndex = (DropDownList)PlaceHolder1.FindControl("ddlPageIndex");
            TextBox objtxtPageLimit = (TextBox)PlaceHolder1.FindControl("txtPageIndex");
            ViewState["PageIndex"] = 1;
            int PageLimit = Convert.ToInt32(objtxtPageLimit.Text);
            ViewState["PageLimit"] = PageLimit;
        }

        private void CreateFilterPageSelected(int SourceItems)
        {
            DropDownList ddlPageIndex = (DropDownList)PlaceHolder1.FindControl("ddlPageIndex");
            ddlPageIndex.Items.Clear();
            int PageLimit = (int)ViewState["PageLimit"];
            int AllPage = (int)Math.Ceiling((decimal)SourceItems / (decimal)PageLimit);
            ddlPageIndex.Items.Add("1");
            for (int i = 2; i <= AllPage; i++)
            {
                ddlPageIndex.Items.Add(i.ToString());
            }
        }

        private void PrepareButtonFilterDisplay()
        {
            Button btnPrevious = (Button)PlaceHolder1.FindControl("btnPrevious");
            Button btnNext = (Button)PlaceHolder1.FindControl("btnNext");
            DropDownList ddlPageIndex = (DropDownList)PlaceHolder1.FindControl("ddlPageIndex");

            if ((int)ViewState["PageIndex"] > 1)
            {
                btnPrevious.Enabled = true;
            }
            else
            {
                btnPrevious.Enabled = false;
            }

            int LastPageIndex = Convert.ToInt32(ddlPageIndex.Items[ddlPageIndex.Items.Count - 1].Text);
            if ((int)ViewState["PageIndex"] < LastPageIndex)
            {
                btnNext.Enabled = true;
            }
            else
            {
                btnNext.Enabled = false;
            }
            PlaceHolder1.Visible = (btnNext.Enabled || btnPrevious.Enabled);
        }
        #endregion

        protected void gdvManageOrderHQ_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
            int DelOrderID = Convert.ToInt32(gdvManageOrderHQ.DataKeys[RowIndex].Values[0].ToString());
            switch (e.CommandName)
            {
                case "ViewDeliveryOrder":
                    {
                        DELIVERY_INDEX objDelivery = cmdDelIndex.Select(DelOrderID);
                        Session["DelOrderIndexSelected"] = objDelivery;
                        Session["DelEdit"] = objDelivery.ISDELETE;
                        Response.RedirectPermanent("~/Page/ManageOrderDeliveryDetail.aspx");
                    }
                    break;
                default:
                    break;

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["OrderSelected"] = null;
            Session["VehicleSelected"] = null;
            Response.RedirectPermanent("~/Page/SendOrderStore.aspx");
        }
    }
}