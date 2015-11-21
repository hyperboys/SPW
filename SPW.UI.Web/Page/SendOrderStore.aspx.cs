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
    public partial class SendOrderStore : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)// Edit for Filter
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()// Edit for Filter
        {
            //if (txtStoreCode.Text.Trim() != "" || txtStoreName.Text.Trim() != "")
            //{
                List<STORE> StoreList = cmdStore.GetAllByCondition(txtStoreCode.Text, txtStoreName.Text);
                FillData(StoreList);
                ddlProvince.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
            //}
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
                PrepareSelectedScreen();
            }
            else
            {
                grideInOrder.EmptyDataText = "ไม่พบข้อมูลการเลือกรายการ";
            }
        }

        private void FillData(List<STORE> StoreItems)
        {
            DataSource = cmdOrder.GetAllByStore(StoreItems);
            List<OrderDisplay> OrderDispItems = ManageDisplayItems(DataSource);
            if (OrderDispItems.Count == 0)
            {
                grideInOrder.EmptyDataText = "ไม่พบข้อมูลการค้นหา";
            }
            grideInOrder.DataSource = OrderDispItems;
            grideInOrder.DataBind();
            PrepareSelectedScreen();
        }

        private void PrepareSelectedScreen() 
        {
            List<ORDER> OrderSelected;
            if (Session["OrderSelected"] != null)
            {
                OrderSelected = (List<ORDER>)Session["OrderSelected"];

                foreach (GridViewRow item in grideInOrder.Rows)
                {
                    CheckBox cb = (CheckBox)item.FindControl("check");
                    if (cb != null)
                    {
                        int StoreID = (int)grideInOrder.DataKeys[item.RowIndex].Values[0];
                        cb.Checked = OrderSelected.Any(x => x.STORE_ID == StoreID);
                    }
                }
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

        private void PrepareSearchScreen()
        {
            List<STORE> StoreItems = cmdStore.GetAllIncludeNotCompleteOrder();
            StoreItems.Insert(0, new STORE() { STORE_NAME = "กรุณาเลือกร้าน", STORE_ID = 0 });
            ddlStore.DataSource = StoreItems;
            ddlStore.DataTextField = "STORE_NAME";
            ddlStore.DataValueField = "STORE_ID";
            ddlStore.DataBind();


            List<PROVINCE> ProvinceItems = cmdProvince.GetAll();
            ProvinceItems.Insert(0, new PROVINCE() { PROVINCE_NAME = "กรุณาเลือกจังหวัด", PROVINCE_ID = 0 });
            ddlProvince.DataSource = ProvinceItems;
            ddlProvince.DataTextField = "PROVINCE_NAME";
            ddlProvince.DataValueField = "PROVINCE_ID";
            ddlProvince.DataBind();
        }
       
        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ProvinceSelected = Convert.ToInt32(ddlProvince.SelectedValue);
            if (ProvinceSelected > 0)
            {
                List<STORE> StoreList = cmdStore.GetAllByProvinceID(ProvinceSelected);
                FillData(StoreList);
                txtStoreCode.Text = "";
                txtStoreName.Text = "";
            }
        }

        protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            int StoreSelected = Convert.ToInt32(ddlStore.SelectedValue);
            if (StoreSelected > 0)
            {
                List<STORE> StoreList = cmdStore.GetAllByStoreID(StoreSelected);
                FillData(StoreList);
                txtStoreCode.Text = "";
                txtStoreName.Text = "";
            }
        }

        protected void btnNextStep_Click(object sender, EventArgs e)
        {
            List<ORDER> OrderSelected;
            if (Session["OrderSelected"] != null)
            {
                OrderSelected = (List<ORDER>)Session["OrderSelected"];
            }
            else
            {
                OrderSelected = new List<ORDER>();
            }
            foreach (GridViewRow item in grideInOrder.Rows)
            {
                CheckBox cb = (CheckBox)item.FindControl("check");
                if (cb != null)
                {
                    int StoreID = (int)grideInOrder.DataKeys[item.RowIndex].Values[0];
                    if (cb.Checked)
                    {
                        if (DataSource != null)
                        {
                            OrderSelected.RemoveAll(x => x.STORE_ID == StoreID);
                            OrderSelected.AddRange(DataSource.Where(x => x.STORE_ID == StoreID).ToList());
                        }
                    }
                    else
                    {
                        OrderSelected.RemoveAll(x => x.STORE_ID == StoreID);
                    }
                }                
            }
            Session["OrderSelected"] = OrderSelected;
            if (OrderSelected.Count > 0)
            {
                Response.RedirectPermanent("~/Page/SendOrderStore2.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Warning Message", "<script type='text/javascript'>alert('กรุณาเลือกรายการ');</script>", true);
            }
        }

    }
}

public class OrderDisplay
{
    public int STORE_ID { get; set; }
    public string SECTOR_NAME { get; set; }
    public string PROVINCE_NAME { get; set; }
    public string STORE_CODE { get; set; }
    public string STORE_NAME { get; set; }
    public decimal WEIGHT { get; set; }
    public decimal TOTAL { get; set; }
}