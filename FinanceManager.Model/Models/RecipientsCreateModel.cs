using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Models
{
    public class RecipientsCreateModel : Recipients
    {
        public IEnumerable<SelectListItem> RecipientTitles { get; set; }
        public IEnumerable<SelectListItem> RecipientSender { get; set; }
        public IEnumerable<SelectListItem> RecipientTown { get; set; }
        public IEnumerable<SelectListItem> ExpBatches { get; set; }
        public IEnumerable<Recipients> AllRecipients { get; set; }
        public string SelectedSenderFullName { get; set; }
        public string RecMsg { get; set; }
    }
}
