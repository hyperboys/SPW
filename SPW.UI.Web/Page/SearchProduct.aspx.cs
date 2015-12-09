using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;

namespace SPW.UI.Web.Page
{
    public partial class SearchProduct : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private ProductService cmdProduct;

        private List<PRODUCT> DataSouce
        {
            get
            {
                var list = (List<PRODUCT>)ViewState["listProduct"];
                return list;
            }
            set
            {
                ViewState["listProduct"] = value;
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
            cmdProduct = (ProductService)_dataServiceEngine.GetDataService(typeof(ProductService));
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
            DataSouce = cmdProduct.GetAll();
            gridProduct.DataSource = DataSouce;
            gridProduct.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtProductCode.Text.Equals(""))
            {
                gridProduct.DataSource = DataSouce;
            }
            else
            {
                gridProduct.DataSource = DataSouce.Where(x => x.PRODUCT_CODE.ToUpper().Contains(txtProductCode.Text.ToUpper())).ToList();
            }
            gridProduct.DataBind();
        }

        protected void gridProduct_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            Response.RedirectPermanent("ManageProduct.aspx?id=" + gridProduct.DataKeys[e.NewEditIndex].Values[0].ToString());
        }

        protected void gridProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridProduct.PageIndex = e.NewPageIndex;
            gridProduct.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtProductCode.Text = "";
            SearchGrid();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("ManageProduct.aspx");
        }

        protected void gridProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (ImageButton button in e.Row.Cells[3].Controls.OfType<ImageButton>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('ต้องการจะลบข้อมูลใช่หรือไม่')){ return false; };";
                    }
                }
            }
        }

        protected void gridProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {


                cmdProduct.Delete(Convert.ToInt32(gridProduct.DataKeys[e.RowIndex].Values[0].ToString()));
            }
            catch(Exception ex)
            {
                string script = "alert(\"ข้อมูลมีการใช้งานแล้ว ไม่สามารถลบได้\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
            }
            InitialData();
        }
    }
}