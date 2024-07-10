using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery_Project.AdminManager
{
    public partial class DriverList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                List<Driver> drivers = (List<Driver>)Application["Drivers"];
                RptProd.DataSource = drivers;
                RptProd.DataBind();
            
        }
        protected void RptAddresses_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                var button = (LinkButton)e.CommandSource;
                string[] arguments = button.CommandArgument.Split(',');
                int DriverId = Convert.ToInt32(arguments[0]); // Assuming the first argument is addressId
                string Id = arguments[1]; // Assuming the second argument is driverId

                // מחיקת הנהג והמשכורת
                DeleteDriver(DriverId);
                Salaries.Delete(Id);

                // עדכון רשימת הנהגים והצגת הנתונים
                List<Driver> managers = Driver.GetAll();
                RptProd.DataSource = managers;
                RptProd.DataBind();
            }
        }


        private void DeleteDriver(int addressId)
        {
            Driver.Delete(addressId);
        }





    }
}