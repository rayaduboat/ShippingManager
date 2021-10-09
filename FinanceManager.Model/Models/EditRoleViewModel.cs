﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FinanceManager.Model.Models
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
             Users = new List<string>();
        }
        public string RoleId { get; set; }
        [Required(ErrorMessage ="Role name is required")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}
