using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FinanceManager.Model.Models
{
   public class RegisterViewModel:Users
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password",ErrorMessage ="Password and Confirmation password do not match")]
        public string ConfirmPassword { get; set; }
        public Users Users { get; set; }
        public IEnumerable<SelectListItem> Town { get; set; }
    }
}
