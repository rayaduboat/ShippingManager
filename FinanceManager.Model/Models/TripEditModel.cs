using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Models
{
    public class TripEditModel: TripDetails
    {
        public IEnumerable<SelectListItem> RecipientsFullName { get; set; }
        public IEnumerable<SelectListItem> SendersFullName { get; set; }
        public IEnumerable<SelectListItem> SenderTown { get; set; }
        public IEnumerable<SelectListItem> ExpBatches { get; set; }
        public IEnumerable<TripDetails> AllTrips { get; set; }
        public TripDetails TripById { get; set; }
        public string SelectedSenderFullName { get; set; }
        public string SelectedRecipientFullName { get; set; }

        public IEnumerable<SelectListItem> ItemNames { get; set; }
        public int SenderID { get; set; }
        public int RecipientID { get; set; }
        public int BatchID { get; set; }
        public IEnumerable<TripDetails> TripList { get; set; }
        public Senders loginUserDetails { get; set; }
        public Shippers loginshipperDetails { get; set; }
        //public string RecipientFullName { get; set; }
    }
    public class UserLoginDetails 
    {
        public string email { get; set; }
        public string telephone { get; set; }
        public string postcode { get; set; }
    }

    }
