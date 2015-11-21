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
    public partial class ManageZone : System.Web.UI.Page
    {
        private ZONE _zone;
        private DataServiceEngine _dataServiceEngine;
        private ZoneService cmdZone;
        private EmployeeService cmdEmp;

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
            cmdZone = (ZoneService)_dataServiceEngine.GetDataService(typeof(ZoneService));
            cmdEmp = (EmployeeService)_dataServiceEngine.GetDataService(typeof(EmployeeService));
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
                _zone = cmdZone.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
                if (_zone != null)
                {
                    popTxtZoneCode.Text = _zone.ZONE_CODE;
                    popTxtZoneName.Text = _zone.ZONE_NAME;
                }

                flag.Text = "Edit";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            USER userItem = Session["user"] as USER;
            var obj = new ZONE();
            obj.ZONE_CODE = popTxtZoneCode.Text;
            obj.ZONE_NAME = popTxtZoneName.Text;
            if (flag.Text.Equals("Add"))
            {
                obj.Action = ActionEnum.Create;
                obj.CREATE_DATE = DateTime.Now;
                obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                cmdZone.Add(obj);
            }
            else
            {
                obj.Action = ActionEnum.Update;
                obj.ZONE_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                cmdZone.Edit(obj);
            }
            alert.Visible = true;
            Response.AppendHeader("Refresh", "2; url=SearchZone.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SearchZone.aspx");
        }
    }
}