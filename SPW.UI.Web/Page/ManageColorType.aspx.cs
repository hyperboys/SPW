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
    public partial class ManageColorType : System.Web.UI.Page
    {
        private COLOR_TYPE _colorType;
        private DataServiceEngine _dataServiceEngine;
        private ColorTypeService cmdColor;

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
            cmdColor = (ColorTypeService)_dataServiceEngine.GetDataService(typeof(ColorTypeService));
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
                _colorType = cmdColor.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
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
            USER userItem = Session["user"] as USER;
            var obj = new COLOR_TYPE();
            obj.COLOR_TYPE_NAME = popTxtColorTypeName.Text;
            obj.COLOR_TYPE_SUBNAME = popTxtColorTypeSubName.Text;
            if (flag.Text.Equals("Add"))
            {
                obj.Action = ActionEnum.Create;
                obj.CREATE_DATE = DateTime.Now;
                obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                cmdColor.Add(obj);
            }
            else
            {
                obj.Action = ActionEnum.Update;
                obj.COLOR_TYPE_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                cmdColor.Edit(obj);
            }
            alert.Visible = true;
            Response.AppendHeader("Refresh", "2; url=SearchColorType.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SearchColorType.aspx");
        }
    }
}