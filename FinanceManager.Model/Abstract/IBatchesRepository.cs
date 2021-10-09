using FinanceManager.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Model.Abstract
{
    public interface IBatchesRepository
    {
        string AddNewBatch(int shippersId,string Newcode);
       Batch UpdateBatch();
    }
}
