using Microsoft.AspNetCore.Mvc;
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
    public class CreditServicesController : Controller
    {
        private readonly ICustomersService customerService;

        private readonly ICyber_Service cyber_Service;
        public CreditServicesController(ICyber_Service cyber_Service, ICustomersService customerService)
        {
            this.customerService = customerService;

            this.cyber_Service = cyber_Service;
        }
        public async Task<IActionResult> Index(Guid Id)
        {
            TempData["TempCustomer"] = Id;  // Defines TempData  

            ViewBag.Services = await cyber_Service.GetAll();

            ViewBag.Customer = await customerService.GetAll();

            return View();
        }
        public async Task<IActionResult> Test(string OrderNumber)
        {

            ViewBag.Services = await cyber_Service.GetAll();

            ViewBag.Customer = await customerService.GetAll();

            return View();
        }
    }
}
