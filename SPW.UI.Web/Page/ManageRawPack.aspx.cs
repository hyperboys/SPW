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
    public partial class ManageRawPack : System.Web.UI.Page
    {
        #region Declare variable
        private RAW_PACK_SIZE _rawPack;
        private DataServiceEngine _dataServiceEngine;
        private RawPackService cmdRawPack;

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
            cmdRawPack = (RawPackService)_dataServiceEngine.GetDataService(typeof(RawPackService));
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
                _rawPack = cmdRawPack.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
                if (_rawPack != null)
                {
                    txtPackSize.Text = _rawPack.RAW_PACK_SIZE1;
                    txtUnitQty.Text = _rawPack.RAW_PACK_SIZE_UNIT_QTY.ToString();
                    txtUnitDesc.Text = _rawPack.RAW_PACK_SIZE_UNIT_DESC;
                    txtPackQty.Text = _rawPack.RAW_PACK_SIZE_PACK_QTY.ToString();
                    txtPackDesc.Text = _rawPack.RAW_PACK_SIZE_PACK_DESC;
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
                var obj = new RAW_PACK_SIZE();
                obj.RAW_PACK_SIZE1 = txtPackSize.Text;
                obj.RAW_PACK_SIZE_UNIT_QTY = decimal.Parse(txtUnitQty.Text);
                obj.RAW_PACK_SIZE_UNIT_DESC = txtUnitDesc.Text;
                obj.RAW_PACK_SIZE_PACK_QTY = decimal.Parse(txtPackQty.Text);
                obj.RAW_PACK_SIZE_PACK_DESC = txtPackDesc.Text;
                if (flag.Text.Equals("Add"))
                {
                    obj.Action = ActionEnum.Create;
                    obj.CREATE_DATE = DateTime.Now;
                    obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    obj.UPDATE_DATE = DateTime.Now;
                    obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    obj.SYE_DEL = false;
                    cmdRawPack.Add(obj);
                }
                else
                {
                    obj.Action = ActionEnum.Update;
                    obj.RAW_PACK_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                    obj.UPDATE_DATE = DateTime.Now;
                    obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    obj.SYE_DEL = false;
                    cmdRawPack.Edit(obj);
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
                Response.AppendHeader("Refresh", "2; url=SearchRawPack.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SearchRawPack.aspx");
        }
        #endregion
    }
}