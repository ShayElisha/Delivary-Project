
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class Manager
    {
        
        public int ManagerID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int status { get; set; }
        public string AddDate { get; set; }

        public static List<Manager> GetAll()
        {
            return ManagerDAL.GetAll();
        }
        public static Manager GetById(int id)
        {
            return ManagerDAL.GetById(id);
        }
        public void Save()
        {
            ManagerDAL.Save(this);
        }

        public static void Delete(int id)
        {
            ManagerDAL.DeleteManager(id);
        }

    }
}