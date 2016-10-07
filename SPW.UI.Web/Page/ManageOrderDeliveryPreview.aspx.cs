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
    public partial class ManageOrderDeliveryPreview : System.Web.UI.Page
    {

        private DataServiceEngine _dataServiceEngine;
        private VehicleService cmdVehicle;
        private StoreService cmdStore;
        private DeliveryIndexService cmdDeliveryOrderIndex;
        private DeliveryIndexDetailsService cmdDelOrderIndexDetails;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CreatePageEngine();
                PrepareDefaultScreen();

                Session["DelOrderIndexSelected"] = null;
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
            cmdDeliveryOrderIndex = (DeliveryIndexService)_dataServiceEngine.GetDataService(typeof(DeliveryIndexService));
            cmdDelOrderIndexDetails = (DeliveryIndexDetailsService)_dataServiceEngine.GetDataService(typeof(DeliveryIndexDetailsService));
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void PrepareDefaultScreen()
        {
            if (Request.QueryString["delID"] != null)
            {
                int delID = Convert.ToInt32(Request.QueryString["delID"].ToString());
                DELIVERY_INDEX objDEl = cmdDeliveryOrderIndex.Select(delID);
                List<DELIVERY_INDEX_DETAIL> DetailItems = cmdDelOrderIndexDetails.GetAllIncludeByIndex(delID);
                DetailItems.RemoveAll(x => x.PRODUCT_SENT_QTY <= 0);
                if (DetailItems.Count == 0)
                {
                    grideInOrder.EmptyDataText = "ไม่พบข้อมูลการเลือกรายการ";
                }
                grideInOrder.DataSource = DetailItems;
                grideInOrder.DataBind();
                txtVehicle.Text = objDEl.VEHICLE.VEHICLE_REGNO.ToString();
                lb_TotalAmount.Text = String.Format("ราคารวม  : {0:C} บาท", ((decimal)objDEl.DELIVERY_INDEX_DETAIL.Where(x=> x.IS_FREE == "N").Sum(x => x.PRODUCT_PRICE_TOTAL)).ToString("n2"));
                lb_TotalWeight.Text = String.Format("น้ำหนักรวม : {0:C} กก.", ((decimal)objDEl.DELIVERY_INDEX_DETAIL.Sum(x => x.PRODUCT_WEIGHT_TOTAL)).ToString("n2"));
            }
            else
            {
                grideInOrder.EmptyDataText = "ไม่พบข้อมูลการเลือกรายการ";
            }
        }

        protected void btnNext_Click1(object sender, EventArgs e)
        {
            Response.RedirectPermanent("~/Page/ManageOrderDelivery.aspx");
        }
    }
}