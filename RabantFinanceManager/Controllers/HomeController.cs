using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FinanceManager.Model.Abstract;
using FinanceManager.Model.Models;
using FinanceManager.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabantFinanceManager.Models;

namespace RabantFinanceManager.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        FinanceManagerDbContext db = new FinanceManagerDbContext();
        private readonly ILogger<HomeController> _logger;
        private readonly ISendersRepository _repository;
        private readonly SignInManager<IdentityUser> signInManager;

        public HomeController(ILogger<HomeController> logger, ISendersRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            ViewBag.BatchExist = db.Batch.ToList().Count();
            ViewBag.ShowTopBar = true;
            Shippers shipper = new Shippers();
            shipper = db.Shippers.Where(i => i.EmailAddress == User.Identity.Name).Select(v => v).FirstOrDefault();
            TripEditModel user = new TripEditModel();
            if (shipper != null)
            {
                shipper = db.Shippers.Where(i => i.EmailAddress == User.Identity.Name).Select(v => v).FirstOrDefault();
                ViewBag.CompanyName = shipper.CompanyName;
                ////user.loginUserDetails.Telephone = shipper.Telephone.ToString();
                ////user.loginUserDetails.EmailAddress = shipper.EmailAddress;
                ////user.loginUserDetails.PostCode = shipper.PostCode;
                user.loginshipperDetails = shipper;
            }
            else
            {
                //collect login user details for navigation
                //==============================================
                    user.loginUserDetails = GetUserNavDetails(User.Identity.Name);
            }
            return View(user);
        }
        public IActionResult LandingPage()
        {
            ViewBag.ShowTopBar = true;
            return View();
        }
       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private Senders GetUserNavDetails(string user)
        {
            return _repository.GetSendersByEmail(user);
        }
    }
}
