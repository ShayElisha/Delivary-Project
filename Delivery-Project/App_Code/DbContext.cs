using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace DATA
{
    public class DbContext
    {
        public string connStr { get; set; }
        public SqlConnection conn { get; set; }
        public DbContext()
        {
            connStr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            conn = new SqlConnection(connStr);
            conn.Open();

        }
        public void Close()
        {
            conn.Close();
        }
        public int ExecuteNonQuery(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            return cmd.ExecuteNonQuery();
        }

        public DataTable Execute(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter Da = new SqlDataAdapter(cmd);
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            return Dt;// החזרת טבלת הנתונים 
        }
        public object ExecuteScalar(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            return cmd.ExecuteScalar();
        }
        public object ExecuteScalar(string sql, SqlParameter[] parameters)
        {
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                return cmd.ExecuteScalar();
            }
        }
    }
}
