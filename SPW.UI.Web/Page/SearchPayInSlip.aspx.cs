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

        #region Declare variable
        private DataServiceEngine _dataServiceEngine;
        #endregion

        #region Sevice control
        private PayInTranService _payInTranService;

        private void InitialPage()
        {
            txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            CreatePageEngine();
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

        private void InitialData()
        {
            BindGridview();
        }

        private void BindGridview()
        {
            DataSouce = _payInTranService.GetAll();

            if(DataSouce != null)
            {
                DataSouce.Where(x => x.ACCOUNT_ID.ToString().Contains(txtAccountNo.Text) && x.CHQ_NO.Contains(txtCheckNo.Text)).ToList();
            }

            if (txtStartDate.Text != "" && txtEndDate.Text != "")
            {
                DataSouce = DataSouce.Where(x => x.PAYIN_DATE >= DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) && x.PAYIN_DATE <= DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToList();
            }
            else if (txtStartDate.Text != "")
            {
                DataSouce = DataSouce.Where(x => x.PAYIN_DATE == DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToList();
            }

            DataSouce = DataSouce.OrderBy(x => x.PAYIN_DATE).ThenBy(x => x.PAYIN_SEQ_NO).ThenBy(x => x.CHQ_NO).ToList();

            grdPayIn.DataSource = DataSouce;
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
            List<string> nameList = SearchAutoCompleteDataService.Search("PAYIN_TRANS", "CHQ_NO", "CHQ_NO", "");
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
            txtCheckNo.Attributes.Add("data-source", str);
        }

        protected void gdvManageOrderHQ_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtnDetail = (LinkButton)e.Row.FindControl("lbtnDetail");
                lbtnDetail.PostBackUrl = "PayInSlip.aspx?id=" + grdPayIn.DataKeys[e.Row.RowIndex].Values[0].ToString() + "&date=" + grdPayIn.DataKeys[e.Row.RowIndex].Values[1].ToString();
            }
        }

        private string convertToDateThai(string date)
        {
            if (date != "")
            {
                string[] tmp = date.Split('/');
                if (Convert.ToInt32(tmp[2]) < 2500)
                {
                    return tmp[0] + "/" + tmp[1] + "/" + (Convert.ToInt32(tmp[2]) + 543);
                }
                else
                {
                    return date;
                }
            }
            else
            {
                return date;
            }

        }
        #endregion
    }
}