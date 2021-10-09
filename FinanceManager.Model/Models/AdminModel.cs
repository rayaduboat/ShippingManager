using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Models
{
    public class AdminModel:TripDetails
    {
        public IEnumerable<SelectListItem> ItemNames { get; set; }
        public int SenderID { get; set; }
        public int SenderName { get; set; }
        public int SenderTelephone { get; set; }
        public int RecipientID { get; set; }
        public int RecipientName { get; set; }
        public int RecipientTelephone { get; set; }
        public int BatchID { get; set; }
        public int ItemID { get; set; }
        public int ItemName { get; set; }
        public IEnumerable<TripDetails> TripList { get; set; }
    }
}

