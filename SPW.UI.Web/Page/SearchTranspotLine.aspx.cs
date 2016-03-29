using SPW.DAL;
using SPW.DataService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPW.UI.Web.Page
{
    public partial class SearchTranspotLine : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private TransportLineService _transpotService;

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
            InitialData();
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

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void InitialDataService()
        {
            _transpotService = (TransportLineService)_dataServiceEngine.GetDataService(typeof(TransportLineService));
        }

        private void InitialData()
        {
            SearchTranspot();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchTranspot();
        }

        private void SearchTranspot()
        {
            SQLUtility sql = new SQLUtility();
            //Dictionary<string, string> listTrans = sql.SelectDistincCondition("TRANSPORT_LINE", new string[] { "TRANS_LINE_ID", "TRANS_LINE_NAME" }, new string[] { "TRANS_LINE_ID", "TRANS_LINE_NAME" }, txtTrans.Text != "" ? "TRANS_LINE_NAME" : "", txtTrans.Text != "" ? txtTrans.Text : "", ddlStatus.SelectedValue);
            Dictionary<string, string> listTrans = sql.SelectDistincCondition("TRANSPORT_LINE", new string[] { "TRANS_LINE_ID", "TRANS_LINE_NAME" }, new string[] { "TRANS_LINE_ID", "TRANS_LINE_NAME" }, txtTrans.Text != "" ? "TRANS_LINE_NAME" : "", txtTrans.Text != "" ? txtTrans.Text : "");
            DataTable dataTrans = new DataTable();
            dataTrans.Columns.Add("TRANS_LINE_ID");
            dataTrans.Columns.Add("TRANS_LINE_NAME");
            if (listTrans != null)
            {
                foreach (KeyValuePair<string, string> entry in listTrans)
                {
                    DataRow dataRow = dataTrans.NewRow();
                    dataRow["TRANS_LINE_ID"] = entry.Key;
                    dataRow["TRANS_LINE_NAME"] = entry.Value;
                    dataTrans.Rows.Add(dataRow);
                }
            }
            grdTrans.DataSource = dataTrans;
            grdTrans.DataBind();
        }

        protected void grdTrans_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string TRANS_LINE_ID = grdTrans.DataKeys[e.Row.RowIndex][0].ToString();
                LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                lbtnEdit.Attributes["href"] = "ManageTranspotLine.aspx?TRANS_LINE_ID=" + TRANS_LINE_ID;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}