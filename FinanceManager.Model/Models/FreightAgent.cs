using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class FreightAgent
    {
        public int FreightAgentId { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string PostCode { get; set; }
        public string TownCity { get; set; }
        public string Country { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public string WebAddress { get; set; }
    }
}
