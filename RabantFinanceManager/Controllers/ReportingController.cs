using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RabantFinanceManager.Controllers
{
    public class ReportingController : Controller
    {
        // GET: Reporting
        public ActionResult Index()
        {
            return View();
        }
        public IActionResult MainReportingPanel()
        {
            ViewBag.ShowTopBar = true;
            return View();
        }
        // GET: Reporting/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reporting/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reporting/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Reporting/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reporting/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Reporting/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reporting/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}