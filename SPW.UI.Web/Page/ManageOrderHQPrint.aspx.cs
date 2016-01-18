using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using SPW.UI.Web.Reports;

namespace SPW.UI.Web.Page
{
    public partial class ManageOrderHQPrint : System.Web.UI.Page
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
        #endregion

        #region Sevice control
        private StoreService _storeService;
        private OrderService _orderService;
        private ProvinceService _provinceService;
        private OrderDetailService _orderDetailService;

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
            _storeService = (StoreService)_dataServiceEngine.GetDataService(typeof(StoreService));
            _orderService = (OrderService)_dataServiceEngine.GetDataService(typeof(OrderService));
            _orderDetailService = (OrderDetailService)_dataServiceEngine.GetDataService(typeof(OrderDetailService));
            _provinceService = (ProvinceService)_dataServiceEngine.GetDataService(typeof(ProvinceService));
        }

        private void ReloadDatasource()
        {
            List<STORE> listStore = _storeService.GetAll();
            ViewState["listStore"] = listStore;

            List<PROVINCE> listProvince = _provinceService.GetAll();
            ViewState["listProvince"] = listProvince;
        }

        private void InitialData()
        {
            List<PROVINCE> listProvince = (List<PROVINCE>)ViewState["listProvince"];
            listProvince.ForEach(item => ddlProvince.Items.Add(new ListItem(item.PROVINCE_NAME, item.PROVINCE_ID.ToString())));
        }
        public static bool isBetweenDate(DateTime input, DateTime start, DateTime end)
        {
            return (input >= start && input <= end);
        }
        #endregion

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
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            List<STORE> listStore = new List<STORE>();
            listStore = _storeService.GetAllIncludeOrder().Where(x => x.ORDER.Where(y => y.ORDER_STEP == "11" || y.ORDER_STEP == "20").FirstOrDefault() != null).ToList();
            listStore = listStore.Where(x =>
                x.ORDER.Any(y => isBetweenDate((DateTime)y.ORDER_DATE, (string.IsNullOrEmpty(txtStartDate.Text) ? (DateTime)y.ORDER_DATE : DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US"))), (string.IsNullOrEmpty(txtEndDate.Text) ? (DateTime)y.ORDER_DATE : DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-US"))))) &&
                x.STORE_CODE.ToUpper() == (txtStoreCode.Text == "" ? x.STORE_CODE.ToUpper():txtStoreCode.Text.ToUpper()) &&
                x.PROVINCE_ID == (ddlProvince.SelectedValue == "0" ? x.PROVINCE_ID : int.Parse(ddlProvince.SelectedValue))).Distinct().ToList();

            List<InOrderForPrint> listInOrder = new List<InOrderForPrint>();
            List<ORDER> tmpListOrder = new List<ORDER>();
            foreach (STORE tmp in listStore)
            {
                InOrderForPrint inOrder = new InOrderForPrint();
                inOrder.Store = tmp;
                inOrder.OrderDetails = new List<ORDER_DETAIL>();
                tmpListOrder = _orderService.GetAllIncludeByStore(tmp.STORE_ID);
                foreach (ORDER tmpOrder in tmpListOrder)
                {
                    inOrder.Order = tmpOrder;
                    foreach (ORDER_DETAIL tmpOD in _orderDetailService.GetAllIncludeByOrder(tmpOrder.ORDER_ID).ToList())
                    {
                        if (tmpOD.PRODUCT_SEND_REMAIN.Value > 0)
                        {
                            if (inOrder.OrderDetails.Where(x => x.PRODUCT_ID == tmpOD.PRODUCT_ID && x.IS_FREE == tmpOD.IS_FREE).FirstOrDefault() != null)
                            {
                                ORDER_DETAIL odItem = inOrder.OrderDetails.Where(x => x.PRODUCT_ID == tmpOD.PRODUCT_ID
                                    && x.IS_FREE == tmpOD.IS_FREE).FirstOrDefault();
                                odItem.PRODUCT_QTY += tmpOD.PRODUCT_QTY;
                                odItem.ColorDesc += tmpOD.COLOR_TYPE.COLOR_TYPE_SUBNAME + " " + tmpOD.COLOR.COLOR_SUBNAME + " " + tmpOD.PRODUCT_SEND_REMAIN + " ";
                                odItem.PRODUCT_SEND_QTY += tmpOD.PRODUCT_SEND_QTY;
                            }
                            else
                            {
                                tmpOD.ColorDesc += tmpOD.COLOR_TYPE.COLOR_TYPE_SUBNAME + " " + tmpOD.COLOR.COLOR_SUBNAME + " " + tmpOD.PRODUCT_SEND_REMAIN + " ";
                                inOrder.OrderDetails.Add(tmpOD);
                            }
                        }
                    }
                }
                listInOrder.Add(inOrder);
            }

            if (tmpListOrder.Count > 0)
            {
                InOrderReportData ds = new InOrderReportData();
                DataTable dt = ds.Tables["Data"];
                DataTable dt2 = ds.Tables["Data2"];

                List<ListOfLineForPrint> lstHead = new List<ListOfLineForPrint>();
                foreach (InOrderForPrint item in listInOrder)
                {
                    List<LineForPrint> lstLine = new List<LineForPrint>();
                    ListOfLineForPrint tmpList = new ListOfLineForPrint();
                    tmpList.LineForPrint = new List<LineForPrint>();
                    LineForPrint linePrint = new LineForPrint();
                    linePrint.line1 = ConvertDateToThai(item.Order.ORDER_DATE.Value);
                    //if (item.Store.PROVINCE_ID == 1)
                    //{
                    //    linePrint.line2 = item.Store.STORE_NAME + " (" + item.Store.STORE_CODE.Substring(item.Store.STORE_CODE.Length - 3, 3) + " )";
                    //}
                    //else
                    //{
                    //    linePrint.line2 = item.Store.STORE_CODE + " " + item.Store.STORE_NAME;
                    //}
                    linePrint.line2 = item.Store.STORE_NAME + " " + item.Store.STORE_CODE;

                    linePrint.line3 = "จำนวน";
                    linePrint.line4 = "";
                    linePrint.line5 = "ราคาต่อชิ้น";
                    lstLine.Add(linePrint);

                    foreach (ORDER_DETAIL od in item.OrderDetails)
                    {
                        if ((od.PRODUCT_QTY - od.PRODUCT_SEND_QTY) > 0)
                        {
                            LineForPrint linePrintItem = new LineForPrint();
                            linePrintItem.line1 = "";
                            if (od.IS_FREE == "F")
                            {
                                linePrintItem.line2 = "แถม " + od.ColorDesc;
                            }
                            else
                            {
                                linePrintItem.line2 = od.PRODUCT.PRODUCT_NAME + " " + od.ColorDesc;
                            }
                            linePrintItem.line3 = (od.PRODUCT_QTY - od.PRODUCT_SEND_QTY).ToString();
                            linePrintItem.line4 = "";
                            linePrintItem.line5 = od.PRODUCT_PRICE.ToString();
                            lstLine.Add(linePrintItem);
                        }
                    }
                    tmpList.LineForPrint.AddRange(lstLine);
                    lstHead.Add(tmpList);
                }

                DataRow dr1;
                int indexData1 = 0;
                for (int i = 0; i < lstHead.Count; i++)
                {
                    for (int j = 0; j < lstHead[i].LineForPrint.Count; j++)
                    {
                        dr1 = dt.NewRow();
                        dr1["LINEX"] = indexData1++;
                        dr1["LINE1"] = lstHead[i].LineForPrint[j].line1;
                        dr1["LINE2"] = lstHead[i].LineForPrint[j].line2;
                        dr1["LINE3"] = lstHead[i].LineForPrint[j].line3;
                        dr1["LINE4"] = lstHead[i].LineForPrint[j].line4;
                        dr1["LINE5"] = lstHead[i].LineForPrint[j].line5;
                        dt.Rows.Add(dr1);
                    }

                    for (int k = 0; k < 10; k++)
                    {
                        dr1 = dt.NewRow();
                        dr1["LINEX"] = indexData1++;
                        dr1["LINE1"] = "";
                        dr1["LINE2"] = "";
                        dr1["LINE3"] = "";
                        dr1["LINE4"] = "";
                        dr1["LINE5"] = "";
                        dt.Rows.Add(dr1);
                    }
                }

                lblError.Text = "";
                Session["DataToReport"] = ds;
                Response.Redirect("../Reports/InOrder.aspx");
            }
            else
                lblError.Text = "Not found any order";
        }


        private string ConvertDateToThai(DateTime date)
        {
            string dateThai = string.Empty;
            dateThai += date.Day + "-";
            switch (date.Month)
            {
                case 1:
                    {
                        dateThai += "ม.ค.";
                    } break;
                case 2:
                    {
                        dateThai += "ก.พ.";
                    } break;
                case 3:
                    {
                        dateThai += "มี.ค.";
                    } break;
                case 4:
                    {
                        dateThai += "เม.ย.";
                    } break;
                case 5:
                    {
                        dateThai += "พ.ค.";
                    } break;
                case 6:
                    {
                        dateThai += "มิ.ย.";
                    } break;
                case 7:
                    {
                        dateThai += "ก.ค.";
                    } break;
                case 8:
                    {
                        dateThai += "ส.ค.";
                    } break;
                case 9:
                    {
                        dateThai += "ก.ย.";
                    } break;
                case 10:
                    {
                        dateThai += "ต.ค.";
                    } break;
                case 11:
                    {
                        dateThai += "พ.ย.";
                    } break;
                case 12:
                    {
                        dateThai += "ธ.ค.";
                    } break;
            }
            return dateThai;
        }
    }
}