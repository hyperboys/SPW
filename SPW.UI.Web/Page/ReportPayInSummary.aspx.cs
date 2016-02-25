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

            Session["ListPayin"] = _payInTranService.GetAll();
            gridProduct.DataSource = Session["ListPayin"] as List<PAYIN_TRANS>;
            gridProduct.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Session["ListPayin"] = _payInTranService.GetAllCondition(Convert.ToDateTime(convertToDateThai(txtStartDate.Text)), Convert.ToDateTime(convertToDateThai(txtEndDate.Text)));
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

                return tmp[0] + "/" + tmp[1] + "/" +(Convert.ToInt32(tmp[2]) + 543);
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
                List<PAYIN_TRANS> lstPayin = Session["ListPayin"] as List<PAYIN_TRANS>;
                foreach (PAYIN_TRANS item in lstPayin) 
                {
                
                }
            }
            catch (Exception ex) 
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }
    }
}