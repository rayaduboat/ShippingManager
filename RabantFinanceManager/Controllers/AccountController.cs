using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManager.Model.Abstract;
using FinanceManager.Model.Models;
using FinanceManager.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RabantFinanceManager.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        public RegisterViewModel model;
        private readonly UserManager<IdentityUser> userManager;
        //private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ISendersRepository _repository;
        private readonly IConfigRepository _config;
        private readonly FinanceManagerDbContext _context;

       public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ISendersRepository repository, IConfigRepository config)
        //public AccountController(UserManager<ApplicationUser> userManager, SignInManager<IdentityUser> signInManager, ISendersRepository repository, IConfigRepository config)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _repository = repository;
            _config = config;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel rg = new RegisterViewModel();
            rg.Town = _config.GetUKtowns();
            return View(rg);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Add User details to the users table

                   // await signInManager.SignInAsync(user, isPersistent: false); //Don't sign user in===================
                    if (User.Identity.Name == "Sysadmin@rabcomsolutions.com")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        //Add user details to the users table
                        var shipperId = _repository.GetShipperId();
                        model.ShippersId = shipperId;
                        AddUserToUsersTable(model);
                        //return RedirectToAction("CreateAccountForNewCustomer", "Account", model);
                        return RedirectToAction("Index", "Users", model);
                       // return RedirectToAction("Login", "Account");
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

               
            }

            return View(model);
        }

        private void AddUserToUsersTable(RegisterViewModel model)
        {
            FinanceManagerDbContext db = new FinanceManagerDbContext();
           
            Users registeredUser = new Users();

            registeredUser.FirstName = model.FirstName;
            registeredUser.LastName = model.LastName;
            registeredUser.ShippersId = model.ShippersId;
            registeredUser.Title = model.Title;
            registeredUser.Gender = model.Gender;
            registeredUser.AddressLineOne = model.AddressLineOne;
            registeredUser.AddressLineTwo = model.AddressLineTwo;
            registeredUser.PostCode = model.PostCode;
            registeredUser.PostTown = model.PostTown;
            registeredUser.Country = model.Country;
            registeredUser.Telephone = model.Telephone;
            registeredUser.EmailAddress = model.Email;
            registeredUser.Role = model.Role;
            registeredUser.Password = model.Password;
            
            db.Users.Add(registeredUser);
            db.SaveChanges();
        }

        public async Task<IActionResult> CreateAccountForNewCustomer(RegisterViewModel model)
        {
            string msg = "";
            if (ModelState.IsValid)
            {
                ViewBag.AdminLogin = User.Identity.Name;
                //var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                try
                {
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        msg = "Account Created Successfully!";
                        ViewBag.Email = model.Email;
                        ViewBag.Password = model.Password;
                        //_repository.AddSender(model.sender);//.Add(senders);
                    }
                    else
                    {
                        msg = "Details Registered but Account creation Failed. Please contact Paco on 0123456789!";
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
                ViewBag.msg = msg;
            }
            return View();
        }
       
        public IActionResult ErrorRegistering(string errorMsg)
        {
            return View(errorMsg);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            //return RedirectToAction("Login", "Account");
            return RedirectToAction("LandingPage", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    //ViewBag.User = model.Email;
                    return RedirectToAction("Index", "Home", model);
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        //private Senders GetUserNavDetails(LoginViewModel user)
        //{
        //  return  _repository.GetSendersByEmail(user.Email);
        //}
    }
}
