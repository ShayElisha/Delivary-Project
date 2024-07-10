using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            if (!IsValidEmail(Email.Text))
            {
                // הצגת הודעת שגיאה עבור אימייל לא תקין
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('אימייל לא תקין');", true);
                return;
            }
            if (IsEmailExists(Email.Text))
            {
                // הצגת הודעת שגיאה עבור אימייל קיים
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('אימייל כבר קיים במערכת');", true);
                return;
            }
            manager.Password = Password.Text;
            if (!IsValidPassword(Password.Text))
            {
                // הצגת הודעת שגיאה עבור סיסמה לא תקינה
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('הסיסמה חייבת להכיל לפחות 8 תווים, כולל אותיות, מספרים וסימנים מיוחדים');", true);
                return;
            }
            manager.CityId = int.Parse(DDLcity.Text);
            manager.Address = Address.Text;
            manager.Phone = Phone.Text;
            if (!IsValidPhone(Phone.Text))
            {
                // הצגת הודעת שגיאה עבור טלפון לא תקין
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('המספר טלפון לא תקין');", true);
                return;
            }
            manager.status = int.Parse(status.Text);
            // שמירת העיר החדשה
            manager.Save();
            // עדכון ה-Application עם רשימת הערים החדשה
            Application["Managers"] = Manager.GetAll();
            // הפנייה לדף רשימת הערים
            Response.Redirect("ManagerList.aspx");
        }
        private bool IsEmailExists(string email)
        {
            DbContext Db = new DbContext(); // יצירת אובייקט מסוג גישה לבסיס נתונים
            string query = "SELECT COUNT(*) FROM T_Customer WHERE Email = @Email";
            SqlCommand cmd = new SqlCommand(query, Db.conn);
            cmd.Parameters.AddWithValue("@Email", email);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }
        private bool IsValidPhone(string phone)
        {
            var phonePattern = new System.Text.RegularExpressions.Regex(@"^05\d\d{3}\d{4}$");
            return phonePattern.IsMatch(phone);
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPassword(string password)
        {
            var hasLetter = new System.Text.RegularExpressions.Regex(@"[a-zA-Z]+");
            var hasDigit = new System.Text.RegularExpressions.Regex(@"\d+");
            var hasSpecialChar = new System.Text.RegularExpressions.Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            return password.Length >= 8 && hasLetter.IsMatch(password) && hasDigit.IsMatch(password) && hasSpecialChar.IsMatch(password);
        }
    }
}

