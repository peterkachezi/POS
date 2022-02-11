using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using POS.Data.DTOs.SalesModule;
using POS.Data.Models;
using POS.Data.Services.ProductModule;
using POS.Data.Services.SalesModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SalesController : Controller
    {
        private readonly IProductService productService;

        private readonly ISalesService  salesService;

        private readonly UserManager<AppUser> userManager;
        public SalesController(ISalesService salesService,UserManager<AppUser> userManager,IProductService productService)
        {
            this.productService = productService;

            this.userManager = userManager;

            this.salesService = salesService;
        }
        public async Task<IActionResult> Index()
        {
            var sales = await salesService.GetAllSalesDetails();

            return View(sales);
        }

        public async Task<IActionResult> DirectSales()
        {

            var products = await productService.GetAll();

            if (products.Count == 0)
            {
                return RedirectToAction("ProductInformation");

            }
            else
            {
                ViewBag.Products = products;
            }



            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SaveTransaction(SalesDTO salesDTO)
        {
            var user = await userManager.FindByEmailAsync(User.Identity.Name);

            salesDTO.CreatedBy = user.Id;

            salesDTO.Id = Guid.NewGuid();

            var result = await salesService.AddSalesOrder(salesDTO);


            if (result != null)
            {

                //return Json(new { success = true, OrderId = salesDTO.Id, OrderNumber = salesDTO.OrderNumber, responseText = "Transaction was successfull" }, JsonRequestBehavior.AllowGet);

                return Json(new { success = true, responseText = salesDTO.OrderNumber });


            }

            else
            {
                return Json(new { success = false, responseText = "Transaction was not successfull" });
            }
        }

        public async Task<ActionResult> GetReceipt(string OrderNumber)
        {

            try
            {


                return View();

                //return File(b,mt, "application/pdf");

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                return null;
            }

        }


    }
}
