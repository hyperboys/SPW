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
    public partial class ManageVehicle : System.Web.UI.Page
    {
        private VEHICLE _item;
        private DataServiceEngine _dataServiceEngine;
        private VehicleService cmdVehicle;

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
            if (Request.QueryString["id"] != null)
            {
                _item = cmdVehicle.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
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
            USER userItem = Session["user"] as USER;
            var obj = new VEHICLE();
            obj.VEHICLE_REGNO = popTxtReg.Text;
            obj.VEHICLE_CODE = popTxtCode.Text;
            obj.VEHICLE_TYPENO = popTxtTypNO.Text;
            if (flag.Text.Equals("Add"))
            {
                obj.Action = ActionEnum.Create;
                obj.CREATE_DATE = DateTime.Now;
                obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                cmdVehicle.Add(obj);
            }
            else
            {
                obj.Action = ActionEnum.Update;
                obj.VEHICLE_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                cmdVehicle.Edit(obj);
            }

            btnSave.Enabled = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            alert.Visible = true;
            Response.AppendHeader("Refresh", "2; url=SearchVehicle.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SearchVehicle.aspx");
        }
    }
}