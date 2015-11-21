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
    public partial class SearchColorType : System.Web.UI.Page
    {
        private COLOR_TYPE _colorType;
        private DataServiceEngine _dataServiceEngine;
        private ColorTypeService cmdColor;

        public List<COLOR_TYPE> DataSouce
        {
            get
            {
                var list = (List<COLOR_TYPE>)ViewState["listColorType"];
                return list;
            }
            set
            {
                ViewState["listColorType"] = value;
            }
        }

        private void BlindGrid()
        {
            gridTracery.DataSource = DataSouce;
            gridTracery.DataBind();
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
            cmdColor = (ColorTypeService)_dataServiceEngine.GetDataService(typeof(ColorTypeService));
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
                InitialData();
            }
            else
            {
                ReloadPageEngine();
            }
        }

        private void InitialData()
        {
            DataSouce = cmdColor.GetAll();
            gridTracery.DataSource = DataSouce;
            gridTracery.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            gridTracery.DataSource = DataSouce.Where(x => x.COLOR_TYPE_SUBNAME.Contains(txtColorTypeSubName.Text)
                && x.COLOR_TYPE_NAME.Contains(txtColorTypeName.Text)).ToList();
            gridTracery.DataBind();
        }

        protected void gridTracery_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            Response.RedirectPermanent("ManageColorType.aspx?id=" + gridTracery.DataKeys[e.NewEditIndex].Values[0].ToString());
        }

        protected void gridTracery_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridTracery.PageIndex = e.NewPageIndex;
            gridTracery.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtColorTypeName.Text = "";
            txtColorTypeSubName.Text = "";
            SearchGrid();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("ManageColorType.aspx");
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