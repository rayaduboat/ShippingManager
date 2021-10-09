using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceManager.Model.Models
{
    public class ExpenditureCreateModel:Expenditure
    {
        public IEnumerable<SelectListItem> ExpNames { get; set; }
        public IEnumerable<SelectListItem> ExpDescription { get; set; }
        public IEnumerable<SelectListItem> ExpPayMode { get; set; }
        public IEnumerable<SelectListItem> ExpBatches { get; set; }
        public string ActualRef { get; set; }
        public string SelectedSender { get; set; }
        public IEnumerable<SelectListItem> ItemDescription { get; set; }
    }
}
