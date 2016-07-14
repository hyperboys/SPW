
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace SPW.UI.Web.Page
{
    public partial class PurchaseOrderRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ReportDocument rpt = new ReportDocument();
            /*set location of report*/
            rpt.Load(Server.MapPath("Reports/PurchaseOrdersRpt.rpt"));
            rpt.SetParameterValue("PO_DATE", DateTime.Now);
            rpt.SetParameterValue("TO_NAME", "123");
            rpt.SetParameterValue("FROM_NAME", "123");
            rpt.SetParameterValue("SEQ", 1);
            rpt.SetParameterValue("RAW_NAME", "123");
            rpt.SetParameterValue("PO_QTY", 8);
            CrystalReportViewer1.ReportSource = rpt;

            //try
            //{
            //    ExportOptions CrExportOptions;
            //    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            //    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            //    CrDiskFileDestinationOptions.DiskFileName = "c:\\csharp.net-informations.pdf";
            //    CrExportOptions = rpt.ExportOptions;
            //    {
            //        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            //        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            //        CrExportOptions.FormatOptions = CrFormatTypeOptions;
            //    }
            //    rpt.Export();
            //}
            //catch (Exception ex)
            //{

            //}
            rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "PurchaseOrderReport");
        }
    }
}