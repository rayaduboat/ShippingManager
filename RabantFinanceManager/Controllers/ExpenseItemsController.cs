using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinanceManager.Model.Models;
using FinanceManager.Repository;

namespace RabantFinanceManager.Controllers
{
    public class ExpenseItemsController : Controller
    {
        private readonly FinanceManagerDbContext _context;

        public ExpenseItemsController(FinanceManagerDbContext context)
        {
            _context = context;
        }

        // GET: ExpenseItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExpenseItems.ToListAsync());
        }

        // GET: ExpenseItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseItems = await _context.ExpenseItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseItems == null)
            {
                return NotFound();
            }

            return View(expenseItems);
        }

        // GET: ExpenseItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExpenseItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,IsSelected")] ExpenseItems expenseItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenseItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenseItems);
        }

        // GET: ExpenseItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseItems = await _context.ExpenseItems.FindAsync(id);
            if (expenseItems == null)
            {
                return NotFound();
            }
            return View(expenseItems);
        }

        // POST: ExpenseItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,IsSelected")] ExpenseItems expenseItems)
        {
            if (id != expenseItems.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenseItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseItemsExists(expenseItems.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(expenseItems);
        }

        // GET: ExpenseItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseItems = await _context.ExpenseItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseItems == null)
            {
                return NotFound();
            }

            return View(expenseItems);
        }

        // POST: ExpenseItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expenseItems = await _context.ExpenseItems.FindAsync(id);
            _context.ExpenseItems.Remove(expenseItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseItemsExists(int id)
        {
            return _context.ExpenseItems.Any(e => e.Id == id);
        }
    }
}
