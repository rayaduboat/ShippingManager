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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RabantFinanceManager.Controllers
{
    //[Authorize]
    public class SendersController : Controller
    {

        private readonly FinanceManagerDbContext _context;
        private ISendersRepository _repository;
        //private AccountController _acct;

        public SendersController(ISendersRepository repository)
        {
            _repository = repository;

        }

        // GET: Senders
        public IActionResult Index()
        {
            var sendersData = _repository.GetAllSenders();
            return View(sendersData);
        }

        public IActionResult RegisteredCustomer(Senders sender)
        {
            return View(sender);
        }
        // GET: Senders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var senders = await _context.Senders
                .Include(s => s.Shippers)
                .FirstOrDefaultAsync(m => m.SenderId == id);
            if (senders == null)
            {
                return NotFound();
            }

            return View(senders);
        }

        // GET: Senders/Create
        public IActionResult Create()
        {
            var selectItems = _repository.GetSenderSelectList();
            return View(selectItems);
        }

        // POST: Senders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SendersCreateModel sender)
        {
            string msg = "";
            if (ModelState.IsValid)
            {
                if (!_repository.ValidateRegistration(sender))
                {
                    _repository.AddSender(sender);//.Add(senders);
                    RegisterViewModel model = new RegisterViewModel();
                    //model.sender = sender;
                    model.Email = sender.EmailAddress;
                    model.Password = sender.Password;
                    model.ConfirmPassword = sender.ConfirmPassword;
                    // model.sender = sender;
                    //AccountController acct = new AccountController(userManager, signInManager);
                    return RedirectToAction("CreateAccountForNewCustomer", "Account", model);
                    //return RedirectToAction("RegisteredCustomer", sender); 
                }
                else
                {
                    msg = "You have already registered!";
                    return RedirectToAction("ErrorRegistering", "Account", msg);
                }

            }
            else
            {
                //return  RedirectToAction("Create");
            }
            return View(sender);
        }



        //Create Customer Account from registration
        //=================================================
        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new IdentityUser { UserName = model.Email, Email = model.Email };
        //        var result = await userManager.CreateAsync(user, model.Password);

        //        if (result.Succeeded)
        //        {
        //            await signInManager.SignInAsync(user, isPersistent: false);
        //            if (User.Identity.Name == "Sysadmin@rabcomsolutions.com")
        //            {
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                return RedirectToAction("Login", "Account");
        //            }
        //        }
        //        else
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError("", error.Description);
        //            }
        //        }
        //    }
        //    return View(model);
        //}



        // GET: Senders/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var selectItems = _repository.GetSenderSelectList();
            var sender = _repository.GetSendersById(id);
            if (sender != null)
            {
                SendersCreateModel scv = new SendersCreateModel()
                {
                    FirstName = sender.FirstName,
                    LastName = sender.LastName,
                    Gender = sender.Gender,
                    Title = sender.Title,
                    SenderTitles = selectItems.SenderTitles,
                    AddressLineOne = sender.AddressLineOne,
                    PostCode = sender.PostCode,
                    PostTown = sender.PostTown,
                    SenderTown = selectItems.SenderTown,
                    Telephone = sender.Telephone,
                    EmailAddress = sender.EmailAddress,
                    SenderId = sender.SenderId,
                    ShippersId = sender.ShippersId,
                    Password=sender.Password
                };
                return View(scv);
            }
            return View();
        }

        // POST: Senders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, SendersCreateModel sender)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdateSenders(sender);
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
                string Email = User.Identity.Name;
                if (Email != null)
                {
                    return RedirectToAction("Index", sender);
                }
                else
                {
                    return RedirectToAction("RegisteredCustomer", sender);
                }
                //return RedirectToAction("Index", sender);
            }
            return View(sender);
        }
        public IActionResult MainSendersPanel()
        {
            ViewBag.ShowTopBar = true;
            return View();
        }
        // GET: Senders/Delete/5
        public IActionResult Delete(int id)
        {
            var senders = _repository.GetSendersById(id);
            if (senders == null)
            {
                return NotFound();
            }

            return View(senders);
        }

        // POST: Senders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Senders sender)
        {
            var senders = _repository.DeleteSenders(sender);
            return RedirectToAction("Index");
        }

        private bool SendersExists(int id)
        {
            return _context.Senders.Any(e => e.SenderId == id);
        }


    }
}
