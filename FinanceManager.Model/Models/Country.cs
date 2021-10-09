using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public bool? IsSelected { get; set; }
    }
}
