using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class ExpenseItems
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsSelected { get; set; }
    }
}
