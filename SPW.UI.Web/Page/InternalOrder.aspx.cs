using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using SPW.UI.Web.Reports;
using System.Data;

namespace SPW.UI.Web.Page
{
    public partial class InternalOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitialData();
            }
        }

        public List<ORDER> DataSouce
        {
            get
            {
                if (ViewState["listOrder"] == null)
                {
                    ViewState["listOrder"] = new List<ORDER>();
                }
                var list = (List<ORDER>)ViewState["listOrder"];
                return list;
            }
            set
            {
                ViewState["listOrder"] = value;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (ddlProvince.SelectedValue != "0")
            {
                if (ddlStore.SelectedValue != "0")
                {
                    grideInOrder.DataSource = DataSouce.Where(x => x.STORE.PROVINCE_ID == Convert.ToInt32(ddlProvince.SelectedValue) && x.STORE_ID == Convert.ToInt32(ddlStore.SelectedValue)).ToList();
                }
                else
                {
                    grideInOrder.DataSource = DataSouce.Where(x => x.STORE.PROVINCE_ID == Convert.ToInt32(ddlProvince.SelectedValue)).ToList();
                }
            }
            else
            {
                grideInOrder.DataSource = DataSouce;
            }
            grideInOrder.DataBind();
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProvince.SelectedIndex == 0)
            {
                ddlStore.SelectedIndex = 0;
                ddlStore.Enabled = false;
            }
            else
            {
                ddlStore.Items.Clear();
                ddlStore.Items.Add(new ListItem("เลือกทั้งหมด", "0"));
                foreach (var item in ((List<STORE>)ViewState["listStore"]).Where(x => x.PROVINCE_ID == Convert.ToInt32(ddlProvince.SelectedValue)))
                {
                    ddlStore.Items.Add(new ListItem(item.STORE_NAME, item.STORE_ID.ToString()));
                }
                ddlStore.Enabled = true;
            }
        }

        private void InitialData()
        {

            var cmdPro = new ProvinceService();
            ViewState["listProvince"] = cmdPro.GetALL();
            foreach (var item in (List<PROVINCE>)ViewState["listProvince"])
            {
                ddlProvince.Items.Add(new ListItem(item.PROVINCE_NAME, item.PROVINCE_ID.ToString()));
            }

            var cmdStore = new StoreService();
            ViewState["listStore"] = cmdStore.GetALL();
            foreach (var item in (List<STORE>)ViewState["listStore"])
            {
                ddlStore.Items.Add(new ListItem(item.STORE_NAME, item.STORE_ID.ToString()));
            }

            var cmdOrder = new OrderService();
            DataSouce = cmdOrder.GetALLIncludeStore();
            grideInOrder.DataSource = DataSouce;
            grideInOrder.DataBind();
        }

        protected void grideInOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grideInOrder.PageIndex = e.NewPageIndex;
            grideInOrder.DataBind();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            List<STORE> list = new List<STORE>();
            var cmd = new StoreService();
            for (int i = 0; i < grideInOrder.Rows.Count; i++)
            {
                if (list.Where(x => x.STORE_ID == Convert.ToInt32(grideInOrder.DataKeys[i].Values[1].ToString())).FirstOrDefault() == null)
                {
                    list.Add(cmd.Select(Convert.ToInt32(grideInOrder.DataKeys[i].Values[1].ToString())));
                }
            }


            List<InOrderForPrint> listInOrder = new List<InOrderForPrint>();
            var cmdOrder = new OrderService();
            var cmdOrderDetail = new OrderDetailService();
            List<ORDER> tmpListOrder = new List<ORDER>();
            foreach (STORE tmp in list)
            {
                InOrderForPrint inOrder = new InOrderForPrint();
                inOrder.Store = tmp;
                inOrder.OrderDetails = new List<ORDER_DETAIL>();
                tmpListOrder = cmdOrder.GetALLIncludeByStore(tmp.STORE_ID);
                foreach (ORDER tmpOrder in tmpListOrder)
                {
                    inOrder.Order = tmpOrder;
                    inOrder.OrderDetails.AddRange(cmdOrderDetail.GetALLIncludeByOrder(tmpOrder.ORDER_ID).ToList());
                }
                listInOrder.Add(inOrder);
            }
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
                if (item.Store.PROVINCE_ID == 1)
                {
                    linePrint.line2 = item.Store.STORE_NAME + " (" + item.Store.STORE_CODE.Substring(item.Store.STORE_CODE.Length - 3, 3) + " )";
                }
                else 
                {
                    linePrint.line2 = item.Store.STORE_CODE + " " + item.Store.STORE_NAME;
                }
                linePrint.line3 = "จำนวน";
                linePrint.line4 = "";
                linePrint.line5 = "ราคาต่อชิ้น";
                lstLine.Add(linePrint);

                foreach (ORDER_DETAIL od in item.OrderDetails)
                {
                    LineForPrint linePrintItem = new LineForPrint();
                    linePrintItem.line1 = "";
                    if (od.IS_FREE.Value)
                    {
                        linePrintItem.line2 = "แถม";
                    }
                    else
                    {
                        linePrintItem.line2 = od.PRODUCT.PRODUCT_NAME;
                    }
                    linePrintItem.line3 = od.PRODUCT_QTY.ToString();
                    linePrintItem.line4 = "";
                    linePrintItem.line5 = od.PRODUCT_PRICE.ToString();
                    lstLine.Add(linePrintItem);
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

                for (int k = 0; k < 5; k++)
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

            Session["DataToReport"] = ds;
            Response.Redirect("../Reports/InOrder.aspx");
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