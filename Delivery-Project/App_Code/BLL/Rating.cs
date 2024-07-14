using DAL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class Rating
    {
        public int RatingId {  get; set; }
        public string DriverId { get; set;}
        public int rating {  get; set; }
        public DateTime AddDate { get; set; }
        public static List<Rating> GetAll()
        {
            return RatingDAL.GetAll();
        }
        public static Rating GetById(int id)
        {
            return RatingDAL.GetById(id);
        }
        public Rating Save()
        {
            return RatingDAL.Save(this);
        }
    }
}