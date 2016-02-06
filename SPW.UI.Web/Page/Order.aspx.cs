using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.Model;
using SPW.DataService;
using SPW.DAL;
using System.Web.Services;

namespace SPW.UI.Web.Page
{
    public partial class Order : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private ZoneDetailService cmdZoneDetailService;
        private StoreService cmdStoreService;
        private SectorService cmdSectorService;
        private ProvinceService cmdProvinceService;
        private RoadService cmdRoadService;

        public List<STORE> DataSouce
        {
            get
            {
                if (ViewState["listStore"] == null)
                {
                    ViewState["listStore"] = new List<STORE>();
                }
                var list = (List<STORE>)ViewState["listStore"];
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
                //CreateFilterPageSelected(cmdStoreService.GetAllCount());
                ClearFilter();
                InitialPage();
            }
            else
            {
                ReloadPageEngine();
                CreateFilterControl();
            }
        }

        private void InitialPage()
        {
            CreatePageEngine();
            ReloadDatasource();
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
            cmdZoneDetailService = (ZoneDetailService)_dataServiceEngine.GetDataService(typeof(ZoneDetailService));
            cmdStoreService = (StoreService)_dataServiceEngine.GetDataService(typeof(StoreService));
            cmdSectorService = (SectorService)_dataServiceEngine.GetDataService(typeof(SectorService));
            cmdProvinceService = (ProvinceService)_dataServiceEngine.GetDataService(typeof(ProvinceService));
            cmdRoadService = (RoadService)_dataServiceEngine.GetDataService(typeof(RoadService));
        }

        private void ReloadDatasource()
        {

        }

        private void PrepareObjectScreen()
        {
            DataSouce = new List<STORE>();
            USER user = Session["user"] as USER;
            if (user == null) Response.RedirectPermanent("MainAdmin.aspx");

            foreach (ZONE_DETAIL zoneId in cmdZoneDetailService.GetAllByUser(user.EMPLOYEE_ID))
            {
                List<STORE> tmp = cmdStoreService.GetAll().Where(x => x.ZONE_ID == zoneId.ZONE_ID).ToList();
                DataSouce.AddRange(tmp);
            }

            var list = cmdSectorService.GetAll();
            foreach (var item in list)
            {
                ddlSector.Items.Add(new ListItem(item.SECTOR_NAME, item.SECTOR_ID.ToString()));
            }

            ViewState["listProvince"] = cmdProvinceService.GetAll();
            foreach (var item in (List<PROVINCE>)ViewState["listProvince"])
            {
                ddlProvince.Items.Add(new ListItem(item.PROVINCE_NAME, item.PROVINCE_ID.ToString()));
            }

            ViewState["listRoad"] = cmdStoreService.GetAllStreet();
            foreach (var item in (List<string>)ViewState["listRoad"])
            {
                ddlRoad.Items.Add(new ListItem(item, item));
            }

            //gdvStore.DataSource = null;
            //gdvStore.DataBind();

            SearchStore();
            CreateFilterControl();
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

        private void SearchGridProvince()
        {
            gdvStore.DataSource = DataSouce.Where(x => x.PROVINCE_ID == Convert.ToInt32(ddlProvince.SelectedValue) && x.STORE_NAME.ToUpper().Contains(txtStoreName.Text.ToUpper()) && x.STORE_CODE.ToUpper().Contains(txtStoreCode.Text.ToUpper())).ToList();
            gdvStore.DataBind();
        }

        private void SearchGridBangKok()
        {
            List<object> ParamItems = new List<object>();
            ParamItems.Insert(0, Convert.ToInt32(ddlProvince.SelectedValue));
            ParamItems.Insert(1, ddlRoad.SelectedValue);
            Session[this.GetType().Name + "Filter2"] = ParamItems;
        }

        protected void gridProduct_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            Response.RedirectPermanent("OrderProduct.aspx?id=" + DataSouce[e.NewEditIndex].STORE_ID);
        }

