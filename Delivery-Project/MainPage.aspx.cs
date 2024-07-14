using DATA;
using BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;


namespace Delivery_Project
{
    public partial class MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCities();
        }

        protected void Register_Click(object sender, EventArgs e)
        {
            // לא נדרש טיפול נוסף כאן כי ה-Modal נפתח על ידי JavaScript
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            // לא נדרש טיפול נוסף כאן כי ה-Modal נפתח על ידי JavaScript
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Customers Customer = new Customers();
            Customer.CustomerID = -1;
            Customer.FullName = txtFullName.Text;
            Customer.Email = txtEmail.Text;
            Customer.Password = txtPassword.Text;
            if (!IsValidEmail(txtEmail.Text))
            {
                // הצגת הודעת שגיאה עבור אימייל לא תקין
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('אימייל לא תקין');", true);
                return;
            }

            if (!IsValidPassword(txtPassword.Text))
            {
                // הצגת הודעת שגיאה עבור סיסמה לא תקינה
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('הסיסמה חייבת להכיל לפחות 8 תווים, כולל אותיות, מספרים וסימנים מיוחדים');", true);
                return;
            }
            Customer.Phone = txtPhone.Text;
            if (!IsValidPhone(txtPhone.Text))
            {
                // הצגת הודעת שגיאה עבור טלפון לא תקין
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('המספר טלפון לא תקין');", true);
                return;
            }
            Customer.Address = txtAddress.Text;
            Customer.CityId = int.Parse(DDLcity.Text);
            Customer.Company = txtCompany.Text;
            if (IsEmailExists(txtEmail.Text))
            {
                // הצגת הודעת שגיאה עבור אימייל קיים
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('אימייל כבר קיים במערכת');", true);
                return;
            }
            Customer.Save();


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
        protected void btnManagerLogin_Click(object sender, EventArgs e)
        {
            DbContext Db = new DbContext();
            string sql = "SELECT * FROM T_Manager";
            SqlCommand cmd = new SqlCommand(sql, Db.conn);

            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                if (txtManagerEmail.Text == Dr["Email"].ToString() && txtManagerPassword.Text == Dr["Password"].ToString())
                {
                    // ניצור סשן ונשמור את האובייקט של המשתמש 
                    Session["Manager"] = Dr;
                    Session["IsManager"] = true;
                    Session["IsDriver"] = false;
                    Session["IsCustomer"] = false;
                    // נעביר את המשתמש לעמוד מוצרים
                    Response.Redirect("/AdminManager/mainPage.aspx");
                }

            }
            LtlMsg.Text = "מייל או סיסמה אינם תקינים";
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
        protected void btnDriverLogin_Click(object sender, EventArgs e)
        {
            DbContext Db = new DbContext();
            string sql = "SELECT * FROM T_Driver";
            SqlCommand cmd = new SqlCommand(sql, Db.conn);

            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                if (txtDriverEmail.Text == Dr["Email"].ToString() && txtDriverPassword.Text == Dr["Password"].ToString())
                {
                    // ניצור סשן ונשמור את האובייקט של המשתמש 
                    Session["Id"] = Dr["Id"].ToString();
                    Session["DriverID"] = Dr["DriverID"].ToString();
                    Session["Driver"] = Dr;
                    Session["IsDriver"] = true;
                    Session["IsManager"] = false;
                    Session["IsCustomer"] = false;
                    // נעביר את המשתמש לעמוד מוצרים
                    Response.Redirect("DriverSide/WorkDriver.aspx");
                }
                
            }
            LtlMsg.Text = "מייל או סיסמה אינם תקינים";
        }
        protected void btnCustomerLogin_Click(object sender, EventArgs e)
        {
            DbContext Db = new DbContext();
            string sql = "SELECT * FROM T_Customer";
            SqlCommand cmd = new SqlCommand(sql, Db.conn);

            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                if (txtCustomerEmail.Text == Dr["Email"].ToString() && txtCustomerPassword.Text == Dr["Password"].ToString())
                {
                    // ניצור סשן ונשמור את האובייקט של המשתמש 
                    Session["CustomerID"] = Dr["CustomerID"].ToString();
                    Session["FullName"] = Dr["FullName"].ToString();
                    Session["Customer"] = Dr;
                    Session["IsCustomer"] = true;
                    Session["IsManager"] = false;
                    Session["IsDriver"] = false;
                    // נעביר את המשתמש לעמוד מוצרים
                    Response.Redirect("/CustomerSide/OrderPage.aspx");
                }

            }
            LtlMsg.Text = "מייל או סיסמה אינם תקינים";
        } 
    }
}