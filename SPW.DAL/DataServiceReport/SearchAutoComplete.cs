using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.Common;

namespace SPW.DAL
{
    public class SearchAutoCompleteDataService : DBBase
    {
        public static List<string> Search(string TABLE, string COLUMN_RESULT, string COLUMN_SEARCH = "", string TEXT_SEARCH = "")
        {
            try
            {
                List<string> tmp = new List<string>();
                DBBase.ConncetDatabase();
                string sqlQuery = @"SELECT " + COLUMN_RESULT + " FROM " + TABLE + " WHERE 1=1 ";
                if (COLUMN_SEARCH != "")
                {
                    sqlQuery += "AND " + COLUMN_SEARCH + " LIKE '%" + TEXT_SEARCH + "%'";
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
                    tmp.Add(dtr[COLUMN_RESULT].ToString());
                    //tmp.Add(string.Format("{0, dtr[COLUMN_RESULT], dtr[COLUMN_RESULT]));
                }
                return tmp;
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
                return null;
            }
        }

        public static List<string> SearchCondition(string TABLE, string COLUMN_RESULT, string COLUMN_SEARCH = "", string TEXT_SEARCH = "", string COLUMN_CONDITION = "", string TEXT_CONDITION = "")
        {
            try
            {
                List<string> tmp = new List<string>();
                DBBase.ConncetDatabase();
                string sqlQuery = @"SELECT " + COLUMN_RESULT + " FROM " + TABLE + " WHERE 1=1 ";
                if (COLUMN_SEARCH != "")
                {
                    sqlQuery += "AND " + COLUMN_SEARCH + " LIKE '%" + TEXT_SEARCH + "%'";
                }
                if (TEXT_CONDITION != "")
                {
                    sqlQuery += " AND " + COLUMN_CONDITION + " = '" + TEXT_CONDITION + "'";
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
                    tmp.Add(dtr[COLUMN_RESULT].ToString());
                    //tmp.Add(string.Format("{0, dtr[COLUMN_RESULT], dtr[COLUMN_RESULT]));
                }
                return tmp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<string> SearchGroupBy(string TABLE, string COLUMN_RESULT, string COLUMN_SEARCH = "", string TEXT_SEARCH = "", string GROUP_BY = "")
        {
            try
            {
                List<string> tmp = new List<string>();
                DBBase.ConncetDatabase();
                string sqlQuery = @"SELECT " + COLUMN_RESULT + " FROM " + TABLE + " WHERE 1=1 ";
                if (COLUMN_SEARCH != "")
                {
                    sqlQuery += "AND " + COLUMN_SEARCH + " LIKE '%" + TEXT_SEARCH + "%'";
                }
                if (GROUP_BY != "")
                {
                    sqlQuery += "GROUP BY " + GROUP_BY;
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
                    tmp.Add(dtr[COLUMN_RESULT].ToString());
                    //tmp.Add(string.Format("{0, dtr[COLUMN_RESULT], dtr[COLUMN_RESULT]));
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
