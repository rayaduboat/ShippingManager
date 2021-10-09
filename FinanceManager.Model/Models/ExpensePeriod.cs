using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class ExpensePeriod
    {
        public int Id { get; set; }
        public string Period { get; set; }
        public bool? IsSelected { get; set; }
    }
}