        protected void gridProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvStore.PageIndex = e.NewPageIndex;
            gdvStore.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchStore();
        }

        private void SearchStore()
        {
            if (ddlProvince.Enabled && ddlProvince.SelectedValue != "0")
            {
                Session[this.GetType().Name + "Filter"] = null;
                List<object> ParamItems = new List<object>();
                ParamItems.Insert(0, txtStoreCode.Text.Trim());
                ParamItems.Insert(1, txtStoreName.Text.Trim());
                ParamItems.Insert(2, Convert.ToInt32(ddlProvince.SelectedValue));
                if (ddlRoad.SelectedValue == "กรุณาเลือก")
                {
                    ParamItems.Insert(3, "");
                }
                else
                {
                    ParamItems.Insert(3, ddlRoad.SelectedValue);
                }
                Session[this.GetType().Name + "Filter2"] = ParamItems;
            }
            else
            {
                Session[this.GetType().Name + "Filter2"] = null;
                List<object> ParamItems = new List<object>();
                ParamItems.Insert(0, txtStoreCode.Text.Trim());
                ParamItems.Insert(1, txtStoreName.Text.Trim());
                Session[this.GetType().Name + "Filter"] = ParamItems;
            }

            CreateFilterDataSource();
            BindData();
            this.gdvStore.Visible = true;
            this.PlaceHolder1.Visible = true;
        }

        protected void gdvStore_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink t1 = (HyperLink)e.Row.FindControl("NAME");
                t1.Text = DataSouce[e.Row.RowIndex].STORE_NAME + " " + DataSouce[e.Row.RowIndex].STORE_CODE;
                t1.NavigateUrl = "OrderProduct.aspx?id=" + DataSouce[e.Row.RowIndex].STORE_ID;
                LinkButton lbtnDetail = (LinkButton)e.Row.FindControl("lbtnDetail");
                lbtnDetail.PostBackUrl = "OrderProduct.aspx?id=" + DataSouce[e.Row.RowIndex].STORE_ID;
                Label lbName1 = (Label)e.Row.FindControl("ADDRESS");
                lbName1.Text = DataSouce[e.Row.RowIndex].STORE_ADDR1;
                Label lbPrice1 = (Label)e.Row.FindControl("TEL");
                lbPrice1.Text = DataSouce[e.Row.RowIndex].STORE_TEL1;
            }
        }

        protected void ddlSector_SelectedIndexChanged1(object sender, EventArgs e)
        {
            gdvStore.DataSource = null;
            gdvStore.DataBind();

            if (!ddlProvince.Enabled) ddlProvince.Enabled = true;
            ddlProvince.Items.Clear();
            foreach (var item in ((List<PROVINCE>)ViewState["listProvince"]).Where(x => x.SECTOR_ID == Convert.ToInt32(ddlSector.SelectedValue)))
            {
                ddlProvince.Items.Add(new ListItem(item.PROVINCE_NAME, item.PROVINCE_ID.ToString()));
            }

            if (ddlProvince.Enabled && ddlProvince != null && ddlProvince.SelectedItem.Text.Equals("กรุงเทพมหานคร"))
            {
                //ddlRoad.Visible = true;
                //divRoad.Visible = true;
                ddlRoad.Enabled = true;
            }
            else
            {
                //ddlRoad.Visible = false;
                //divRoad.Visible = false;
                //ddlRoad.Items.Clear();
                //ddlRoad.Items.Add(new ListItem("กรุณาเลือก", "0"));
                ddlRoad.SelectedValue = "0";
                ddlRoad.Enabled = false;
            }
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProvince.Enabled && ddlProvince != null && ddlProvince.SelectedItem.Text.Equals("กรุงเทพมหานคร"))
            {
                //ddlRoad.Enabled = true;
                //divRoad.Visible = true;
                ddlRoad.Enabled = true;
            }
            else
            {
                //ddlRoad.Visible = false;
                //divRoad.Visible = false;
                //ddlRoad.Items.Clear();
                //ddlRoad.Items.Add(new ListItem("กรุณาเลือก", "0"));
                ddlRoad.SelectedValue = "0";
                ddlRoad.Enabled = false;
            }
        }

        [WebMethod]
        public static string[] SearchTxtStoreCode(string STORE_CODE)
        {
            return SearchAutoCompleteDataService.Search("STORE", "STORE_CODE", "STORE_CODE", STORE_CODE).ToArray(); 
        }

        [WebMethod]
        public static string[] SearchTxtStoreName(string STORE_NAME)
        {
            return SearchAutoCompleteDataService.Search("STORE", "STORE_NAME", "STORE_NAME", STORE_NAME).ToArray();
        }

        #region FilterControl
        private void ClearFilter()
        {
            Session[this.GetType().Name + "Filter"] = null;
            Session[this.GetType().Name + "Filter2"] = null;
        }

        private void BindData()
        {
            if (Session[this.GetType().Name + "Filter2"] != null)
            {
                List<object> ParamItems = (List<object>)Session[this.GetType().Name + "Filter2"];
                int SourceItemCount = 0;
                DataSouce = cmdStoreService.GetAllByFilterConditionDropdown((string)ParamItems[0], (string)ParamItems[1], (int)ParamItems[2], (string)ParamItems[3], (int)ViewState["PageIndex"], (int)ViewState["PageLimit"], ref SourceItemCount);
                CreateFilterPageSelected(SourceItemCount);
                UpdatePageControl((int)ViewState["PageIndex"]);

            }
            else if (Session[this.GetType().Name + "Filter"] != null)
            {
                List<object> ParamItems = (List<object>)Session[this.GetType().Name + "Filter"];
                int SourceItemCount = 0;
                DataSouce = cmdStoreService.GetAllByFilterCondition((string)ParamItems[0], (string)ParamItems[1], (int)ViewState["PageIndex"], (int)ViewState["PageLimit"], ref SourceItemCount);
                CreateFilterPageSelected(SourceItemCount);
                UpdatePageControl((int)ViewState["PageIndex"]);
            }
            else
            {
                DataSouce = cmdStoreService.GetAllByFilter((int)ViewState["PageIndex"], (int)ViewState["PageLimit"]);
            }

            gdvStore.DataSource = DataSouce;
            gdvStore.DataBind();
            PrepareButtonFilterDisplay();
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
            objPageIndex.Text = "10";
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

    }
}