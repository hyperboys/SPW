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
    public partial class SearchCategory : System.Web.UI.Page
    {
        private CATEGORY _category;
        public List<CATEGORY> DataSouce
        {
            get
            {
                if (ViewState["listCategory"] == null)
                {
                    ViewState["listCategory"] = new List<CATEGORY>();
                }

                var list = (List<CATEGORY>)ViewState["listCategory"];
                return list;
            }
            set
            {
                ViewState["listCategory"] = value;
            }
        }

        private void BlindGrid()
        {
            gridCategory.DataSource = DataSouce;
            gridCategory.DataBind();
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
            var cmd = new CategoryService();
            DataSouce = cmd.GetALL();
            gridCategory.DataSource = DataSouce;
            gridCategory.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtCategoryCode.Text.Equals(""))
            {
                gridCategory.DataSource = DataSouce;
            }
            else
            {
                gridCategory.DataSource = DataSouce.Where(x => x.CATEGORY_CODE.Contains(txtCategoryCode.Text)).ToList();
            }
            gridCategory.DataBind();
        }

        protected void gridCategory_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            ViewState["catId"] = gridCategory.DataKeys[e.NewEditIndex].Values[0].ToString();
            InitialDataPopup();
            this.popup.Show();
        }

        protected void gridCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridCategory.PageIndex = e.NewPageIndex;
            gridCategory.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtCategoryCode.Text = "";
            SearchGrid();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ViewState["catId"] = null;
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
            if (ViewState["catId"] != null)
            {
                var cmd = new CategoryService();
                _category = cmd.Select(Convert.ToInt32(ViewState["catId"].ToString()));
                if (_category != null)
                {
                    popTxtCategoryCode.Text = _category.CATEGORY_CODE;
                    txtCategoryName.Text = _category.CATEGORY_NAME;
                    flag.Text = "Edit";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var obj = new CATEGORY();
            obj.CATEGORY_CODE = popTxtCategoryCode.Text;
            obj.CATEGORY_NAME = txtCategoryName.Text;
            var cmd = new CategoryService(obj);
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
                obj.CATEGORY_ID = Convert.ToInt32(ViewState["catId"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = 0;
                obj.SYE_DEL = true;
                cmd.Edit();
            }
            ViewState["catId"] = null;
            Response.Redirect("SearchCategory.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["catId"] = null;
            Response.Redirect("SearchCategory.aspx");
        }
    }
}