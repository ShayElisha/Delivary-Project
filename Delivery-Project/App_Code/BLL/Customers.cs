using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class Customers
    {
        public int CustomerID {  get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public string Company { get; set; }
        public int status { get; set; }
        public string AddDate { get; set; }

        public static List<Customers> GetAll()
        {
            return CustomerDAL.GetAll();
        }
        public static Customers GetById(int id)
        {
            return CustomerDAL.GetById(id);
        }
        public void Save()
        {
            CustomerDAL.Save(this);
        }
        public static void Delete(int OrderId)
        {
            CustomerDAL.DeleteCustomer(OrderId);
        }
    }
}