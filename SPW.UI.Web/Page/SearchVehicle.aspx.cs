using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;

namespace SPW.UI.Web.Page
{
    public partial class SearchVehicle : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private VehicleService cmdVehicle;

        public List<VEHICLE> DataSouce
        {
            get
            {
                var list = (List<VEHICLE>)ViewState["listVehicle"];
                return list;
            }
            set
            {
                ViewState["listVehicle"] = value;
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
            cmdVehicle = (VehicleService)_dataServiceEngine.GetDataService(typeof(VehicleService));
        }


        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void BlindGrid()
        {
            gridVehicle.DataSource = DataSouce;
            gridVehicle.DataBind();
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

        private void InitialData()
        {
            DataSouce = cmdVehicle.GetAll();
            gridVehicle.DataSource = DataSouce;
            gridVehicle.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            gridVehicle.DataSource = DataSouce.Where(x => x.VEHICLE_CODE.Contains(txtColorTypeSubName.Text)
                && x.VEHICLE_REGNO.Contains(txtColorTypeName.Text)).ToList();
            gridVehicle.DataBind();
        }

        protected void gridVehicle_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            Response.RedirectPermanent("ManageVehicle.aspx?id=" + gridVehicle.DataKeys[e.NewEditIndex].Values[0].ToString());
        }

        protected void gridVehicle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridVehicle.PageIndex = e.NewPageIndex;
            gridVehicle.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtColorTypeName.Text = "";
            txtColorTypeSubName.Text = "";
            SearchGrid();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("ManageVehicle.aspx");
        }

        protected void gridCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int NumCells = e.Row.Cells.Count;
                for (int i = 0; i < NumCells - 1; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                }
            }
        }
    }
}