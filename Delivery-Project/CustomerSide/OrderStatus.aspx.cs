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
            string sql = "INSERT INTO H_Orders (OrderID,CustomerID, FullName, Email, Phone, CityId, Address, Quantity, Notes, ChooseDeliveryTime,DeliveryTime,Datedelivery,status) VALUES ";
            sql += $"(N'{order.OrderID}',N'{order.CustomerID}', N'{order.FullName}', N'{order.Email}', N'{order.Phone}', {order.CityId}, N'{order.Address}', {order.Quantity}, N'{order.Notes}', {(order.ChooseDeliveryTime.HasValue ? $"'{order.ChooseDeliveryTime.Value.ToString("yyyy-MM-dd HH:mm:ss")}'" : "NULL")},N'{order.DeliveryTime}', N'{order.Datedelivery}', N'{order.status}')";

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