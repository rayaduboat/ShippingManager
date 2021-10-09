using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FinanceManager.Model.Models
{
   public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
