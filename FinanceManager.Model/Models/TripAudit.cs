using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class TripAudit
    {
        public int Id { get; set; }
        public int RefId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int BatchId { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public int? OriginalStatus { get; set; }
        public int? NewStatus { get; set; }
        public DateTime? TimeOfChange { get; set; }
        public string UpdatedBy { get; set; }
        public string StatusDetails { get; set; }
    }
}
