using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.DAL
{
    public class ReportMonthlySaleDataService : DBBase
    {
        public static DataTable LoadALL(string startDate = "",string endDate ="")
        {
            try
            {
                DBBase.ConncetDatabase();
                string sqlQuery = @"SELECT     DBO.EMPLOYEE.EMPLOYEE_NAME,SUM(DBO.[ORDER].ORDER_TOTAL) AS TOTAL
                        , 0 AS DEBT,(SUM(DBO.[ORDER].ORDER_TOTAL)-0) AS BALANCE
                        FROM DBO.[ORDER] INNER JOIN
                      DBO.STORE ON DBO.[ORDER].STORE_ID = DBO.STORE.STORE_ID INNER JOIN
                      DBO.ZONE_DETAIL ON DBO.STORE.ZONE_DETAIL_ID = DBO.ZONE_DETAIL.ZONE_DETAIL_ID INNER JOIN
                      DBO.EMPLOYEE ON DBO.ZONE_DETAIL.EMPLOYEE_ID = DBO.EMPLOYEE.EMPLOYEE_ID
                      WHERE 1=1";

                if (startDate != "")
                {
                    sqlQuery += " And convert(varchar, DBO.[ORDER].ORDER_DATE, 112) >= " + startDate;
                }
                if (endDate != "")
                {
                    sqlQuery += " And convert(varchar, DBO.[ORDER].ORDER_DATE, 112) <= " + endDate;
                }

                sqlQuery += "  GROUP BY DBO.EMPLOYEE.EMPLOYEE_NAME,DBO.EMPLOYEE.EMPLOYEE_SURNAME";

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
