using DATA;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Delivery_Project.DriverSide
{
    public partial class HistoryShipments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadOrderHistory();
            PopulateMonths();
            PopulateYears();
        }
        private void PopulateMonths()
        {
            ddlMonth.Items.Clear();
            ddlMonth.Items.Add(new ListItem("בחר חודש", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlMonth.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        private void PopulateYears()
        {
            ddlYear.Items.Clear();
            ddlYear.Items.Add(new ListItem("בחר שנה", "0"));
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear; i >= currentYear - 10; i--)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
        private void LoadOrderHistory()
        {
            if (Session["DriverID"] != null)
            {
                string DriverId = Session["Id"].ToString();
                int month = 0;
                int year = 0;
                if (int.TryParse(ddlMonth.SelectedValue, out int tempMonth))
                {
                    month = tempMonth;
                }

                if (int.TryParse(ddlYear.SelectedValue, out int tempYear))
                {
                    year = tempYear;
                }
                DbContext db = new DbContext();

                try
                {
                    string sql = "SELECT * FROM H_Shipment WHERE DriverId = @DriverId ";
                    if (month != 0 && year != 0)
                    {
                        sql += " AND MONTH(AddDate) = @Month AND YEAR(AddDate) = @Year";
                    }
                    SqlCommand cmd = new SqlCommand(sql, db.conn);
                    cmd.Parameters.AddWithValue("@DriverId", DriverId);
                    if (month != 0 && year != 0)
                    {
                        cmd.Parameters.AddWithValue("@Month", month);
                        cmd.Parameters.AddWithValue("@Year", year);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    RptProd.DataSource = dt;
                    RptProd.DataBind();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    db.conn.Close();
                }
            }
            else
            {
                Response.Redirect("/MainPage.aspx");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}