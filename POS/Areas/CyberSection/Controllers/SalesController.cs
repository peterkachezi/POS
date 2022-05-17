using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POS.Data.Models;
using POS.Data.Services.CyberPOSModule;
using POS.Data.Services.CyberServiceModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Areas.CyberSection.Controllers
{
    [Area("CyberSection")]
    public class SalesController : Controller
    {

        private readonly ICyberPOSService cyberPOSService;

        private readonly UserManager<AppUser> userManager;

        public SalesController(ICyberPOSService cyberPOSService, UserManager<AppUser> userManager)
        {
            this.userManager = userManager;

            this.cyberPOSService = cyberPOSService;
        }
        public IActionResult Index()
        {
            try
            {
                DateTime startDateTime = DateTime.Today; //Today at 00:00:00

                DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59                          

                // var sales = cyberPOSService.GetAllSalesDetails().Where(x => x.CreateDate >= startDateTime && x.CreateDate <= endDateTime);

                var sales = cyberPOSService.GetAllSalesDetails();

                return View(sales);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
