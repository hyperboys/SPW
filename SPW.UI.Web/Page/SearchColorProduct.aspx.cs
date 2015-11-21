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
    public partial class SearchColorProduct : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private ColorService cmdColor;
        public List<COLOR> DataSouce
        {
            get
            {
                if (ViewState["listColor"] == null) 
                {
                    ViewState["listColor"] = new List<COLOR>();
                }
                var list = (List<COLOR>)ViewState["listColor"];
                return list;
            }
            set
            {
                ViewState["listColor"] = value;
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
            cmdColor = (ColorService)_dataServiceEngine.GetDataService(typeof(ColorService));
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
            gridColor.DataSource = DataSouce;
            gridColor.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtColorTypeSubName.Text == "" && txtColorTypeName.Text == "")
            {
                gridColor.DataSource = DataSouce;
            }
            else if (txtColorTypeSubName.Text != "" && txtColorTypeName.Text == "") 
            {
                gridColor.DataSource = DataSouce.Where(x => x.COLOR_SUBNAME.ToUpper().Contains(txtColorTypeSubName.Text.ToUpper())).ToList();
            }
            else if (txtColorTypeSubName.Text == "" && txtColorTypeName.Text != "")
            {
                gridColor.DataSource = DataSouce.Where(x => x.COLOR_NAME.ToUpper().Contains(txtColorTypeName.Text.ToUpper())).ToList();
            }
            else
            {
                gridColor.DataSource = DataSouce.Where(x => x.COLOR_SUBNAME.ToUpper().Contains(txtColorTypeSubName.Text.ToUpper())
                   && x.COLOR_NAME.ToUpper().Contains(txtColorTypeName.Text.ToUpper())).ToList();
            }
           
            gridColor.DataBind();
        }

        protected void gridColor_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            Response.RedirectPermanent("ManageColorProduct.aspx?id=" + gridColor.DataKeys[e.NewEditIndex].Values[0].ToString());
        }

        protected void gridColor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridColor.PageIndex = e.NewPageIndex;
            gridColor.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("ManageColorProduct.aspx");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtColorTypeName.Text = "";
            txtColorTypeSubName.Text = "";
            SearchGrid();
        }
    }
}