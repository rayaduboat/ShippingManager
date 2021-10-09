using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Models
{
    public class ExpenditureEditView: Expenditure
    {
        public IEnumerable<SelectListItem> ExpNames { get; set; }
        public IEnumerable<SelectListItem> ExpDescription { get; set; }
        public IEnumerable<SelectListItem> ExpPayMode { get; set; }
        public IEnumerable<SelectListItem> ExpBatches { get; set; }
        public string ActualRef { get; set; }
    }
}
