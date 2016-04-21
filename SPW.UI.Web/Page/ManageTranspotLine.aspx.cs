using SPW.Common;
using SPW.DAL;
using SPW.DataService;
using SPW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPW.UI.Web.Page
{
    public partial class ManageTranspotLine : System.Web.UI.Page
    {
        private StoreService _storeService;
        private DataServiceEngine _dataServiceEngine;
        private TransportLineService _transpotService;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CreatePageEngine();
                Session["listItem"] = new List<TRANSPORT_LINE>();
                InitialData();
                AutoCompleteStoreName();
                AutoCompleteStoreCode();

            }
            else
            {
                ReloadPageEngine();
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

        private void InitialDataService()
        {
            _storeService = (StoreService)_dataServiceEngine.GetDataService(typeof(StoreService));
            _transpotService = (TransportLineService)_dataServiceEngine.GetDataService(typeof(TransportLineService));
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void InitialData()
        {
            BindData();
        }

        private void BindData()
        {
            if (Request.QueryString["TRANS_LINE_ID"] != null && ((List<TRANSPORT_LINE>)Session["listItem"]).Count() == 0)
            {
                List<TRANSPORT_LINE> listItem = _transpotService.SelectAll(Convert.ToInt32(Request.QueryString["TRANS_LINE_ID"].ToString()));
                txtTrans.Text = listItem[0].TRANS_LINE_NAME;
                grdTrans.DataSource = listItem;
                grdTrans.DataBind();
            }
            //else if (Session["TRANS_LINE_ID"] != null)
            //{
            //    List<TRANSPORT_LINE> listItem = _transpotService.SelectAll(Convert.ToInt32(Session["TRANS_LINE_ID"].ToString()));
            //    txtTrans.Text = listItem[0].TRANS_LINE_NAME;
            //    grdTrans.DataSource = listItem;
            //    grdTrans.DataBind();
            //}
            else if (((List<TRANSPORT_LINE>)Session["listItem"]).Count() > 0)
            {
                List<TRANSPORT_LINE> listItem = Session["listItem"] as List<TRANSPORT_LINE>;
                txtTrans.Text = listItem[0].TRANS_LINE_NAME;
                grdTrans.DataSource = listItem;
                grdTrans.DataBind();
            }
            else
            {
                txtTrans.Enabled = true;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                STORE store = null;
                store = _storeService.Select(txtStoreCode.Text, txtStoreName.Text);
                if (store == null)
                {
                    //string script = "alert(\"ข้อมูลร้านค้าไม่ถูกต้อง\");";
                    //ScriptManager.RegisterStartupScript(this, GetType(),
                    //                      "ServerControlScript", script, true);
                    lblWarning.Text = "ข้อมูลร้านค้าไม่ถูกต้อง";
                    warning.Visible = true;
                    txtStoreCode.Focus();
                    return;
                }
                else
                {
                    TRANSPORT_LINE tmp = _transpotService.CheckStoreID(store.STORE_ID);
                    if (tmp != null)
                    {
                        //string script = "alert(\"ข้อมูลร้านค้านี้อยู่ในสายจัดรถ " + tmp.TRANS_LINE_NAME + "แล้ว\");";
                        //ScriptManager.RegisterStartupScript(this, GetType(),
                        //                      "ServerControlScript", script, true);
                        lblWarning.Text = "ข้อมูลร้านค้านี้อยู่ในสายจัดรถ " + tmp.TRANS_LINE_NAME + " แล้ว";
                        warning.Visible = true;
                        txtStoreCode.Focus();
                        return;
                    }
                }

                USER userItem = Session["user"] as USER;
                List<TRANSPORT_LINE> listItem = Session["listItem"] as List<TRANSPORT_LINE>;
                TRANSPORT_LINE item = new TRANSPORT_LINE();
                item.SYE_DEL = false;
                item.STORE = store;
                item.CREATE_DATE = DateTime.Now;
                item.CREATE_EMPLOYEE_ID = userItem.USER_ID;
                item.UPDATE_DATE = DateTime.Now;
                item.UPDATE_EMPLOYEE_ID = userItem.USER_ID;
                if (Request.QueryString["TRANS_LINE_ID"] != null && ((List<TRANSPORT_LINE>)Session["listItem"]).Count() == 0)
                {
                    listItem = _transpotService.SelectAll(Convert.ToInt32(Request.QueryString["TRANS_LINE_ID"].ToString()));
                    item.TRANS_LINE_ID = listItem[0].TRANS_LINE_ID;
                    item.TRANS_LINE_NAME = listItem[0].TRANS_LINE_NAME;

                }
                //else if (Session["TRANS_LINE_ID"] != null)
                //{
                //    List<TRANSPORT_LINE> listItem = _transpotService.SelectAll(Convert.ToInt32(Session["TRANS_LINE_ID"].ToString()));
                //    item.TRANS_LINE_ID = listItem[0].TRANS_LINE_ID;
                //    item.TRANS_LINE_NAME = listItem[0].TRANS_LINE_NAME;
                //}
                else
                {
                    item.TRANS_LINE_ID = _transpotService.GetCount() + 1;
                    item.TRANS_LINE_NAME = txtTrans.Text;
                }

                listItem.Add(item);
                Session["listItem"] = listItem;
                txtStoreCode.Text = "";
                txtStoreName.Text = "";
                txtTrans.Enabled = false;
                BindData();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        private void AutoCompleteStoreName()
        {
            List<string> nameList = SearchAutoCompleteDataService.Search("STORE", "STORE_NAME", "STORE_NAME", "");
            string str = "";
            for (int i = 0; i < nameList.Count; i++)
            {
                str = str + '"' + nameList[i].ToString() + '"' + ',';
            }
            if (str != "")
            {
                str = str.Remove(str.Length - 1);
            }
            str = "[" + str + "]";
            txtStoreName.Attributes.Add("data-source", str);
        }

        private void AutoCompleteStoreCode()
        {
            List<string> nameList = SearchAutoCompleteDataService.Search("STORE", "STORE_CODE", "STORE_CODE", "");
            string str = "";
            for (int i = 0; i < nameList.Count; i++)
            {
                str = str + '"' + nameList[i].ToString() + '"' + ',';
            }
            if (str != "")
            {
                str = str.Remove(str.Length - 1);
            }
            str = "[" + str + "]";
            txtStoreCode.Attributes.Add("data-source", str);
        }

        protected void grdTrans_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                _transpotService.Delete(Convert.ToInt32(grdTrans.DataKeys[e.RowIndex].Values[0].ToString()), Convert.ToInt32(grdTrans.DataKeys[e.RowIndex].Values[1].ToString()));
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
            InitialData();
        }

        protected void grdTrans_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (LinkButton button in e.Row.Cells[3].Controls.OfType<LinkButton>())
            {
                if (button.CommandName == "Delete")
                {
                    button.Attributes["onclick"] = "if(!confirm('ต้องการจะลบข้อมูลใช่หรือไม่')){ return false; };";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<TRANSPORT_LINE> listItem = Session["listItem"] as List<TRANSPORT_LINE>;
                foreach (TRANSPORT_LINE item in listItem)
                {
                    if (_transpotService.CheckStoreID(item.STORE.STORE_ID) == null) 
                    {
                        item.STORE_ID = item.STORE.STORE_ID;
                        item.STORE = null;
                        _transpotService.Add(item);
                    }
                }


                Session["listItem"] = null;
                alert.Visible = true;
                Response.AppendHeader("Refresh", "2; url=SearchTranspotLine.aspx");
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }
    }
}