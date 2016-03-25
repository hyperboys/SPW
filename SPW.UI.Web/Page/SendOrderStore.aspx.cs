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
using SPW.DAL;
using SPW.Common;

namespace SPW.UI.Web.Page
{
    public partial class SendOrderStore : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private StoreService cmdStore;
        private ProvinceService cmdProvince;
        private OrderService cmdOrder;
        private TransportLineService _transpotService;

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

        public List<PROVINCE> listProvince
        {
            get
            {
                if (ViewState["listProvince"] == null)
                {
                    ViewState["listProvince"] = new List<PROVINCE>();
                }
                var list = (List<PROVINCE>)ViewState["listProvince"];
                return list;
            }
            set
            {
                ViewState["listProvince"] = value;
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
            _transpotService = (TransportLineService)_dataServiceEngine.GetDataService(typeof(TransportLineService));
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
                PrepareSearchScreen();
                PrepareDefaultScreen();
                AutoCompleteStoreName();
                AutoCompleteStoreCode();
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

        private void SearchGrid()
        {
            List<STORE> StoreList = cmdStore.GetAllByCondition(txtStoreCode.Text, txtStoreName.Text);
            if (ddlTranspot.SelectedValue != "0")
            {
                List<int> listTrans = _transpotService.SelectListStoreID(Convert.ToInt32(ddlTranspot.SelectedValue));
                StoreList = StoreList.Where(x => listTrans.Contains(x.STORE_ID)).ToList();
            }

            if (listProvince.Count() > 0)
            {
                List<int> listTrans = listProvince.Select(x => x.PROVINCE_ID).ToList();
                StoreList = StoreList.Where(x => listTrans.Contains(x.PROVINCE_ID)).ToList();
            }

            //if (ddlProvince.SelectedValue != "0")
            //{
            //    StoreList = StoreList.Where(x => x.PROVINCE_ID == Convert.ToInt32(ddlProvince.SelectedValue)).ToList();
            //}

            FillData(StoreList);
            //ddlProvince.SelectedIndex = 0;
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
            //List<PROVINCE> ProvinceItems = cmdProvince.GetAll();
            //ProvinceItems.Insert(0, new PROVINCE() { PROVINCE_NAME = "กรุณาเลือกจังหวัด", PROVINCE_ID = 0 });
            //ddlProvince.DataSource = ProvinceItems;
            //ddlProvince.DataTextField = "PROVINCE_NAME";
            //ddlProvince.DataValueField = "PROVINCE_ID";
            //ddlProvince.DataBind();


            SQLUtility sql = new SQLUtility();
            Dictionary<string, string> listTrans = sql.SelectDistinc("TRANSPORT_LINE", new string[] { "TRANS_LINE_ID", "TRANS_LINE_NAME" }, new string[] { "TRANS_LINE_ID", "TRANS_LINE_NAME" });
            foreach (KeyValuePair<string, string> entry in listTrans)
            {
                ddlTranspot.Items.Add(new ListItem(entry.Value, entry.Key));
            }
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int ProvinceSelected = Convert.ToInt32(ddlProvince.SelectedValue);
            //if (ProvinceSelected > 0)
            //{
            //    List<STORE> StoreList = cmdStore.GetAllByProvinceID(ProvinceSelected);
            //    FillData(StoreList);
            //    txtStoreCode.Text = "";
            //    txtStoreName.Text = "";
            //}
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

        private void AutoCompleteStoreName()
        {
            List<string> nameList = SearchAutoCompleteDataService.Search("STORE", "STORE_NAME", "STORE_NAME", "");
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
            txtStoreName.Attributes.Add("data-source", str);
        }

        private void AutoCompleteStoreCode()
        {
            List<string> nameList = SearchAutoCompleteDataService.Search("STORE", "STORE_CODE", "STORE_CODE", "");
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
            txtStoreCode.Attributes.Add("data-source", str);
        }

        private void AutoCompleteProvince()
        {
            List<string> nameList = SearchAutoCompleteDataService.Search("PROVINCE", "PROVINCE_NAME", "PROVINCE_NAME", "");
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


        private void textChange()
        {

            if (txtProvince.Text != "")
            {
                List<PROVINCE> tmpToGrid = new List<PROVINCE>();
                List<PROVINCE> tmpListProvince = cmdProvince.GetAllLike(txtProvince.Text);
                if (tmpListProvince != null && tmpListProvince.Count > 0 && listProvince.Count() > 0)
                {
                    if (tmpListProvince.Count() == 5)
                    {
                        tmpListProvince = tmpListProvince.Skip(listProvince.Count()).ToList();
                    }
                    tmpToGrid.AddRange(listProvince);
                    tmpToGrid.AddRange(tmpListProvince);
                }
                else if (tmpListProvince.Count > 0)
                {
                    tmpToGrid.AddRange(tmpListProvince);
                }
                else if (listProvince.Count > 0)
                {
                    tmpToGrid.AddRange(listProvince);
                }
                grdProvince.DataSource = tmpToGrid;
                grdProvince.DataBind();
                for (int index = 0; index < listProvince.Count(); index++)
                {
                    if (listProvince[index].PROVINCE_NAME == grdProvince.Rows[index].Cells[2].Text)
                    {
                        CheckBox checkBox = (CheckBox)grdProvince.Rows[index].FindControl("check");
                        checkBox.Checked = true;
                    }
                    else 
                    {
                        CheckBox checkBox = (CheckBox)grdProvince.Rows[index].FindControl("check");
                        checkBox.Checked = false;
                    }
                }
                txtProvince.Focus();
            }
        }

        protected void txtProvince_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textChange();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        protected void grdProvince_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                listProvince.RemoveAll(x => x.PROVINCE_NAME == grdProvince.Rows[e.RowIndex].Cells[2].Text);
                textChange();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        protected void chkview_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            int index = row.RowIndex;
            CheckBox checkBox = (CheckBox)grdProvince.Rows[index].FindControl("check");
            if (checkBox.Checked)
            {
                PROVINCE temp = new PROVINCE();
                temp.PROVINCE_NAME = grdProvince.Rows[index].Cells[2].Text;
                temp.PROVINCE_ID = cmdProvince.GetID(temp.PROVINCE_NAME);
                listProvince.Add(temp);
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