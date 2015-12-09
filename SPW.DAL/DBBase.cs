using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SPW.DAL
{
    public class DBBase
    {
        protected static SqlConnection con;
        protected static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["SPWConnect"].ConnectionString;
            }
        }

        protected static void ConncetDatabase()
        {
            con = new SqlConnection();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.ConnectionString = ConnectionString;
            con.Open();
        }

        protected static void DisConncetDatabase()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
