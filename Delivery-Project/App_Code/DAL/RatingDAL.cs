using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DAL
{
    public class RatingDAL: Rating
    {
        public static List<Rating> GetAll()
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג גישה לבסיס נתנים
            string sql = "select * from Rating";//הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql);// הפעלת השאילתה וקבלת התוצאות לטבלת נתטנים
            List<Rating> city = new List<Rating>();
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Rating C = new Rating()
                {
                    RatingId = int.Parse(Dt.Rows[i]["RatingId"] + ""),
                    DriverId = Dt.Rows[i]["DriverId"] + "",
                    rating = int.Parse(Dt.Rows[i]["rating"] + ""),
                    AddDate = DateTime.Parse(Dt.Rows[i]["AddDate"] + "")
                };
                city.Add(C);
            }
            Db.Close();
            return city;
        }
        public static Rating GetById(int Id)
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג גישה לבסיס נתנים
            string sql = $"select * from Rating where DriverId={Id}";//הגדרת משפט שאילתה
            DataTable Dt = Db.Execute(sql);// הפעלת השאילתה וקבלת התוצאות לטבלת נתטנים
            Rating city = null;
            if (Dt.Rows.Count > 0)
            {
                city = new Rating()
                {
                    RatingId = int.Parse(Dt.Rows[0]["RatingId"] + ""),
                    DriverId = Dt.Rows[0]["DriverId"] + "",
                    rating = int.Parse(Dt.Rows[0]["rating"] + ""),
                    AddDate = DateTime.Parse(Dt.Rows[0]["AddDate"] + "")
                };
            }
            Db.Close();
            return city;
        }
        public static Rating Save(Rating rating)
        {
            string sql;
            
                sql = "INSERT INTO Rating (DriverId,rating) VALUES ";
                sql += $"(N'{rating.DriverId}',N'{rating.rating}')";
            
            

            DbContext Db = new DbContext();
            Db.ExecuteNonQuery(sql);
            GetAll();
            return rating;
        }
    }
}