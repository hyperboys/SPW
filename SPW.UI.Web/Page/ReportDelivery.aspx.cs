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
            string query = @"SELECT  TOP 50 O.ORDER_CODE,CONVERT(VARCHAR(10),O.ORDER_DATE,101) AS ORDER_DATE,S.STORE_CODE,
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

            if (txtProductCode.Text != "")
            {
                query += " AND PRD.PRODUCT_CODE LIKE '%" + txtProductCode.Text + "%'";
            }

            if (txtStartDate.Text != "" && txtEndDate.Text != "")
            {
                query += @" AND O.ORDER_DATE BETWEEN CONVERT(DATE,'" + convertToDate102(txtStartDate.Text) + "') AND CONVERT(DATE,'" + convertToDate102(txtEndDate.Text) + "') ";
            }

            query += @" GROUP BY OD.PRODUCT_ID,OD.COLOR_ID,OD.COLOR_TYPE_ID,O.ORDER_CODE,O.ORDER_DATE,
                S.STORE_CODE,PRD.PRODUCT_NAME,CT.COLOR_TYPE_NAME,C.COLOR_NAME";

            DataSouce = sql.Query(query);
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