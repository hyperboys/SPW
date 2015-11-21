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
    public partial class SendOrderStore2 : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private StoreService cmdStore;
        private ProvinceService cmdProvince;
        private OrderService cmdOrder;
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
            cmdStore = (StoreService)_dataServiceEngine.GetDataService(typeof(StoreService));
            cmdProvince = (ProvinceService)_dataServiceEngine.GetDataService(typeof(ProvinceService));
            cmdOrder = (OrderService)_dataServiceEngine.GetDataService(typeof(OrderService));
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }


        protected void grideInOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DelStore")
            {
                int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                int StoreID = Convert.ToInt32(grideInOrder.DataKeys[RowIndex].Values[0].ToString());
                if (Session["OrderSelected"] != null)
                {
                    List<ORDER> OrderSelected = (List<ORDER>)Session["OrderSelected"];
                    OrderSelected.RemoveAll(x => x.STORE_ID == StoreID);
                    PrepareDefaultScreen();
                }
            }
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
            bool isValid = false;
            if (Session["OrderSelected"] != null)
            {
                isValid = ((List<ORDER>)Session["OrderSelected"]).Count > 0;
            }
            if (isValid)
            {
                Response.RedirectPermanent("~/Page/SendOrderStore3.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Warning Message", "<script>alert('กรุณาเลือกรายการ');</script>", true);
            }
        }

    }
}