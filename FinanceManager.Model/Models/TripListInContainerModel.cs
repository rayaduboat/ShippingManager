using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Models
{
    public class TripListInContainerModel: TripDetails
    {
        public IEnumerable<SelectListItem> BatchList { get; set; }
        public List <TripDetails> tripdata { get; set; }
    }
}
