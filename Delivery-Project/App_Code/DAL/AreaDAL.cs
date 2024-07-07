using BLL;
using DATA;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DAL
{
    public class AreaDAL: Area
    {
        public static List<Area> GetAll()
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג גישה לבסיס נתנים
            string sql = "select * from AreaList";//הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql);// הפעלת השאילתה וקבלת התוצאות לטבלת נתטנים
            List<Area> Area = new List<Area>();
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Area C = new Area()
                {
                    AreaId = int.Parse(Dt.Rows[i]["AreaId"] + ""),
                    AreaName = Dt.Rows[i]["AreaName"] + "",
                    ListCities = Dt.Rows[i]["ListCities"] + "",
                    CreateTime = DateTime.Parse(Dt.Rows[i]["addDate"] + "")
                };
                Area.Add(C);
            }
            Db.Close();
            return Area;
        }
        public static Area GetById(int Id)
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג גישה לבסיס נתנים
            string sql = $"select * from AreaList where AreaId={Id}";//הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql);// הפעלת השאילתה וקבלת התוצאות לטבלת נתטנים
            Area area = null;
            if (Dt.Rows.Count > 0)
            {
                area = new Area()
                {
                    AreaId = int.Parse(Dt.Rows[0]["AreaId"] + ""),
                    AreaName = Dt.Rows[0]["AreaName"] + "",
                    ListCities = Dt.Rows[0]["ListCities"] + "",
                };
            }
            Db.Close();
            return area;
        }
        public static Area Save(Area area)
        {
            string sql;
            if (area.AreaId == -1)
            {
                sql = "INSERT INTO T_Cities (AreaName,ListCities) VALUES ";
                sql += $"(N'{area.AreaName}',N'{area.ListCities}')";
            }
            else
            {
                sql = "UPDATE T_Cities SET ";
                sql += $"AreaName = N'{area.AreaName}' ";
                sql += $"ListCities = N'{area.ListCities}' ";
                sql += $"WHERE CityId = {area.AreaId}";
            }

            DbContext Db = new DbContext();
            Db.ExecuteNonQuery(sql);
            GetAll();
            return area;
        }
    }
}