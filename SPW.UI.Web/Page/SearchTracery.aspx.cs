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
    public partial class SearchTracery : System.Web.UI.Page
    {
        private COLOR_TYPE _colorType;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitialData();
            }
        }

        private void InitialData()
        {
            var cmd = new ColorTypeService();
            DataSouce = cmd.GetAll();
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
            ViewState["colorTypeId"] = gridTracery.DataKeys[e.NewEditIndex].Values[0].ToString();
            InitialDataPopup();
            this.popup.Show();
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
            ViewState["colorTypeId"] = null;
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
            if (ViewState["colorTypeId"] != null)
            {
                var cmd = new ColorTypeService();
                _colorType = cmd.Select(Convert.ToInt32(ViewState["colorTypeId"].ToString()));
                if (_colorType != null)
                {
                    popTxtColorTypeName.Text = _colorType.COLOR_TYPE_NAME;
                    popTxtColorTypeSubName.Text = _colorType.COLOR_TYPE_SUBNAME;
                    flag.Text = "Edit";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var obj = new COLOR_TYPE();
            obj.COLOR_TYPE_NAME = popTxtColorTypeName.Text;
            obj.COLOR_TYPE_SUBNAME = popTxtColorTypeSubName.Text;
            var cmd = new ColorTypeService();
            if (flag.Text.Equals("Add"))
            {
                obj.Action = ActionEnum.Create;
                obj.CREATE_DATE = DateTime.Now;
                obj.CREATE_EMPLOYEE_ID = 0;
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = 0;
                obj.SYE_DEL = true;
                cmd.Add(obj);
            }
            else
            {
                obj.Action = ActionEnum.Update;
                obj.COLOR_TYPE_ID = Convert.ToInt32(ViewState["colorTypeId"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = 0;
                obj.SYE_DEL = true;
                cmd.Edit(obj);
            }
            ViewState["colorTypeId"] = null;
            Response.Redirect("SearchTracery.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["colorTypeId"] = null;
            Response.Redirect("SearchTracery.aspx");
        }
    }
}