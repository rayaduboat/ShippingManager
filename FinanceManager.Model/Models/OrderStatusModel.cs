using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Models
{
    public class OrderStatusModel:LinkCreator
    {
        public string oStatus { get; set; }
        public string lStatus { get; set; }
        public DateTime orderDate { get; set; }
        public string RefNumber { get; set; }
        public int BatchId { get; set; }
        public int recipientId { get; set; }
        public int senderId { get; set; }
        public int Quantity { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string status { get; set; }
        public decimal? Total { get; set; }

    }
}
