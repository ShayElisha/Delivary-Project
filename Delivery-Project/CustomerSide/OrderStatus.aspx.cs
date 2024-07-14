using DATA;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Delivery_Project.CustomerSide
{
    public partial class OrderStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Orders> orders = Orders.GetAll();
                if (orders != null && orders.Count > 0)
                {
                    LoadOrderHistory();
                }
                else
                {
                    NoOrdersPanel.Visible = true;
                }
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
                    string sql = "SELECT * FROM T_Orders WHERE CustomerID = @CustomerID";
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
        public static void SaveOrderH(Orders order)
        {
            string sql = "INSERT INTO H_Orders (OrderID, CustomerID, FullName, Email, Phone, CityId, Address, Quantity, Notes, ChooseDeliveryTime, DeliveryTime, Datedelivery, status) " +
                         "VALUES (@OrderID, @CustomerID, @FullName, @Email, @Phone, @CityId, @Address, @Quantity, @Notes, @ChooseDeliveryTime, @DeliveryTime, @Datedelivery, @status)";

            DbContext db = new DbContext();
            SqlCommand cmd = new SqlCommand(sql, db.conn);

            cmd.Parameters.AddWithValue("@OrderID", order.OrderID);
            cmd.Parameters.AddWithValue("@CustomerID", order.CustomerID);
            cmd.Parameters.AddWithValue("@FullName", order.FullName);
            cmd.Parameters.AddWithValue("@Email", order.Email);
            cmd.Parameters.AddWithValue("@Phone", order.Phone);
            cmd.Parameters.AddWithValue("@CityId", order.CityId);
            cmd.Parameters.AddWithValue("@Address", order.Address);
            cmd.Parameters.AddWithValue("@Quantity", order.Quantity);
            cmd.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(order.Notes) ? (object)DBNull.Value : order.Notes);
            cmd.Parameters.AddWithValue("@ChooseDeliveryTime", order.ChooseDeliveryTime.HasValue ? (object)order.ChooseDeliveryTime.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@DeliveryTime", order.DeliveryTime.HasValue ? (object)order.DeliveryTime.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@Datedelivery", order.Datedelivery.HasValue ? (object)order.Datedelivery.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@status", order.status);

            try
            {
                if (db.conn.State != ConnectionState.Open)
                {
                    db.conn.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה
                throw new Exception("Error saving order: " + ex.Message);
            }
            finally
            {
                if (db.conn.State != ConnectionState.Closed)
                {
                    db.conn.Close();
                }
            }
        }


        public static void SaveRating(int id, int num)
        {
            string sql;

            sql = "INSERT INTO Rating (DriverId,rating) VALUES ";
            sql += $"(N'{id}',N'{num}')";



            DbContext Db = new DbContext();
            Db.ExecuteNonQuery(sql);
        }
        public string ConvertStatusToText(object status)
        {
            switch (Convert.ToInt32(status))
            {
                case 1:
                    return "PENDING";
                case 2:
                    return "IN PROCESS";
                case 3:
                    return "COMPLETED";
                case 4:
                    return "CANCELED";
                default:
                    return "UNKNOWN";
            }
        }
        protected void ConfirmOrder_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int OrderID = Convert.ToInt32(btn.CommandArgument);
            // פעולה לאישור הזמנה כאן
            Orders order = Orders.GetById(OrderID);
            SaveOrderH(order);
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;
            DropDownList ddlRating = (DropDownList)item.FindControl("RatingDriver");
            int ratingValue = int.Parse(ddlRating.SelectedValue);
            DbContext db = new DbContext();
            // Save the rating
            string sql = "SELECT s.DriverID FROM T_Orders o INNER JOIN H_Shipment s ON o.OrderID = s.OrderID WHERE o.OrderID = @OrderID";
            SqlCommand cmd = new SqlCommand(sql, db.conn);
            cmd.Parameters.AddWithValue("@OrderID", order.OrderID); // Change to appropriate order ID
            
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int driverId = reader.GetInt32(reader.GetOrdinal("DriverID"));

                    // יצירת אובייקט דירוג
                    Rating rating = new Rating();

                    SaveRating(driverId, ratingValue);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message.Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "\\r");
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", $"alert('Error: {errorMessage}');", true);
            }

            finally
            {
                db.conn.Close();
            }

            Orders.Delete(OrderID);

            List<Orders> orders = Orders.GetAll();
            if (orders != null && orders.Count > 0)
            {
                List<Cities> cities = Application["cities"] as List<Cities>;

                foreach (var Order in orders)
                {
                    // עדכון שם העיר לפי מזהה העיר
                    Order.CityId = Cities.GetCityNameById(cities, Order.CityId);

                }
                
             }
            RptProd.DataSource = orders;
            RptProd.DataBind();
            NoOrdersPanel.Visible = false;
        }
    }
}