using BLL;
using Google.Apis.Admin.Directory.directory_v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery_Project.CustomerSide
{
    public partial class OrderPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CustomerID"] != null)
                {
                    string Id=Session["CustomerID"]+"";
                    cusID.Value = Id.ToString();
                }
                else
                {
                    Response.Redirect("/MainPage.aspx");
                }
                LoadCities();
            }
        }
        private void LoadCities()
        {
            List<Cities> cities = Cities.GetAll();
            DDLcity.DataSource = cities;
            DDLcity.DataTextField = "CityName";
            DDLcity.DataValueField = "CityId";
            DDLcity.DataBind();

            // הוסף אופציה ברירת מחדל לבחירת עיר
            DDLcity.Items.Insert(0, new ListItem("בחר עיר", "0"));
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Orders Orders = new Orders();

            // קביעת מזהה העיר מהטופס
            if (hidCid.Value == "-1")
            {
                Orders.OrderID = -1; // עיר חדשה
            }
            else
            {
                Orders.OrderID = int.Parse(hidCid.Value); // עיר קיימת
            }

            // קביעת מזהה הלקוח מה-hidden field
            Orders.CustomerID = cusID.Value;

            // קביעת שם העיר מהטופס
            Orders.FullName = FullName.Text;
            Orders.Email = txtEmail.Text;
            Orders.Phone = txtPhone.Text;
            Orders.Address = txtAddress.Text;

            // בדיקת תקינות והמרה של CityId
            
            Orders.CityId = DDLcity.SelectedValue;
            

            // בדיקת תקינות והמרה של Quantity
            if (int.TryParse(txtQuantity.Text, out int quantity))
            {
                Orders.Quantity = quantity;
            }
            else
            {
                Orders.Quantity = 0;
            }

            Orders.Notes = txtNotes.Text;

            int deliveryDays = int.Parse(ddlDeliveryTime.SelectedValue);

            // קביעת ChooseDeliveryTime באופן בטוח
            Orders.ChooseDeliveryTime = DateTime.Now.AddDays(deliveryDays);

            // שמירת העיר החדשה
            Orders.Save();

            // עדכון ה-Application עם רשימת הערים החדשה
            Application["Orders"] = Orders.GetAll();
            // הפנייה לדף רשימת הערים
            Response.Redirect("OrderStatus.aspx");
        }
    }
}
