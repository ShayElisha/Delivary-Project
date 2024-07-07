using BLL;
using Delivery_Project.CustomerSide;
using Google.Apis.Admin.Directory.directory_v1.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery_Project.AdminManager
{
    public partial class shipmentAddEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Driver> driver = (List<Driver>)Application["Drivers"];
                ddlDriverId.Items.Clear();  // מנקה את כל הפריטים הקיימים
                ddlDriverId.Items.Add(new ListItem("בחר נהג", "0"));  // מוסיף את פריט ברירת המחדל

                for (int i = 0; i < driver.Count; i++)
                {
                    ddlDriverId.Items.Add(new ListItem(driver[i].Id + " - " + driver[i].FullName, driver[i].Id.ToString()));
                }

                string OrderId = Request["OrderId"] + "";
                if (string.IsNullOrEmpty(OrderId))
                {
                    OrderId = "-1";
                }
                else
                {
                    int oID = int.Parse(OrderId);
                    List<Orders> order = (List<Orders>)Application["Orders"];
                    for (int i = 0; i < order.Count; i++)
                    {
                        if (order[i].OrderID == oID)
                        {
                            OrderID.Text = order[i].OrderID.ToString();
                            CustomerID.Text = order[i].CustomerID + "";
                            DestinationAdd.Text = order[i].Address;
                            DestinationCity.Text = order[i].CityId + "";
                            customerName.Text = order[i].FullName;
                            Phone.Text = order[i].Phone + "";
                            hidChooseDeliveryTime.Value = order[i].ChooseDeliveryTime+"";
                            Quantity.Text = order[i].Quantity + "";
                            hidShipId.Value = OrderId;
                        }
                    }
                }
                string ShipId = Request["ShipId"] + "";
                if (string.IsNullOrEmpty(ShipId))
                {
                    ShipId = "-1";
                }
                else
                {
                    int sID = int.Parse(ShipId);
                    List<Shipments> shipments = (List<Shipments>)Application["Shipments"];
                    for (int i = 0; i < shipments.Count; i++)
                    {
                        if (shipments[i].ShipId == sID)
                        {
                            OrderID.Text = shipments[i].OrderID.ToString();
                            CustomerID.Text = shipments[i].CustomerID + "";
                            SourceAdd.Text = shipments  [i].SourceAdd + "";
                            SourceCity.Text = shipments[i].SourceCity + "";
                            DestinationAdd.Text = shipments[i].DestinationAdd;
                            DestinationCity.Text = shipments[i].DestinationCity + "";
                            customerName.Text = shipments[i].CustomerName+"";
                            Phone.Text = shipments[i].Phone + "";
                            Quantity.Text = shipments[i].Quantity + "";
                            ddlDriverId.Text = shipments[i].DriverId + "";
                            hidShipId.Value = ShipId;
                        }
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(OrderID.Text) ||
                string.IsNullOrEmpty(CustomerID.Text) ||
                string.IsNullOrEmpty(SourceAdd.Text) ||
                string.IsNullOrEmpty(SourceCity.Text) ||
                string.IsNullOrEmpty(DestinationAdd.Text) ||
                string.IsNullOrEmpty(DestinationCity.Text) ||
                string.IsNullOrEmpty(customerName.Text) ||
                string.IsNullOrEmpty(Phone.Text) ||
                 hidChooseDeliveryTime.Value==null ||
                ddlDriverId.SelectedValue == "0" ||
                string.IsNullOrEmpty(Quantity.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('אנא מלא את כל השדות');", true);
                return;
            }

            Shipments shipment = new Shipments();
            shipment.ShipId = -1;
            shipment.OrderID = OrderID.Text;
            shipment.CustomerID = CustomerID.Text;
            shipment.SourceAdd = SourceAdd.Text;
            shipment.SourceCity = SourceCity.Text;
            shipment.DestinationAdd = DestinationAdd.Text;
            shipment.DestinationCity = DestinationCity.Text;
            DateTime dateDelivery;
            if (DateTime.TryParse(hidChooseDeliveryTime.Value, out dateDelivery) &&
                dateDelivery >= new DateTime(1900, 1, 1) && dateDelivery <= new DateTime(2079, 6, 6))
            {
                shipment.DateDelivery = dateDelivery;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('אנא הכנס תאריך משלוח חוקי');", true);
                return;
            }
            shipment.CustomerName = customerName.Text;
            shipment.Phone = Phone.Text;
            shipment.DriverId = int.Parse(ddlDriverId.SelectedValue);
            shipment.Quantity = int.Parse(Quantity.Text);

            Driver driver = Driver.GetById(shipment.DriverId);
            if (driver == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('הנהג לא נמצא');", true);
                return;
            }
            if (driver.CurrentAmount + shipment.Quantity <= driver.MaxAmountShipment)
            {
                shipment.Save();
                Application["Shipments"] = Shipments.GetAll();
                Response.Redirect("OrderList.aspx?hideOrderId=" + OrderID.Text);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('אי אפשר מעבר לכמות המותרת לנהג');", true);
                return;
            }



        }

    }
}