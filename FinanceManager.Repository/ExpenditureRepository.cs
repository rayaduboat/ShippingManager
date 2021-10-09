using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
//using System.Web.Mvc;
using FinanceManager.Model.Abstract;
using FinanceManager.Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace FinanceManager.Repository
{
    public class ExpenditureRepository : IExpenditureRepository
    {
        private FinanceManagerDbContext _context;
        private readonly IDbConnection db;

        public ExpenditureRepository(FinanceManagerDbContext context)
        {
            _context = context;
        }
        ////public ExpenditureRepository(IDbConnections context)
        ////{
        ////    this.db = context;
        ////}
        public Expenditure AddExpenditure(Expenditure expenditure)
        {
            //string sqlstring = "Insert into Expenditure(,,,) Values('@name','@value1')";
            //SqlParameter[] param = new SqlParameter[]
            //{
            //    new SqlParameter("@name",expenditure.Name),
            //    new SqlParameter("@name",expenditure.Name),
            //    new SqlParameter("@name",expenditure.Name),
            //};

            //db.Add(sqlstring, param);
            _context.Add(expenditure);
            _context.SaveChanges();
            return expenditure;
        }

        public IEnumerable<Expenditure> GetAllExpenditure()
        {
          //var list=  db.Execute<Expenditure>("Select * from Expenditure");
          return  _context.Expenditure;
        }

        public Expenditure GetExpenditureById(int id)
        {
            //var expense=  db.Execute<Expenditure>("Select * from Expenditure","@Id",id.ToString()).FirstOrDefault();
            return _context.Expenditure.FirstOrDefault(x => x.ExpenditureId == id);
        }
        public IEnumerable<SelectListItem> GetExpBatches()
        {
            var data = _context.Batch.OrderByDescending(u => u.BatchId);
           var expBatch= data.Select(x=> new SelectListItem {
                Text=x.ActualBatch,
                Value=x.BatchId.ToString()
            });

            return expBatch;
        }

        public Expenditure UpdateExpenditure(Expenditure updateExpenditure)
        {
            var expToUpdate = _context.Expenditure.Attach(updateExpenditure);
            expToUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return updateExpenditure;
        }
        public ExpenditureCreateModel GetExpenseSelectList()
        {
           ExpenditureCreateModel ecv = new ExpenditureCreateModel();

            ecv.ExpNames = GetExpNames();
            ecv.ExpDescription = GetExpDescription();
            ecv.ExpPayMode = GetPayMode();
            ecv.ExpBatches = GetExpBatches();
            return ecv;
        }
        public IEnumerable<SelectListItem> GetExpNames()
        {
            var expItem = _context.ExpenseItems.Distinct();
            var ExpNames = expItem.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name
            }).Distinct();
            return ExpNames;
        }
        public IEnumerable<SelectListItem> GetExpDescription()
        {
            var expItem = _context.ExpenseItems;
            var ExpDescription = expItem.Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = x.Description
            });
            return ExpDescription;
        }
        public IEnumerable<SelectListItem> GetPayMode()
        {
            var expItem = _context.PayMode;
            var ExpNames = expItem.Select(x => new SelectListItem
            {
                Text = x.Paymode1,
                Value = x.Paymode1
            });
            return ExpNames;
        }
        public Expenditure DeleteExpenditure(Expenditure expenditure)
        {
            string msg = "";
            var expenseToDelete = _context.Expenditure.Find(expenditure.ExpenditureId);
            if (expenseToDelete !=null)
            {
                _context.Remove(expenseToDelete);
                _context.SaveChanges();
                msg = "Expense Deleted Successfully!!";
            }
            return expenseToDelete;
        }

        public IEnumerable<SelectListItem> GetExpenseDescriptionByItemName(string itemName)
        {
            ExpenditureCreateModel ecm = new ExpenditureCreateModel();
            var data = _context.ExpenseItems.Where(u => u.Name == itemName).Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = x.Description
            });
            return data; //Json(myDescription, JsonRequestBehavior.AllowGet);
        }
    }
}
