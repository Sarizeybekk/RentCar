using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentCarProject.DataAccess.Data;

[assembly: HostingStartup(typeof(RentCarProject.Areas.Identity.IdentityHostingStartup))]
namespace RentCarProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<RentCarProjectContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("RentCarProjectContextConnection")));

                //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<RentCarProjectContext>();
            });
        }
    }
}