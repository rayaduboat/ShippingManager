using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
//using RabantFinanceManager.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FinanceManager.Repository;
using FinanceManager.Model.Abstract;
using RabantFinanceManager.Data;
using FinanceManager.Model.Models;
using Microsoft.CodeAnalysis.Options;
using Microsoft.AspNetCore.Http;
using RabantFinanceManager.Security;
using RabantFinanceManager.Controllers;
//using System.Data;
//using System.Data.Common;
//using FinanceManager.Model.Models;

namespace RabantFinanceManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<FinanceManagerDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDefaultIdentity<ApplicationUser>();
            //services.AddIdentity<ApplicationUser, IdentityUser>();
            //services.AddIdentity<ApplicationUser, IdentityRole>();
            services.AddIdentity<IdentityUser, IdentityRole>(Option =>
        //services.AddIdentity<ApplicationUser, IdentityRole>(Option =>
            {
                //Option.Password.RequiredLength = 6;
                //Option.Password.RequiredUniqueChars = 3;
                Option.Password.RequireDigit = false;
                Option.Password.RequiredLength = 6;
                Option.Password.RequireNonAlphanumeric = false;
                Option.Password.RequireUppercase = false;
                Option.Password.RequireLowercase = false;

            }).AddEntityFrameworkStores<ApplicationDbContext>();

            //builder.AddSignInManager<SignInManager<ApplicationUser>>();
            //services.AddScoped<SignInManager<ApplicationUser>>();
            //services.AddDefaultIdentity<ApplicationUser>();


            //////services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //////    .AddEntityFrameworkStores<ApplicationDbContext>();
            //AddIdentity adds cookies based authorisation
            //services.AddIdentity<FinanceManager.Model.Models.ApplicationUser, IdentityUser>();
            //////services.Configure<IdentityOptions>(Option =>
            //////{
            //////    Option.Password.RequiredLength = 10;
            //////    Option.Password.RequiredUniqueChars = 3;
            //////});
            services.AddControllersWithViews();
            services.AddRazorPages();
            //services.AddScoped<FinanceManagerDbContext>();
            services.AddScoped<IExpenditureRepository, ExpenditureRepository>();
            services.AddScoped<ISendersRepository, SendersRepository>();
            services.AddScoped<IRecipientsRepository, RecipientsRepository>();
            services.AddScoped<ITripDetailsRepository, TripDetailsRepository>();
            services.AddScoped<ILinCreatorTableRepository, LinCreatorTableRepository>();
            services.AddScoped<IBatchesRepository, BatchesRepository>();
            services.AddScoped<IShippersRepository, ShippersRepository>();
            services.AddScoped<ILicensesRepository, LicensesRepository>();
            services.AddScoped<IConfigRepository, ConfigRepository>();
            services.AddScoped<IItemsRepository, ItemsRepository>();
            //services.AddSingleton<IDbConnection,DbConnection>();
            services.AddSingleton<HttpContextAccessor>();
            services.AddSingleton<CustomerLinkEncryption>();
            //services.AddSingleton<UserManager<IdentityUser>>();
            //services.AddSingleton<SignInManager<IdentityUser>>();
            //services.AddSingleton<AccountController>();
           // services.AddScoped<SignInManager<ApplicationUser>, SignInManager<ApplicationUser>>();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hosting Environment:" + env.EnvironmentName);
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=LandingPage}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
