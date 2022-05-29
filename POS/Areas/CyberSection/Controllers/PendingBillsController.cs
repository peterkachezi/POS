using Microsoft.AspNetCore.Mvc;
using POS.Data.DTOs.CyberPOSModule;
using POS.Data.Services.CyberPOSModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Areas.CyberSection.Controllers
{
    [Area("CyberSection")]
    public class PendingBillsController : Controller
    {
        private readonly ICyberPOSService cyberPOSService;

        public PendingBillsController(ICyberPOSService cyberPOSService)
        {
            this.cyberPOSService = cyberPOSService;
        }
        public IActionResult Index()
        {
            try
            {
                var data = cyberPOSService.GetAllPendingPayments();

                return View(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public IActionResult Details(Guid Id)
        {
            try
            {            
                AllSalesDTO data = new AllSalesDTO()
                {
                };

                data.cyberSaleDTO = cyberPOSService.GetSalesById(Id);

                data.cyberSaleDetailsDTOs = cyberPOSService.GetAllSalesDetailsByOrderId(Id);

                // Keep this _r as a member, not local
                var _r = new Random();

                // Gen a random number
                int rand = _r.Next(1, 10000);

                // Get the "2016-" prefix
                string yearPrefix = (DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond) + "-" + rand;

                // Remove the first 2 digits of the year prefix, now it is "16-"
                yearPrefix = yearPrefix.Substring(2);

                // Put the year prefix together with the random number into the textbox
                var orderNumber = yearPrefix + rand;

                rand = _r.Next(1, 10000);


                ViewBag.InvoiceNo = rand;

                return View(data);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
