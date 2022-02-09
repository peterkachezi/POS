using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POS.Data.DTOs.ProductModule;
using POS.Data.DTOs.StockModule;
using POS.Data.Models;
using POS.Data.Services.ProductModule;
using POS.Data.Services.StockModule;
using POS.Data.Services.SupplerModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StockControlController : Controller
    {
        private readonly IStockService stockService;

        private readonly IProductService productService;

        private readonly ISupplierService supplierService;

        private readonly UserManager<AppUser> userManager;
        public StockControlController(IProductService productService,ISupplierService supplierService, UserManager<AppUser> userManager, IStockService stockService)
        {
            this.stockService = stockService;

            this.userManager = userManager;

            this.supplierService = supplierService;

            this.productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var stock = await stockService.GetAll();

            return View(stock);
        }

        public async Task<IActionResult> BulkStockEntry()
        {
            ViewBag.Suppliers = await supplierService.GetAll();

            ViewBag.Products = await productService.GetAll();

            return View();
        }

        public async Task<IActionResult> GetStocks()
        {
            var stock = await stockService.GetAll();


            return Json(new { data = stock });
        }
        
        public IActionResult StockEntry()
        {
           
            return View();
        }

        public async Task<IActionResult> Create(StockDTO stockDTO)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                stockDTO.CreatedBy = user.Id;

                var result = await stockService.Create(stockDTO);

                if (result != null)
                {

                    return Json(new { success = true, responseText = "Stock has been successfully updated" });
                }
                else
                {
                    return Json(new { success = false, responseText = "Unable to updated stock" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<IActionResult> Update(StockDTO StockDTO)
        {
            try
            {
                var results = await stockService.Update(StockDTO);

                if (results != null)

                {
                    return Json(new { success = true, responseText = "Record  has been  successfully updated!" });
                }

                else
                {
                    return Json(new { success = false, responseText = "Failed to update the record  !" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }
            

        public async Task<IActionResult> GetById(Guid Id)
        {
            try
            {
                var data = await stockService.GetById(Id);

                if (data != null)
                {
                    StockDTO file = new StockDTO
                    {
                        Id = data.Id,


                        CreateDate = data.CreateDate,

                        CreatedBy = data.CreatedBy
                    };

                    return Json(new { data = file });
                }

                return Json(new { data = false });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                var results = await stockService.Delete(Id);

                if (results == true)
                {
                    return Json(new { success = true, responseText = "Record  has been  successfully Deleted!" });
                }
                else
                {
                    return Json(new { success = false, responseText = "Failed to Delete because the file is in use with other records!" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
