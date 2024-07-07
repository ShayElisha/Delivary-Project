using dotenv.net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery_Project.DriverSide
{
    public partial class DriverSide : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Driver"] == null)
            {
                Response.Redirect("/MainPage.aspx");
            }
        }
        
    }
}