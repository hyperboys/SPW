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
    public partial class ReportMonthlySale : System.Web.UI.Page
    {
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
                PrepareObjectScreen();
            }

        }

        private void PrepareObjectScreen()
        {
            USER user = Session["user"] as USER;
            if (user == null) Response.RedirectPermanent("MainAdmin.aspx");

            DataSouce = ReportMonthlySaleDataService.LoadALL();
            gridProduct.DataSource = DataSouce;
            gridProduct.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataSouce = ReportMonthlySaleDataService.LoadALL(convertToDate102(txtStartDate.Text), convertToDate102(txtEndDate.Text));
            gridProduct.DataSource = DataSouce;
            gridProduct.DataBind();
        }

        private string convertToDate102(string date)
        {
            if (date != "")
            {
                string[] tmp = date.Split('/');

                return tmp[2] + tmp[1] + tmp[0];
            }
            else 
            {
                return date;
            }
            
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            MonthlySaleData ds = new MonthlySaleData();
            DataTable SaleVolume = ds.Tables["MonSale"];

            foreach (DataRow dr in DataSouce.Rows)
            {
                DataRow drSaleVolume = SaleVolume.NewRow();
                drSaleVolume["SALE"] = dr["EMPLOYEE_NAME"].ToString();
                drSaleVolume["AMT"] = dr["TOTAL"].ToString();
                drSaleVolume["CN"] = dr["DEBT"].ToString();
                drSaleVolume["BALANCE"] = dr["BALANCE"].ToString();
                SaleVolume.Rows.Add(drSaleVolume);
            }

            Session["DataToReport"] = ds;
            Response.RedirectPermanent("../Reports/MonthlySale.aspx");
        }
    }
}