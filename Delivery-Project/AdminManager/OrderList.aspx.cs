using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery_Project.AdminManager
{
    public partial class OrderList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrders();
            }
        }

        private void LoadOrders()
        {
            List<Orders> orders = Orders.GetAll();
            if (orders != null && orders.Count > 0)
            {
                foreach (var order in orders)
                {
                    order.HasShipment = CheckOrderShipment(order.OrderID);
                }
                RptProd.DataSource = orders;
                RptProd.DataBind();
                NoShipmentsPanel.Visible = false;
            }
            else
            {
                NoShipmentsPanel.Visible = true;
            }
        }

        private bool CheckOrderShipment(int orderId)
        {
            DbContext db = new DbContext();
            string sql = "SELECT COUNT(*) FROM T_Orders o INNER JOIN T_Shipments s ON o.OrderID = s.OrderID WHERE o.OrderID = @OrderId";
            SqlCommand cmd = new SqlCommand(sql, db.conn);
            cmd.Parameters.AddWithValue("@OrderId", orderId);
            int count = (int)cmd.ExecuteScalar();
            db.Close();
            return count > 0;
        }

        protected void RptProd_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var order = (Orders)e.Item.DataItem;
                var linkButton = (HyperLink)e.Item.FindControl("ExportButton");

                if (order.HasShipment)
                {
                    linkButton.Text = "הזמנה נמצאת במשלוחים";
                    linkButton.Attributes.Add("class", "disabled-link");
                    linkButton.NavigateUrl = "#"; // Prevent navigation
                }
                else if (order.status == 3)
                {
                    linkButton.Text = "ממתין לאישור הלקוח";
                    linkButton.Attributes.Add("class", "disabled-link");
                    linkButton.NavigateUrl = "#"; // Prevent navigation
                }
                else
                {
                    linkButton.Text = "ייצא לנהג";
                }
            }
        }

        protected void btnWork_Click(object sender, EventArgs e)
        {
            string dateValue = Request.Form["dateToWork"];
            if (string.IsNullOrEmpty(dateValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('אנא בחר תאריך');", true);
                return;
            }

            DateTime date;
            if (!DateTime.TryParse(dateValue, out date))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('תאריך לא חוקי');", true);
                return;
            }

            List<Driver> drivers = Driver.GetAll();
            List<Area> listArea = Area.GetAll();

            foreach (var driver in drivers)
            {
                DbContext db = new DbContext();
                try
                {
                    string sql = "SELECT o.* FROM T_Orders o LEFT JOIN T_Shipments s ON o.OrderId = s.OrderId WHERE s.OrderId IS NULL AND o.ChooseDeliveryTime = @date";
                    SqlCommand cmd = new SqlCommand(sql, db.conn);
                    cmd.Parameters.AddWithValue("@date", date);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        reader.Close();
                        continue; // Move to the next driver
                    }

                    while (reader.Read())
                    {
                        int quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                        if (driver.CurrentAmount + quantity <= driver.MaxAmountShipment)
                        {
                            int cityId = int.Parse(reader.GetString(reader.GetOrdinal("CityId")));
                            if (cityId == driver.CityId)
                            {
                                driver.CurrentAmount += quantity;
                                driver.Save();

                                Shipments shipment = new Shipments
                                {
                                    ShipId = -1,
                                    OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")).ToString(),
                                    CustomerID = reader.GetString(reader.GetOrdinal("CustomerId")).ToString(),
                                    SourceAdd = "אריה בן אליעזר 106",
                                    SourceCity = "89",
                                    DestinationAdd = reader.GetString(reader.GetOrdinal("Address")),
                                    DestinationCity = reader.GetString(reader.GetOrdinal("CityId")),
                                    DateDelivery = reader.GetDateTime(reader.GetOrdinal("ChooseDeliveryTime")),
                                    CustomerName = reader.GetString(reader.GetOrdinal("FullName")),
                                    Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                    DriverId = driver.DriverID,
                                    Quantity = quantity,
                                    Status = reader.GetInt32(reader.GetOrdinal("status"))
                                };

                                shipment.Save();
                            }
                            else
                            {
                                int k = -1;
                                for (int i = 0; i < listArea.Count; i++)
                                {
                                    string[] cities = listArea[i].ListCities.Split(','); // Split the cities list once

                                    for (int j = 0; j < cities.Length; j++)
                                    {
                                        if (driver.CityId+"" == cities[j].Trim())
                                        {
                                            Console.WriteLine(cities[j]);
                                            k = i;
                                            break;
                                        }
                                    }
                                }

                                if (k != -1)
                                {
                                    string[] selectedCities = listArea[k].ListCities.Split(',');
                                    for (int j = 0; j < selectedCities.Length; j++)
                                    {
                                        if (selectedCities[j].Trim() == driver.CityId.ToString())
                                        {
                                            driver.CurrentAmount += quantity;
                                            driver.Save();

                                            Shipments shipment = new Shipments
                                            {
                                                ShipId = -1, // Consider using a meaningful default value
                                                OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")).ToString(),
                                                CustomerID = reader.GetString(reader.GetOrdinal("CustomerId")).ToString(),
                                                SourceAdd = "אריה בן אליעזר 106",
                                                SourceCity = "89",
                                                DestinationAdd = reader.GetString(reader.GetOrdinal("Address")),
                                                DestinationCity = reader.GetString(reader.GetOrdinal("CityId")),
                                                DateDelivery = reader.GetDateTime(reader.GetOrdinal("ChooseDeliveryTime")),
                                                CustomerName = reader.GetString(reader.GetOrdinal("FullName")),
                                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                                DriverId = driver.DriverID,
                                                Quantity = quantity,
                                                Status = reader.GetInt32(reader.GetOrdinal("status"))
                                            };

                                            shipment.Save();
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                       
                    } reader.Close();
                }
                finally
                {
                    db.Close();
                }
            }

            // לטעון מחדש את ההזמנות והמשלוחים לאחר העדכון
            LoadOrders();
        }

        private bool IsCityInArea(int cityId, int driverCityId, List<Area> listArea)
        {
            foreach (var area in listArea)
            {
              
            }
            return false;
        }
    }
}
