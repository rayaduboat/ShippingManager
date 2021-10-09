using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class Batch
    {
        public Batch()
        {
            Expenditure = new HashSet<Expenditure>();
            Income = new HashSet<Income>();
            Performance = new HashSet<Performance>();
            TripDetails = new HashSet<TripDetails>();
        }

        public int BatchId { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public string CreatedBy { get; set; }
        public int ShippersId { get; set; }
        public string ActualBatch { get; set; }
        public bool? IsSelected { get; set; }

        public virtual Shippers Shippers { get; set; }
        public virtual ICollection<Expenditure> Expenditure { get; set; }
        public virtual ICollection<Income> Income { get; set; }
        public virtual ICollection<Performance> Performance { get; set; }
        public virtual ICollection<TripDetails> TripDetails { get; set; }
    }
}
