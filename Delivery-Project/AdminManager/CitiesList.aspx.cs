using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery_Project.AdminManager
{
    public partial class CitiesList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Cities> lsCategory = (List<Cities>)Application["Cities"];
            RptProd.DataSource = lsCategory;
            RptProd.DataBind();
        }
    }
}