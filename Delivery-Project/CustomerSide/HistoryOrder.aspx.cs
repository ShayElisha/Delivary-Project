using DATA;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery_Project.CustomerSide
{
    public partial class HistoryOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrderHistory();
            }
        }

        private void LoadOrderHistory()
        {
            if (Session["CustomerID"] != null)
            {
                string customerId = Session["CustomerID"].ToString();
                DbContext db = new DbContext();

                try
                {
                    string sql = "SELECT * FROM H_Orders WHERE CustomerID = @CustomerID";
                    SqlCommand cmd = new SqlCommand(sql, db.conn);
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    RptProd.DataSource = dt;
                    RptProd.DataBind();
                }
                catch (Exception ex)
                {
                    
                }
                finally
                {
                    db.conn.Close();
                }
            }
            else
            {
                Response.Redirect("/MainPage.aspx");
            }
        }
    }
}