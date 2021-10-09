using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinanceManager.Model.Models;
using FinanceManager.Repository;
using FinanceManager.Model.Abstract;
using FinanceManager.Model.DataTransferModel;
using Nancy.Json;
using Microsoft.AspNetCore.Authorization;

namespace RabantFinanceManager.Controllers
{
    [Authorize]
    public class BatchesController : Controller
    {
        static JavaScriptSerializer sz = new JavaScriptSerializer();
        private readonly IBatchesRepository _repository;
        private readonly FinanceManagerDbContext _context;

        public BatchesController(FinanceManagerDbContext context, IBatchesRepository repository)
        {
            _repository = repository;
            _context = context;

        }

        // GET: Batches
        public async Task<IActionResult> Index()
        {
            var financeManagerDbContext = _context.Batch.Include(b => b.Shippers);
            return View(await financeManagerDbContext.ToListAsync());
        }
        public IActionResult MainBatchesPanel()
        {
            ViewBag.ShowTopBar = true;
            return View();
        }
        // GET: Batches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batch = await _context.Batch
                .Include(b => b.Shippers)
                .FirstOrDefaultAsync(m => m.BatchId == id);
            if (batch == null)
            {
                return NotFound();
            }

            return View(batch);
        }

        // GET: Batches/Create
        public IActionResult Create()
        {
            string loginUser = User.Identity.Name;
            ViewBag.Shippers = _context.Shippers.Where(i=>i.EmailAddress==loginUser).Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName.ToString(),
                Value = x.ShippersId.ToString()
            });
            return View();
        }

        // POST: Batches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Batch batch)
        {
            if (ModelState.IsValid)
            {
                //batch.IsSelected = true;
                _context.Add(batch);
                await _context.SaveChangesAsync();
                _repository.UpdateBatch();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShippersId"] = new SelectList(_context.Shippers, "Id", "Vatnumber", batch.ShippersId);
            return View(batch);
        }
        [HttpGet]
        public JsonResult GetNewBatch(ShipperID obj)
        {
            string message = "Failed Creating Batch";
            int shipID = int.Parse(obj.id.ToString());
            var shipCode = _context.Shippers.Where(u => u.ShippersId == shipID).Select(i => i.Code).FirstOrDefault();
            var result=_repository.AddNewBatch(shipID,shipCode);

            message = sz.Serialize(new
            {
                message = "Batch Created Successfully",
                newBatch = result
            });
            return Json(message);
        }
        // GET: Batches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batch = await _context.Batch.FindAsync(id);
            if (batch == null)
            {
                return NotFound();
            }

            ViewBag.Shippers = _context.Shippers.Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName.ToString(),
                Value = x.ShippersId.ToString()
            });
            //ViewData["ShippersId"] = new SelectList(_context.Shippers, "Id", "Vatnumber", batch.ShippersId);
            return View(batch);
        }

        // POST: Batches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BatchId,CreatedOnDate,CreatedBy,ShippersId,ActualBatch,IsSelected")] Batch batch)
        {
            if (id != batch.BatchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(batch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BatchExists(batch.BatchId))
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
            ViewData["ShippersId"] = new SelectList(_context.Shippers, "Id", "Vatnumber", batch.ShippersId);
            return View(batch);
        }

        // GET: Batches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batch = await _context.Batch
                .Include(b => b.Shippers)
                .FirstOrDefaultAsync(m => m.BatchId == id);
            if (batch == null)
            {
                return NotFound();
            }

            return View(batch);
        }

        // POST: Batches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var batch = await _context.Batch.FindAsync(id);
            _context.Batch.Remove(batch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BatchExists(int id)
        {
            return _context.Batch.Any(e => e.BatchId == id);
        }
    }
}
