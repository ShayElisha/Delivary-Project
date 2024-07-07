using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DAL
{
    public class OrderDAL:Orders
    {
        public static List<Orders> GetAll()
        {
            DbContext Db = new DbContext(); // יצירת אובייקט מסוג גישה לבסיס נתונים
            string sql = "select * from T_Orders"; // הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql); // הפעלת השאילתה וקבלת התוצאות לטבלת נתונים
            List<Orders> order = new List<Orders>();

            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Orders C = new Orders()
                {
                    OrderID = int.Parse(Dt.Rows[i]["OrderID"].ToString()),
                    CustomerID =Dt.Rows[i]["CustomerID"].ToString(),
                    FullName = Dt.Rows[i]["FullName"].ToString(),
                    Email = Dt.Rows[i]["Email"].ToString(),
                    Phone = Dt.Rows[i]["Phone"].ToString(),
                    CityId = Dt.Rows[i]["CityId"].ToString(),
                    Address = Dt.Rows[i]["Address"].ToString(),
                    Quantity = int.Parse(Dt.Rows[i]["Quantity"].ToString()),
                    Notes = Dt.Rows[i]["Notes"].ToString(),
                    OrderDate = DateTime.Parse(Dt.Rows[i]["OrderDate"].ToString()),
                    status = int.Parse(Dt.Rows[i]["status"].ToString()),
                    ChooseDeliveryTime = Dt.Rows[i]["ChooseDeliveryTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(Dt.Rows[i]["ChooseDeliveryTime"]),
                    DeliveryTime = Dt.Rows[i]["DeliveryTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(Dt.Rows[i]["DeliveryTime"]),
                    Datedelivery = Dt.Rows[i]["Datedelivery"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(Dt.Rows[i]["Datedelivery"])

                };
                order.Add(C);
            }
            Db.Close();
            return order;
        }

        public static Orders GetById(int Id)
        {
            DbContext Db = new DbContext(); // יצירת אובייקט מסוג גישה לבסיס נתונים
            string sql = $"select * from T_Orders where OrderID={Id}"; // הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql); // הפעלת השאילתה וקבלת התוצאות לטבלת נתונים
            Orders order = null;
            if (Dt.Rows.Count > 0)
            {
                order = new Orders()
                {
                    OrderID = int.Parse(Dt.Rows[0]["OrderID"].ToString()),
                    CustomerID = Dt.Rows[0]["CustomerID"].ToString(),
                    FullName = Dt.Rows[0]["FullName"].ToString(),
                    Email = Dt.Rows[0]["Email"].ToString(),
                    Phone = Dt.Rows[0]["Phone"].ToString(),
                    CityId = Dt.Rows[0]["CityId"].ToString(),
                    Address = Dt.Rows[0]["Address"].ToString(),
                    Quantity = int.Parse(Dt.Rows[0]["Quantity"].ToString()),
                    Notes = Dt.Rows[0]["Notes"].ToString(),
                    OrderDate = DateTime.Parse(Dt.Rows[0]["OrderDate"].ToString()),
                    status = int.Parse(Dt.Rows[0]["status"].ToString()),
                    ChooseDeliveryTime = Dt.Rows[0]["ChooseDeliveryTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(Dt.Rows[0]["ChooseDeliveryTime"]),
                    DeliveryTime = Dt.Rows[0]["DeliveryTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(Dt.Rows[0]["DeliveryTime"]),
                    Datedelivery = Dt.Rows[0]["Datedelivery"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(Dt.Rows[0]["Datedelivery"])
                };
            }
            Db.Close();
            return order;
        }

        public static void Save(Orders order)
        {
            string sql = "";
            if (order.OrderID == -1)
            {
                sql = "INSERT INTO T_Orders (CustomerID, FullName, Email, Phone, CityId, Address, Quantity, Notes, ChooseDeliveryTime) VALUES ";
                sql += $"(N'{order.CustomerID}', N'{order.FullName}', N'{order.Email}', N'{order.Phone}', {order.CityId}, N'{order.Address}', {order.Quantity}, N'{order.Notes}', {(order.ChooseDeliveryTime.HasValue ? $"'{order.ChooseDeliveryTime.Value.ToString("yyyy-MM-dd HH:mm:ss")}'" : "NULL")})";
            }
            else
            {
                sql = "UPDATE T_Orders SET ";
                sql += $"FullName=N'{order.FullName}', ";
                sql += $"Email=N'{order.Email}', ";
                sql += $"Phone=N'{order.Phone}', ";
                sql += $"CityId={order.CityId}, ";
                sql += $"Address=N'{order.Address}', ";
                sql += $"Quantity={order.Quantity}, ";
                sql += $"Notes=N'{order.Notes}', ";
                sql += $"status='{order.status}' ";
                if (order.Datedelivery != null)
                {
                    sql += $", Datedelivery='{order.Datedelivery?.ToString("yyyy-MM-dd HH:mm:ss")}' ";
                }
                if (order.DeliveryTime != null)
                {
                    sql += $", DeliveryTime='{order.DeliveryTime?.ToString("yyyy-MM-dd HH:mm:ss")}' ";
                }
               

                sql += $"WHERE OrderID={order.OrderID}";
            }

            DbContext Db = new DbContext();
            Db.ExecuteNonQuery(sql);
        }
        public static void DeleteOrder(int id)
        {
            DbContext Db = new DbContext();
            string Sql = $"DELETE FROM T_Orders WHERE OrderID = {id}";
            Db.Execute(Sql);
            Db.Close();
        }

    }
}