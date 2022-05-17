using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POS.Data.Models;
using POS.Data.Services.CustomerModule;
using POS.Data.Services.CyberPOSModule;
using POS.Data.Services.CyberServiceModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Areas.CyberSection.Controllers
{
    [Area("CyberSection")]
    public class DashboardController : Controller
    {
        private readonly ICustomersService customerService;

        private readonly ICyberPOSService cyberPOSService;

        private readonly UserManager<AppUser> userManager;

        private readonly ICyber_Service cyber_Service;
        public DashboardController(ICyber_Service cyber_Service,ICyberPOSService cyberPOSService, ICustomersService customerService, UserManager<AppUser> userManager)
        {
            this.customerService = customerService;

            this.userManager = userManager;

            this.cyberPOSService = cyberPOSService;

            this.cyber_Service = cyber_Service;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Customers = (await customerService.GetAll()).Count();

            var sales = cyberPOSService.GetAllSalesDetails();

            DateTime startDateTime = DateTime.Today; //Today at 00:00:00

            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59                          

            var todaysSales = sales.Where(x => x.CreateDate >= startDateTime && x.CreateDate <= endDateTime);

            ViewBag.TodaysSales = todaysSales.Count();

            ViewBag.Transactions = sales.Count();              

            decimal totalamount = sales.Sum(item => item.Total);                 

            ViewBag.Sales = totalamount;


            ViewBag.Services = (await cyber_Service.GetAll()).Count();

            return View();
        }
    }
}
