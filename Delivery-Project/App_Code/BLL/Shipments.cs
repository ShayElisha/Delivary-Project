using DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class Shipments
    {
        public int ShipId { get; set; }
        public string OrderID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string SourceAdd { get; set; }
        public string SourceCity { get; set; }
        public string DestinationAdd { get; set; }
        public string DestinationCity { get; set; }
        public string Phone { get; set; }
        public DateTime? CollectionDate { get; set; }//תאריך איסוף
        public DateTime? DateDelivery { get; set; }// תאריך שהמשלוח אמור להגי
        public DateTime? ReleaseDate { get; set; }// תאריך שחרור מהמחסן 
        public int Quantity { get; set; }
        public int DriverId { get; set; }
        public int Status { get; set; }
        public DateTime AddDate { get; set; }

        public static List<Shipments> GetAll()
        {
            return ShipmentsDAL.GetAll();
        }
        public static Shipments GetById(int id)
        {
            return ShipmentsDAL.GetShipmentById(id);
        }
        public void Save()
        {
            ShipmentsDAL.Save(this);
        }
        public static void Delete(int id)
        {
             ShipmentsDAL.DeleteShipment(id);
        }
       
    }
}
