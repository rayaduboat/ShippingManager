using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Model.Models
{
    public partial class Users
    {
        public int UserId { get; set; }
        public int ShippersId { get; set; }
        public string Title { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string PostCode { get; set; }
        [Required]
        public string PostTown { get; set; }
        public string Country { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        //public int UserId { get; set; }
        //public int ShippersId { get; set; }
        //public string Title { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Gender { get; set; }
        //public string AddressLineOne { get; set; }
        //public string AddressLineTwo { get; set; }
        //public string PostCode { get; set; }
        //public string PostTown { get; set; }
        //public string Country { get; set; }
        //public string Telephone { get; set; }
        //public string EmailAddress { get; set; }
        //public string Role { get; set; }
        //public string Password { get; set; }
    }
}
