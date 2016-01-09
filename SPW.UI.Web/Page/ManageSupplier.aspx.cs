﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;


namespace SPW.UI.Web.Page
{
    public partial class ManageSupplier : System.Web.UI.Page
    {
        private VENDOR _vendor;
        private DataServiceEngine _dataServiceEngine;
        private SupplierService cmdSupplier;
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
            cmdSupplier = (SupplierService)_dataServiceEngine.GetDataService(typeof(SupplierService));
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
            //foreach (var item in list)
            //{
            //    ddlSector.Items.Add(new ListItem(item.SECTOR_NAME, item.SECTOR_ID.ToString()));
            //}

            ViewState["listProvince"] = cmdProvice.GetAll();
            foreach (var item in (List<PROVINCE>)ViewState["listProvince"])
            {
                ddlProvince.Items.Add(new ListItem(item.PROVINCE_NAME, item.PROVINCE_ID.ToString()));
            }

            if (Request.QueryString["id"] != null)
            {
                _vendor = cmdSupplier.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
                if (_vendor != null)
                {
                    txtAddress.Text = _vendor.VENDOR_ADDR1;
                    txtAmpur.Text = _vendor.VENDOR_DISTRICT;
                    txtFax.Text = _vendor.VENDOR_FAX;
                    txtMobli.Text = _vendor.VENDOR_MOBILE;
                    txtPostCode.Text = _vendor.VENDOR_POSTCODE;
                    popTxtStoreCode.Text = _vendor.VENDOR_CODE;
                    poptxtStoreName.Text = _vendor.VENDOR_NAME;
                    txtTel1.Text = _vendor.VENDOR_TEL1;
                    txtTel2.Text = _vendor.VENDOR_TEL2;
                    txtTumbon.Text = _vendor.VENDOR_SUBDISTRICT;
                    //ddlSector.SelectedValue = _vendor.SECTOR_ID.ToString();
                    ddlProvince.SelectedValue = _vendor.PROVINCE_ID.ToString();
                    ddlProvince.Enabled = true;
                    txtRoad.Text = _vendor.VENDOR_STREET;
                    flag.Text = "Edit";
                    lblName.Text = "แก้ไขซัพพลายเออร์";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            USER userItem = Session["user"] as USER;
            var obj = new VENDOR();
            obj.PROVINCE_ID = Convert.ToInt32(ddlProvince.SelectedValue);
            //obj.SECTOR_ID = Convert.ToInt32(ddlSector.SelectedValue);
            obj.VENDOR_ADDR1 = txtAddress.Text;
            obj.VENDOR_CODE = popTxtStoreCode.Text;
            obj.VENDOR_DISTRICT = txtAmpur.Text;
            obj.VENDOR_FAX = txtFax.Text;
            obj.VENDOR_MOBILE = txtMobli.Text;
            obj.VENDOR_NAME = poptxtStoreName.Text;
            obj.VENDOR_POSTCODE = txtPostCode.Text;
            obj.VENDOR_STREET = txtRoad.Text;
            obj.VENDOR_SUBDISTRICT = txtTumbon.Text;
            obj.VENDOR_TEL1 = txtTel1.Text;
            obj.VENDOR_TEL2 = txtTel2.Text;
            if (flag.Text.Equals("Add"))
            {
                obj.Action = ActionEnum.Create;
                obj.CREATE_DATE = DateTime.Now;
                obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                cmdSupplier.Add(obj);
            }
            else
            {
                //obj.ZONE_DETAIL = cmdSupplier.Select(Convert.ToInt32(Request.QueryString["id"].ToString())).ZONE_DETAIL;
                obj.Action = ActionEnum.Update;
                obj.VENDOR_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                //obj.STORE_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                cmdSupplier.Edit(obj);
            }

            alert.Visible = true;
            Response.AppendHeader("Refresh", "2; url=SearchSupplier.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SearchSupplier.aspx");
        }
    }
}