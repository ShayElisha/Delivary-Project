using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DAL
{
    public class DriverDAL:Driver
    {
        public static List<Driver> GetAll()
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג גישה לבסיס נתנים
            string sql = "select * from T_Driver";//הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql);// הפעלת השאילתה וקבלת התוצאות לטבלת נתטנים
            List<Driver> driver = new List<Driver>();
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Driver C = new Driver()
                {
                    DriverID = int.Parse(Dt.Rows[i]["DriverID"] + ""),
                    Id = Dt.Rows[i]["Id"] + "",
                    FullName = Dt.Rows[i]["FullName"] + "",
                    Email = Dt.Rows[i]["Email"] + "",
                    Password = Dt.Rows[i]["Password"] + "",
                    CityId = int.Parse(Dt.Rows[i]["CityId"] + ""),
                    Address = Dt.Rows[i]["Address"] + "",
                    Phone = Dt.Rows[i]["Phone"] + "",
                    MaxAmountShipment = int.Parse(Dt.Rows[i]["MaxAmountShipment"] + ""),
                    CurrentAmount = int.Parse(Dt.Rows[i]["CurrentAmount"] + ""),
                    status = int.Parse(Dt.Rows[i]["status"] + ""),
                    AddDate = Dt.Rows[i]["AddDate"] + ""
                };
                driver.Add(C);
            }
            Db.Close();
            return driver;
        }
        public static Driver GetById(int Id)
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג גישה לבסיס נתנים
            string sql = $"select * from T_Driver where Id={Id}";//הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql);// הפעלת השאילתה וקבלת התוצאות לטבלת נתטנים
            Driver city = null;
            if (Dt.Rows.Count > 0)
            {
                city = new Driver()
                {
                    DriverID = int.Parse(Dt.Rows[0]["DriverID"] + ""),
                    Id = Dt.Rows[0]["Id"] + "",
                    FullName = Dt.Rows[0]["FullName"] + "",
                    Email = Dt.Rows[0]["Email"] + "",
                    Password = Dt.Rows[0]["Password"] + "",
                    CityId = int.Parse(Dt.Rows[0]["CityId"] + ""),
                    Address = Dt.Rows[0]["Address"] + "",
                    Phone = Dt.Rows[0]["Phone"] + "",
                    MaxAmountShipment = int.Parse(Dt.Rows[0]["MaxAmountShipment"] + ""),
                    CurrentAmount = int.Parse(Dt.Rows[0]["CurrentAmount"] + ""),
                    status = int.Parse(Dt.Rows[0]["status"] + ""),
                    AddDate = Dt.Rows[0]["AddDate"] + "",
                };
            }
            Db.Close();
            return city;
        }
        public static Driver GetByDriverId(int Id)
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג גישה לבסיס נתנים
            string sql = $"select * from T_Driver where DriverID={Id}";//הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql);// הפעלת השאילתה וקבלת התוצאות לטבלת נתטנים
            Driver city = null;
            if (Dt.Rows.Count > 0)
            {
                city = new Driver()
                {
                    DriverID = int.Parse(Dt.Rows[0]["DriverID"] + ""),
                    Id = Dt.Rows[0]["Id"] + "",
                    FullName = Dt.Rows[0]["FullName"] + "",
                    Email = Dt.Rows[0]["Email"] + "",
                    Password = Dt.Rows[0]["Password"] + "",
                    CityId = int.Parse(Dt.Rows[0]["CityId"] + ""),
                    Address = Dt.Rows[0]["Address"] + "",
                    Phone = Dt.Rows[0]["Phone"] + "",
                    MaxAmountShipment = int.Parse(Dt.Rows[0]["MaxAmountShipment"] + ""),
                    CurrentAmount = int.Parse(Dt.Rows[0]["CurrentAmount"] + ""),
                    status = int.Parse(Dt.Rows[0]["status"] + ""),
                    AddDate = Dt.Rows[0]["AddDate"] + "",
                };
            }
            Db.Close();
            return city;
        }
        public static Driver Save(Driver driver)
        {
            string sql;
            if (driver.DriverID == -1)
            {
                sql = "INSERT INTO T_Driver (Id,FullName,Email,Password,CityId,Address,Phone,MaxAmountShipment,status) VALUES ";
                sql += $"(N'{driver.Id}',N'{driver.FullName}',N'{driver.Email}',N'{driver.Password}',N'{driver.CityId}',N'{driver.Address}',N'{driver.Phone}',N'{driver.MaxAmountShipment}',N'{driver.status}')";
            }
            else
            {
                sql = "UPDATE T_Driver SET ";
                sql += $"Id = N'{driver.Id}', ";
                sql += $"FullName = N'{driver.FullName}', ";
                sql += $"Email = N'{driver.Email}', ";
                sql += $"Password = N'{driver.Password}', ";
                sql += $"CityId = N'{driver.CityId}', ";
                sql += $"Address = N'{driver.Address}', ";
                sql += $"Phone = N'{driver.Phone}', ";
                sql += $"MaxAmountShipment = N'{driver.MaxAmountShipment}', ";
                sql += $"CurrentAmount = N'{driver.CurrentAmount}', ";
                sql += $"status = N'{driver.status}' ";
                sql += $"WHERE DriverID = {driver.DriverID}";
            }

            DbContext Db = new DbContext();
            Db.ExecuteNonQuery(sql);
            GetAll();
            return driver;
        }
        public static void DeleteDriver(int id)
        {
            DbContext Db = new DbContext();
            string Sql = $"DELETE FROM T_Driver WHERE DriverID = {id}";
            Db.Execute(Sql);
            Db.Close();
        }
        public static void RemoveQuantityFromDriver(int driverId, int quantity)
        {
            Driver driver = Driver.GetByDriverId(driverId);
            if (driver != null)
            {
                if (driver.CurrentAmount >= quantity)
                {
                    driver.CurrentAmount -= quantity;
                    driver.Save();
                }
            }
        }
    }
}