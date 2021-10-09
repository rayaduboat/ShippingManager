using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Model.Models
{
    public partial class Senders
    {
        public Senders()
        {
            Recipients = new HashSet<Recipients>();
            TripDetails = new HashSet<TripDetails>();
        }
        //public int Id { get; set; }
        [Key]
        public int SenderId { get; set; }
        public int ShippersId { get; set; }
        public string Title { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        [Required]
        public string PostCode { get; set; }
        public string PostTown { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public virtual Shippers Shippers { get; set; }
        public virtual ICollection<Recipients> Recipients { get; set; }
        public virtual ICollection<TripDetails> TripDetails { get; set; }
    }
}
