using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class Shippers
    {
        public Shippers()
        {
            Batch = new HashSet<Batch>();
            BatchLabel = new HashSet<BatchLabel>();
            Senders = new HashSet<Senders>();
        }

        public int ShippersId { get; set; }
        public int LicenseId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string CompanyName { get; set; }
        public string Vatnumber { get; set; }
        public string CompanyNumber { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string PostTown { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string WebAddress { get; set; }
        public string Code { get; set; }

        public virtual License License { get; set; }
        public virtual ICollection<Batch> Batch { get; set; }
        public virtual ICollection<BatchLabel> BatchLabel { get; set; }
        public virtual ICollection<Senders> Senders { get; set; }
    }
}
