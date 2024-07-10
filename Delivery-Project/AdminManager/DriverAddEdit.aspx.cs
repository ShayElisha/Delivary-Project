using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            driver.Password = Password.Text;
            if (!IsValidPassword(Password.Text))
            {
                // הצגת הודעת שגיאה עבור סיסמה לא תקינה
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('הסיסמה חייבת להכיל לפחות 8 תווים, כולל אותיות, מספרים וסימנים מיוחדים');", true);
                return;
            }
            driver.CityId = int.Parse(DDLcity.Text);
            driver.Address = Address.Text;
            driver.Phone = Phone.Text;
            if (!IsValidPhone(Phone.Text))
            {
                // הצגת הודעת שגיאה עבור טלפון לא תקין
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('המספר טלפון לא תקין');", true);
                return;
            }
            driver.MaxAmountShipment = int.Parse(MaxAmountShipment.Text);
            driver.status = int.Parse(status.Text);



            

            // שמירת העיר החדשה
            driver.Save();
            Salaries salary = new Salaries();
            string sqlCheck = $"SELECT COUNT(*) FROM Salaries WHERE DriverID = N'{salary.DriverID}' AND MONTH(AddDate) = {DateTime.Now.Month} AND YEAR(AddDate) = {DateTime.Now.Year}";
            DbContext Db = new DbContext();

            int recordCount = (int)Db.ExecuteScalar(sqlCheck);
            DateTime date = DateTime.Now;
            string sql;
            if (recordCount == 0)
            {
                Salaries salaries = new Salaries();
                salaries.SalaryID = -1;
                salaries.DriverID = driver.Id;
                salaries.Save();
            }
            // עדכון ה-Application עם רשימת הערים החדשה
            Application["Drivers"] = Driver.GetAll();

            // הפנייה לדף רשימת הערים
            Response.Redirect("DriverList.aspx");
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