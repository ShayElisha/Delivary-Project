using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class Area
    {
        public int AreaId {  get; set; }
        public string AreaName { get; set; }
        public string ListCities { get; set; }
        public DateTime CreateTime { get; set; }
        public static List<Area> GetAll()
        {
            return AreaDAL.GetAll();
        }
        public static Area GetById(int id)
        {
            return AreaDAL.GetById(id);
        }
        public Area Save()
        {
            return AreaDAL.Save(this);
        }
    }
}