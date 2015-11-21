using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.DAL
{
    public class ReportSaleVolumeDataService : DBBase
    {
        public static DataTable LoadALL(string productCode = "",string productName ="",string sectorId = "0",string empId = "0")
        {
            try
            {
                DBBase.ConncetDatabase();
                string sqlQuery = @"SELECT dbo.PRODUCT.PRODUCT_CODE, dbo.PRODUCT.PRODUCT_NAME, dbo.SECTOR.SECTOR_NAME, dbo.EMPLOYEE.EMPLOYEE_NAME, 
                      SUM(dbo.ORDER_DETAIL.PRODUCT_QTY) AS SUMQTY, SUM(dbo.ORDER_DETAIL.PRODUCT_PRICE_TOTAL) AS SUMAMT
                        FROM dbo.PRODUCT INNER JOIN
                      dbo.ORDER_DETAIL ON dbo.PRODUCT.PRODUCT_ID = dbo.ORDER_DETAIL.PRODUCT_ID INNER JOIN
                      dbo.PRODUCT_PRICELIST ON dbo.PRODUCT.PRODUCT_ID = dbo.PRODUCT_PRICELIST.PRODUCT_ID INNER JOIN
                      dbo.[ORDER] ON dbo.ORDER_DETAIL.ORDER_ID = dbo.[ORDER].ORDER_ID INNER JOIN
                      dbo.STORE ON dbo.[ORDER].STORE_ID = dbo.STORE.STORE_ID INNER JOIN
                      dbo.ZONE_DETAIL ON dbo.STORE.ZONE_DETAIL_ID = dbo.ZONE_DETAIL.ZONE_DETAIL_ID INNER JOIN
                      dbo.EMPLOYEE ON dbo.ZONE_DETAIL.EMPLOYEE_ID = dbo.EMPLOYEE.EMPLOYEE_ID INNER JOIN
                      dbo.ZONE ON dbo.PRODUCT_PRICELIST.ZONE_ID = dbo.ZONE.ZONE_ID AND dbo.STORE.ZONE_ID = dbo.ZONE.ZONE_ID AND 
                      dbo.ZONE_DETAIL.ZONE_ID = dbo.ZONE.ZONE_ID INNER JOIN
                      dbo.SECTOR ON dbo.STORE.SECTOR_ID = dbo.SECTOR.SECTOR_ID
                        WHERE (dbo.ORDER_DETAIL.IS_FREE = 'N') ";

                if (productCode != "")
                {
                    sqlQuery += " And dbo.PRODUCT.PRODUCT_CODE like '%" + productCode + "%'";
                }
                if (productName != "")
                {
                    sqlQuery += " And dbo.PRODUCT.PRODUCT_NAME like '%" + productName + "%'";
                }
                if (sectorId != "0")
                {
                    sqlQuery += " And dbo.SECTOR.SECTOR_ID = '" + sectorId + "'";
                }
                if (empId != "0")
                {
                    sqlQuery += " And dbo.EMPLOYEE.EMPLOYEE_ID = '" + empId + "'";
                }

                sqlQuery += "GROUP BY dbo.PRODUCT.PRODUCT_CODE, dbo.PRODUCT.PRODUCT_NAME, dbo.ZONE.ZONE_NAME, dbo.EMPLOYEE.EMPLOYEE_NAME,dbo.SECTOR.SECTOR_NAME";

                SqlDataReader dr;
                SqlCommand command = new SqlCommand(sqlQuery, DBBase.con);
                dr = command.ExecuteReader();
                DataTable dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                }
                DBBase.DisConncetDatabase();
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
