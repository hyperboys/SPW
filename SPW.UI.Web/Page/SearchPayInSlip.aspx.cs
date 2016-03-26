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
using SPW.DAL;

namespace SPW.UI.Web.Page
{
    public partial class SearchPayInSlip : System.Web.UI.Page
    {
        private class DATAGRID
        {
            public string ACCOUNT_ID { get; set; }
            public string ACCOUNT_NAME { get; set; }
            public int PAYIN_SEQ_NO { get; set; }
            public string BANK_NAME { get; set; }
            public DateTime PAYIN_DATE { get; set; }
            public decimal PAYIN_TOTAL_AMOUNT { get; set; }
        }

        #region Declare variable
        private DataServiceEngine _dataServiceEngine;
        #endregion

        #region Sevice control
        private PayInTranService _payInTranService;

        private void InitialPage()
        {
            CreatePageEngine();
            ReloadDatasource();
            InitialData();
            AutoCompletetxtAccountName();
            AutoCompletetxtAccountNo();
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
            _payInTranService = (PayInTranService)_dataServiceEngine.GetDataService(typeof(PayInTranService));
        }

        private void ReloadDatasource()
        {

        }

        private void InitialData()
        {
            BindGridview();
        }

        private void BindGridview()
        {
            DataSouce = _payInTranService.GetAll().Where(x => x.ACCOUNT_ID.ToString().Contains(txtAccountNo.Text) && x.ACCOUNT_NAME.Contains(txtAccountName.Text)).ToList();

            if (txtStartDate.Text != "" && txtEndDate.Text != "")
            {
                DataSouce = DataSouce.Where(x => x.PAYIN_DATE >= Convert.ToDateTime(convertToDateThai(txtStartDate.Text)) && x.PAYIN_DATE <= Convert.ToDateTime(convertToDateThai(txtEndDate.Text))).ToList();
            }
            else if (txtStartDate.Text != "")
            {
                DataSouce = DataSouce.Where(x => x.PAYIN_DATE == Convert.ToDateTime(convertToDateThai(txtStartDate.Text))).ToList();
            }

            DataSouce = DataSouce.OrderBy(x => x.PAYIN_DATE).ToList();
            List<DATAGRID> tmp = DataSouce.Select(x => new DATAGRID
            {
                ACCOUNT_ID = x.ACCOUNT_ID,
                ACCOUNT_NAME = x.ACCOUNT_NAME,
                PAYIN_SEQ_NO = x.PAYIN_SEQ_NO,
                BANK_NAME = x.BANK_NAME,
                PAYIN_DATE = x.PAYIN_DATE,
                PAYIN_TOTAL_AMOUNT = x.PAYIN_TOTAL_AMOUNT
            }).ToList();

            var tmpDatagrid = tmp.GroupBy(x => x.PAYIN_SEQ_NO).ToList();
            List<DATAGRID> data = new List<DATAGRID>();
            foreach (var item in tmpDatagrid)
            {
                data.Add(item.ElementAt(0));
            }
            
            grdPayIn.DataSource = data;
            grdPayIn.DataBind();
        }

        public static bool isBetweenDate(DateTime input, DateTime start, DateTime end)
        {
            return (input >= start && input <= end);
        }
        #endregion


        public List<PAYIN_TRANS> DataSouce
        {
            get
            {
                if (ViewState["PAYIN_TRANS"] == null)
                {
                    ViewState["PAYIN_TRANS"] = new List<PAYIN_TRANS>();
                }
                var list = (List<PAYIN_TRANS>)ViewState["PAYIN_TRANS"];
                return list;
            }
            set
            {
                ViewState["PAYIN_TRANS"] = value;
            }
        }

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


        private void AutoCompletetxtAccountNo()
        {
            List<string> nameList = SearchAutoCompleteDataService.Search("ACCOUNT_MAST", "ACCOUNT_ID", "ACCOUNT_ID", "");
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
            txtAccountNo.Attributes.Add("data-source", str);
        }

        private void AutoCompletetxtAccountName()
        {
            List<string> nameList = SearchAutoCompleteDataService.Search("ACCOUNT_MAST", "ACCOUNT_NAME", "ACCOUNT_NAME", "");
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
            txtAccountName.Attributes.Add("data-source", str);
        }

        protected void gdvManageOrderHQ_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtnDetail = (LinkButton)e.Row.FindControl("lbtnDetail");
                lbtnDetail.PostBackUrl = "PayInSlip.aspx?id=" + DataSouce[e.Row.RowIndex].PAYIN_SEQ_NO;
            }
        }

        private string convertToDateThai(string date)
        {
            if (date != "")
            {
                string[] tmp = date.Split('/');

                return tmp[0] + "/" + tmp[1] + "/" + (Convert.ToInt32(tmp[2]) + 543);
            }
            else
            {
                return date;
            }

        }
        #endregion
    }
}