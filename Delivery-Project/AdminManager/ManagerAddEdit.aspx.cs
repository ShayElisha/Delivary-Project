using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Delivery_Project.AdminManager
{
    public partial class ManagerAddEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCities();
                string managerId = Request["ManagerId"] + "";
                if (string.IsNullOrEmpty(managerId))
                {
                    managerId = "-1";
                }
                else
                {
                    int dID = int.Parse(managerId);
                    List<Manager> manager = (List<Manager>)Application["Manegers"];
                    for (int i = 0; i < manager.Count; i++)
                    {
                        if (manager[i].ManagerID == dID)
                        {
                            FullName.Text = manager[i].FullName;
                            Email.Text = manager[i].Email + "";
                            Password.Text = manager[i].Password;
                            DDLcity.Text = manager[i].CityId + "";
                            Address.Text = manager[i].Address;
                            Phone.Text = manager[i].Phone + "";
                            Phone.Text = manager[i].Phone + "";
                            status.Text = manager[i].status + "";
                            hidCid.Value = managerId;
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
            Manager manager = new Manager();

            // קביעת מזהה העיר מהטופס
            if (hidCid.Value == "-1")
            {
                manager.ManagerID = -1; // עיר חדשה
            }
            else
            {
                manager.ManagerID = int.Parse(hidCid.Value); // עיר קיימת
            }
            // קביעת שם העיר מהטופס
            manager.FullName = FullName.Text;
            manager.Email = Email.Text;
            manager.Password = Password.Text;
            manager.CityId = int.Parse(DDLcity.Text);
            manager.Address = Address.Text;
            manager.Phone = Phone.Text;
            manager.status = int.Parse(status.Text);
            // שמירת העיר החדשה
            manager.Save();
            // עדכון ה-Application עם רשימת הערים החדשה
            Application["Managers"] = Manager.GetAll();
            // הפנייה לדף רשימת הערים
            Response.Redirect("ManagerList.aspx");
        }
    
    }
}