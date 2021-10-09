using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class ClientUsage
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public string Title { get; set; }
        public string SendName { get; set; }
        public string RecName { get; set; }
        public string SendTown { get; set; }
        public string RecTown { get; set; }
        public int? NumOfTrips { get; set; }
        public int? NumOfRecipients { get; set; }
    }
}
