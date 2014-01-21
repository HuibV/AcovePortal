using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AcovePortal
{
    public class SqlHandler
    {      
        /// <summary>
        /// Send a select query to the database and return the results as datatable
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["AcoveDb"].ConnectionString))
            {
                conn.Open();
                using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public static void ExecuteQuery(string query)
        {
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["AcoveDb"].ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query,conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}