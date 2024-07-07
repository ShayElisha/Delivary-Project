using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class Cities
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int status { get; set; }
        public string AddDate { get; set; }
        public static List<Cities> GetAll()
        {
        return CitiesDAL.GetAll();
        }
        public static Cities GetById(int id)
        {
            return CitiesDAL.GetById(id);
        }
        public Cities Save()
        {
            return CitiesDAL.Save(this);
        }
        public static string GetCityNameById(List<Cities> cities, string cityId)
        {
            int id;
            if (int.TryParse(cityId, out id))
            {
                var city = cities?.Find(c => c.CityId == id);
                if (city != null)
                {
                    return city.CityName;
                }
            }
            return cityId; // Return the ID itself if the name is not found
        }

    }
    
}