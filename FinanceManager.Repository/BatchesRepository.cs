using FinanceManager.Model.Abstract;
using FinanceManager.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceManager.Repository
{
    public class BatchesRepository : IBatchesRepository
    {
        private FinanceManagerDbContext _context;

        public BatchesRepository(FinanceManagerDbContext context)
        {
            _context = context;
        }
        public string AddNewBatch(int shippersid, string Code)
        {
            int rounds = 0;
            string message = "";
            string ExistingCode = "";
            string NewBatch = "";
            int BatchCount = 0;
            Batch b = new Batch();
            string PrevDateStr = "";
            var lastActualBatchId = _context.Batch.Where(u=>u.ShippersId==shippersid).OrderByDescending(x => x.BatchId).Select(i => i).FirstOrDefault();
            if (lastActualBatchId != null)
            {
                PrevDateStr = lastActualBatchId.ActualBatch.Split('-')[1];
                
                if (PrevDateStr == DateTime.Today.ToString("MMMyyyy").ToUpper())
                {
                    BatchCount = int.Parse(lastActualBatchId.ActualBatch.Split('-')[2]);
                    rounds = BatchCount + 1;
                    NewBatch = Code.ToUpper().Trim() + "-" + DateTime.Today.ToString("MMMyyyy").ToUpper() + "-" + rounds;
                }
                else
                {
                    rounds = BatchCount + 1;
                    NewBatch = Code.ToUpper().Trim() + "-" + DateTime.Today.ToString("MMMyyyy").ToUpper() + "-" + rounds;
                }
               

            }
            else
            {
                rounds = BatchCount + 1;
                NewBatch = Code.ToUpper().Trim() + "-" + DateTime.Today.ToString("MMMyyyy").ToUpper() + "-" + rounds;
                message = "No Existing Batch!";
            }

            return NewBatch;
        }
        public Batch UpdateBatch()
        {
            var currentBatch = GetCurrentBatch();
            currentBatch.ActualBatch = currentBatch.ActualBatch + "-" + currentBatch.BatchId;

            var batchToUpdate = _context.Batch.Attach(currentBatch);
            batchToUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            //_context.TripDetails.ToList()

            return currentBatch;
        }

        public Batch  GetCurrentBatch()
        {
            var currentBatch = _context.Batch.OrderByDescending(x => x.BatchId).FirstOrDefault();

            return currentBatch;
        }
    }
}
