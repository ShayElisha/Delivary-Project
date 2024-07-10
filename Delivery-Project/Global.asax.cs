using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Delivery_Project
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

            Application["cities"] = Cities.GetAll();
            Application["Manegers"] = Manager.GetAll();
            Application["Drivers"] = Driver.GetAll();
            Application["Customers"] = Customers.GetAll();
            Application["Orders"] = Orders.GetAll();
            Application["Shipments"] = Shipments.GetAll();
            Application["Salaries"] = Salaries.GetAll();




        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}