using FinanceManager.Model.Models;
using System;

namespace RabantFinanceManager.Controllers
{
    public class ExpenditureAddModel //:Expenditure
    {
        public string BatchId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ModeOfPayment { get; set; }
        public string Amount { get; set; }
        public string CreatedDate { get; set; }

    }
}