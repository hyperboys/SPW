using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;


namespace SPW.UI.Web.Page
{
    public partial class SearchRawPack : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private RawPackService cmdRawPack;

        public List<RAW_PACK_SIZE> DataSouce
        {
            get
            {
                var list = (List<RAW_PACK_SIZE>)ViewState["listRawProduct"];
                return list;
            }
            set
            {
                ViewState["listRawProduct"] = value;
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
            cmdRawPack = (RawPackService)_dataServiceEngine.GetDataService(typeof(RawPackService));
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void BlindGrid()
        {
            gridRawPack.DataSource = DataSouce;
            gridRawPack.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)// Edit for Filter
        {
            if (!Page.IsPostBack)
            {
                CreatePageEngine();
                CreateFilterControl();
                CreateFilterDataSource();
                CreateFilterPageSelected(cmdRawPack.GetAllCount());
                ClearFilter();
                BindData();
            }
            else
            {
                ReloadPageEngine();
                CreateFilterControl();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e) // Edit for Filter
        {
            List<object> ParamItems = new List<object>();
            ParamItems.Insert(0, txtRawPackCode.Text.Trim());
            ParamItems.Insert(1, txtRawPackName.Text.Trim());
            Session[this.GetType().Name + "Filter"] = ParamItems;
            CreateFilterDataSource();
            BindData();
        }

        protected void gridRawPack_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            Response.RedirectPermanent("ManageRawPack.aspx?id=" + gridRawPack.DataKeys[e.NewEditIndex].Values[0].ToString());
        }

        protected void gridRawPack_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridRawPack.PageIndex = e.NewPageIndex;
            gridRawPack.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)// Edit for Filter
        {
            txtRawPackCode.Text = "";
            txtRawPackName.Text = "";
            CreateFilterDataSource();
            CreateFilterPageSelected(cmdRawPack.GetAllCount());
            ClearFilter();
            BindData();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("ManageRawPack.aspx");
        }

        #region FilterControl
        private void ClearFilter()
        {
            Session[this.GetType().Name + "Filter"] = null;
        }
        private void BindData()
        {
            if (Session[this.GetType().Name + "Filter"] != null)
            {
                //List<object> ParamItems = (List<object>)Session[this.GetType().Name + "Filter"];
                //int SourceItemCount = 0;
                //DataSouce = cmdRawPack.GetAllByFilterCondition((string)ParamItems[0], (string)ParamItems[1], (int)ViewState["PageIndex"], (int)ViewState["PageLimit"], ref SourceItemCount);
                //CreateFilterPageSelected(SourceItemCount);
                //UpdatePageControl((int)ViewState["PageIndex"]);
                //gridRawPack.DataSource = DataSouce;
                //gridRawPack.DataBind();
                //PrepareButtonFilterDisplay();
            }
            else
            {
                DataSouce = cmdRawPack.GetAllByFilter((int)ViewState["PageIndex"], (int)ViewState["PageLimit"]);
                gridRawPack.DataSource = DataSouce;
                gridRawPack.DataBind();
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

        protected void gridRawPack_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (ImageButton button in e.Row.Cells[3].Controls.OfType<ImageButton>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('ต้องการจะลบข้อมูลใช่หรือไม่')){ return false; };";
                    }
                }
            }
        }

        protected void gridRawPack_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                cmdRawPack.Delete(Convert.ToInt32(gridRawPack.DataKeys[e.RowIndex].Values[0].ToString()));
                Response.RedirectPermanent("SearchRawProduct.aspx");
            }
            catch
            {
                string script = "alert(\"ข้อมูลมีการใช้งานแล้ว ไม่สามารถลบได้\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }
    }
}