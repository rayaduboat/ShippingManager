using FinanceManager.Model.Models;
using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Text;

namespace FinanceManager.Model.Abstract
{
    public interface ILicensesRepository
    {
        License GetLicenseByEmail(string shipperEmail);
        License UpdateLicense(License licenseToUpdate);
        License GetCurrentLicense();
    }
}
