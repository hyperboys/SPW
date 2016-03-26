using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using SPW.DAL;

namespace SPW.UI.Web.Page
{
    public partial class ManageStore : System.Web.UI.Page
    {
        private STORE _store;
        private DataServiceEngine _dataServiceEngine;
        private StoreService cmdStore;
        private SectorService cmdSector;
        private ProvinceService cmdProvice;
        private RoadService cmdRoad;
        private ZoneService cmdZone;
        private EmployeeService cmdEmp;
        private ZoneDetailService cmdZoneDetail;

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
            cmdStore = (StoreService)_dataServiceEngine.GetDataService(typeof(StoreService));
            cmdSector = (SectorService)_dataServiceEngine.GetDataService(typeof(SectorService));
            cmdProvice = (ProvinceService)_dataServiceEngine.GetDataService(typeof(ProvinceService));
            cmdRoad = (RoadService)_dataServiceEngine.GetDataService(typeof(RoadService));
            cmdZone = (ZoneService)_dataServiceEngine.GetDataService(typeof(ZoneService));
            cmdEmp = (EmployeeService)_dataServiceEngine.GetDataService(typeof(EmployeeService));
            cmdZoneDetail = (ZoneDetailService)_dataServiceEngine.GetDataService(typeof(ZoneDetailService));
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

        private void InitialData()
        {
            var list = cmdSector.GetAll();
            foreach (var item in list)
            {
                ddlSector.Items.Add(new ListItem(item.SECTOR_NAME, item.SECTOR_ID.ToString()));
            }

            foreach (var item in cmdEmp.GetAllInclude().Where(x => x.DEPARTMENT.DEPARTMENT_NAME == "Sale").ToList())
            {
                ddlSell.Items.Add(new ListItem((item.EMPLOYEE_NAME + " " + item.EMPLOYEE_SURNAME), item.EMPLOYEE_ID.ToString()));
            }

            foreach (var item in cmdZone.GetAll())
            {
                ddlZone.Items.Add(new ListItem((item.ZONE_NAME), item.ZONE_ID.ToString()));
            }

            if (Request.QueryString["id"] != null)
            {
                _store = cmdStore.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
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
                    txtProvince.Text = _store.PROVINCE.PROVINCE_NAME;
                    ddlZone.SelectedValue = _store.ZONE_ID.ToString();
                    ddlSell.SelectedValue = _store.ZONE_DETAIL.EMPLOYEE_ID.ToString();
                    txtRoad.Text = _store.STORE_STREET;
                    flag.Text = "Edit";
                    AutoCompleteProvince(ddlSector.SelectedValue);
                }
                else 
                {
                    AutoCompleteProvince();
                }
            }
        }

        private void AutoCompleteProvince(string ID = "")
        {
            List<string> nameList = SearchAutoCompleteDataService.SearchCondition("PROVINCE", "PROVINCE_NAME", "PROVINCE_NAME", "", "SECTOR_ID", ID);
            string str = "";
            for (int i = 0; i < nameList.Count; i++)
            {
                str = str + '"' + nameList[i].ToString() + '"' + ',';
            }
            if (str != "")
            {
                str = str.Remove(str.Length - 1);
            }
            str = "[" + str + "]";
            txtProvince.Attributes.Add("data-source", str);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            USER userItem = Session["user"] as USER;
            var obj = new STORE();

            obj.PROVINCE_ID = cmdProvice.Select(txtProvince.Text).PROVINCE_ID;
            obj.SECTOR_ID = Convert.ToInt32(ddlSector.SelectedValue);
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
            if (flag.Text.Equals("Add"))
            {
                obj.Action = ActionEnum.Create;
                obj.ZONE_DETAIL = new ZONE_DETAIL();
                obj.ZONE_DETAIL.EMPLOYEE_ID = Convert.ToInt32(ddlSell.SelectedValue);
                obj.ZONE_DETAIL.ZONE_ID = Convert.ToInt32(ddlZone.SelectedValue);
                obj.ZONE_DETAIL.CREATE_DATE = DateTime.Now;
                obj.ZONE_DETAIL.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.ZONE_DETAIL.UPDATE_DATE = DateTime.Now;
                obj.ZONE_DETAIL.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;

                obj.CREATE_DATE = DateTime.Now;
                obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                cmdStore.Add(obj);
            }
            else
            {
                if (obj.PROVINCE_ID == 0)
                {
                    _store = cmdStore.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
                    obj.PROVINCE_ID = _store.PROVINCE_ID;
                }
                obj.ZONE_DETAIL = cmdStore.Select(Convert.ToInt32(Request.QueryString["id"].ToString())).ZONE_DETAIL;
                obj.ZONE_DETAIL.EMPLOYEE_ID = Convert.ToInt32(ddlSell.SelectedValue);
                obj.Action = ActionEnum.Update;
                obj.STORE_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                cmdStore.Edit(obj);
            }

            btnSave.Enabled = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            alert.Visible = true;
            Response.AppendHeader("Refresh", "2; url=SearchStore.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SearchStore.aspx");
        }

        protected void ddlSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteProvince(ddlSector.SelectedValue);
        }
    }
}