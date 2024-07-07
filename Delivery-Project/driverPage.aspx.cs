using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery_Project
{
    public partial class driverPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void StartBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/workPage.aspx");
        }
    }
}