using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.Model;
using SPW.UI.Web.Reports;
using System.Data;
using SPW.DataService;

namespace SPW.UI.Web.Page
{
    public partial class SendOrderStore3 : System.Web.UI.Page
    {

        private DataServiceEngine _dataServiceEngine;
        private VehicleService cmdVehicle;
        private StoreService cmdStore;
        public List<ORDER> DataSource
        {
            get
            {
                var list = (List<ORDER>)ViewState["listStore"];
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
                PrepareSearchScreen();
                PrepareDefaultScreen();
            }
            else
            {
                ReloadPageEngine();
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
            cmdVehicle = (VehicleService)_dataServiceEngine.GetDataService(typeof(VehicleService));
            cmdStore = (StoreService)_dataServiceEngine.GetDataService(typeof(StoreService));
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

        private void PrepareDefaultScreen()
        {
            List<ORDER> OrderSelected;
            if (Session["OrderSelected"] != null)
            {
                OrderSelected = (List<ORDER>)Session["OrderSelected"];
                List<OrderDisplay> OrderDispItems = ManageDisplayItems(OrderSelected);
                if (OrderDispItems.Count == 0)
                {
                    grideInOrder.EmptyDataText = "ไม่พบข้อมูลการเลือกรายการ";
                }
                grideInOrder.DataSource = OrderDispItems;
                grideInOrder.DataBind();
            }
            else
            {
                grideInOrder.EmptyDataText = "ไม่พบข้อมูลการเลือกรายการ";
            }
            CalculateSummaryScreen();
        }

        private void CalculateSummaryScreen()
        {
            List<ORDER> OrderSelected;
            if (Session["OrderSelected"] != null)
            {
                OrderSelected = (List<ORDER>)Session["OrderSelected"];

                lb_TotalAmount.Text = String.Format("ราคารวม  : {0:C} บาท", ((decimal)OrderSelected.Sum(x => (decimal)x.ORDER_DETAIL.Where(z => z.IS_FREE == "N").Sum(z => z.PRODUCT_PRICE * z.PRODUCT_SEND_REMAIN))).ToString("n2"));
                lb_TotalWeight.Text = String.Format("น้ำหนักรวม : {0:C} กก.", ((decimal)OrderSelected.Sum(x => (decimal)x.ORDER_DETAIL.Sum(z => z.PRODUCT_WEIGHT * z.PRODUCT_SEND_REMAIN))).ToString("n2"));
                lb_TotalAmount.Visible = true;
                lb_TotalWeight.Visible = true;
                if (OrderSelected.Count == 0)
                {
                    lb_TotalAmount.Visible = false;
                    lb_TotalWeight.Visible = false;
                }
            }
            else
            {
                lb_TotalAmount.Visible = false;
                lb_TotalWeight.Visible = false;
            }
        }
        private List<OrderDisplay> ManageDisplayItems(List<ORDER> OrderItems)
        {
            List<OrderDisplay> OrderDispItems = new List<OrderDisplay>();
            foreach (var item in OrderItems)
            {
                OrderDisplay objOrderDisplay = OrderDispItems.FirstOrDefault(x => x.STORE_ID == item.STORE_ID);
                if (objOrderDisplay != null)
                {
                    objOrderDisplay.WEIGHT += (decimal)item.ORDER_DETAIL.Sum(x => x.PRODUCT_SEND_REMAIN * x.PRODUCT_WEIGHT);
                    objOrderDisplay.TOTAL += (decimal)item.ORDER_DETAIL.Where(x => x.IS_FREE == "N").Sum(x => x.PRODUCT_SEND_REMAIN * x.PRODUCT_PRICE);
                }
                else
                {
                    objOrderDisplay = new OrderDisplay();
                    STORE objStore = cmdStore.GetStoreIndex(item.STORE_ID);
                    objOrderDisplay.STORE_ID = objStore.STORE_ID;
                    objOrderDisplay.SECTOR_NAME = objStore.SECTOR.SECTOR_NAME;
                    objOrderDisplay.PROVINCE_NAME = objStore.PROVINCE.PROVINCE_NAME;
                    objOrderDisplay.STORE_CODE = objStore.STORE_CODE;
                    objOrderDisplay.STORE_NAME = objStore.STORE_NAME;
                    objOrderDisplay.WEIGHT = (decimal)item.ORDER_DETAIL.Sum(x => x.PRODUCT_SEND_REMAIN * x.PRODUCT_WEIGHT);
                    objOrderDisplay.TOTAL = (decimal)item.ORDER_DETAIL.Where(x => x.IS_FREE == "N").Sum(x => x.PRODUCT_SEND_REMAIN * x.PRODUCT_PRICE);
                    OrderDispItems.Add(objOrderDisplay);
                }
            }

            return OrderDispItems;
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            int VehicleSelected = Convert.ToInt32(ddlVehicle.SelectedValue.ToString());
            bool isValid = false;
            if (Session["OrderSelected"] != null)
            {
                isValid = ((List<ORDER>)Session["OrderSelected"]).Count > 0;
            }
            if (VehicleSelected > 0 && isValid)
            {
                Session["VehicleSelected"] = VehicleSelected;
                Response.RedirectPermanent("~/Page/SendOrderStore4.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Warning Message", "<script>alert('กรุณาเลือกรถ');</script>", true);
            }
        }

    }
}