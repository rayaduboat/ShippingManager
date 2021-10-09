using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class Income1
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string SenderFirstname { get; set; }
        public string SenderLastname { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? Discount { get; set; }
    }
}
