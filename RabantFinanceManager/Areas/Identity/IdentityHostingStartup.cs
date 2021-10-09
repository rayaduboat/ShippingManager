using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using RabantFinanceManager.Data;

//[assembly: HostingStartup(typeof(RabantFinanceManager.Areas.Identity.IdentityHostingStartup))]
namespace RabantFinanceManager.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}