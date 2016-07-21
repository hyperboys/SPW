using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Data;
using System.Reflection;
using System.Data.SqlClient;

namespace SPW.UI.Web.Page
{
    public partial class PurchaseOrderRpt : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private PoHdTransService cmdPoHdTransService;
        private PoDtTransService cmdPoDtTransService;
        private RawProductService cmdRawProductService;

        public class PODATATABLE
        {
            public int SEQ { get; set; }
            public string RAW_NAME { get; set; }
            public int PO_QTY { get; set; }
        }

        public List<PO_HD_TRANS> DataSouce
        {
            get
            {
                var list = (List<PO_HD_TRANS>)ViewState["listPO"];
                return list;
            }
            set
            {
                ViewState["listPO"] = value;
            }
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

        private void InitialDataService()
        {
            cmdPoHdTransService = (PoHdTransService)_dataServiceEngine.GetDataService(typeof(PoHdTransService));
            cmdPoDtTransService = (PoDtTransService)_dataServiceEngine.GetDataService(typeof(PoDtTransService));
            cmdRawProductService = (RawProductService)_dataServiceEngine.GetDataService(typeof(RawProductService));
        }


        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CreatePageEngine();
                InitialData();
            }
            else
            {
                ReloadPageEngine();
            }
        }

        private void BlindGrid()
        {
            gridPO.DataSource = DataSouce;
            gridPO.DataBind();
        }

        private void InitialData()
        {
            DataSouce = cmdPoHdTransService.GetAllByStatusApprove();
            gridPO.DataSource = DataSouce;
            gridPO.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtBkNo.Text.Equals(""))
            {
                gridPO.DataSource = DataSouce;
            }
            else
            {
                gridPO.DataSource = DataSouce.Where(x => x.PO_BK_NO.Contains(txtBkNo.Text) && x.PO_RN_NO.Contains(txtRnNo.Text) && x.SYE_DEL == false).ToList();
            }
            gridPO.DataBind();
        }

        protected void gridPO_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            PO_HD_TRANS _PO_HD_TRANS = cmdPoHdTransService.Select(gridPO.DataKeys[e.NewEditIndex].Values[0].ToString(), gridPO.Rows[e.NewEditIndex].Cells[2].Text);
            List<PO_DT_TRANS> listPoDtTrans = cmdPoDtTransService.Select(gridPO.DataKeys[e.NewEditIndex].Values[0].ToString(), gridPO.Rows[e.NewEditIndex].Cells[2].Text);
            List<PODATATABLE> lstPODATATABLE = new List<PODATATABLE>();
            if (_PO_HD_TRANS != null)
            {
                listPoDtTrans.ForEach(x =>
                {
                    PODATATABLE _PODATATABLE = new PODATATABLE();
                    _PODATATABLE.SEQ = Convert.ToInt32(x.PO_SEQ_NO);
                    _PODATATABLE.RAW_NAME = cmdRawProductService.Select(x.RAW_ID).RAW_NAME1;
                    _PODATATABLE.PO_QTY = Convert.ToInt32(x.PO_QTY);
                    lstPODATATABLE.Add(_PODATATABLE);
                });

                ReportDocument rpt = new ReportDocument();
                rpt.Load(Server.MapPath("Reports/PurchaseOrdersReport.rpt"));
                rpt.SetDataSource(ToDataTable(lstPODATATABLE));
                // Assign Paramters after set datasource
                rpt.SetParameterValue("PO_DATE", DateTime.Now);
                rpt.SetParameterValue("TO_NAME", _PO_HD_TRANS.VENDOR_NAME);
                rpt.SetParameterValue("FROM_NAME", "S.P.W. Sunshine");
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "PurchaseOrderReport");
            }
        }

        protected void gridPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridPO.PageIndex = e.NewPageIndex;
            gridPO.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("IssuePurchaseOrders.aspx");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtBkNo.Text = "";
            SearchGrid();
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