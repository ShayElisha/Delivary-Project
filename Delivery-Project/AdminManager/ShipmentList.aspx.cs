using BLL;
using DAL;
using Delivery_Project.DriverSide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery_Project.AdminManager
{
    public partial class ShipmentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Shipments> shipment = Shipments.GetAll();
                if (shipment != null && shipment.Count > 0)
                {
                    RptProd.DataSource = shipment;
                    RptProd.DataBind();
                    NoShipmentsPanel.Visible = false;

                }
                else
                {
                    NoShipmentsPanel.Visible = true;
                }

            }
        }
        protected void RptAddresses_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int shipmentsId = Convert.ToInt32(e.CommandArgument);

                // קבלת המשלוח לפי מזהה
                Shipments shipment = Shipments.GetById(shipmentsId);
                if (shipment != null)
                {
                    // הסרת הכמות מהנהג המתאים
                    Driver.RemoveQuantityFromDriver(shipment.DriverId, shipment.Quantity);

                    // מחיקת המשלוח
                    DeleteShipment(shipmentsId);
                }

                // רענון ה-Repeater
                List<Shipments> shipments = Shipments.GetAll();
                RptProd.DataSource = shipments;
                RptProd.DataBind();
            }
        }

        private void DeleteShipment(int shipmentsId)
        {
            Shipments.Delete(shipmentsId);
        }
    }
}