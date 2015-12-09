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
    public partial class ManageColorProduct : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private ColorService cmdColorService;
        private COLOR _color;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitialPage();
            }
            else
            {
                ReloadPageEngine();
            }
        }

        private void InitialPage()
        {
            CreatePageEngine();
            //ReloadDatasource();
            PrepareObjectScreen();
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void InitialDataService()
        {
            cmdColorService = (ColorService)_dataServiceEngine.GetDataService(typeof(ColorService));
        }

        private void ReloadDatasource()
        {
            //DataSouce = cmdColorService.GetAll();
        }

        private void PrepareObjectScreen()
        {
            if (Request.QueryString["id"] != null)
            {
                _color = cmdColorService.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
                if (_color != null)
                {
                    popTxtColorTypeName.Text = _color.COLOR_NAME;
                    popTxtColorTypeSubName.Text = _color.COLOR_SUBNAME;
                    lblName.Text = _color.COLOR_NAME;
                    flag.Text = "Edit";
                }
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            USER userItem = Session["user"] as USER;
            var obj = new COLOR();
            obj.COLOR_NAME = popTxtColorTypeName.Text;
            obj.COLOR_SUBNAME = popTxtColorTypeSubName.Text;
            if (flag.Text.Equals("Add"))
            {
                obj.Action = ActionEnum.Create;
                obj.CREATE_DATE = DateTime.Now;
                obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                cmdColorService.Add(obj);
            }
            else
            {
                obj.Action = ActionEnum.Update;
                obj.COLOR_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                obj.SYE_DEL = false;
                cmdColorService.Edit(obj);
            }

            btnSave.Enabled = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            alert.Visible = true;
            Response.AppendHeader("Refresh", "2; url=SearchColorProduct.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SearchColorProduct.aspx");
        }
    }
}