using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Model.Models
{
    public partial class TripDetails
    {
        public int TripId { get; set; }
        public string ActualRef { get; set; }
        public int? ItemId { get; set; }
        public int SenderId { get; set; }
        [Display(Name="Select Recipient")]
        public int RecipientId { get; set; }
        public int BatchId { get; set; }
        [Display(Name = "Items")]
        public string Name { get; set; }
        public string Comment { get; set; }
        public int? Quantity { get; set; }
        public decimal? Total { get; set; }
        public decimal? GrandTotal { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Status { get; set; }

        public virtual Batch Batch { get; set; }
        public virtual Items Item { get; set; }
        public virtual Recipients Recipient { get; set; }
        public virtual Senders Sender { get; set; }
    }
}
