using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class LinkCreator1
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int SenderId { get; set; }
        public int BatchId { get; set; }
        public int ShippersId { get; set; }
        public string HashString { get; set; }
        public string ActualString { get; set; }
        public string LinkStatus { get; set; }
        public string ExpiryFlag { get; set; }
        public string ActualLink { get; set; }
    }
}
