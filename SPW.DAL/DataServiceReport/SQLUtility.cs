using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.DAL
{
    public class SQLUtility : DBBase
    {
        public Dictionary<string, string> SelectDistinc(string TABLE, string[] COLUMN_RESULT, string[] COLUMN_GROUPBY)
        {
            try
            {
                Dictionary<string, string> tmp = new Dictionary<string, string>();
                DBBase.ConncetDatabase();
                string sqlQuery = @"SELECT ";
                foreach (string column in COLUMN_RESULT)
                {
                    sqlQuery += column + ",";
                }
                sqlQuery = sqlQuery.Substring(0,sqlQuery.Length - 1);
                sqlQuery += " FROM " + TABLE + " WHERE SYE_DEL = 0 ";
                if (COLUMN_GROUPBY.Length > 0)
                {
                    sqlQuery += "GROUP BY ";
                    foreach (string groupBy in COLUMN_GROUPBY)
                    {
                        sqlQuery += groupBy + ",";
                    }
                }
                sqlQuery = sqlQuery.Substring(0, sqlQuery.Length - 1);
                SqlDataReader dr;
                SqlCommand command = new SqlCommand(sqlQuery, DBBase.con);
                dr = command.ExecuteReader();
                DataTable dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                }
                DBBase.DisConncetDatabase();
                foreach (DataRow dtr in dt.Rows)
                {
                    tmp.Add(dtr[0].ToString(), dtr[1].ToString());
                }
                return tmp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
