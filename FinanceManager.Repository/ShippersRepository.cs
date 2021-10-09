using FinanceManager.Model.Abstract;
using FinanceManager.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceManager.Repository
{
    public class ShippersRepository:IShippersRepository
    {
        private FinanceManagerDbContext _context;

        public ShippersRepository(FinanceManagerDbContext context)
        {
            _context = context;
        }
        public int GetShipperId()
        {
            var shipperId = _context.Shippers.Select(x=>x.ShippersId).FirstOrDefault();
            return shipperId;
        }
        public Shippers GetShippersIdByEmail(string email)
        {
            var shipper= _context.Shippers.Where(i => i.EmailAddress == email).FirstOrDefault();
            return shipper;
        }
    }
}
