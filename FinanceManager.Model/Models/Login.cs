using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class Login
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string HashedPassword { get; set; }
        public string PasswordSalt { get; set; }
        public string Role { get; set; }
    }
}
