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
        private VEHICLE _item;
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

        private void BlindGrid()
        {
            gridVehicle.DataSource = DataSouce;
            gridVehicle.DataBind();
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
            var cmd = new VehicleService();
            DataSouce = cmd.GetALL();
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
            ViewState["VehicleID"] = gridVehicle.DataKeys[e.NewEditIndex].Values[0].ToString();
            InitialDataPopup();
            this.popup.Show();
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
            ViewState["VehicleID"] = null;
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
            if (ViewState["VehicleID"] != null)
            {
                var cmd = new VehicleService();
                _item = cmd.Select(Convert.ToInt32(ViewState["VehicleID"].ToString()));
                if (_item != null)
                {
                    popTxtCode.Text = _item.VEHICLE_CODE;
                    popTxtReg.Text = _item.VEHICLE_REGNO;
                    popTxtTypNO.Text = _item.VEHICLE_TYPENO;
                    flag.Text = "Edit";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var obj = new VEHICLE();
            obj.VEHICLE_REGNO = popTxtReg.Text;
            obj.VEHICLE_CODE = popTxtCode.Text;
            obj.VEHICLE_TYPENO = popTxtTypNO.Text;
            var cmd = new VehicleService(obj);
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
                obj.VEHICLE_ID = Convert.ToInt32(ViewState["VehicleID"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = 0;
                obj.SYE_DEL = true;
                cmd.Edit();
            }
            ViewState["VehicleID"] = null;
            Response.Redirect("SearchVehicle.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["VehicleID"] = null;
            Response.Redirect("SearchVehicle.aspx");
        }
    }
}