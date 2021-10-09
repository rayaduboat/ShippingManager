using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Abstract
{
    public interface IConfigRepository
    {
        IEnumerable<SelectListItem> GetUKtowns();
    }
}
