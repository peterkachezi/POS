using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using POS.Data.DTOs.SalesModule;
using POS.Data.Models;
using POS.Data.Services.ProductModule;
using POS.Data.Services.SalesModule;
using System;
using System.Collections.Generic;
using System.Data;
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

        private readonly IWebHostEnvironment env;
        public SalesController(IWebHostEnvironment env,ISalesService salesService,UserManager<AppUser> userManager,IProductService productService)
        {
            this.env = env;

            this.productService = productService;

            this.userManager = userManager;

            this.salesService = salesService;
        }
        public async Task<IActionResult> Index()
        {
            var sales = await salesService.GetAllSalesDetails();

            return View(sales);
        }

        public IActionResult ProductInformation()
        {

            return View();
        }

        public async Task<IActionResult> DirectSales()
        {

            var products = await productService.GetAll();

            if (products.Count == 0)
            {
                TempData["Error"] = "There are no products to sell at the moment .Please add products and try again";

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
                var dt = new DataTable();

                dt = await GetTransaction(OrderNumber);

                string mimetype = "";

                int extension = 1;

                var path = $"{env.WebRootPath}\\Reports\\Receipt.rdlc";

                Dictionary<string, string> parameters = new Dictionary<string, string>();

                parameters.Add("prm", "Report");

                LocalReport localReport = new LocalReport(path);

                localReport.AddDataSource("Receipts", dt);

                var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);

                return File(result.MainStream, "application/pdf");

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                return null;
            }

        }

        public async Task<DataTable> GetTransaction(string OrderNumber)
        {
            var list = (await salesService.GetAllSalesDetails()).Where(x=>x.OrderNumber== OrderNumber);

            var dt = new DataTable();

            dt.Columns.Add("Id");

            dt.Columns.Add("OrderId");

            dt.Columns.Add("ProductId");

            dt.Columns.Add("Quantity");

            dt.Columns.Add("SellingPrice");

            dt.Columns.Add("Discount");

            dt.Columns.Add("Total");

            dt.Columns.Add("OrderNumber");

            dt.Columns.Add("CreateDate");

            dt.Columns.Add("CreatedBy");

            //dt.Columns.Add("ProductName");

            //dt.Columns.Add("CustomerName");

            dt.Columns.Add("OrderTotalAmount");

            dt.Columns.Add("CreateByName");

            dt.Columns.Add("Change");

            dt.Columns.Add("CashPaid");

            DataRow row;

            foreach (var item in list)
            {
                row = dt.NewRow();

                row["Id"] = item.Id.ToString();

                row["OrderId"] = item.OrderId.ToString();

                row["ProductId"] = item.ProductId.ToString();

                row["Quantity"] = item.Quantity.ToString();

                row["SellingPrice"] = item.SellingPrice.ToString();

                row["Discount"] = item.Discount.ToString();

                row["Total"] = item.Total.ToString();

                row["OrderNumber"] = item.OrderNumber.ToString();

                row["CreateDate"] = item.CreateDate.ToString();

                row["CreatedBy"] = item.CreatedBy.ToString();

                //row["ProductName"] = item.ProductName.ToString();

               // row["CustomerName"] = item.CustomerName.ToString();

                row["OrderTotalAmount"] = item.OrderTotalAmount.ToString();

                row["CreateByName"] = item.CreateByName.ToString();

                row["Change"] = item.Change.ToString();

                row["CashPaid"] = item.CashPaid.ToString();


                dt.Rows.Add(row);
            }
            return dt;
        }




    }
}
