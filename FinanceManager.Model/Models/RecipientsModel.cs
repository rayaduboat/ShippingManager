using FinanceManager.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RabantFinanceManager.Models
{
    public class RecipientsModel
    {
       
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
            //public Recipient()
            //{
            //    this.TripDetails = new HashSet<TripDetail>();
            //}

            public int RecipientID { get; set; }
            public int SenderID { get; set; }
            [Required(ErrorMessage = "Select a Title")]
            public string Title { get; set; }
            [Required(ErrorMessage = "Enter firstname")]
            public string FirstName { get; set; }
            //[Required(ErrorMessage = "Enter lastname")]
            public string FullName { get; set; }
            public string RecipientDetails { get; set; }
            [Required(ErrorMessage = "Enter Lastname")]
            public string LastName { get; set; }
            [Required(ErrorMessage = "Enter AddressOne")]
            public string AddressLineOne { get; set; }
            [Required(ErrorMessage = "Enter AddressTwo")]
            public string AddressLineTwo { get; set; }
            [Required(ErrorMessage = "Enter Postcode")]
            public string PostCode { get; set; }
            [Required(ErrorMessage = "Enter Post Town")]
            public string PostTown { get; set; }

            public string County { get; set; }
            public string Country { get; set; }
            [Required(ErrorMessage = "Enter Telephone")]
            public string Telephone { get; set; }
            public string EmailAddress { get; set; }
            public string MySender { get; set; }

            public virtual Senders Sender { get; set; }
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public virtual ICollection<TripDetails> TripDetails { get; set; }
        }
    }

