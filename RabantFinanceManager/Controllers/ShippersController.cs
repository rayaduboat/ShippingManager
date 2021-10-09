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
    public class ShippersController : Controller
    {
        private readonly FinanceManagerDbContext _context;

        public ShippersController(FinanceManagerDbContext context)
        {
            _context = context;
        }

        // GET: Shippers
        public async Task<IActionResult> Index()
        {
            var financeManagerDbContext = _context.Shippers.Include(s => s.License);
            return View(await financeManagerDbContext.ToListAsync());
        }
        public IActionResult MainShippersPanel()
        {
            ViewBag.ShowTopBar = true;
            return View();
        }
        // GET: Shippers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippers = await _context.Shippers
                .Include(s => s.License)
                .FirstOrDefaultAsync(m => m.ShippersId == id);
            if (shippers == null)
            {
                return NotFound();
            }

            return View(shippers);
        }

        // GET: Shippers/Create
        public IActionResult Create()
        {
            ViewData["LicenseId"] = new SelectList(_context.License, "LicenseId", "LicenseNumber");
            return View();
        }

        // POST: Shippers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LicenseId,Title,FirstName,LastName,Dob,CompanyName,Vatnumber,CompanyNumber,AddressLineOne,AddressLineTwo,PostTown,County,PostCode,Country,Telephone,EmailAddress,Password,WebAddress")] Shippers shippers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shippers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LicenseId"] = new SelectList(_context.License, "LicenseId", "LicenseNumber", shippers.LicenseId);
            return View(shippers);
        }

        // GET: Shippers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippers = await _context.Shippers.FindAsync(id);
            if (shippers == null)
            {
                return NotFound();
            }
            ViewData["LicenseId"] = new SelectList(_context.License, "LicenseId", "LicenseNumber", shippers.LicenseId);
            return View(shippers);
        }

        // POST: Shippers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LicenseId,Title,FirstName,LastName,Dob,CompanyName,Vatnumber,CompanyNumber,AddressLineOne,AddressLineTwo,PostTown,County,PostCode,Country,Telephone,EmailAddress,Password,WebAddress")] Shippers shippers)
        {
            if (id != shippers.ShippersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shippers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippersExists(shippers.ShippersId))
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
            ViewData["LicenseId"] = new SelectList(_context.License, "LicenseId", "LicenseNumber", shippers.LicenseId);
            return View(shippers);
        }

        // GET: Shippers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippers = await _context.Shippers
                .Include(s => s.License)
                .FirstOrDefaultAsync(m => m.ShippersId == id);
            if (shippers == null)
            {
                return NotFound();
            }

            return View(shippers);
        }

        // POST: Shippers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shippers = await _context.Shippers.FindAsync(id);
            _context.Shippers.Remove(shippers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippersExists(int id)
        {
            return _context.Shippers.Any(e => e.ShippersId == id);
        }
    }
}
