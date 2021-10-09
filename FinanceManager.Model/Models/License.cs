using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class License
    {
        public License()
        {
            Shippers = new HashSet<Shippers>();
        }

        public int LicenseId { get; set; }
        public string LicenseNumber { get; set; }
        public string UserEmailAddress { get; set; }
        public DateTime LicenceStartDate { get; set; }
        public DateTime LicenceEndDate { get; set; }
        public int NumberOfUsers { get; set; }

        public virtual ICollection<Shippers> Shippers { get; set; }
    }
}
