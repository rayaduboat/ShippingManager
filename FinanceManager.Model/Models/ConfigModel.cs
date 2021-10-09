using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Models
{
   public class ConfigModel: Senders
    {
        public IEnumerable<SelectListItem> UkTown { get; set; }
        //public IEnumerable<SelectListItem> ExpDescription { get; set; }
    }
}
