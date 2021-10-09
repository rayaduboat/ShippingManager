using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class LinkCreator
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int SenderId { get; set; }
        public int BatchId { get; set; }
        public int RefNum { get; set; }
        public string UrlLink { get; set; }
        public string UniqueString { get; set; }
        public int? OrderStatus { get; set; }
        public int? LinkStatus { get; set; }
    }
}
