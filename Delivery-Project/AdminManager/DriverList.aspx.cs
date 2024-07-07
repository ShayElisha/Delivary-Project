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
                int addressId = Convert.ToInt32(e.CommandArgument);
                DeleteDriver(addressId);
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