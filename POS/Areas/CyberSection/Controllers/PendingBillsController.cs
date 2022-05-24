using Microsoft.AspNetCore.Mvc;
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

        public IActionResult GetPendingBillsDetailsById(Guid Id)
        {
            try
            {
                var data = cyberPOSService.GetAllSalesDetailsByOrderId(Id);

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
