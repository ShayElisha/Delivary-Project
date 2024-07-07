using BLL;
using DAL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DAL
{
    public class CitiesDAL:Cities
    { 
        public static List<Cities> GetAll()
        {
        DbContext Db = new DbContext();//יצירת אובייקט מסוג גישה לבסיס נתנים
        string sql = "select * from T_Cities";//הגדרת משפט שאילתה
        DataTable Dt = Db.Execute(sql);// הפעלת השאילתה וקבלת התוצאות לטבלת נתטנים
        List<Cities> city = new List<Cities>();
        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            Cities C = new Cities()
            {
                CityId = int.Parse(Dt.Rows[i]["CityId"] + ""),
                CityName = Dt.Rows[i]["CityName"] + "",
                AddDate = Dt.Rows[i]["AddDate"] + ""
            };
            city.Add(C);
        }
        Db.Close();
        return city;
        }
        public static Cities GetById(int Id)
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג גישה לבסיס נתנים
            string sql = $"select * from T_Cities where CityId={Id}";//הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql);// הפעלת השאילתה וקבלת התוצאות לטבלת נתטנים
            Cities city = null;
            if (Dt.Rows.Count > 0)
            {
                city = new Cities()
                {
                    CityId = int.Parse(Dt.Rows[0]["CityId"] + ""),
                    CityName = Dt.Rows[0]["CityName"] + "",
                };
            }
            Db.Close();
            return city;
        }
        public static Cities Save(Cities city)
        {
            string sql;
            if (city.CityId == -1)
            {
                sql = "INSERT INTO T_Cities (CityName) VALUES ";
                sql += $"(N'{city.CityName}')";
            }
            else
            {
                sql = "UPDATE T_Cities SET ";
                sql += $"CityName = N'{city.CityName}' ";
                sql += $"WHERE CityId = {city.CityId}";
            }

            DbContext Db = new DbContext();
            Db.ExecuteNonQuery(sql);
            GetAll();
            return city;
        }
    }
}