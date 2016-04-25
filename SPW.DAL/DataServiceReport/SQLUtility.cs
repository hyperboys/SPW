using SPW.Common;
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
                if (COLUMN_RESULT.Count() > 1)
                {
                    foreach (string column in COLUMN_RESULT)
                    {
                        sqlQuery += column + ",";
                    }
                }
                else
                {
                    sqlQuery += COLUMN_RESULT[0] + " ";
                }

                sqlQuery = sqlQuery.Substring(0, sqlQuery.Length - 1);
                sqlQuery += " FROM " + TABLE + " WHERE SYE_DEL = 0 ";
                if (COLUMN_GROUPBY.Length > 0)
                {
                    sqlQuery += "GROUP BY ";
                    if (COLUMN_GROUPBY.Length > 1)
                    {
                        foreach (string groupBy in COLUMN_GROUPBY)
                        {
                            sqlQuery += groupBy + ",";
                        }
                        sqlQuery = sqlQuery.Substring(0, sqlQuery.Length - 1);
                    }
                    else
                    {
                        sqlQuery += COLUMN_GROUPBY[0] + "";
                    }
                }

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
                    if (COLUMN_RESULT.Count() > 1)
                    {
                        tmp.Add(dtr[0].ToString(), dtr[1].ToString());
                    }
                    else
                    {
                        tmp.Add(dtr[0].ToString(), "");
                    }
                }
                return tmp;
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
                return null;
            }
        }

        public Dictionary<string, string> SelectDistincCondition(string TABLE, string[] COLUMN_RESULT, string[] COLUMN_GROUPBY, string COLUMN_WHERE = "", string TEXT_SEARCH = "", string SYE_DEL = "0")
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
                sqlQuery = sqlQuery.Substring(0, sqlQuery.Length - 1);
                sqlQuery += " FROM " + TABLE + " WHERE SYE_DEL = " + SYE_DEL;

                if (COLUMN_WHERE != "")
                {
                    sqlQuery += " AND " + COLUMN_WHERE + " LIKE '%" + TEXT_SEARCH + "%'  ";
                }

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
                DebugLog.WriteLog(ex.ToString());
                return null;
            }
        }

        public int GetCount(string sql)
        {
            try
            {
                SqlDataReader dr;
                DBBase.ConncetDatabase();
                SqlCommand command = new SqlCommand(sql, DBBase.con);
                dr = command.ExecuteReader();
                DataTable dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                }
                DBBase.DisConncetDatabase();
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
                return 0;
            }
        }

        public decimal GetAmount(string sql)
        {
            try
            {
                SqlDataReader dr;
                DBBase.ConncetDatabase();
                SqlCommand command = new SqlCommand(sql, DBBase.con);
                dr = command.ExecuteReader();
                DataTable dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                }
                DBBase.DisConncetDatabase();
                return Convert.ToDecimal(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
                return 0;
            }
        }

        public void SumAmount(string sql)
        {
            try
            {
                DBBase.ConncetDatabase();
                SqlCommand command = new SqlCommand(sql, DBBase.con);
                command.ExecuteNonQuery();
                DBBase.DisConncetDatabase();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }
    }
}
