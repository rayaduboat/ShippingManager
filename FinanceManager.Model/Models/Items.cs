using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class Items
    {
        public Items()
        {
            TripDetails = new HashSet<TripDetails>();
        }

        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string Colour { get; set; }
        public decimal? UnitCost { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Specifications { get; set; }
        public string Condition { get; set; }
        public string Make { get; set; }

        public virtual ICollection<TripDetails> TripDetails { get; set; }
    }
}
