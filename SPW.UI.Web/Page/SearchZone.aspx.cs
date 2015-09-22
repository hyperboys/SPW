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
    public partial class SearchZone : System.Web.UI.Page
    {
        private ZONE _zone;
        public List<ZONE> DataSouce
        {
            get
            {
                var list = (List<ZONE>)ViewState["listZone"];
                return list;
            }
            set
            {
                ViewState["listZone"] = value;
            }
        }

        private void BlindGrid()
        {
            gridZone.DataSource = DataSouce;
            gridZone.DataBind();
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
            var cmd = new ZoneService();
            DataSouce = cmd.GetALL();
            gridZone.DataSource = DataSouce;
            gridZone.DataBind();
        }

        private void InitialDataPopup()
        {
            var cmdEmp = new EmployeeService();
            foreach (var item in cmdEmp.GetALLInclude())
            {
                ddlSell.Items.Add(new ListItem(("แผนก " + item.DEPARTMENT.DEPARTMENT_NAME + " ชื่อ " + item.EMPLOYEE_NAME + " " + item.EMPLOYEE_SURNAME), item.EMPLOYEE_ID.ToString()));
            }

            if (ViewState["zoneID"] != null)
            {
                var cmd = new ZoneService();
                _zone = cmd.Select(Convert.ToInt32(ViewState["zoneID"].ToString()));
                if (_zone != null)
                {
                    popTxtZoneCode.Text = _zone.ZONE_CODE;
                    popTxtZoneName.Text = _zone.ZONE_NAME;
                    flag.Text = "Edit";
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtZoneCode.Text.Equals(""))
            {
                gridZone.DataSource = DataSouce;
            }
            else
            {
                gridZone.DataSource = DataSouce.Where(x => x.ZONE_CODE.Contains(txtZoneCode.Text)).ToList();
            }
            gridZone.DataBind();
        }

        protected void gridZone_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            ViewState["zoneID"] = gridZone.DataKeys[e.NewEditIndex].Values[0].ToString();
            InitialDataPopup();
            this.popup.Show();
        }

        protected void gridZone_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridZone.PageIndex = e.NewPageIndex;
            gridZone.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtZoneCode.Text = "";
            SearchGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var obj = new ZONE();
            obj.ZONE_CODE = popTxtZoneCode.Text;
            obj.ZONE_NAME = popTxtZoneName.Text;
            var cmd = new ZoneService(obj);
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
                obj.ZONE_ID = Convert.ToInt32(ViewState["zoneID"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = 0;
                obj.SYE_DEL = true;
                cmd.Edit();
            }
            flag.Text = "Add";
            ViewState["zoneID"] = null;
            Response.Redirect("SearchZone.aspx");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            InitialDataPopup();
            this.popup.Show();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["zoneID"] = null;
            Response.Redirect("SearchZone.aspx");
        }
    }
}