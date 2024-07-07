using DATA;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery_Project.DriverSide
{
    public partial class HistoryShipments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadOrderHistory();
        }
        private void LoadOrderHistory()
        {
            if (Session["DriverID"] != null)
            {
                string DriverId = Session["Id"].ToString();
                DbContext db = new DbContext();

                try
                {
                    string sql = "SELECT * FROM H_Shipment WHERE DriverId = @DriverId";
                    SqlCommand cmd = new SqlCommand(sql, db.conn);
                    cmd.Parameters.AddWithValue("@DriverId", DriverId);

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