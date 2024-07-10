using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DAL;
using DATA;
using dotenv.net;

namespace Delivery_Project.DriverSide
{
    public partial class WorkDriver : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var key= ConfigurationManager.AppSettings["SecretKey"].ToString();
                if (Session["Driver"] == null)
                {
                    // אם לא, להעביר לעמוד התחברות
                    Response.Redirect("/MainPage.aspx");
                }
                string driverId = Session["Id"].ToString();
                List<Shipments> shipments = GetShipmentsByDriverId(driverId);
                List<Cities> cities = Application["cities"] as List<Cities>;

                foreach (var shipment in shipments)
                {
                    shipment.SourceCity = Cities.GetCityNameById(cities, shipment.SourceCity);
                    shipment.DestinationCity = Cities.GetCityNameById(cities, shipment.DestinationCity);
                }

                RptProd.DataSource = shipments;
                RptProd.DataBind();
                string jsonShipments = Newtonsoft.Json.JsonConvert.SerializeObject(shipments);
                ClientScript.RegisterStartupScript(this.GetType(), "loadMap", $"loadMap({jsonShipments});", true);
            }
        }

        public static List<Shipments> GetShipmentsByDriverId(string driverId)
        {
            DbContext Db = new DbContext(); // יצירת אובייקט מסוג גישה לבסיס נתונים
            string sql = $"SELECT * FROM T_Shipments WHERE DriverId = @DriverId";

            SqlCommand cmd = new SqlCommand(sql, Db.conn);
            cmd.Parameters.AddWithValue("@DriverId", driverId);
            SqlDataReader Dr = cmd.ExecuteReader();

            List<Shipments> shipments = new List<Shipments>();
            while (Dr.Read())
            {
                Shipments shipment = new Shipments()
                {
                    ShipId = int.Parse(Dr["ShipId"].ToString()),
                    OrderID = Dr["OrderID"].ToString(),
                    CustomerID = Dr["CustomerID"].ToString(),
                    CustomerName = Dr["CustomerName"].ToString(),
                    SourceAdd = Dr["SourceAdd"].ToString(),
                    SourceCity = Dr["SourceCity"].ToString(),
                    DestinationAdd = Dr["DestinationAdd"].ToString(),
                    DestinationCity = Dr["DestinationCity"].ToString(),
                    Phone = Dr["Phone"].ToString(),
                    CollectionDate = Dr["CollectionDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(Dr["CollectionDate"]),
                    DateDelivery = Dr["DateDelivery"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(Dr["DateDelivery"]),
                    ReleaseDate = Dr["ReleaseDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(Dr["ReleaseDate"]),
                    Quantity = int.Parse(Dr["Quantity"].ToString()),
                    DriverId = int.Parse(Dr["DriverId"].ToString()),
                    Status = int.Parse(Dr["Status"].ToString())
                };
                shipments.Add(shipment);
            }
            Dr.Close();
            Db.Close();
            return shipments;
        }

        [System.Web.Services.WebMethod]
        public static List<Shipments> GetShipments()
        {
            return Shipments.GetAll();
        }

        public static void SaveShipH(Shipments shipment)
        {
            // Convert dates to proper format or set as NULL if null
            string collectionDate = shipment.CollectionDate.HasValue ? shipment.CollectionDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "NULL";
            string dateDelivery = shipment.DateDelivery.HasValue ? shipment.DateDelivery.Value.ToString("yyyy-MM-dd HH:mm:ss") : "NULL";
            string releaseDate = shipment.ReleaseDate.HasValue ? shipment.ReleaseDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "NULL";

            // Ensure proper formatting for SQL insertion
            collectionDate = collectionDate != "NULL" ? $"'{collectionDate}'" : collectionDate;
            dateDelivery = dateDelivery != "NULL" ? $"'{dateDelivery}'" : dateDelivery;
            releaseDate = releaseDate != "NULL" ? $"'{releaseDate}'" : releaseDate;

            string sql = "INSERT INTO H_Shipment (ShipId, OrderID, CustomerID, SourceAdd, SourceCity, DestinationAdd, DestinationCity, Phone, Quantity, CollectionDate, DateDelivery, ReleaseDate, DriverId, Status) VALUES ";
            sql += $"({shipment.ShipId}, {shipment.OrderID}, {shipment.CustomerID}, N'{shipment.SourceAdd}', N'{shipment.SourceCity}', N'{shipment.DestinationAdd}', N'{shipment.DestinationCity}', N'{shipment.Phone}', {shipment.Quantity}, {collectionDate}, {dateDelivery}, {releaseDate}, {shipment.DriverId}, {shipment.Status})";

            DbContext Db = new DbContext();
            Db.ExecuteNonQuery(sql);
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var dataItem = (RepeaterItem)button.NamingContainer;
            var sourceCity = ((Literal)dataItem.FindControl("SourceCity")).Text;
            var sourceAdd = ((Literal)dataItem.FindControl("SourceAdd")).Text;
            var destinationCity = ((Literal)dataItem.FindControl("DestinationCity")).Text;
            var destinationAdd = ((Literal)dataItem.FindControl("DestinationAdd")).Text;

            string script = $"navigateFromServer('{sourceCity}', '{sourceAdd}', '{destinationCity}', '{destinationAdd}');";
            ClientScript.RegisterStartupScript(this.GetType(), "navigate", script, true);
        }

        protected void SendWhatsAppButton_Click(object sender, EventArgs e)
        {
            string customerPhone = ((Button)sender).CommandArgument;

            // Ensure the phone number is in international format
            if (!customerPhone.StartsWith("+"))
            {
                customerPhone = "+972" + customerPhone.TrimStart('0');
            }

            string messageBody = "החבילה נאספה והיא בדרך אלייך";
            string url = $"https://wa.me/{customerPhone}?text={Uri.EscapeDataString(messageBody)}";

            // Register a script to open the URL in a new tab
            ClientScript.RegisterStartupScript(this.GetType(), "openWhatsApp", $"window.open('{url}', '_blank');", true);
        }

        protected void DeliverOrderButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string[] arguments = button.CommandArgument.Split(',');
            int shipId = Convert.ToInt32(arguments[0]);
            string phone = "+972" + arguments[1];
            int OrderID = Convert.ToInt32(arguments[2]);
            string DriverId = arguments[3];
            UpdateShipmentStatusAndDate(shipId, OrderID, 3);

            string messageBody = "החבילה נמסרה תודה שהשתמשתם בשירותים שלנו חברת FHD";
            string url = $"https://wa.me/{phone}?text={Uri.EscapeDataString(messageBody)}";

            ClientScript.RegisterStartupScript(this.GetType(), "openWhatsApp", $"window.open('{url}', '_blank');", true);
            Shipments shipment = Shipments.GetById(shipId);
            if (shipment != null)
            {
                Driver.RemoveQuantityFromDriver(shipment.DriverId, shipment.Quantity);
                Orders Order = Orders.GetById(OrderID);
                SaveShipH(shipment);
                Shipments.Delete(shipId);

                List<Shipments> shipments = Shipments.GetAll();
                List<Cities> cities = Application["cities"] as List<Cities>;

                foreach (var Shipment in shipments)
                {
                    Shipment.SourceCity = Cities.GetCityNameById(cities, Shipment.SourceCity);
                    Shipment.DestinationCity = Cities.GetCityNameById(cities, Shipment.DestinationCity);
                }

                RptProd.DataSource = shipments;
                RptProd.DataBind();
                Salaries.calculateSalary(DriverId);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "shipmentNotFound", "alert('המשלוח לא נמצא');", true);
            }
        }

        protected void AddToTruckButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string[] arguments = button.CommandArgument.Split(',');
            int shipId = Convert.ToInt32(arguments[0]);
            string phone = "+972" + arguments[1];
            int OrderID = Convert.ToInt32(arguments[2]);
            UpdateOrderStatusAndDate(shipId, OrderID, 2);
            string messageBody = "החבילה נאספה והיא בדרך אלייך";
            string url = $"https://wa.me/{phone}?text={Uri.EscapeDataString(messageBody)}";

            // Register a script to open the URL in a new tab
            ClientScript.RegisterStartupScript(this.GetType(), "openWhatsApp", $"window.open('{url}', '_blank');", true);
            // לאחר העדכון, החלפת הכפתורים תתבצע
            ClientScript.RegisterStartupScript(this.GetType(), "showHiddenButtons", "showHiddenButtons(document.getElementById('" + button.ClientID + "'));", true);
        }

        private void UpdateOrderStatusAndDate(int shipId, int OrderId, int status)
        {
            Shipments shipment = Shipments.GetById(shipId);

            if (shipment == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "OrderNotFound", "alert('המשלוח לא נמצא');", true);
                return;
            }

            shipment.Status = status;
            shipment.ReleaseDate = DateTime.Now;
            ShipmentsDAL.Save(shipment);

            Orders order = Orders.GetById(OrderId);

            if (order == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "OrderNotFound", "alert('המשלוח לא נמצא');", true);
                return;
            }

            order.status = status;
            order.DeliveryTime = DateTime.Now;
            order.Save();
        }

        private void UpdateShipmentStatusAndDate(int shipId, int OrderId, int status)
        {
            Shipments shipment = Shipments.GetById(shipId);

            if (shipment == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "OrderNotFound", "alert('המשלוח לא נמצא');", true);
                return;
            }

            shipment.Status = status;
            shipment.CollectionDate = DateTime.Now;
            ShipmentsDAL.Save(shipment);
            Orders order = Orders.GetById(OrderId);

            if (order == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "OrderNotFound", "alert('המשלוח לא נמצא');", true);
                return;
            }

            order.status = status;
            order.Datedelivery = DateTime.Now;
            order.Save();

            // Display success message
            ClientScript.RegisterStartupScript(this.GetType(), "DeliverySuccess", "alert('המשלוח נמסר בהצלחה');", false);
        }
    }
}
