using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Delivery_Project.AdminManager
{
    public partial class DriverAddEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCities();
                string DriverId = Request["DriverId"] + "";
                if (string.IsNullOrEmpty(DriverId))
                {
                    DriverId = "-1";
                }
                else
                {
                    int dID = int.Parse(DriverId);
                    List<Driver> driver = (List<Driver>)Application["Drivers"];
                    for (int i = 0; i < driver.Count; i++)
                    {
                        if (driver[i].DriverID == dID)
                        {
                            Id.Text = driver[i].Id;
                            Dname.Text = driver[i].FullName;
                            Email.Text = driver[i].Email + "";
                            Password.Text = driver[i].Password;
                            DDLcity.Text = driver[i].CityId + "";
                            Address.Text = driver[i].Address;
                            Phone.Text = driver[i].Phone + "";
                            MaxAmountShipment.Text = driver[i].MaxAmountShipment+"";
                            Phone.Text = driver[i].Phone + "";
                            status.Text = driver[i].status + "";
                            hidCid.Value = DriverId;
                        }
                    }
                }
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Driver driver = new Driver();

            // קביעת מזהה העיר מהטופס
            if (hidCid.Value == "-1")
            {
                driver.DriverID = -1; // עיר חדשה
            }
            else
            {
                driver.DriverID = int.Parse(hidCid.Value); // עיר קיימת
            }

            // קביעת שם העיר מהטופס
            driver.Id = Id.Text;
            driver.FullName = Dname.Text;
            driver.Email = Email.Text;
            driver.Password = Password.Text;
            driver.CityId = int.Parse(DDLcity.Text);
            driver.Address = Address.Text;
            driver.Phone = Phone.Text;
            driver.MaxAmountShipment = int.Parse(MaxAmountShipment.Text);
            driver.status = int.Parse(status.Text);

            List<Cities> allCities = Cities.GetAll();


            

            // שמירת העיר החדשה
            driver.Save();

            // עדכון ה-Application עם רשימת הערים החדשה
            Application["Drivers"] = Driver.GetAll();

            // הפנייה לדף רשימת הערים
            Response.Redirect("DriverList.aspx");
        }
    }
    
}