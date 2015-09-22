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
    public partial class Order : System.Web.UI.Page
    {
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

        private void BlindGrid()
        {
            gridStore.DataSource = DataSouce;
            gridStore.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitialData();
            }
        }

        private void InitialData()
        {
            Session["lstOrderDetail"] = null;
            var cmdStore = new StoreService();
            //DataSouce = cmdStore.GetALL();
            DataSouce = new List<STORE>();
            USER user = Session["user"] as USER;
            if (user == null) Response.Redirect("MainAdmin.aspx");
            var cmdZone = new ZoneDetailService();
            foreach (ZONE_DETAIL zoneId in cmdZone.GetALLByUser(user.EMPLOYEE_ID))
            {
                List<STORE> tmp = cmdStore.GetALL().Where(x => x.ZONE_ID == zoneId.ZONE_ID).ToList();
                DataSouce.AddRange(tmp);
            }

            var cmd = new SectorService();
            var list = cmd.GetALL();
            foreach (var item in list)
            {
                ddlSector.Items.Add(new ListItem(item.SECTOR_NAME, item.SECTOR_ID.ToString()));
            }
            var cmdPro = new ProvinceService();
            ViewState["listProvince"] = cmdPro.GetALL();
            foreach (var item in (List<PROVINCE>)ViewState["listProvince"])
            {
                ddlProvince.Items.Add(new ListItem(item.PROVINCE_NAME, item.PROVINCE_ID.ToString()));
            }

            var cmdRoad = new RoadService();
            ViewState["listRoad"] = cmdRoad.GetALL();
            foreach (var item in (List<ROAD>)ViewState["listRoad"])
            {
                ddlRoad.Items.Add(new ListItem(item.ROAD_NAME, item.ROAD_ID.ToString()));
            }

            gridStore.DataSource = null;
            gridStore.DataBind();
        }

        private void SearchGrid()
        {
            gridStore.DataSource = DataSouce.Where(x => x.PROVINCE_ID == Convert.ToInt32(ddlProvince.SelectedValue)).ToList();
            gridStore.DataBind();
        }

        private void SearchGridBangKok()
        {
            gridStore.DataSource = DataSouce.Where(x => x.PROVINCE_ID == Convert.ToInt32(ddlProvince.SelectedValue) && x.ROAD_ID == Convert.ToInt32(ddlRoad.SelectedValue)).ToList();
            gridStore.DataBind();
        }

        protected void gridProduct_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            Session["store"] = DataSouce.Where(x => x.STORE_ID == Convert.ToInt32(gridStore.DataKeys[e.NewEditIndex].Values[0].ToString())).FirstOrDefault();
            Response.Redirect("OrderProduct.aspx");
        }

        protected void gridProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridStore.PageIndex = e.NewPageIndex;
            gridStore.DataBind();
        }

        protected void ddlSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridStore.DataSource = null;
            gridStore.DataBind();

            if (!ddlProvince.Enabled) ddlProvince.Enabled = true;
            ddlProvince.Items.Clear();
            foreach (var item in ((List<PROVINCE>)ViewState["listProvince"]).Where(x => x.SECTOR_ID == Convert.ToInt32(ddlSector.SelectedValue)))
            {
                ddlProvince.Items.Add(new ListItem(item.PROVINCE_NAME, item.PROVINCE_ID.ToString()));
            }

            if (ddlProvince.Enabled && ddlProvince != null && ddlProvince.SelectedItem.Text.Equals("กรุงเทพมหานคร"))
            {
                lblComma.Visible = true;
                lblRoad.Visible = true;
                ddlRoad.Visible = true;
            }
            else
            {
                lblComma.Visible = false;
                lblRoad.Visible = false;
                ddlRoad.Visible = false;
            }
        }

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            if (ddlProvince.Enabled && ddlProvince.SelectedValue != "0")
            {
                if (ddlRoad.Visible)
                    SearchGridBangKok();
                else
                    SearchGrid();

                this.gridStore.Visible = true;
            }
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridStore.DataSource = null;
            gridStore.DataBind();
            if (ddlProvince.Enabled && ddlProvince.SelectedItem.Text.Equals("กรุงเทพมหานคร"))
            {
                lblComma.Visible = true;
                lblRoad.Visible = true;
                ddlRoad.Visible = true;
            }
            else
            {
                lblComma.Visible = false;
                lblRoad.Visible = false;
                ddlRoad.Visible = false;
            }
        }
    }
}