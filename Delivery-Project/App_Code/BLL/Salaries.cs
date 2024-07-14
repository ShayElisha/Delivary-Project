using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class Salaries
    {
        public int SalaryID {  get; set; }
        public string DriverID { get; set; }
        public int MinimumQuantity {  get; set; }
        public int DeliaryAmount { get; set; }
        public decimal Bonuse { get; set; }
        public int faults { get; set; }
        public int Report { get; set; }
        public decimal salary { get; set; }
        public DateTime AddDate { get; set; }
        public static List<Salaries> GetAll()
        {
            return SalariesDAL.GetAll();
        }
        public static Salaries GetById(string id)
        {
            return SalariesDAL.GetById(id);
        }
        public Salaries Save()
        {
            return SalariesDAL.Save(this);
        }
        public static Salaries GetBySalaryId(int Id)
        {
            return SalariesDAL.GetBySalaryId(Id);

        }

        public static void Delete(string id)
        {
            SalariesDAL.Delete(id);
        }
        public static void calculateSalary(string Id)
        {
            SalariesDAL.calculateSalary(Id);
        }

    }
}