using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class Driver
    {
        public int DriverID { get; set; }
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int MaxAmountShipment { get; set; }
        public int CurrentAmount { get; set; }
        public int status { get; set; }
        public string AddDate { get; set; }


        public static List<Driver> GetAll()
        {
            return DriverDAL.GetAll();
        }
        public static Driver GetById(int id)
        {
            return DriverDAL.GetById(id);
        }
        public static Driver GetByDriverId(int id)
        {
            return DriverDAL.GetByDriverId(id);
        }

        public Driver Save()
        {
            return DriverDAL.Save(this);

        }
        public static void Delete(int OrderId)
        {
            DriverDAL.DeleteDriver(OrderId);
        }
        public static void RemoveQuantityFromDriver(int driverId, int quantity)
        {
            DriverDAL.RemoveQuantityFromDriver(driverId, quantity);
        }
    }
}