using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return null;
            }
        }
    }
}
