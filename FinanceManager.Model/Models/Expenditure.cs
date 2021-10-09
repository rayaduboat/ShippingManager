using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class Expenditure
    {
        public int ExpenditureId { get; set; }
        public int? BatchId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ModeOfPayment { get; set; }
        public string ExpenseType { get; set; }
        public decimal? Amount { get; set; }
        public string IsReceiptIssued { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Batch Batch { get; set; }
    }
}
