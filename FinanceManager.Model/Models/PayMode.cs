using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class PayMode
    {
        public int Id { get; set; }
        public string Paymode1 { get; set; }
        public bool? IsSelected { get; set; }
    }
}
