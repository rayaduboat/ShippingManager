using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FinanceManager.Model.Models
{
    public class SendersCreateModel:Senders
    {
        public IEnumerable<Senders> AllSenders { get; set; }
        public IEnumerable<SelectListItem> SenderTitles { get; set; }
        public IEnumerable<SelectListItem> SenderFullName { get; set; }
        public IEnumerable<SelectListItem> SenderTown { get; set; }
        public IEnumerable<SelectListItem> ExpBatches { get; set; }
        public IEnumerable<SelectListItem> UkTown { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation password do not match")]
        public string ConfirmPassword { get; set; }
        //public string ConfirmPassword { get; set; }
    }

}
