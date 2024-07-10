using BLL;
using DATA;
using Delivery_Project.DriverSide;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class SalariesDAL:Salaries
    {
        public static List<Salaries> GetAll()
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג גישה לבסיס נתנים
            string sql = "select * from Salaries";//הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql);// הפעלת השאילתה וקבלת התוצאות לטבלת נתטנים
            List<Salaries> salary = new List<Salaries>();
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Salaries C = new Salaries()
                {
                    SalaryID = int.Parse(Dt.Rows[i]["SalaryID"] + ""),
                    DriverID = Dt.Rows[i]["DriverID"] + "",
                    DeliaryAmount = int.Parse(Dt.Rows[i]["DeliaryAmount"] + ""),
                    faults = int.Parse(Dt.Rows[i]["faults"] + ""),
                    Report = int.Parse(Dt.Rows[i]["Report"] + ""),
                    Bonuse = decimal.Parse(Dt.Rows[i]["Bonuse"].ToString()),
                    salary = decimal.Parse(Dt.Rows[i]["salary"].ToString()),
                    AddDate = DateTime.Parse(Dt.Rows[i]["AddDate"] + "")
                };
                salary.Add(C);
            }
            Db.Close();
            return salary;
        }
        public static Salaries GetById(string Id)
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג גישה לבסיס נתנים
            string sql = $"select * from Salaries where DriverID={Id}";//הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql);// הפעלת השאילתה וקבלת התוצאות לטבלת נתטנים
            Salaries salaries = null;
            if (Dt.Rows.Count > 0)
            {
                salaries = new Salaries()
                {
                    SalaryID = int.Parse(Dt.Rows[0]["SalaryID"] + ""),
                    DriverID = Dt.Rows[0]["DriverID"] + "",
                    DeliaryAmount = int.Parse(Dt.Rows[0]["DeliaryAmount"] + ""),
                    faults = int.Parse(Dt.Rows[0]["faults"] + ""),
                    Report = int.Parse(Dt.Rows[0]["Report"] + ""),
                    Bonuse = decimal.Parse(Dt.Rows[0]["Bonuse"].ToString()),
                    salary = decimal.Parse(Dt.Rows[0]["salary"].ToString()),
                    AddDate = DateTime.Parse(Dt.Rows[0]["AddDate"] + "")
                };
            }
            Db.Close();
            return salaries;
        }
        public static Salaries GetBySalaryId(int Id)
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג גישה לבסיס נתנים
            string sql = $"select * from Salaries where SalaryID={Id}";//הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql);// הפעלת השאילתה וקבלת התוצאות לטבלת נתטנים
            Salaries salaries = null;
            if (Dt.Rows.Count > 0)
            {
                salaries = new Salaries()
                {
                    SalaryID = int.Parse(Dt.Rows[0]["SalaryID"] + ""),
                    DriverID = Dt.Rows[0]["DriverID"] + "",
                    DeliaryAmount = int.Parse(Dt.Rows[0]["DeliaryAmount"] + ""),
                    faults = int.Parse(Dt.Rows[0]["faults"] + ""),
                    Report = int.Parse(Dt.Rows[0]["Report"] + ""),
                    Bonuse = decimal.Parse(Dt.Rows[0]["Bonuse"].ToString()),
                    salary = decimal.Parse(Dt.Rows[0]["salary"].ToString()),
                    AddDate = DateTime.Parse(Dt.Rows[0]["AddDate"] + "")
                };
            }
            Db.Close();
            return salaries;
        }
        public static Salaries Save(Salaries salaries)
        {
            string sqlCheck = $"SELECT COUNT(*) FROM Salaries WHERE DriverID = N'{salaries.DriverID}' AND MONTH(AddDate) = {DateTime.Now.Month} AND YEAR(AddDate) = {DateTime.Now.Year}";
            DbContext Db = new DbContext();

            int recordCount = (int)Db.ExecuteScalar(sqlCheck);
            DateTime date = DateTime.Now;
            string sql;
            if (recordCount > 0)
            {
                // Update existing record
                sql = "UPDATE Salaries SET ";
                sql += $"DriverID = N'{salaries.DriverID}', ";
                sql += $"DeliaryAmount = N'{salaries.DeliaryAmount}', ";
                sql += $"faults = N'{salaries.faults}', ";
                sql += $"Report = N'{salaries.Report}', ";
                sql += $"Bonuse = N'{salaries.Bonuse}', ";
                sql += $"salary = N'{salaries.salary}' ";
                sql += $"WHERE SalaryID = {salaries.SalaryID}";
                Db.ExecuteNonQuery(sql);
                GetAll();
                return salaries;
            }
            else
            {
                if (salaries.SalaryID == -1 || date.Day==1)
                {
                sql = "INSERT INTO Salaries (DriverID) VALUES ";
                sql += $"(N'{salaries.DriverID}')";
                Db.ExecuteNonQuery(sql);
                GetAll();
                return salaries;
                }
                return salaries;
                
            }

            
        }

        public static void Delete(string id)
        {
            DbContext Db = new DbContext();
            string Sql = $"DELETE FROM Salaries WHERE DriverID = {id}";
            Db.Execute(Sql);
            Db.Close();
        }
        public static void calculateSalary(string Id)
        {
            DbContext Db = new DbContext();
            DateTime now = DateTime.Now;
            try
            {
                string Sql = "SELECT * FROM H_Shipment WHERE DriverID = @DriverId and MONTH(AddDate) = @Month AND YEAR(AddDate) = @Year;";
                SqlCommand cmd = new SqlCommand(Sql, Db.conn);
                cmd.Parameters.AddWithValue("@DriverId", Id);
                cmd.Parameters.AddWithValue("@Month", now.Month);
                cmd.Parameters.AddWithValue("@Year", now.Year);

                SqlDataReader Dr = cmd.ExecuteReader();
                int deliveryCount = 0;
                int faults = 0;
                while (Dr.Read())
                {
                    DateTime DateDelivery = Dr.GetDateTime(Dr.GetOrdinal("DateDelivery"));
                    DateTime CollectionDate = Dr.GetDateTime(Dr.GetOrdinal("CollectionDate"));

                    deliveryCount += 1;
                    if (DateDelivery < CollectionDate)
                    {
                        faults++;
                    }
                }
                Dr.Close(); // Close the reader when done

                // Check if salary record exists for current month and year
                string checkSql = "SELECT * FROM Salaries WHERE DriverID = @DriverId AND MONTH(AddDate) = @Month AND YEAR(AddDate) = @Year;";
                SqlCommand checkCmd = new SqlCommand(checkSql, Db.conn);
                checkCmd.Parameters.AddWithValue("@DriverId", Id);
                checkCmd.Parameters.AddWithValue("@Month", now.Month);
                checkCmd.Parameters.AddWithValue("@Year", now.Year);
                Salaries salary = Salaries.GetById(Id);
                if (salary == null)
                {
                    salary = new Salaries();
                    salary.DriverID = Id;
                    salary.AddDate = now;
                }
                if (deliveryCount > 25)
                {
                    salary.Bonuse = 10000;
                }
                salary.DeliaryAmount = deliveryCount;
                salary.faults = faults;
                salary.salary = 200 * deliveryCount-faults*salary.Report+salary.Bonuse;
                salary.Save();
            }
            finally
            {
                if (Db.conn.State == ConnectionState.Open)
                {
                    Db.conn.Close();
                }
            }
        }

    }
}