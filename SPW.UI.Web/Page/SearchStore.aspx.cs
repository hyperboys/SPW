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
    public partial class SearchStore : System.Web.UI.Page
    {
        private STORE _store;
        public List<STORE> DataSouce
        {
            get
            {
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
            var cmd = new StoreService();
            DataSouce = cmd.GetALL();
            gridStore.DataSource = DataSouce;
            gridStore.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtStoreCode.Text.Equals("") && txtStoreName.Text.Equals(""))
            {
                gridStore.DataSource = DataSouce;
            }
            else
            {
                gridStore.DataSource = DataSouce.Where(x => x.STORE_CODE.Contains(txtStoreCode.Text) && x.STORE_NAME.Contains(txtStoreName.Text)).ToList();
            }
            gridStore.DataBind();
        }

        protected void gridProduct_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            ViewState["stoId"] = gridStore.DataKeys[e.NewEditIndex].Values[0].ToString();
            InitialDataPopup();
            this.popup.Show();
        }

        protected void gridProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridStore.PageIndex = e.NewPageIndex;
            gridStore.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtStoreCode.Text = "";
            txtStoreName.Text = "";
            SearchGrid();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ViewState["stoId"] = null;
            InitialDataPopup();
            this.popup.Show();
        }


        private void InitialDataPopup()
        {
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

            var cmdZone = new ZoneService();
            var listZone = cmdZone.GetALL();
            foreach (var item in listZone)
            {
                ddlZone.Items.Add(new ListItem(item.ZONE_NAME, item.ZONE_ID.ToString()));
            }

            if (ViewState["stoId"] != null)
            {
                var cmdStore = new StoreService();
                _store = cmdStore.Select(Convert.ToInt32(ViewState["stoId"].ToString()));
                if (_store != null)
                {
                    txtAddress.Text = _store.STORE_ADDR1;
                    txtAmpur.Text = _store.STORE_DISTRICT;
                    txtFax.Text = _store.STORE_FAX;
                    txtMobli.Text = _store.STORE_MOBILE;
                    txtPostCode.Text = _store.STORE_POSTCODE;
                    popTxtStoreCode.Text = _store.STORE_CODE;
                    poptxtStoreName.Text = _store.STORE_NAME;
                    txtTel1.Text = _store.STORE_TEL1;
                    txtTel2.Text = _store.STORE_TEL2;
                    txtTumbon.Text = _store.STORE_SUBDISTRICT;
                    ddlSector.SelectedValue = _store.SECTOR_ID.ToString();
                    ddlProvince.SelectedValue = _store.PROVINCE_ID.ToString();
                    ddlProvince.Enabled = true;
                    ddlZone.SelectedValue = _store.ZONE_ID.ToString();
                    //if (ddlProvince.SelectedItem.Text.Equals("กรุงเทพมหานคร"))
                    //{
                    ddlRoad.Visible = true;
                    ddlRoad.SelectedValue = _store.ROAD_ID.ToString();
                    txtRoad.Visible = false;
                    //}
                    //else
                    //{
                    //    txtRoad.Text = _store.STORE_STREET;
                    //    ddlRoad.Visible = false;
                    //}
                    flag.Text = "Edit";
                }
            }
        }

        protected void ddlSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlProvince.Enabled) ddlProvince.Enabled = true;
            ddlProvince.Items.Clear();
            foreach (var item in ((List<PROVINCE>)ViewState["listProvince"]).Where(x => x.SECTOR_ID == Convert.ToInt32(ddlSector.SelectedValue)))
            {
                ddlProvince.Items.Add(new ListItem(item.PROVINCE_NAME, item.PROVINCE_ID.ToString()));
            }
            this.popup.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var obj = new STORE();
            obj.PROVINCE_ID = Convert.ToInt32(ddlProvince.SelectedValue);
            obj.SECTOR_ID = Convert.ToInt32(ddlSector.SelectedValue);
            obj.ROAD_ID = Convert.ToInt32(ddlRoad.SelectedValue);
            obj.STORE_ADDR1 = txtAddress.Text;
            obj.STORE_CODE = popTxtStoreCode.Text;
            obj.STORE_DISTRICT = txtAmpur.Text;
            obj.STORE_FAX = txtFax.Text;
            obj.STORE_MOBILE = txtMobli.Text;
            obj.STORE_NAME = poptxtStoreName.Text;
            obj.STORE_POSTCODE = txtPostCode.Text;
            obj.STORE_STREET = txtRoad.Text;
            obj.STORE_SUBDISTRICT = txtTumbon.Text;
            obj.STORE_TEL1 = txtTel1.Text;
            obj.STORE_TEL2 = txtTel2.Text;
            obj.ZONE_ID = Convert.ToInt32(ddlZone.SelectedValue);
            var cmd = new StoreService(obj);
            if (flag.Text.Equals("Add"))
            {
                obj.Action = ActionEnum.Create;
                obj.CREATE_DATE = DateTime.Now;
                obj.CREATE_EMPLOYEE_ID = 0;
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = 0;
                obj.SYE_DEL = true;
                cmd.Add();
            }
            else
            {
                obj.Action = ActionEnum.Update;
                obj.STORE_ID = Convert.ToInt32(ViewState["stoId"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = 0;
                obj.SYE_DEL = true;
                cmd.Edit();
            }

            ViewState["stoId"] = null;
            Response.Redirect("SearchStore.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["stoId"] = null;
            Response.Redirect("SearchStore.aspx");
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProvince.Enabled && ddlProvince.SelectedItem.Text.Equals("กรุงเทพมหานคร"))
            {
                txtRoad.Visible = false;
                ddlRoad.Visible = true;
            }
            this.popup.Show();
        }
    }
}