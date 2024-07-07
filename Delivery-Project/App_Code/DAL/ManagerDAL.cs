using BLL;
using DATA;
using Delivery_Project.App_Code.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DAL
{
    public class ManagerDAL:Manager
    {
        public static List<Manager> GetAll()
        {
            DbContext Db = new DbContext(); // יצירת אובייקט מסוג גישה לבסיס נתונים
            string sql = "select * from T_Manager"; // הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql); // הפעלת השאילתה וקבלת התוצאות לטבלת נתונים
            List<Manager> Customer = new List<Manager>();

            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Manager C = new Manager()
                {
                    ManagerID = int.Parse(Dt.Rows[i]["ManagerID"].ToString()),
                    FullName = Dt.Rows[i]["FullName"].ToString(),
                    Email = Dt.Rows[i]["Email"].ToString(),
                    Password = Dt.Rows[i]["Password"].ToString(),
                    CityId = int.Parse(Dt.Rows[i]["CityId"].ToString()),
                    Address =Dt.Rows[i]["Address"].ToString(),
                    Phone = Dt.Rows[i]["Phone"].ToString(),
                    status = int.Parse(Dt.Rows[i]["status"].ToString()),
                    AddDate = Dt.Rows[i]["AddDate"].ToString()
                };
                Customer.Add(C);
            }
            Db.Close();
            return Customer;
        }

        public static Manager GetById(int Id)
        {
            DbContext Db = new DbContext(); // יצירת אובייקט מסוג גישה לבסיס נתונים
            string sql = $"select * from T_Manager where ManagerID={Id}"; // הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql); // הפעלת השאילתה וקבלת התוצאות לטבלת נתונים
            Manager customer = null;
            if (Dt.Rows.Count > 0)
            {
                customer = new Manager()
                {
                    ManagerID = int.Parse(Dt.Rows[0]["ManagerID"] + ""),
                    FullName = Dt.Rows[0]["FullName"] + "",
                    Email = Dt.Rows[0]["Email"] + "",
                    Password = Dt.Rows[0]["Password"] + "",
                    CityId = int.Parse(Dt.Rows[0]["CityId"] + ""),
                    Address = Dt.Rows[0]["Address"] + "",
                    Phone = Dt.Rows[0]["Phone"] + "",
                    status = int.Parse(Dt.Rows[0]["status"] + ""),
                    AddDate = Dt.Rows[0]["AddDate"] + "",
                };
            }
            Db.Close();
            return customer;
        }

        public static void Save(Manager customer)
        {
            string sql = "";
            if (customer.ManagerID == -1)
            {

                sql = "insert into T_Manager(FullName, Email, Password, CityId, Address,Phone, status) values ";
                sql += $"(N'{customer.FullName}', N'{customer.Email}', N'{customer.Password}', N'{customer.CityId}', N'{customer.Address}',N'{customer.Phone}', N'{customer.status}')";
            }
            else
            {
                sql = "update T_Manager set ";
                sql += $"FullName=N'{customer.FullName}', ";
                sql += $"Email=N'{customer.Email}', ";
                sql += $"Password=N'{customer.Password}', ";
                sql += $"CityId={customer.CityId}, ";
                sql += $"Address=N'{customer.Address}', ";
                sql += $"Phone={customer.Phone}, ";
                sql += $"status={customer.status} ";
                sql += $"where ManagerID=N'{customer.ManagerID}'";
            }

            DbContext Db = new DbContext();
            Db.ExecuteNonQuery(sql);
        }

        public static void DeleteManager(int id)
        {
            DbContext Db = new DbContext();
            string Sql = $"DELETE FROM T_Manager WHERE ManagerID = {id}";
            Db.Execute(Sql);
            Db.Close();
        }

    }
}