using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class ShipmentsDAL
    {
        public static List<Shipments> GetAll()
        {
            DbContext Db = new DbContext(); // יצירת אובייקט מסוג גישה לבסיס נתונים
            string sql = "select * from T_Shipments"; // הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql); // הפעלת השאילתה וקבלת התוצאות לטבלת נתונים
            List<Shipments> shipments = new List<Shipments>();

            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Shipments s = new Shipments()
                {
                    ShipId = int.Parse(Dt.Rows[i]["ShipId"].ToString()),
                    OrderID = Dt.Rows[i]["OrderID"].ToString(),
                    CustomerID = Dt.Rows[i]["CustomerID"].ToString(),
                    CustomerName = Dt.Rows[i]["CustomerName"].ToString(),
                    SourceAdd = Dt.Rows[i]["SourceAdd"].ToString(),
                    SourceCity = Dt.Rows[i]["SourceCity"].ToString(),
                    DestinationAdd = Dt.Rows[i]["DestinationAdd"].ToString(),
                    DestinationCity = Dt.Rows[i]["DestinationCity"].ToString(),
                    Phone = Dt.Rows[i]["Phone"].ToString(),
                    CollectionDate = Dt.Rows[i]["CollectionDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(Dt.Rows[i]["CollectionDate"]),
                    DateDelivery = Dt.Rows[i]["DateDelivery"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(Dt.Rows[i]["DateDelivery"]),
                    ReleaseDate = Dt.Rows[i]["ReleaseDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(Dt.Rows[i]["ReleaseDate"]),
                    Quantity = int.Parse(Dt.Rows[i]["Quantity"] + ""),
                    DriverId = int.Parse(Dt.Rows[i]["DriverId"].ToString()),
                    Status = int.Parse(Dt.Rows[i]["Status"].ToString()),
                    AddDate = DateTime.Parse(Dt.Rows[i]["AddDate"].ToString())
                };
                shipments.Add(s);
            }
            Db.Close();
            return shipments;
        }

        public static Shipments GetShipmentById(int id)
        {
            DbContext Db = new DbContext(); // יצירת אובייקט מסוג גישה לבסיס נתונים
            string sql = $"select * from T_Shipments where ShipId={id}"; // הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql); // הפעלת השאילתה וקבלת התוצאות לטבלת נתונים
            Shipments shipment = null;
            if (Dt.Rows.Count > 0)
            {
                shipment = new Shipments()
                {
                    ShipId = int.Parse(Dt.Rows[0]["ShipId"].ToString()),
                    OrderID = Dt.Rows[0]["OrderID"].ToString(),
                    CustomerID = Dt.Rows[0]["CustomerID"].ToString(),
                    CustomerName = Dt.Rows[0]["CustomerName"].ToString(),
                    SourceAdd = Dt.Rows[0]["SourceAdd"].ToString(),
                    SourceCity = Dt.Rows[0]["SourceCity"].ToString(),
                    DestinationAdd = Dt.Rows[0]["DestinationAdd"].ToString(),
                    DestinationCity = Dt.Rows[0]["DestinationCity"].ToString(),
                    Phone = Dt.Rows[0]["Phone"].ToString(),
                    CollectionDate = Dt.Rows[0]["CollectionDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(Dt.Rows[0]["CollectionDate"]),
                    DateDelivery = Dt.Rows[0]["DateDelivery"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(Dt.Rows[0]["DateDelivery"]),
                    ReleaseDate = Dt.Rows[0]["ReleaseDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(Dt.Rows[0]["ReleaseDate"]),
                    Quantity = int.Parse(Dt.Rows[0]["Quantity"] + ""),
                    DriverId = int.Parse(Dt.Rows[0]["DriverId"].ToString()),
                    Status = int.Parse(Dt.Rows[0]["Status"].ToString())
                };
            }
            Db.Close();
            return shipment;
        }

        public static void Save(Shipments shipment)
        {
            string sql = "";
            if (shipment.ShipId == -1)
            {
                sql = "INSERT INTO T_Shipments (OrderID, CustomerID, CustomerName, SourceAdd, SourceCity, DestinationAdd, DestinationCity, Phone, Quantity, DriverId, DateDelivery, Status) VALUES ";
                sql += $"('{shipment.OrderID}', '{shipment.CustomerID}', '{shipment.CustomerName}', N'{shipment.SourceAdd}', N'{shipment.SourceCity}', N'{shipment.DestinationAdd}', N'{shipment.DestinationCity}', '{shipment.Phone}', {shipment.Quantity}, {shipment.DriverId}, '{shipment.DateDelivery?.ToString("yyyy-MM-dd")}', {shipment.Status})";
            }
            else
            {
                sql = "UPDATE T_Shipments SET ";
                sql += $"OrderID='{shipment.OrderID}', ";
                sql += $"CustomerID='{shipment.CustomerID}', ";
                sql += $"CustomerName='{shipment.CustomerName}', ";
                sql += $"SourceAdd=N'{shipment.SourceAdd}', ";
                sql += $"SourceCity=N'{shipment.SourceCity}', ";
                sql += $"DestinationAdd=N'{shipment.DestinationAdd}', ";
                sql += $"DestinationCity=N'{shipment.DestinationCity}', ";
                sql += $"Phone='{shipment.Phone}', ";
                sql += $"Quantity={shipment.Quantity}, ";
                sql += $"DriverId={shipment.DriverId}, ";
                sql += $"Status={shipment.Status} ";

                if (shipment.CollectionDate != null)
                {
                    sql += $", CollectionDate='{shipment.CollectionDate?.ToString("yyyy-MM-dd HH:mm:ss")}' ";
                }
                if (shipment.DateDelivery != null)
                {
                    sql += $", DateDelivery='{shipment.DateDelivery?.ToString("yyyy-MM-dd HH:mm:ss")}' ";
                }
                if (shipment.ReleaseDate != null)
                {
                    sql += $", ReleaseDate='{shipment.ReleaseDate?.ToString("yyyy-MM-dd HH:mm:ss")}' ";
                }
                sql += $"WHERE ShipId={shipment.ShipId}";
            }
            DbContext Db = new DbContext();
            Db.ExecuteNonQuery(sql);
        }
            public static void DeleteShipment(int id)
        {
            DbContext Db = new DbContext();
            string Sql = $"DELETE FROM T_Shipments WHERE ShipId = {id}";
            Db.Execute(Sql);
            Db.Close();
        }
        
    }
}
