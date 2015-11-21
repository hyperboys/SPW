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

namespace SPW.UI.Web.Page
{
    public partial class ReportSaleVolume : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private SectorService cmdSectorService;
        private EmployeeService cmdEmployeeService;

        public DataTable DataSouce
        {
            get
            {
                if (ViewState["listStore"] == null)
                {
                    ViewState["listStore"] = new DataTable();
                }
                var list = (DataTable)ViewState["listStore"];
                return list;
            }
            set
            {
                ViewState["listStore"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CreatePageEngine();
                InitialPage();
            }
            else
            {
                ReloadPageEngine();
            }
        }

        private void InitialPage()
        {
            CreatePageEngine();
            ReloadDatasource();
            PrepareObjectScreen();
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void InitialDataService()
        {
            cmdEmployeeService = (EmployeeService)_dataServiceEngine.GetDataService(typeof(EmployeeService));
            cmdSectorService = (SectorService)_dataServiceEngine.GetDataService(typeof(SectorService));
        }

        private void ReloadDatasource()
        {

        }

        private void PrepareObjectScreen()
        {
            USER user = Session["user"] as USER;
            if (user == null) Response.RedirectPermanent("MainAdmin.aspx");

            var list = cmdSectorService.GetAll();
            foreach (var item in list)
            {
                ddlSector.Items.Add(new ListItem(item.SECTOR_NAME, item.SECTOR_ID.ToString()));
            }

            var listSale = cmdEmployeeService.GetAllInclude().Where(x=>x.DEPARTMENT.DEPARTMENT_NAME.ToUpper() == "SALE").ToList();
            foreach (var item in listSale)
            {
                ddlSale.Items.Add(new ListItem(item.EMPLOYEE_NAME + " " + item.EMPLOYEE_SURNAME, item.EMPLOYEE_ID.ToString()));
            }

            DataSouce = ReportSaleVolumeDataService.LoadALL();
            gridProduct.DataSource = DataSouce;
            gridProduct.DataBind();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataSouce = ReportSaleVolumeDataService.LoadALL(txtProductCode.Text, txtProductName.Text, ddlSector.SelectedValue, ddlSale.SelectedValue);
            gridProduct.DataSource = DataSouce;
            gridProduct.DataBind();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            SaleVolumeData ds = new SaleVolumeData();
            DataTable SaleVolume = ds.Tables["SaleVolume"];

            foreach (DataRow dr in DataSouce.Rows) 
            {
                DataRow drSaleVolume = SaleVolume.NewRow();
                drSaleVolume["PRODUCT_CODE"] = dr["PRODUCT_CODE"].ToString();
                drSaleVolume["PRODUCT_NAME"] = dr["PRODUCT_NAME"].ToString();
                drSaleVolume["SECTOR"] = dr["SECTOR_NAME"].ToString();
                drSaleVolume["SALE"] = dr["EMPLOYEE_NAME"].ToString();
                drSaleVolume["QTY"] = dr["SUMQTY"].ToString();
                drSaleVolume["AMT"] = dr["SUMAMT"].ToString();
                SaleVolume.Rows.Add(drSaleVolume);
            }

            Session["DataToReport"] = ds;
            Response.RedirectPermanent("../Reports/SaleVolume.aspx");

        }
    }
}