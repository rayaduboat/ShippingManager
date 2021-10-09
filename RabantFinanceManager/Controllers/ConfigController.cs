using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using FinanceManager.Model.Abstract;
using FinanceManager.Model.Models;
using FinanceManager.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;

namespace RabantFinanceManager.Controllers
{
    [Authorize]
    public class ConfigController : Controller
    {
        static JavaScriptSerializer sz = new JavaScriptSerializer();
        private IConfigRepository _repository;
        private ISendersRepository _sRepository;
        private FinanceManagerDbContext _db;

        public ConfigController(IConfigRepository repository, ISendersRepository sRepository, FinanceManagerDbContext db)
        {
            _repository = repository;
            _sRepository = sRepository;
            _db = db;
        }
        // GET: Config
        public ActionResult Customers()
        {
            SendersCreateModel cm = new SendersCreateModel();
           // ConfigModel cm = new ConfigModel();
            cm.UkTown = _repository.GetUKtowns();
            return View(cm);
        }
        public IActionResult MainConfigPanel()
        {
            ViewBag.ShowTopBar = true;
            return View();
        }
        public IActionResult MainFinancePanel()
        {
            ViewBag.ShowTopBar = true;
            return View();
        }
        // GET: Config/Details/5  
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Config/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Config/Create
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

        // GET: Config/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Config/Edit/5
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

        // GET: Config/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Config/Delete/5
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