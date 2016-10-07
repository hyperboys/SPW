using SPW.DAL;
using SPW.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPW.UI.Web.Page
{
    public partial class ReportDelivery : System.Web.UI.Page
    {
        public DataTable DataSouce
        {
            get
            {
                if (ViewState["listStore"] == null)
                {
                    ViewState["listStore"] = new DataTable();
                }
                var list = (DataTable)ViewState["listStore"];
                return list;
            }
            set
            {
                ViewState["listStore"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SQLUtility sql = new SQLUtility();
            string query = @"SELECT O.ORDER_CODE,CONVERT(VARCHAR(10),O.ORDER_DATE,101) AS ORDER_DATE,S.STORE_CODE,
                PRD.PRODUCT_NAME,CT.COLOR_TYPE_NAME,C.COLOR_NAME,
                SUM(OD.PRODUCT_QTY) AS PRODUCT_QTY
                ,SUM(OD.PRODUCT_SEND_QTY) AS PRODUCT_SEND_QTY,SUM(OD.PRODUCT_SEND_REMAIN) AS PRODUCT_SEND_REMAIN
                FROM [ORDER] O INNER JOIN [ORDER_DETAIL] OD ON O.ORDER_ID = OD.ORDER_ID
                INNER JOIN PRODUCT PRD ON OD.PRODUCT_ID = PRD.PRODUCT_ID
                INNER JOIN COLOR C ON OD.COLOR_ID = C.COLOR_ID
                INNER JOIN COLOR_TYPE CT ON OD.COLOR_TYPE_ID = CT.COLOR_TYPE_ID
                INNER JOIN STORE S ON O.STORE_ID = S.STORE_ID WHERE 1=1";

            if (txtOrderCode.Text != "")
            {
                query += " AND O.ORDER_CODE LIKE '%" + txtOrderCode.Text + "%'";
            }

            if (txtStoreCode.Text != "")
            {
                query += " AND S.STORE_CODE  LIKE '%" + txtStoreCode.Text + "%'";
            }

            if (txtProductCode.Text != "")
            {
                query += " AND PRD.PRODUCT_CODE LIKE '%" + txtProductCode.Text + "%'";
            }

            if (ddlStatus.SelectedValue != "0")
            {
                query += " AND O.ORDER_STEP = " + ddlStatus.SelectedValue;
            }

            if (txtStartDate.Text != "" && txtEndDate.Text != "")
            {
                query += @" AND O.ORDER_DATE BETWEEN CONVERT(DATE,'" + convertToDate102(txtStartDate.Text) + "') AND CONVERT(DATE,'" + convertToDate102(txtEndDate.Text) + "') ";
            }

            query += @" GROUP BY OD.PRODUCT_ID,OD.COLOR_ID,OD.COLOR_TYPE_ID,O.ORDER_CODE,O.ORDER_DATE,
                S.STORE_CODE,PRD.PRODUCT_NAME,CT.COLOR_TYPE_NAME,C.COLOR_NAME";

            #region SqlSumOld
//            query += " UNION ";

//            query += @" SELECT  TOP 1 'ยอดรวม' AS ORDER_CODE,'' AS ORDER_DATE,'' AS STORE_CODE,
//                '' AS PRODUCT_NAME,'' AS COLOR_TYPE_NAME,'' AS COLOR_NAME,
//                SUM(OD.PRODUCT_QTY) AS PRODUCT_QTY
//                ,SUM(OD.PRODUCT_SEND_QTY) AS PRODUCT_SEND_QTY,SUM(OD.PRODUCT_SEND_REMAIN) AS PRODUCT_SEND_REMAIN
//                FROM [ORDER] O INNER JOIN [ORDER_DETAIL] OD ON O.ORDER_ID = OD.ORDER_ID
//                INNER JOIN PRODUCT PRD ON OD.PRODUCT_ID = PRD.PRODUCT_ID
//                INNER JOIN COLOR C ON OD.COLOR_ID = C.COLOR_ID
//                INNER JOIN COLOR_TYPE CT ON OD.COLOR_TYPE_ID = CT.COLOR_TYPE_ID
//                INNER JOIN STORE S ON O.STORE_ID = S.STORE_ID WHERE 1=1 ";

//            if (txtOrderCode.Text != "")
//            {
//                query += " AND O.ORDER_CODE LIKE '%" + txtOrderCode.Text + "%'";
//            }

//            if (txtStoreCode.Text != "")
//            {
//                query += " AND S.STORE_CODE  LIKE '%" + txtStoreCode.Text + "%'";
//            }

//            if (txtProductCode.Text != "")
//            {
//                query += " AND PRD.PRODUCT_CODE LIKE '%" + txtProductCode.Text + "%'";
//            }

//            if (ddlStatus.SelectedValue != "0")
//            {
//                query += " AND O.ORDER_STEP = " + ddlStatus.SelectedValue;
//            }

//            if (txtStartDate.Text != "" && txtEndDate.Text != "")
//            {
//                query += @" AND O.ORDER_DATE BETWEEN CONVERT(DATE,'" + convertToDate102(txtStartDate.Text) + "') AND CONVERT(DATE,'" + convertToDate102(txtEndDate.Text) + "') ";
//            }
            #endregion

            Sumary.Visible = true;
            DataSouce = sql.Query(query);
            int sumOrder = 0;
            int sumSend = 0;
            int sumTotal = 0;

            foreach (DataRow item in DataSouce.Rows)
            {
                sumOrder += Convert.ToInt32(item[6].ToString());
                sumSend += Convert.ToInt32(item[7].ToString());
                sumTotal += Convert.ToInt32(item[8].ToString());
            }

            this.sumOrder.Text = sumOrder.ToString();
            this.sumSend.Text = sumSend.ToString();
            this.sumTotal.Text = sumTotal.ToString();

            gridProduct.DataSource = DataSouce;
            gridProduct.DataBind();
        }

        private string convertToDate102(string date)
        {
            if (date != "")
            {
                string[] tmp = date.Split('/');

                return tmp[2] + tmp[1] + tmp[0];
            }
            else
            {
                return date;
            }

        }
    }
}