using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class ExpenditureReport
    {
        public int ExpReportId { get; set; }
        public int? Batch { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ModeOfPayment { get; set; }
        public string ExpenseType { get; set; }
        public decimal? Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ActualBatch { get; set; }
    }
}
