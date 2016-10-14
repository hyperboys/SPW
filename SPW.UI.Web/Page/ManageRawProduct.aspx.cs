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
    public partial class ManageRawProduct : System.Web.UI.Page
    {
        #region Declare variable
        private RAW_PRODUCT _rawProduct;
        private DataServiceEngine _dataServiceEngine;
        private RawProductService cmdRawProduct;
        private UnitTypeService cmdUnitType;
        private RawTypeService cmdRawType;

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
            cmdRawProduct = (RawProductService)_dataServiceEngine.GetDataService(typeof(RawProductService));
            cmdUnitType = (UnitTypeService)_dataServiceEngine.GetDataService(typeof(UnitTypeService));
            cmdRawType = (RawTypeService)_dataServiceEngine.GetDataService(typeof(RawTypeService));
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }


        private void InitialData()
        {
            ViewState["listRawType"] = cmdRawType.GetAll();
            foreach (var item in (List<RAW_TYPE>)ViewState["listRawType"])
            {
                ddlRawProductType.Items.Add(new ListItem(item.RAW_TYPE_NAME, item.RAW_TYPE_ID.ToString()));
            }

            ViewState["listUnitType"] = cmdUnitType.GetAll();
            foreach (var item in (List<UNIT_TYPE>)ViewState["listUnitType"])
            {
                ddlWDUnit.Items.Add(new ListItem(item.UNIT_TYPE_NAME, item.UNIT_TYPE_ID.ToString()));
                ddlHGUnit.Items.Add(new ListItem(item.UNIT_TYPE_NAME, item.UNIT_TYPE_ID.ToString()));
                ddlBDUnit.Items.Add(new ListItem(item.UNIT_TYPE_NAME, item.UNIT_TYPE_ID.ToString()));
            }

            if (Request.QueryString["id"] != null)
            {
                _rawProduct = cmdRawProduct.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
                if (_rawProduct != null)
                {
                    txtRawName1.Text = _rawProduct.RAW_NAME1;
                    txtRawName2.Text = _rawProduct.RAW_NAME2;
                    txtRawWD.Text = _rawProduct.RAW_WD.ToString();
                    txtRawHG.Text = _rawProduct.RAW_HG.ToString();
                    txtRawBD.Text = _rawProduct.RAW_BD.ToString();
                    ddlRawProductType.SelectedValue = _rawProduct.RAW_TYPE_ID.ToString();
                    ddlWDUnit.SelectedValue = _rawProduct.RAW_WD_UID.ToString();
                    ddlHGUnit.SelectedValue = _rawProduct.RAW_HG_UID.ToString();
                    ddlBDUnit.SelectedValue = _rawProduct.RAW_BD_UID.ToString();
                    flag.Text = "Edit";
                    lblName.Text = "แก้ไขวัตถุดิบ";
                }
            }
            else
            {
                rawOptionDiv.Visible = false;
                otherOptionDiv.Visible = false;
                btnSave.Visible = false;
            }
        }
        #endregion

        #region Business
        bool SaveData()
        {
            try
            {
                USER userItem = Session["user"] as USER;
                var obj = new RAW_PRODUCT();
                obj.RAW_NAME1 = txtRawName1.Text;
                if (ddlRawProductType.SelectedValue == "1")
                {
                    obj.RAW_NAME2 = txtRawName2.Text;
                    obj.RAW_WD = txtRawWD.Text;
                    obj.RAW_HG = txtRawHG.Text;
                    obj.RAW_BD = txtRawBD.Text;
                    obj.RAW_TYPE_ID = int.Parse(ddlRawProductType.SelectedValue);
                    obj.RAW_WD_UID = int.Parse(ddlWDUnit.SelectedValue);
                    obj.RAW_HG_UID = int.Parse(ddlHGUnit.SelectedValue);
                    obj.RAW_BD_UID = int.Parse(ddlBDUnit.SelectedValue);
                }
                else
                {
                    obj.RAW_NAME2 = ddlRawProductType.SelectedItem.Text;
                    obj.RAW_TYPE_ID = int.Parse(ddlRawProductType.SelectedValue);
                }
                if (flag.Text.Equals("Add"))
                {
                    obj.Action = ActionEnum.Create;
                    obj.CREATE_DATE = DateTime.Now;
                    obj.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    obj.UPDATE_DATE = DateTime.Now;
                    obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    obj.SYE_DEL = false;
                    cmdRawProduct.Add(obj);
                }
                else
                {
                    obj.Action = ActionEnum.Update;
                    obj.RAW_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                    obj.UPDATE_DATE = DateTime.Now;
                    obj.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    obj.SYE_DEL = false;
                    cmdRawProduct.Edit(obj);
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
                Response.AppendHeader("Refresh", "2; url=SearchRawProduct.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SearchRawProduct.aspx");
        }
        #endregion

        protected void ddlRawProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRawProductType.SelectedValue == "0")
            {
                rawOptionDiv.Visible = false;
                otherOptionDiv.Visible = false;
                btnSave.Visible = false;
            }
            else if (ddlRawProductType.SelectedValue == "1")
            {
                rawOptionDiv.Visible = true;
                otherOptionDiv.Visible = true;
                btnSave.Visible = true;
            }
            else
            {
                rawOptionDiv.Visible = true;
                otherOptionDiv.Visible = false;
                btnSave.Visible = true;
            }
        }
    }
}