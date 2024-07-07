using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace BLL
{
    public class Jobs
    {
        public int JobId {  get; set; }
        public string DriverId {  get; set; }
        public string DriverName { get; set; }
        public int customerID {  get; set; }
        public string CustomerName { get; set; }
        public string address {  get; set; }
        public int vehicleID {  get; set; }
    }
}