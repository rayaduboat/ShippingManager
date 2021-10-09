using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class Recipients
    {
        public Recipients()
        {
            TripDetails = new HashSet<TripDetails>();
        }

        public int RecipientId { get; set; }
        public int SenderId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string PostCode { get; set; }
        public string PostTown { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }

        public virtual Senders Sender { get; set; }
        public virtual ICollection<TripDetails> TripDetails { get; set; }
    }
}
