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
    public partial class ManageUnitType : System.Web.UI.Page
    {
        #region Declare variable
        private UNIT_TYPE _UnitType;
        private DataServiceEngine _dataServiceEngine;
        private UnitTypeService cmdUnitType;

        #endregion

        #region Sevice control
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
            cmdUnitType = (UnitTypeService)_dataServiceEngine.GetDataService(typeof(UnitTypeService));
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }


        private void InitialData()
        {

            if (Request.QueryString["id"] != null)
            {
                _UnitType = cmdUnitType.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
                if (_UnitType != null)
                {
                    txtUnitTypeName.Text = _UnitType.UNIT_TYPE_NAME;
                    txtUnitTypeSubName.Text = _UnitType.UNIT_TYPE_SUB_NAME;
                    flag.Text = "Edit";
                    lblName.Text = "แก้ไขขนาดบรรจุภัณฑ์";
                }
            }
        }
        #endregion

        #region Business
        bool SaveData()
        {
            try
            {
                USER userItem = Session["user"] as USER;
                var obj = new UNIT_TYPE();
                obj.UNIT_TYPE_NAME = txtUnitTypeName.Text;
                obj.UNIT_TYPE_SUB_NAME = txtUnitTypeSubName.Text;
                if (flag.Text.Equals("Add"))
                {
                    obj.Action = ActionEnum.Create;
                    obj.CREATE_DATE = DateTime.Now;
                    obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    obj.UPDATE_DATE = DateTime.Now;
                    obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    obj.SYE_DEL = false;
                    cmdUnitType.Add(obj);
                }
                else
                {
                    obj.Action = ActionEnum.Update;
                    obj.UNIT_TYPE_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                    obj.UPDATE_DATE = DateTime.Now;
                    obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    obj.SYE_DEL = false;
                    cmdUnitType.Edit(obj);
                }
                return true;
            }
            catch (Exception e)
            {
                lblError.Text = e.Message;
                return false;
            }
        }
        #endregion

        #region ASP control
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                alert.Visible = true;
                Response.AppendHeader("Refresh", "2; url=SearchUnitType.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SearchUnitType.aspx");
        }
        #endregion
    }
}