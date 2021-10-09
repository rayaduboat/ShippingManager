using FinanceManager.Model.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceManager.Repository
{
   public class ConfigRepository:IConfigRepository
    {
        private FinanceManagerDbContext _context;

        public ConfigRepository(FinanceManagerDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetUKtowns()
        {
            var UkTowns = _context.Ukcity.ToList();
            var getUkTowns = UkTowns.Select(x => new SelectListItem
            {
                Text = x.City,
                Value = x.City
            });

            return getUkTowns;
        }
    }
}
