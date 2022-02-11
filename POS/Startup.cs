using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using POS.Data.Models;
using POS.Data.Services.BrandModule;
using POS.Data.Services.CountyModule;
using POS.Data.Services.CustomerModule;
using POS.Data.Services.ProductModule;
using POS.Data.Services.SalesModule;
using POS.Data.Services.SMSModule;
using POS.Data.Services.StockModule;
using POS.Data.Services.SupplerModule;
using POS.Data.Services.UnitOfMeasureModule;
using POS.Extensions;
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

            app.UseEndpoints(endpoints =>
            {
              endpoints.MapControllerRoute(
              name: "Admin",
              pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

              endpoints.MapControllerRoute(
              name: "default",
              pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            try
            {
                if (!roleManager.RoleExistsAsync("Admin").Result)

                {
                    var role = new IdentityRole();

                    role.Name = "Admin";

                    roleManager.CreateAsync(role);
                }
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }


        private void CreateUsers(UserManager<AppUser> userManager)
        {
            try
            {
                var admin = userManager.FindByEmailAsync("admin@gmail.com");

                if (admin.Result == null)
                {
                    var user = new AppUser();

                    user.UserName = "admin@gmail.com";

                    user.Email = "admin@gmail.com";

                    user.PhoneNumber = "0704509484";

                    user.FirstName = "Alex";

                    user.LastName = "Jobs";

                    user.EmailConfirmed = true;

                    user.isActive = true;

                    user.CreateDate = DateTime.Now;

                    string userPWD = "Admin@2022";

                    var chkUser = userManager.CreateAsync(user, userPWD);

                    //Add default User to Role Admin    
                    if (chkUser.Result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Admin").Wait();

                    }

                }

              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
