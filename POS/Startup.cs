using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using POS.Data.Models;
using POS.Data.Services.ApplicationUserServiceModule;
using POS.Data.Services.BrandModule;
using POS.Data.Services.CountyModule;
using POS.Data.Services.CustomerModule;
using POS.Data.Services.CyberPOSModule;
using POS.Data.Services.CyberServiceModule;
using POS.Data.Services.ExpenseModule;
using POS.Data.Services.ExpenseTypeModule;
using POS.Data.Services.PaymentTypeModule;
using POS.Data.Services.ProductModule;
using POS.Data.Services.SalesModule;
using POS.Data.Services.SMSModule;
using POS.Data.Services.StockModule;
using POS.Data.Services.SupplerModule;
using POS.Data.Services.UnitOfMeasureModule;
using POS.Extensions;
using POS.SeedAppUsers;
using POS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS
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
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, ApplicationUserClaimsPrincipalFactory>();

            services.AddScoped<IUOMService, UOMService>();

            services.AddScoped<ISupplierService, SupplierService>();

            services.AddScoped<ICountyService, CountyService>();

            services.AddScoped<IBrandService,BrandService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IStockService, StockService>();

            services.AddScoped<ICustomersService, CustomersService>();

            services.AddScoped<IMailService, MailService>();

            services.AddScoped<IMessagingService, MessagingService>();

            services.AddScoped<ISalesService, SalesService>();

            services.AddScoped<IExpenseTypeService, ExpenseTypeService>();

            services.AddScoped<IExpenseService, ExpenseService>();

            services.AddScoped<ICyber_Service, Cyber_Service>();

            services.AddScoped<ICyberPOSService, CyberPOSService>();

            services.AddScoped<IApplicationUserService, ApplicationUserService>();

            services.AddScoped<IPaymentTypeService, PaymentTypeService>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //CreateRoles(roleManager);

            //CreateUsers(userManager);

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            SeedUsers.seed(userManager, roleManager);

            app.UseEndpoints(endpoints =>
            {
              endpoints.MapControllerRoute(
              name: "CyberSection",
              pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
                
              endpoints.MapControllerRoute(
              name: "Admin",
              pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

              endpoints.MapControllerRoute(
              name: "default",
              pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }

    }
}
