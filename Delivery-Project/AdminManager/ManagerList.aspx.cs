using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace Delivery_Project.AdminManager
{
    public partial class ManagerList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                List<Manager> managers = Manager.GetAll();
                RptProd.DataSource = managers;
                RptProd.DataBind();
            }
        }

        protected void RptAddresses_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int addressId = Convert.ToInt32(e.CommandArgument);
                DeleteManager(addressId);
                List<Manager> managers = Manager.GetAll();
                RptProd.DataSource = managers;
                RptProd.DataBind();
            }
        }

        private void DeleteManager(int addressId)
        {
            Manager.Delete(addressId);
        }

    }
}