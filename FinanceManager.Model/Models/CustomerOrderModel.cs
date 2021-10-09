using FinanceManager.Model.DataTransferModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Models
{
    public class CustomerOrderModel:TripDetails
    {
        //public int RecipientId { get; set; }
        //public int SenderId { get; set; }
        public string Telephone { get; set; }
        public string Postcode { get; set; }
        public List<ItemModel> ItemList { get; set; }
    }
}
