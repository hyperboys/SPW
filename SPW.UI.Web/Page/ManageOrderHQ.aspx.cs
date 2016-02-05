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
    public partial class ManageOrderHQ : System.Web.UI.Page
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
        private class DROPDOWN
        {
            public string STORE_NAME { get; set; }
            public int STORE_ID { get; set; }
            public string PROVINCE_NAME { get; set; }
            public int PROVINCE_ID { get; set; }
            public string SECTOR_NAME { get; set; }
            public int SECTOR_ID { get; set; }
        }
        //public List<DATAGRID> DataSouce
        //{
        //    get
        //    {
        //        return (List<DATAGRID>)ViewState["DATAGRID"];
        //    }
        //    set
        //    {
        //        ViewState["DATAGRID"] = value;
        //    }
        //}
        #endregion

        #region Sevice control
        private SectorService _sectorService;
        private StoreService _storeService;
        private ProvinceService _provinceService;
        private OrderService _orderService;

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
            _sectorService = (SectorService)_dataServiceEngine.GetDataService(typeof(SectorService));
            _storeService = (StoreService)_dataServiceEngine.GetDataService(typeof(StoreService));
            _provinceService = (ProvinceService)_dataServiceEngine.GetDataService(typeof(ProvinceService));
            _orderService = (OrderService)_dataServiceEngine.GetDataService(typeof(OrderService));
        }

        private void ReloadDatasource()
        {
            List<SECTOR> listSector = _sectorService.GetAll();
            ViewState["listSector"] = listSector;

            List<STORE> listStore = _storeService.GetAll();
            ViewState["listStore"] = listStore;

            List<PROVINCE> listProvince = _provinceService.GetAll();
            ViewState["listProvince"] = listProvince;
        }

        private void InitialData()
        {
            ddlStatus.Items.Clear();
            ddlStatus.Items.Insert(0, new ListItem("กรุณาเลือก", "0"));
            ddlStatus.Items.Insert(1, new ListItem("ยกเลิก", "12"));
            ddlStatus.Items.Insert(2, new ListItem("สำเร็จ", "11"));
            ddlStatus.Items.Insert(3, new ListItem("ไม่สำเร็จ", "10"));

            ddlStatus.SelectedValue = "10";

            List<SECTOR> listSector = (List<SECTOR>)ViewState["listSector"];
            listSector.ForEach(item => ddlSector.Items.Add(new ListItem(item.SECTOR_NAME, item.SECTOR_ID.ToString())));

            List<STORE> listStore = (List<STORE>)ViewState["listStore"];
            listStore.ForEach(item => ddlStore.Items.Add(new ListItem(item.STORE_NAME, item.STORE_ID.ToString())));

            List<PROVINCE> listProvince = (List<PROVINCE>)ViewState["listProvince"];
            listProvince.ForEach(item => ddlProvince.Items.Add(new ListItem(item.PROVINCE_NAME, item.PROVINCE_ID.ToString())));
            //if (Session["DATAGRID"] != null)
            //{
            //    gdvManageOrderHQ.DataSource = (List<DATAGRID>)Session["DATAGRID"];
            //    gdvManageOrderHQ.DataBind();
            //}
            BindGridview();
        }

        private void BindGridview()
        {
            List<SECTOR> listSector = (List<SECTOR>)ViewState["listSector"];
            List<PROVINCE> listProvince = (List<PROVINCE>)ViewState["listProvince"];
            List<ORDER> listOrder = _orderService.GetStoreInOrder().OrderBy(x=>x.ORDER_STEP).ThenBy(y=>y.ORDER_DATE).ToList();
            List<STORE> listStore = _storeService.GetAll();

            List<DATAGRID> query = (from order in listOrder
                                    join province in listProvince on order.STORE.PROVINCE_ID equals province.PROVINCE_ID into joinA
                                    from x in joinA.DefaultIfEmpty()
                                    join sector in listSector on x.SECTOR_ID equals sector.SECTOR_ID into joinB
                                    from y in joinB.DefaultIfEmpty()
                                    join store in listStore on order.STORE.STORE_ID equals store.STORE_ID into joinC
                                    from z in joinC.DefaultIfEmpty()
                                    where order.STORE_ID.Equals((ddlStore.SelectedValue == "0" ? order.STORE_ID : int.Parse(ddlStore.SelectedValue))) &&
                                        x.PROVINCE_ID.Equals((ddlProvince.SelectedValue == "0" ? x.PROVINCE_ID : int.Parse(ddlProvince.SelectedValue))) &&
                                        z.STORE_CODE.Equals((txtStoreCode.Text == "" ? z.STORE_CODE : txtStoreCode.Text)) &&
                                        y.SECTOR_ID.Equals((ddlSector.SelectedValue == "0" ? y.SECTOR_ID : int.Parse(ddlSector.SelectedValue))) &&
                                        order.ORDER_STEP.Equals((ddlStatus.SelectedValue == "0" ? order.ORDER_STEP : ddlStatus.SelectedValue)) &&
                                        isBetweenDate((DateTime)order.ORDER_DATE, (string.IsNullOrEmpty(txtStartDate.Text) ? (DateTime)order.ORDER_DATE : DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US"))), (string.IsNullOrEmpty(txtEndDate.Text) ? (DateTime)order.ORDER_DATE : DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US"))))
                                    select new DATAGRID
                                    {
                                        ORDER_ID = order.ORDER_ID,
                                        ORDER_CODE = order.ORDER_CODE,
                                        ORDER_DATE = order.ORDER_DATE,
                                        SECTOR_NAME = y.SECTOR_NAME,
                                        PROVINCE_NAME = x.PROVINCE_NAME,
                                        STORE_CODE = order.STORE.STORE_CODE,
                                        STORE_NAME = order.STORE.STORE_NAME,
                                        ORDER_TOTAL = order.ORDER_TOTAL,
                                        ORDER_STEP = order.ORDER_STEP
                                    }).ToList();
            Session["DATAGRID"] = query;
            //gdvManageOrderHQ.DataSource = null;
            gdvManageOrderHQ.DataSource = query;
            gdvManageOrderHQ.DataBind();
        }
        public static bool isBetweenDate(DateTime input, DateTime start, DateTime end)
        {
            return (input >= start && input <= end);
        }
        private List<DROPDOWN> GetDropdownChanged(int SECTOR_ID)
        {
            List<STORE> listStore = (List<STORE>)ViewState["listStore"];
            List<PROVINCE> listProvince = (List<PROVINCE>)ViewState["listProvince"];
            List<SECTOR> listSector = (List<SECTOR>)ViewState["listSector"];

            List<DROPDOWN> query = (from store in listStore
                                    join province in listProvince on store.PROVINCE_ID equals province.PROVINCE_ID
                                    join sector in listSector on province.SECTOR_ID equals sector.SECTOR_ID
                                    where sector.SECTOR_ID.Equals(SECTOR_ID)
                                    select new DROPDOWN
                                    {
                                        STORE_NAME = store.STORE_NAME,
                                        STORE_ID = store.STORE_ID,
                                        PROVINCE_NAME = province.PROVINCE_NAME,
                                        PROVINCE_ID = province.PROVINCE_ID,
                                        SECTOR_NAME = sector.SECTOR_NAME,
                                        SECTOR_ID = sector.SECTOR_ID
                                    }).ToList();

            return query;
        }
        private List<DROPDOWN> GetDropdownChanged(int SECTOR_ID, int PROVINCE_ID)
        {
            List<STORE> listStore = (List<STORE>)ViewState["listStore"];
            List<PROVINCE> listProvince = (List<PROVINCE>)ViewState["listProvince"];
            List<SECTOR> listSector = (List<SECTOR>)ViewState["listSector"];

            List<DROPDOWN> query = (from store in listStore
                                    join province in listProvince on store.PROVINCE_ID equals province.PROVINCE_ID
                                    join sector in listSector on province.SECTOR_ID equals sector.SECTOR_ID
                                    where sector.SECTOR_ID.Equals(SECTOR_ID) && province.PROVINCE_ID.Equals(PROVINCE_ID)
                                    select new DROPDOWN
                                    {
                                        STORE_NAME = store.STORE_NAME,
                                        STORE_ID = store.STORE_ID,
                                        PROVINCE_NAME = province.PROVINCE_NAME,
                                        PROVINCE_ID = province.PROVINCE_ID,
                                        SECTOR_NAME = sector.SECTOR_NAME,
                                        SECTOR_ID = sector.SECTOR_ID
                                    }).ToList();

            return query;
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridview();
        }
        protected void gdvManageOrderHQ_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ORDER_ID = int.Parse(gdvManageOrderHQ.DataKeys[e.RowIndex].Value.ToString());
            _orderService.EditOrderStepCancel(ORDER_ID);
            BindGridview();
        }
        protected void ddlSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<DROPDOWN> query = GetDropdownChanged(int.Parse(ddlSector.SelectedValue));

            var p = query.Select(j => new { j.PROVINCE_NAME, j.PROVINCE_ID }).ToList().GroupBy(k => new { k.PROVINCE_NAME, k.PROVINCE_ID }).ToList();
            if (p.Count > 0)
            {
                ddlProvince.Items.Clear();
                ddlProvince.Items.Insert(0, new ListItem("กรุณาเลือก", "0"));
                foreach (var item in p)
                {
                    ddlProvince.Items.Add(new ListItem(item.Key.PROVINCE_NAME, item.Key.PROVINCE_ID.ToString()));
                }
            }

            var s = query.Select(j => new { j.STORE_NAME, j.STORE_ID }).ToList().GroupBy(k => new { k.STORE_NAME, k.STORE_ID }).ToList();
            if (s.Count > 0)
            {
                ddlStore.Items.Clear();
                ddlStore.Items.Insert(0, new ListItem("กรุณาเลือก", "0"));
                foreach (var item in s)
                {
                    ddlStore.Items.Add(new ListItem(item.Key.STORE_NAME, item.Key.STORE_ID.ToString()));
                }
            }

        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<DROPDOWN> query = GetDropdownChanged(int.Parse(ddlSector.SelectedValue), int.Parse(ddlProvince.SelectedValue));

            var s = query.Select(j => new { j.STORE_NAME, j.STORE_ID }).ToList().GroupBy(k => new { k.STORE_NAME, k.STORE_ID }).ToList();
            if (s.Count > 0)
            {
                ddlStore.Items.Clear();
                ddlStore.Items.Insert(0, new ListItem("กรุณาเลือก", "0"));
                foreach (var item in s)
                {
                    ddlStore.Items.Add(new ListItem(item.Key.STORE_NAME, item.Key.STORE_ID.ToString()));
                }
            }
        }
        protected void gdvManageOrderHQ_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ORDER_ID = gdvManageOrderHQ.DataKeys[e.Row.RowIndex][0].ToString();
                string STORE_CODE = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "STORE_CODE"));
                string STORE_NAME = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "STORE_NAME"));
                LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton lbtnDetail = (LinkButton)e.Row.FindControl("lbtnDetail");
                HiddenField hfORDER_STEP = (HiddenField)e.Row.FindControl("hfORDER_STEP");
                lbtnDetail.Attributes["href"] = "ManageOrderHQDetail.aspx?ORDER_ID=" + ORDER_ID + "&STORE_CODE=" + STORE_CODE + "&STORE_NAME=" + STORE_NAME;
                lbtnEdit.Attributes["href"] = "ManageOrderHQEdit.aspx?ORDER_ID=" + ORDER_ID + "&STORE_CODE=" + STORE_CODE + "&STORE_NAME=" + STORE_NAME;

                if (hfORDER_STEP.Value == "11")
                    e.Row.BackColor = Color.LightGreen;

                if (hfORDER_STEP.Value != "10" && hfORDER_STEP.Value != "11")
                {
                    e.Row.BackColor = Color.Moccasin;
                }
            }
        }
        #endregion
    }
}