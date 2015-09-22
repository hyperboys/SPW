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
    public partial class InOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            InOrderReportData ds = new InOrderReportData();
            if (Session["DataToReport"] != null)
            {
                ds = Session["DataToReport"] as InOrderReportData;
            }

            InOrderReport objRpt = new InOrderReport();
            objRpt.SetDataSource(ds);

            this.CrystalReportViewer1.ReportSource = objRpt;
            this.CrystalReportViewer1.RefreshReport();

        }
    }
}