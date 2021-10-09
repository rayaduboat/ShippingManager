using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class ReferenceNumber1
    {
        public int Id { get; set; }
        public int ShippersId { get; set; }
        public int BatchId { get; set; }
        public int SenderId { get; set; }
        public string RefLabel { get; set; }
        public string StatusFlag { get; set; }
    }
}
