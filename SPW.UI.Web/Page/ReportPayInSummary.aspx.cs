using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.Model;
using SPW.DataService;
using SPW.DAL;
using SPW.UI.Web.Reports;
using System.Data;
using SPW.Common;

namespace SPW.UI.Web.Page
{
    public partial class ReportPayInSummary : System.Web.UI.Page
    {
        private PayInTranService _payInTranService;
        private DataServiceEngine _dataServiceEngine;

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
            _payInTranService = (PayInTranService)_dataServiceEngine.GetDataService(typeof(PayInTranService));
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
                PrepareObjectScreen();
            }
            else
            {
                ReloadPageEngine();
            }

        }

        private void PrepareObjectScreen()
        {
            USER user = Session["user"] as USER;
            if (user == null) Response.RedirectPermanent("MainAdmin.aspx");

            Session["ListPayin"] = _payInTranService.GetAll().OrderBy(x => x.PAYIN_DATE).ToList();
            gridProduct.DataSource = Session["ListPayin"] as List<PAYIN_TRANS>;
            gridProduct.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Session["ListPayin"] = _payInTranService.GetAllCondition(Convert.ToDateTime(convertToDateThai(txtStartDate.Text)), Convert.ToDateTime(convertToDateThai(txtEndDate.Text))).OrderBy(x => x.PAYIN_DATE).ToList();
                gridProduct.DataSource = Session["ListPayin"] as List<PAYIN_TRANS>;
                gridProduct.DataBind();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string tmpDate = "";
                List<PAYIN_TRANS> lstPayin = Session["ListPayin"] as List<PAYIN_TRANS>;
                lstPayin = lstPayin.OrderBy(x => x.PAYIN_DATE).ToList();
                
                Reports.PayInSummaryData ds = new Reports.PayInSummaryData();
                DataTable payInSummaryData = ds.Tables["PAYIN"];
                foreach (PAYIN_TRANS item in lstPayin)
                {
                    DataRow drpayInSummaryData = payInSummaryData.NewRow();
                    if (tmpDate != item.PAYIN_DATE.ToShortDateString())
                    {
                        tmpDate = item.PAYIN_DATE.ToShortDateString();
                        drpayInSummaryData["PAYIN_DATE"] = tmpDate;
                    }
                    else
                    {
                        drpayInSummaryData["PAYIN_DATE"] = "";
                    }
                    drpayInSummaryData["CHQ_NO"] = item.CHQ_NO;
                    drpayInSummaryData["ACCOUNT_ID"] = item.ACCOUNT_ID;
                    drpayInSummaryData["BANK_NAME"] = item.BANK_NAME;
                    drpayInSummaryData["CHQ_AMOUNT"] = item.CHQ_AMOUNT.ToString("#,###.00");
                    drpayInSummaryData["START_DATE"] = ((PAYIN_TRANS)lstPayin.OrderBy(x => x.PAYIN_DATE).FirstOrDefault()).PAYIN_DATE.ToString("dd/MM/yyyy");
                    drpayInSummaryData["END_DATE"] = ((PAYIN_TRANS)lstPayin.OrderByDescending(x => x.PAYIN_DATE).FirstOrDefault()).PAYIN_DATE.ToString("dd/MM/yyyy");
                    payInSummaryData.Rows.Add(drpayInSummaryData);
                }

                Session["DataToReport"] = ds;
                Response.RedirectPermanent("../Reports/PayInSummary.aspx");
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }
    }
}