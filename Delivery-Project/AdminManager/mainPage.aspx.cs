using DATA;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery_Project.AdminManager
{
    public partial class mainPage : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSummary();
                LoadRecentOrders();
                LoadDrivers();
                LoadManagers();
            }
        }

        private void LoadSummary()
        {
            lblTotalOrders.Text = GetScalarValue("SELECT COUNT(*) FROM T_Orders").ToString();
            lblTotalRevenue.Text = $"{GetScalarValue("SELECT SUM(Quantity) as TotalAmount FROM H_Orders")}";
            int OrderNum = Convert.ToInt32(GetScalarValue("SELECT COUNT(OrderID) as TotalAmount FROM H_Orders"));
            int numOrder = Convert.ToInt32(GetScalarValue("SELECT COUNT(OrderID) as TotalAmount FROM T_Orders"));
            lblTotalOrder.Text = (OrderNum + numOrder).ToString();
            lblActiveCustomers.Text = GetScalarValue("SELECT COUNT(DISTINCT CustomerID) FROM T_Orders").ToString();
            lblTodayOrders.Text = GetScalarValue("SELECT COUNT(*) FROM T_Orders WHERE CONVERT(date, OrderDate) = CONVERT(date, GETDATE())").ToString();
        }

        private object GetScalarValue(string query)
        {
            DbContext db= new DbContext();
            {
                SqlCommand cmd = new SqlCommand(query,db.conn);
                return cmd.ExecuteScalar();
            }
        }

        private void LoadRecentOrders()
        {
            DataTable dt = GetDataTable("SELECT OrderID, FullName, OrderDate, Quantity,status FROM T_Orders");
            rptRecentOrders.DataSource = dt;
            rptRecentOrders.DataBind();
        }

        private void LoadDrivers()
        {
            // טעינת נתונים מטבלת הנהגים
            DataTable dtDrivers = GetDataTable("SELECT Id, FullName FROM T_Driver");

            // טעינת נתונים מטבלת המשלוחים
            DataTable dtDeliveries = GetDataTable("SELECT DriverID, COUNT(*) as TotalDeliveries FROM T_Shipments GROUP BY DriverID");
            DataTable dtCountDeliveries = GetDataTable("SELECT DriverID, COUNT(*) as TotalDeliver FROM H_Shipment GROUP BY DriverID");
            DataTable RatingDriver = GetDataTable("SELECT DriverID, AVG(rating) as RatingDriver FROM Rating GROUP BY DriverID");

            // הוספת עמודה לטבלת הנהגים עבור כמות המשלוחים
            dtDrivers.Columns.Add("TotalDeliveries", typeof(int));
            dtDrivers.Columns.Add("TotalDeliver", typeof(int));
            dtDrivers.Columns.Add("RatingDriver", typeof(double));


            // שילוב הנתונים מטבלת המשלוחים לטבלת הנהגים
            foreach (DataRow driverRow in dtDrivers.Rows)
            {
                string driverID = driverRow["Id"].ToString();
                DataRow[] deliveryRows = dtDeliveries.Select($"DriverID = {driverID}");
                DataRow[] countDeliveryRows = dtCountDeliveries.Select($"DriverID = {driverID}");
                DataRow[] DtRatingDriver = RatingDriver.Select($"DriverID = {driverID}");

                if (deliveryRows.Length > 0)
                {
                    driverRow["TotalDeliveries"] = deliveryRows[0]["TotalDeliveries"];
                }
                else
                {
                    driverRow["TotalDeliveries"] = 0; // אם לנהג אין משלוחים
                }

                if (countDeliveryRows.Length > 0)
                {
                    driverRow["TotalDeliver"] = countDeliveryRows[0]["TotalDeliver"];
                }
                else
                {
                    driverRow["TotalDeliver"] = 0; // אם לנהג אין משלוחים
                }
                if (DtRatingDriver.Length > 0)
                {
                    driverRow["RatingDriver"] = Math.Round(Convert.ToDouble(DtRatingDriver[0]["RatingDriver"]), 2);
                }
                else
                {
                    driverRow["RatingDriver"] = 0; // אם לנהג אין משלוחים
                }
            }

            rptDrivers.DataSource = dtDrivers;
            rptDrivers.DataBind();
        }


        private void LoadManagers()
        {
            DataTable dt = GetDataTable("SELECT ManagerID, FullName FROM T_Manager");
            rptManagers.DataSource = dt;
            rptManagers.DataBind();
        }

        private DataTable GetDataTable(string query)
        {
            DataTable dt = new DataTable();
            DbContext db = new DbContext();
            {
                SqlDataAdapter da = new SqlDataAdapter(query, db.conn);
                da.Fill(dt);
            }
            return dt;
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime? startDate = string.IsNullOrEmpty(txtStartDate.Text) ? (DateTime?)null : DateTime.Parse(txtStartDate.Text);
            DateTime? endDate = string.IsNullOrEmpty(txtEndDate.Text) ? (DateTime?)null : DateTime.Parse(txtEndDate.Text);

            string query = "SELECT OrderID, CustomerName, OrderDate, TotalAmount, Status FROM Orders WHERE 1=1";
            if (startDate.HasValue)
                query += " AND OrderDate >= @startDate";
            if (endDate.HasValue)
                query += " AND OrderDate <= @endDate";

            DataTable dt = new DataTable();
            DbContext db = new DbContext();
            {
                SqlCommand cmd = new SqlCommand(query, db.conn);
                if (startDate.HasValue)
                    cmd.Parameters.AddWithValue("@startDate", startDate.Value);
                if (endDate.HasValue)
                    cmd.Parameters.AddWithValue("@endDate", endDate.Value);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            rptRecentOrders.DataSource = dt;
            rptRecentOrders.DataBind();
        }
    }
}
