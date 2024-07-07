using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delivery_Project.App_Code.BLL
{
    public class Salaries
    {
        public int SalaryID {  get; set; }
        public string DriverID { get; set; }
        public decimal Amount {  get; set; }
        public decimal Bonuse { get; set; }
        public static int Transactions =0;
        public DateTime PayDate { get; set; }
    }
}