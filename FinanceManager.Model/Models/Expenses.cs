using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class Expenses
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Vatnumber { get; set; }
        public string CompanyNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ModeOfPayment { get; set; }
        public string ExpenseType { get; set; }
        public decimal? Amount { get; set; }
        public string IsReceiptIssued { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ExpenditureId { get; set; }
        public int BatchId { get; set; }
        public string CreatedBy { get; set; }
        public int ShippersId { get; set; }
        public string Expr1 { get; set; }
    }
}
