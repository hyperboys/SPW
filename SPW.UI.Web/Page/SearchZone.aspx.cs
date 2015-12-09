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
    public partial class SearchZone : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private ZoneService cmdZone;

        public List<ZONE> DataSouce
        {
            get
            {
                var list = (List<ZONE>)ViewState["listZone"];
                return list;
            }
            set
            {
                ViewState["listZone"] = value;
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
            cmdZone = (ZoneService)_dataServiceEngine.GetDataService(typeof(ZoneService));
        }


        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void BlindGrid()
        {
            gridZone.DataSource = DataSouce;
            gridZone.DataBind();
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

        private void InitialData()
        {
            DataSouce = cmdZone.GetAll();
            gridZone.DataSource = DataSouce;
            gridZone.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtZoneCode.Text.Equals(""))
            {
                gridZone.DataSource = DataSouce;
            }
            else
            {
                gridZone.DataSource = DataSouce.Where(x => x.ZONE_CODE.Contains(txtZoneCode.Text)).ToList();
            }
            gridZone.DataBind();
        }

        protected void gridZone_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            Response.RedirectPermanent("ManageZone.aspx?id=" + gridZone.DataKeys[e.NewEditIndex].Values[0].ToString());
        }

        protected void gridZone_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridZone.PageIndex = e.NewPageIndex;
            gridZone.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtZoneCode.Text = "";
            SearchGrid();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("ManageZone.aspx");
        }
    }
}