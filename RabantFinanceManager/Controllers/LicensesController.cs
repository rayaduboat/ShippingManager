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

namespace RabantFinanceManager.Controllers
{
    public class LicensesController : Controller
    {
        private readonly FinanceManagerDbContext _context;
        private readonly ILicensesRepository _repository;

        public LicensesController(FinanceManagerDbContext context, ILicensesRepository repository)
        {
            _context = context;
            _repository = repository;
        }
        public IActionResult MainLicensePanel()
        {
            ViewBag.ShowTopBar = true;
            return View();
        }
        // GET: Licenses
        public async Task<IActionResult> Index()
        {
            return View(await _context.License.ToListAsync());
        }

        // GET: Licenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var license = await _context.License
                .FirstOrDefaultAsync(m => m.LicenseId == id);
            if (license == null)
            {
                return NotFound();
            }

            return View(license);
        }

        // GET: Licenses/Create
        public IActionResult Create()
        {
            ViewBag.PreLicense = DateTime.Today.ToString("yyyyMMdd").Replace("/", "").Replace(":", "").Trim();
            return View();
        }

        // POST: Licenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(License license)
        {
            string msg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var isLicenseExist = _repository.GetLicenseByEmail(license.UserEmailAddress);
                    if (isLicenseExist != null)
                    {
                        msg = "User has License already!";
                        ViewBag.message = msg;
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        _context.Add(license);
                        await _context.SaveChangesAsync();
                        var currentLicense =  _repository.GetCurrentLicense();//_context.License.OrderByDescending(x => x.LicenseId).FirstOrDefault();//
                        currentLicense.LicenseNumber = currentLicense.LicenseId + "-" + GetKey() + "-" + currentLicense.LicenseNumber;
                        var ActuallicenseToUpdate = _context.License.Attach(currentLicense);
                        ActuallicenseToUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();
                        msg = "Successful!";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    msg=ex.Message;
                }

            }
            return View(license);
        }
        public string GetKey()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            return GuidString;
        }
        // GET: Licenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var license = await _context.License.FindAsync(id);
            if (license == null)
            {
                return NotFound();
            }
            return View(license);
        }

        // POST: Licenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LicenseId,LicenseNumber,UserEmailAddress,LicenceStartDate,LicenceEndDate,NumberOfUsers")] License license)
        {
            if (id != license.LicenseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(license);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LicenseExists(license.LicenseId))
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
            return View(license);
        }

        // GET: Licenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var license = await _context.License
                .FirstOrDefaultAsync(m => m.LicenseId == id);
            if (license == null)
            {
                return NotFound();
            }

            return View(license);
        }

        // POST: Licenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var license = await _context.License.FindAsync(id);
            _context.License.Remove(license);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LicenseExists(int id)
        {
            return _context.License.Any(e => e.LicenseId == id);
        }
    }
}
