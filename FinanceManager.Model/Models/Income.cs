using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class Income
    {
        public int IncomeId { get; set; }
        public int? BatchId { get; set; }
        public string ModeOfPayment { get; set; }
        public string ActualRef { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Batch Batch { get; set; }
    }
}
