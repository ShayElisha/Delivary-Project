using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery_Project.AdminManager
{
    public partial class CustomerList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Customers> customer = (List<Customers>)Application["Customers"];
            RptProd.DataSource = customer;
            RptProd.DataBind();
        }
        protected void RptAddresses_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int CustomerID = Convert.ToInt32(e.CommandArgument);
                DeleteCustomers(CustomerID);
                List<Customers> customer = Customers.GetAll();
                RptProd.DataSource = customer;
                RptProd.DataBind();
            }
        }

        private void DeleteCustomers(int customer)
        {
            Customers.Delete(customer);
        }

    }
}