using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;

namespace SPW.UI.Web.Page
{
    public partial class ManageInvoice : System.Web.UI.Page
    {
        #region Declare variable
        private DataServiceEngine _dataServiceEngine;
        private class DATAGRID
        {
            public int ORDER_ID { get; set; }
            public string ORDER_CODE { get; set; }
            public Nullable<System.DateTime> ORDER_DATE { get; set; }
            public string SECTOR_NAME { get; set; }
            public string PROVINCE_NAME { get; set; }
            public string STORE_CODE { get; set; }
            public string STORE_NAME { get; set; }
            public Nullable<decimal> ORDER_TOTAL { get; set; }
            public string ORDER_STEP { get; set; }
        }
        #endregion

        #region Sevice control
        private AssetTypeService _assetTypeService;
        private VehicleService _vehicleService;
        private SupplierService _supplierService;
        private APVehicleTransService _apVehicleTransService;

        private void InitialPage()
        {
            CreatePageEngine();
            ReloadDatasource();
            InitialData();
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

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void InitialDataService()
        {
            _assetTypeService = (AssetTypeService)_dataServiceEngine.GetDataService(typeof(AssetTypeService));
            _vehicleService = (VehicleService)_dataServiceEngine.GetDataService(typeof(VehicleService));
            _supplierService = (SupplierService)_dataServiceEngine.GetDataService(typeof(SupplierService));
            _apVehicleTransService = (APVehicleTransService)_dataServiceEngine.GetDataService(typeof(APVehicleTransService));
        }

        private void ReloadDatasource()
        {
            List<ASSET_TYPE> listAssetType = _assetTypeService.GetAll();
            ViewState["listAssetType"] = listAssetType;

            List<VEHICLE> listVehicle = _vehicleService.GetAll();
            ViewState["listVehicle"] = listVehicle;
        }

        private void InitialData()
        {
            List<ASSET_TYPE> listAssetType = (List<ASSET_TYPE>)ViewState["listAssetType"];
            listAssetType.ForEach(item => ddlAssetType.Items.Add(new ListItem(item.ASSET_TYPE_NAME, item.ASSET_TYPE_ID.ToString())));

            List<VEHICLE> listVehicle = (List<VEHICLE>)ViewState["listVehicle"];
            listVehicle.ForEach(item => ddlVehicle.Items.Add(new ListItem(item.VEHICLE_REGNO, item.VEHICLE_ID.ToString())));

            lblUser.Text = ((USER)Session["user"]).USER_NAME;
            pnl.Visible = false;


            if (Request.QueryString["AP_VEHICLE_TRANS_ID"] != null)
            {
                AP_VEHICLE_TRANS apTrans = _apVehicleTransService.Select(int.Parse(Request.QueryString["AP_VEHICLE_TRANS_ID"].ToString()));
                if (apTrans != null)
                {
                    VENDOR vendor = _supplierService.Select(apTrans.VENDOR_ID);
                    ddlAssetType.SelectedValue = apTrans.ASSET_TYPE_ID.ToString();
                    ddlVehicle.SelectedValue = apTrans.VEHICLE_ID.ToString();
                    txtVendorName.Text = vendor.VENDOR_NAME;
                    txtVendorCode.Text = vendor.VENDOR_CODE;
                    txtMileNo.Text = apTrans.MILE_NO.ToString();
                    txtStartDate.Text = apTrans.MA_START_DATE.ToString("dd/MM/yyyy");
                    txtEndDate.Text = apTrans.MA_FINISH_DATE.ToString("dd/MM/yyyy");
                    txtAmt.Text = apTrans.MA_AMOUNT.ToString();
                    txtReason.Text = apTrans.MA_DESC;
                    lblUser.Text = apTrans.CREATE_EMPLOYEE_ID.ToString();
                    btnShow.Visible = false;
                    btnSave.Visible = false;
                }
            }
        }
        private bool SaveData()
        {
            bool returnValue = true;

            try
            {
                AP_VEHICLE_TRANS data = new AP_VEHICLE_TRANS();
                data.AP_VEHICLE_TRANS_ID = _apVehicleTransService.GetNextTransID();
                data.AP_VEHICLE_TRANS_DATE = DateTime.Now;
                data.ASSET_TYPE_ID = int.Parse(ddlAssetType.SelectedValue);
                data.VEHICLE_ID = int.Parse(ddlVehicle.SelectedValue);
                data.VEHICLE_CODE = _vehicleService.GetVehicleCode(int.Parse(ddlVehicle.SelectedValue));
                data.VENDOR_ID = _supplierService.GetVendorID(txtVendorCode.Text);
                data.VENDOR_CODE = txtVendorCode.Text;
                data.MILE_NO = int.Parse(txtMileNo.Text);
                data.MA_START_DATE = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", null);
                data.MA_FINISH_DATE = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", null);
                data.MA_AMOUNT = Decimal.Parse(txtAmt.Text);
                data.PAY_TYPE = null;
                data.PAY_DATE = null;
                data.CREATE_DATE = DateTime.Now;
                data.UPDATE_DATE = DateTime.Now;
                data.CREATE_EMPLOYEE_ID = ((USER)Session["user"]).USER_ID;
                data.UPDATE_EMPLOYEE_ID = ((USER)Session["user"]).USER_ID;
                data.SYE_DEL = false;
                data.MA_DESC = txtReason.Text;
                data.Action = ActionEnum.Create;

                _apVehicleTransService.Add(data);
            }
            catch (Exception ex)
            {
                returnValue = false;
            }


            return returnValue;
        }
        private bool UpdateData()
        {
            bool returnValue = true;

            try
            {
                AP_VEHICLE_TRANS data = new AP_VEHICLE_TRANS();
                data.AP_VEHICLE_TRANS_ID = int.Parse(Request.QueryString["AP_VEHICLE_TRANS_ID"].ToString());
                data.AP_VEHICLE_TRANS_DATE = DateTime.Now;
                data.ASSET_TYPE_ID = int.Parse(ddlAssetType.SelectedValue);
                data.VEHICLE_ID = int.Parse(ddlVehicle.SelectedValue);
                data.VEHICLE_CODE = _vehicleService.GetVehicleCode(int.Parse(ddlVehicle.SelectedValue));
                data.VENDOR_ID = _supplierService.GetVendorID(txtVendorCode.Text);
                data.VENDOR_CODE = txtVendorCode.Text;
                data.MILE_NO = int.Parse(txtMileNo.Text);
                data.MA_START_DATE = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", null);
                data.MA_FINISH_DATE = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", null);
                data.MA_AMOUNT = Decimal.Parse(txtAmt.Text);
                data.PAY_TYPE = null;
                data.PAY_DATE = null;
                data.CREATE_DATE = DateTime.Now;
                data.UPDATE_DATE = DateTime.Now;
                data.CREATE_EMPLOYEE_ID = ((USER)Session["user"]).USER_ID;
                data.UPDATE_EMPLOYEE_ID = ((USER)Session["user"]).USER_ID;
                data.SYE_DEL = false;
                data.MA_DESC = txtReason.Text;
                data.Action = ActionEnum.Update;

                _apVehicleTransService.Edit(data);
            }
            catch (Exception ex)
            {
                returnValue = false;
            }


            return returnValue;
        }

        private bool ValidateData()
        {
            bool returnValue = true;
            if (ddlAssetType.SelectedValue == "0" || ddlVehicle.SelectedValue == "0" || txtStartDate.Text == "" || txtEndDate.Text == "" ||
                txtVendorName.Text == "" || txtVendorCode.Text == "" || txtMileNo.Text == "" || txtAmt.Text == "")
            {
                returnValue = false;
            }
            else
            {
                int i;
                Decimal dc;
                if (int.TryParse(txtMileNo.Text, out i) && Decimal.TryParse(txtAmt.Text, out dc))
                {
                    returnValue = true;
                }
                else
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }

        #endregion

        #region ASP control
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
        protected void btnShow_Click(object sender, EventArgs e)
        {
            modalSearch.Show();
        }
        protected void btnModalSearch_Click(object sender, EventArgs e)
        {
            gridSupplier.DataSource = _supplierService.GetAll(txtModalCode.Text,txtModalName.Text);
            gridSupplier.DataBind();
        }
        protected void btnModalClose_Click(object sender, EventArgs e)
        {
            modalSearch.Hide();
        }
        protected void btnModalSave_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridSupplier.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    RadioButton RowSelector = (RadioButton)gridSupplier.Rows[row.RowIndex].FindControl("RowSelector");
                    Label lblVENDOR_CODE = (Label)gridSupplier.Rows[row.RowIndex].FindControl("lblVENDOR_CODE");
                    Label lblVENDOR_NAME = (Label)gridSupplier.Rows[row.RowIndex].FindControl("lblVENDOR_NAME");
                    if (RowSelector.Checked)
                    {
                        txtVendorCode.Text = lblVENDOR_CODE.Text;
                        txtVendorName.Text = lblVENDOR_NAME.Text;
                    }
                }
            }
            modalSearch.Hide();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool returnValue = false;
            try
            {
                if (ValidateData())
                {
                    returnValue = SaveData();
                }
                else
                {
                    lblError.Text = "กรุณากรอกข้อมูลให้ถูกต้อง";
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }

            if (returnValue)
            {
                btnSave.Enabled = false;
                btnSave.Visible = false;
                pnl.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='ManageInvoice.aspx';},2000);", true);
            }
            else
            {
                lblError.Text = "พบข้อผิดพลาดในการบันทึก";
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            bool returnValue = false;
            try
            {
                if (ValidateData())
                {
                    returnValue = UpdateData();
                }
                else
                {
                    lblError.Text = "กรุณากรอกข้อมูลให้ถูกต้อง";
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }

            if (returnValue)
            {
                btnSave.Enabled = false;
                btnSave.Visible = false;
                pnl.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='SearchInvoice.aspx';},2000);", true);
            }
            else
            {
                lblError.Text = "พบข้อผิดพลาดในการบันทึก";
            }
        }
        #endregion

    }
}