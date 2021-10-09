using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class Triplist
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string SenderFirstName { get; set; }
        public string SenderSecondName { get; set; }
        public string SendersTown { get; set; }
        public string SendersTelephone { get; set; }
        public string ReceiverFirstName { get; set; }
        public string ReceiverSecondName { get; set; }
        public string ReceiverAddress1 { get; set; }
        public string ReceiverAddress2 { get; set; }
        public string ReceiverTown { get; set; }
        public string ReceiverTelephone { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? Discount { get; set; }
        public int BatchId { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
    }
}
