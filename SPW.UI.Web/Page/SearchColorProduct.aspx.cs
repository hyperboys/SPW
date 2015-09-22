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
        private COLOR _color;
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

        private void BlindGrid()
        {
            gridColor.DataSource = DataSouce;
            gridColor.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitialData();
            }
        }

        private void InitialData()
        {
            var cmd = new ColorService();
            DataSouce = cmd.GetALL();
            gridColor.DataSource = DataSouce;
            gridColor.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            gridColor.DataSource = DataSouce.Where(x => x.COLOR_SUBNAME.Contains(txtColorTypeSubName.Text)
                && x.COLOR_NAME.Contains(txtColorTypeName.Text)).ToList();
            gridColor.DataBind();
        }

        protected void gridColor_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            ViewState["colorId"] = gridColor.DataKeys[e.NewEditIndex].Values[0].ToString();
            InitialDataPopup();
            this.popup.Show();
        }

        protected void gridColor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridColor.PageIndex = e.NewPageIndex;
            gridColor.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtColorTypeName.Text = "";
            txtColorTypeSubName.Text = "";
            SearchGrid();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ViewState["colorId"] = null;
            InitialDataPopup();
            this.popup.Show();
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

        private void InitialDataPopup()
        {
            if (ViewState["colorId"] != null)
            {
                var cmd = new ColorService();
                _color = cmd.Select(Convert.ToInt32(ViewState["colorId"].ToString()));
                if (_color != null)
                {
                    popTxtColorTypeName.Text = _color.COLOR_NAME;
                    popTxtColorTypeSubName.Text = _color.COLOR_SUBNAME;
                    flag.Text = "Edit";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var obj = new COLOR();
            obj.COLOR_NAME = popTxtColorTypeName.Text;
            obj.COLOR_SUBNAME = popTxtColorTypeSubName.Text;
            var cmd = new ColorService(obj);
            if (flag.Text.Equals("Add"))
            {
                obj.Action = ActionEnum.Create;
                obj.CREATE_DATE = DateTime.Now;
                obj.CREATE_EMPLOYEE_ID = 0;
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = 0;
                obj.SYE_DEL = true;
                cmd.Add();
            }
            else
            {
                obj.Action = ActionEnum.Update;
                obj.COLOR_ID = Convert.ToInt32(ViewState["colorId"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = 0;
                obj.SYE_DEL = true;
                cmd.Edit();
            }
            ViewState["colorId"] = null;
            Response.Redirect("SearchColorProduct.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["colorId"] = null;
            Response.Redirect("SearchColorProduct.aspx");
        }
    }
}