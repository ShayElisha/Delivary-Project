using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BLL;
using System.Net;

namespace DAL
{
    public class CustomerDAL:Customers
    {
        public static List<Customers> GetAll()
        {
            DbContext Db = new DbContext(); // יצירת אובייקט מסוג גישה לבסיס נתונים
            string sql = "select * from T_Customer"; // הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql); // הפעלת השאילתה וקבלת התוצאות לטבלת נתונים
            List<Customers> Customer = new List<Customers>();

            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Customers C = new Customers()
                {
                    CustomerID = int.Parse(Dt.Rows[i]["CustomerID"].ToString()),
                    FullName = Dt.Rows[i]["FullName"].ToString(),
                    Email = Dt.Rows[i]["Email"].ToString(),
                    Password = Dt.Rows[i]["Password"].ToString(),
                    Phone = Dt.Rows[i]["Phone"].ToString(),
                    Address = Dt.Rows[i]["Address"].ToString(),
                    CityId = int.Parse(Dt.Rows[i]["CityId"].ToString()),
                    Company = Dt.Rows[i]["Company"].ToString(),
                    status = int.Parse(Dt.Rows[i]["status"].ToString()),
                    AddDate = Dt.Rows[i]["AddDate"].ToString()
                };
                Customer.Add(C);
            }
            Db.Close();
            return Customer;
        }

        public static Customers GetById(int Id)
        {
            DbContext Db = new DbContext(); // יצירת אובייקט מסוג גישה לבסיס נתונים
            string sql = $"select * from T_Customer where CusID={Id}"; // הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql); // הפעלת השאילתה וקבלת התוצאות לטבלת נתונים
            Customers customer = null;
            if (Dt.Rows.Count > 0)
            {
                customer = new Customers()
                {
                    CustomerID = int.Parse(Dt.Rows[0]["CustomerID"] + ""),
                    FullName = Dt.Rows[0]["FullName"] + "",
                    Email = Dt.Rows[0]["Email"] + "",
                    Password = Dt.Rows[0]["Password"].ToString(),
                    Phone = Dt.Rows[0]["Phone"] + "",
                    Address = Dt.Rows[0]["Address"] + "",
                    CityId = int.Parse(Dt.Rows[0]["City"] + ""),
                    Company = Dt.Rows[0]["Company"] + "",
                    status = int.Parse(Dt.Rows[0]["status"] + ""),
                    AddDate = Dt.Rows[0]["AddDate"] + "",
                };
            }
            Db.Close();
            return customer;
        }

        public static void Save(Customers customer)
        {
            string sql = "";
            if (customer.CustomerID == -1)
            {

                sql = "insert into T_Customer(FullName, Email,Password ,Phone, Address, CityID,Company, status) values ";
                sql += $"(N'{customer.FullName}',N'{customer.Email}', N'{customer.Password}', N'{customer.Phone}', N'{customer.Address}', N'{customer.CityId}',N'{customer.Company}', {customer.status})";
            }
            else
            {
                sql = "update T_Customers set ";
                sql += $"FullName=N'{customer.FullName}', ";
                sql += $"Email=N'{customer.Email}', ";
                sql += $"Phone=N'{customer.Phone}', ";
                sql += $"Address={customer.Address}, ";
                sql += $"CityID=N'{customer.CityId}', ";
                sql += $"Company={customer.Company} ";
                sql += $"status={customer.status} ";
                sql += $"where CustomerID=N'{customer.CustomerID}'";
            }

            DbContext Db = new DbContext();
            Db.ExecuteNonQuery(sql);
        }
        public static void DeleteCustomer(int id)
        {
            DbContext Db = new DbContext();
            string Sql = $"DELETE FROM T_Customer WHERE CustomerID = {id}";
            Db.Execute(Sql);
            Db.Close();
        }
    }
}