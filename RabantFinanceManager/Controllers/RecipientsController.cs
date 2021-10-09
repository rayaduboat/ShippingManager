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
using Microsoft.AspNetCore.Authorization;
namespace RabantFinanceManager.Controllers
{
    [Authorize]
    public class RecipientsController : Controller
    {
        private readonly FinanceManagerDbContext _context;
        private IRecipientsRepository _repository;

        //private string _all;

        public RecipientsController(IRecipientsRepository repository)
        {
            _repository = repository;
           
        }

        // GET: Recipients
        public IActionResult Index()
        {
            
            var selectItems = _repository.GetRecipientSelectList(); 
                return View(selectItems);
        }
        public IActionResult SenderRecipients(string selFullName)
        {
            var senderRecipients = _repository.GetSendersRecipients(selFullName);
            if (senderRecipients != null)
            {
                return View(senderRecipients);
            }
            return View();
        }
        
        // GET: Recipients/Create
        public IActionResult Create()
        {
            var selectItems = _repository.GetRecipientSelectList();
            return View(selectItems);
        }

        // POST: Recipients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Recipients recipient)
        {
            if (ModelState.IsValid)
            {
                _repository.AddRecipient(recipient);//.Add(recipients);
                return RedirectToAction("Index", recipient);
            }
            return View(recipient);
        }

        // GET: Recipients/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var selectItems = _repository.GetRecipientSelectList();
            var recipient = _repository.GetRecipientsById(id);
            if (recipient != null)
            {
                RecipientEditModel scv = new RecipientEditModel()
                {
                    FirstName = recipient.FirstName,
                    LastName = recipient.LastName,
                    Gender = recipient.Gender,
                    Title = recipient.Title,
                    RecipientTitles = selectItems.RecipientTitles,
                    AddressLineOne = recipient.AddressLineOne,
                    AddressLineTwo = recipient.AddressLineTwo,
                    PostCode = recipient.PostCode,
                    PostTown = recipient.PostTown,
                    RecipientTown = selectItems.RecipientTown,
                    Telephone = recipient.Telephone,
                    EmailAddress = recipient.EmailAddress,
                    RecipientId = recipient.RecipientId,
                    SenderId = recipient.SenderId
                };
                return View(scv);
            }
            return View();
        }

        // POST: Recipients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, RecipientEditModel recipient)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdateRecipients(recipient);
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
                return RedirectToAction("Index", recipient);
            }
            return View(recipient);
        }
        public IActionResult MainRecipientsPanel()
        {
            ViewBag.ShowTopBar = true;
            return View();
        }
        // GET: Recipients/Delete/5
        public IActionResult Delete(int id)
        {
            var recipients = _repository.GetRecipientsById(id);
            if (recipients == null)
            {
                return NotFound();
            }

            return View(recipients);
        }

        // POST: Recipients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Recipients recipient)
        {
            var recipients = _repository.DeleteRecipients(recipient);
            return RedirectToAction("Index");
        }

        private bool RecipientsExists(int id)
        {
            return _context.Recipients.Any(e => e.RecipientId == id);
        }
    }
}
