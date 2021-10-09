using FinanceManager.Model.Models;
using FinanceManager.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabantFinanceManager.Controllers
{
    public class UsersController : Controller
    {
        FinanceManagerDbContext db = new FinanceManagerDbContext();
        public UsersController()
        {

        }
        public IActionResult Index()
        {
            return View(db.Users.OrderByDescending(x=>x.UserId).ToList());
        }
        public async Task<IActionResult> CreateAccountForUsers(RegisterViewModel model)
        {
            string msg = "";
            if (ModelState.IsValid)
            {
                var getShipper = db.Shippers.Where(i => i.ShippersId == model.ShippersId).Select(x => x).FirstOrDefault();
                var user = new IdentityUser { UserName = getShipper.EmailAddress, Email = getShipper.EmailAddress };
                try
                {
                    //await signInManager.SignInAsync(user, isPersistent: false);
                    msg = "Account Created Successfully!";
                    ViewBag.Email = model.Email;
                    ViewBag.Password = model.Password;
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
                ViewBag.msg = msg;
            }
            return View();
        }
    }
}
