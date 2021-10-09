using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinanceManager.Model.Models;
//using RabantFinanceManager.Data;
using FinanceManager.Repository;
using FinanceManager.Model.Abstract;
using Microsoft.AspNetCore.Authorization;
using System.Xml.Linq;

namespace RabantFinanceManager.Controllers
{
    [Authorize]
    public class ExpendituresController : Controller
    {
        private readonly IExpenditureRepository _repository;

        public ExpendituresController(IExpenditureRepository repository)
        {
            _repository = repository;
        }

        // GET: Expenditures
        public IActionResult Index()
        {
            return View(_repository.GetAllExpenditure());
        }

        public IActionResult MainExpenditurePanel()
        {
            ViewBag.ShowTopBar = true;
            return View();
        }

        // GET: Expenditures/Details/5
        ////public async Task<IActionResult> Details(int? id)
        ////{
        ////    if (id == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    var expenditure = await _context.Expenditure
        ////        .Include(e => e.Batch)
        ////        .FirstOrDefaultAsync(m => m.ExpenditureId == id);
        ////    if (expenditure == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    return View(expenditure);
        ////}

        //GET: Expenditures/Create
        ////[HttpGet]
        public IActionResult Create()
        {
            var selectItems = _repository.GetExpenseSelectList();

            return View(selectItems);
        }

        //POST: Expenditures/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public IActionResult Create(ExpenditureCreateModel expenditure)
        {
            var selectItems = _repository.GetExpenseSelectList();
            string msg = "";
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.AddExpenditure(expenditure);
                    msg = "Added Successful!";
                    
                    return RedirectToAction("Create");
                }
            }
            catch (Exception ex)
            {
                msg=ex.Message;
            }
            return View(msg);
        }


        // GET: Expenditures/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var selectItems = _repository.GetExpenseSelectList();
            //return View(selectItems);

            var expenseToEdit = _repository.GetExpenditureById(id);
            ExpenditureCreateModel ex = new ExpenditureCreateModel()
            {
                ExpenditureId = expenseToEdit.ExpenditureId,
                BatchId = expenseToEdit.BatchId,
                Name = expenseToEdit.Name,
                Description = expenseToEdit.Description,
                ModeOfPayment = expenseToEdit.ModeOfPayment,
                Amount = expenseToEdit.Amount,
                CreatedDate = expenseToEdit.CreatedDate,
                ExpBatches = selectItems.ExpBatches,
                ExpDescription = selectItems.ExpDescription,
                ExpNames = selectItems.ExpNames,
                ExpPayMode = selectItems.ExpPayMode
            };
            return View(ex);
        }

        // POST: Expenditures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Expenditure expenditure)
        {
            if (id != expenditure.ExpenditureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdateExpenditure(expenditure);
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
                return RedirectToAction("Index", expenditure);
            }
            return View(expenditure);
        }

        // GET: Expenditures/Delete/5
        public IActionResult Delete(int id)
        {
            var expenditure = _repository.GetExpenditureById(id);
            if (expenditure == null)
            {
                return NotFound();
            }
            return View(expenditure);
        }
        public JsonResult GetDescription(string data)
        {
            ExpenditureCreateModel eModel = new ExpenditureCreateModel();
            var ActualData = data.Substring(1, data.Length - 2);
            var result = _repository.GetExpenseDescriptionByItemName(ActualData);
            return Json(result);
        }

        // POST: Expenditures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Expenditure expenditure)
        {
            _repository.DeleteExpenditure(expenditure);
            return RedirectToAction("Index");
        }

        //[AcceptVerbs("Get","Post")]
        //public ActionResult PostExpenditure(Expenditure expenditure)
        //{
        //    var selectItems = _repository.GetExpenseSelectList();

        //    string msg = "";
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _repository.AddExpenditure(expenditure);
        //            msg = "Added Successful!";
        //            ViewBag.msg = msg;
        //            return RedirectToAction("PostExpenditure");
        //        }
        //        //else
        //        //{
        //        //    var selectItems = _repository.GetExpenseSelectList();
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = ex.Message;
        //    }
        //    return View(msg);
        //}
    }
}
