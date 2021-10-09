//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace FinanceManager.Model.Models
//{
//    public class ApplicationUserManager : UserManager<ApplicationUser>
//    {
//        //public ApplicationUserManager(IUserStore<ApplicationUser> store)
//        //    : base(store)
//        //{
//        //}

//        //    //public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
//        //    //{
//        //    //    ///Calling the non-default constructor of the UserStore class
//        //    //    var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));

//        //    //    /// Rest of the class ...
//        //    //}
//    }

//    //public class ApplicationRoleManager : RoleManager<IdentityRole>
//    //{
//    //    public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
//    //        : base(roleStore)
//    //    {
//    //    }

//    //    public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
//    //    {
//    //        ///It is based on the same context as the ApplicationUserManager
//    //        var appRoleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<AppUsersDbContext>()));

//    //        return appRoleManager;
//    //    }
//    //}
//}
