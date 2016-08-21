using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using SPW.Common;

namespace SPW.UI.Web.Reports
{
    public partial class ResultEmpListAssessmentPageReport : System.Web.UI.Page
    {
        ResultEmpListAssessmentReport objRpt = new ResultEmpListAssessmentReport();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ResultEmpListAssessmentData ds = new ResultEmpListAssessmentData();
                if (Session["ResultEmpListAssessmentData"] != null)
                {
                    ds = Session["ResultEmpListAssessmentData"] as ResultEmpListAssessmentData;
                }

                objRpt.SetDataSource(ds);
                this.CrystalReportViewer1.ReportSource = objRpt;
                this.CrystalReportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            objRpt.Close();
            objRpt.Dispose();
        }
    }
}