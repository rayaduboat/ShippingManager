using FinanceManager.Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Abstract
{
    public interface IExpenditureRepository
    {
        Expenditure AddExpenditure(Expenditure expenditure);
        IEnumerable<Expenditure> GetAllExpenditure();
        Expenditure UpdateExpenditure(Expenditure updateExpenditure);
        ExpenditureCreateModel GetExpenseSelectList();
        Expenditure GetExpenditureById(int id);
        IEnumerable<SelectListItem> GetExpenseDescriptionByItemName(string itemName);
        Expenditure DeleteExpenditure(Expenditure expenditure);
    }
}
