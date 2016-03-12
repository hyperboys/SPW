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
            if (Request.QueryString["TRANS_LINE_ID"] != null)
            {
                List<TRANSPORT_LINE> listItem = _transpotService.SelectAll(Convert.ToInt32(Request.QueryString["TRANS_LINE_ID"].ToString()));
                txtTrans.Text = listItem[0].TRANS_LINE_NAME;
                grdTrans.DataSource = listItem;
                grdTrans.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

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

        }

        protected void txtStoreCode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}