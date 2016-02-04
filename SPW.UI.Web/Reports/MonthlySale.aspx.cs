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
    public partial class MonthlySale : System.Web.UI.Page
    {
        MonthlySaleReport objRpt = new MonthlySaleReport();
        protected void Page_Load(object sender, EventArgs e)
        {
            MonthlySaleData ds = new MonthlySaleData();
            if (Session["DataToReport"] != null)
            {
                ds = Session["DataToReport"] as MonthlySaleData;
            }

            objRpt.SetDataSource(ds);
            this.CrystalReportViewer1.ReportSource = objRpt;
            this.CrystalReportViewer1.RefreshReport();
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            objRpt.Close();
            objRpt.Dispose();
        }
    }
}