using FinanceManager.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.DataTransferModel
{
    public class DataTransferModel
    {
        public string batchId { get; set; }
        public string senderId { get; set; }
        public string recipientId { get; set; }
        public string actualRef { get; set; }
        public string orderDate       { get; set; }
        public string urlLink { get; set; }
        public string orderList { get; set; }
        public string orderTelephone { get; set; }
        public string orderPostcode{ get; set; }

    }

    public class ItemModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string quantity { get; set; }
    }

    public class InvoiceOrders
    {
        public string batchId { get; set; }
        public string refNumber { get; set; }
        public string grandTotal { get; set; }
        public string hashString { get; set; }
    }

    public class ShipperID
    {
        public string id { get; set; }
    }
    public class RefNum
    {
        public string id { get; set; }
    }

}
