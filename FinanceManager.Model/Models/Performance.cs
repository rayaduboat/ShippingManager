using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class Performance
    {
        public int Id { get; set; }
        public int? BatchId { get; set; }
        public string AccountYear { get; set; }
        public string AccountMonth { get; set; }
        public DateTime? AcountDate { get; set; }
        public int? AccountPeriod { get; set; }
        public decimal? TotalIncome { get; set; }
        public decimal? TotalExpenditure { get; set; }
        public decimal? AditionalIncome { get; set; }
        public decimal? ProfitLoss { get; set; }
        public decimal? ProfitLossPercent { get; set; }

        public virtual Batch Batch { get; set; }
    }
}
