using DAL;
using DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
    public class Orders
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CityId { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }
        public int status { get; set; }
        public DateTime OrderDate { get; set; }// תאריך הזמנה 
        public DateTime? ChooseDeliveryTime { get; set; }// תאריך רצוי הזמנה 
        public DateTime? DeliveryTime { get; set; } // תאריך העמסה
        public DateTime? Datedelivery { get; set; } // מועד מסירה
        public bool HasShipment { get; set; }
        public static List<Orders> GetAll()
        {
            return OrderDAL.GetAll();
        }

        public static Orders GetById(int id)
        {
            return OrderDAL.GetById(id);
        }

        public void Save()
        {
            OrderDAL.Save(this);
        }
        public static void Delete(int OrderId)
        {
             OrderDAL.DeleteOrder(OrderId);
        }
    }
}