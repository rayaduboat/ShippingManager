using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class Ghcity
    {
        public int Id { get; set; }
        public string City { get; set; }
        public bool? IsSelected { get; set; }
    }
}
