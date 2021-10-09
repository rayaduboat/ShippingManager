using FinanceManager.Model.Abstract;
using FinanceManager.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceManager.Repository
{
    public class LicensesRepository:ILicensesRepository
    {
        private FinanceManagerDbContext _context;

        public LicensesRepository(FinanceManagerDbContext context)
        {
            _context = context;
        }
        public License UpdateLicense(License licenseToUpdate)
        {

            return licenseToUpdate;
        }
        public License GetLicenseByEmail(string shipperEmail)
        {
            return _context.License.Where(i=>i.UserEmailAddress==shipperEmail).FirstOrDefault();
        }
        public License GetCurrentLicense()
        {
           return _context.License.OrderByDescending(x => x.LicenseId).FirstOrDefault();
        }
    }
}
