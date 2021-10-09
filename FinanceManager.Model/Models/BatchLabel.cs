using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class BatchLabel
    {
        public int BatchLabelId { get; set; }
        public int ShippersId { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public string LabelCode { get; set; }

        public virtual Shippers Shippers { get; set; }
    }
}
