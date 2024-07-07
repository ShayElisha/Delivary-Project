using BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery_Project.AdminManager
{
    public partial class CitiesAddEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string CityId = Request["CityId"] + "";
                if (string.IsNullOrEmpty(CityId))
                {
                    CityId = "-1";
                }
                else
                {
                    int Cit = int.Parse(CityId);
                    List<Cities> city = (List<Cities>)Application["cities"];
                    for (int i = 0; i < city.Count; i++)
                    {
                        if (city[i].CityId == Cit)
                        {
                            TxtCname.Text = city[i].CityName;
                            TxtStatus.Text = city[i].status + "";
                            hidCid.Value = CityId;
                        }
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // יצירת אובייקט חדש של עיר
            Cities city = new Cities();

            // קביעת מזהה העיר מהטופס
            if (hidCid.Value == "-1")
            {
                city.CityId = -1; // עיר חדשה
            }
            else
            {
                city.CityId = int.Parse(hidCid.Value); // עיר קיימת
            }

            // קביעת שם העיר מהטופס
            city.CityName = TxtCname.Text;

            // בדיקה אם העיר כבר קיימת במערכת
            bool cityExists = false;
            List<Cities> allCities = Cities.GetAll();
            foreach (Cities existingCity in allCities)
            {
                if (existingCity.CityName == city.CityName)
                {
                    cityExists = true;
                    break;
                }
            }

            if (cityExists)
            {
                // אם העיר קיימת, הצגת הודעת שגיאה למשתמש ולא ביצוע שמירה
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('העיר כבר קיימת.');", true);
                return;
            }

            // שמירת העיר החדשה
            city.Save();

            // עדכון ה-Application עם רשימת הערים החדשה
            Application["cities"] = Cities.GetAll();

            // הפנייה לדף רשימת הערים
            Response.Redirect("citiesList.aspx");
        }

    }
}