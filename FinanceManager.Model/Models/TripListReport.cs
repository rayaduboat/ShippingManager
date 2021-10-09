using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class TripListReport
    {
        public int TripReportId { get; set; }
        public string ActualBatch { get; set; }
        public string ActualRef { get; set; }
        public int ItemId { get; set; }
        public string SenderName { get; set; }
        public string RecipientName { get; set; }
        public int BatchId { get; set; }
        public string ItemName { get; set; }
        public string Comment { get; set; }
        public string TelSender { get; set; }
        public string TelRecipient { get; set; }
        public string SendTown { get; set; }
        public string RecTown { get; set; }
        public int? Quantity { get; set; }
        public decimal? Total { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
