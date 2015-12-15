using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

namespace SPW.UI.Web.Reports
{
    public partial class SendOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SendOrderReportData ds = new SendOrderReportData();
            if (Session["SendOrderReportData"] != null)
            {
                ds = Session["SendOrderReportData"] as SendOrderReportData;
            }

            SendOrderReport objRpt = new SendOrderReport();
            objRpt.SetDataSource(ds);

            this.CrystalReportViewer1.ReportSource = objRpt;
            this.CrystalReportViewer1.RefreshReport();
        }
    }
}