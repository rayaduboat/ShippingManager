using FinanceManager.Model.Abstract;
using FinanceManager.Model.Models;
using FinanceManager.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabantFinanceManager.Controllers
{
    public class ItemsController : Controller
    {
        private readonly FinanceManagerDbContext _context;
        private IItemsRepository _repository;

        public ItemsController(IItemsRepository repository)
        {
            _repository = repository;
        }
        // GET: ItemsController
        public ActionResult Index()
        {
            var itemsData = _repository.GetAllShippingItems();
            return View(itemsData);
        }
        public IActionResult MainItemsPanel()
        {
            ViewBag.ShowTopBar = true;
            return View();
        }
        // GET: ItemsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ItemsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Items shippingItem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  var itemData=  _repository.AddShippingItem(shippingItem);
                    return RedirectToAction("Index", itemData);
                }
                else
                {

                }
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
                return View("GeneralError", msg);
            }

            return View();
        }

        // GET: ItemsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_repository.GetShippingItemById(id));
        }

        // POST: ItemsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Items shippingItem)
        {
            if (ModelState.IsValid)
            {
                 try
            {
              var updatedItem=  _repository.UpdateShippingItem(shippingItem);
                return RedirectToAction(nameof(Index),updatedItem);
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
                return View("GeneralError",msg);
            }
            }
            return View(shippingItem);
        }


        // GET: ItemsController/Delete/5
        public ActionResult Delete(int id)
        {
            var shippingItem = _repository.GetShippingItemById(id);
            if (shippingItem == null)
            {
                return NotFound();
            }
            return View(shippingItem);
        }

        // POST: ItemsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Items shippingItem)
        {
            var item = _repository.DeleteItem(shippingItem);
            return RedirectToAction("Index");
        }
    }
}
