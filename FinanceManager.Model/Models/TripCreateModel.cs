using FinanceManager.Model.DataTransferModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Models
{
    public class TripCreateModel:TripDetails
    {
        public IEnumerable<SelectListItem> RecipientsFullName { get; set; }
        public IEnumerable<SelectListItem> SendersFullName { get; set; }
        public string SenderFullName { get; set; }
        public IEnumerable<SelectListItem> SenderTown { get; set; }
        public IEnumerable<SelectListItem> TripBatches { get; set; }
        public IEnumerable<SelectListItem> ShippingItemNames { get; set; }
        public IEnumerable<Items> AllItems { get; set; }
        public IEnumerable<SelectListItem> SenderRecipientsFullName { get; set; }
        public int NewRef { get; set; }
        public string Telephone { get; set; }
        public string Postcode { get; set; }
        public List<ItemModel> ItemList { get; set; }
        //public string RecipientFullName { get; set; }
    }
}
