using FinanceManager.Model.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RabantFinanceManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabantFinanceManager.Controllers
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Roles.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(_context.Roles.ToList());
        }
        [HttpPost]
        public IActionResult actionResult(Roles role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Roles.Add(new IdentityRole()
                    {
                        Name = role.Name

                    });
                    _context.SaveChanges();
                    ViewBag.SuccessMessage = "Role added successfuly";
                }
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
            }

            return View(_context.Roles.ToList());
        }
        public ActionResult ManageRole()
        {
            var list = _context.Roles.OrderBy(r => r.Name).ToList().Select(x =>
            new SelectListItem
            {
                Value = x.Name.ToString(),
                Text = x.Name
            }).ToList();
            ViewBag.Roles = list;

            return View();
        }
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public async Task<ActionResult> AddRoleToUser(string UserName, string RoleName)
        ////{
        ////    ApplicationUser user = _context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        ////    var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
        ////   await manager.AddToRoleAsync(user, RoleName);

        ////    //manager.AddToRole(user.Id, RoleName);
        ////    ViewBag.SuccessMessage = "Role Added Successfully";

        ////    var list = _context.Roles.OrderBy(r => r.Name).ToList().Select(x => new SelectListItem { Value = x.Name.ToString(), Text = x.Name }).ToList();
        ////    ViewBag.Roles = list;
        ////    return View("ManageRole");
        ////}

        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public async Task<ActionResult> GetUserRole(string UserName)
        ////{
        ////    if (!string.IsNullOrWhiteSpace(UserName))
        ////    {
        ////        ApplicationUser user = _context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        ////        var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
        ////        ViewBag.RoleToUser =await manager.GetRolesAsync(user);//.GetRoles(user.Id);
        ////        var list = _context.Roles.OrderBy(r => r.Name).ToList().Select(x => new SelectListItem { Value = x.Name.ToString(), Text = x.Name }).ToList();
        ////        ViewBag.Roles = list;
        ////    }
        ////    return View("ManageRole");
        ////}

        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public async Task<ActionResult> DeleteUserRole(string UserName, string RoleName)
        ////{
        ////    ApplicationUser user = _context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        ////    var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));

        ////    if (await manager.IsInRoleAsync(user,RoleName))//.IsInRole(user.Id, RoleName))
        ////    {
        ////        await manager.RemoveFromRoleAsync(user, RoleName);//.RemoveFromRole(user.Id, RoleName);
        ////        ViewBag.ResultMessage = "Role removed successfully";
        ////    }
        ////    else
        ////    {
        ////        ViewBag.ResultMessage = "User does not belongs to the selected Role";
        ////    }
        ////    var list = _context.Roles.OrderBy(r => r.Name).ToList().Select(x => new SelectListItem { Value = x.Name.ToString(), Text = x.Name }).ToList();
        ////    ViewBag.Roles = list;

        ////    return View("ManageRole");
        ////}

        public ActionResult Delete(string RoleName)
        {
            var myRole = _context.Roles.Where(x => x.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            _context.Roles.Remove(myRole);
            _context.SaveChanges();
            ViewBag.SuccessDeleteMessage = "Role deleted successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string roleName)
        {
            var myRole = _context.Roles.Where(x => x.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(myRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                //_context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                //_context.SaveChanges();
                //ViewBag.EditSuccess = "Edited successfully";

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }

        }
    }
}
