using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POS.Data.DTOs.CyberPOSModule;
using POS.Data.Models;
using POS.Data.Services.CyberPOSModule;
using POS.Data.Services.CyberServiceModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Areas.CyberSection.Controllers
{
    [Area("CyberSection")]
    public class POSController : Controller
    {
        private readonly ICyber_Service cyber_Service;

        private readonly ICyberPOSService  cyberPOSService;

        private readonly UserManager<AppUser> userManager;

        private readonly IWebHostEnvironment env;

        public POSController(IWebHostEnvironment env,ICyberPOSService cyberPOSService,UserManager<AppUser> userManager,ICyber_Service cyber_Service)
        {
            this.cyber_Service = cyber_Service;

            this.userManager = userManager;

            this.cyberPOSService = cyberPOSService;

            this.env = env;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Services = await cyber_Service.GetAll();

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SaveTransaction(CyberSaleDTO  cyberSaleDTO)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                cyberSaleDTO.CreatedBy = user.Id;

                cyberSaleDTO.Id = Guid.NewGuid();

                var result = await cyberPOSService.AddPaidServices(cyberSaleDTO);

                if (result != null)
                {
                    return Json(new { success = true, responseText = cyberSaleDTO.OrderNumber });

                }

                else
                {
                    return Json(new { success = false, responseText = "Transaction was not successfull" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveCreditTransaction(CyberSaleDTO cyberSaleDTO)
        {

            try
            {
                if (cyberSaleDTO.CustomerId == null || cyberSaleDTO.CustomerId == Guid.Empty)
                {
                    return Json(new { success = false, responseText = "Please select a customer and try again" });

                }

                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                cyberSaleDTO.CreatedBy = user.Id;

                cyberSaleDTO.Id = Guid.NewGuid();

                var result = await cyberPOSService.AddCreditServices(cyberSaleDTO);

                if (result != null)
                {

                    return Json(new { success = true, responseText = cyberSaleDTO.OrderNumber });

                }

                else
                {
                    return Json(new { success = false, responseText = "Transaction was not successfull" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }















        public ActionResult GetReceipt(string OrderNumber)
        {
            try
            {
                var dt = new DataTable();

                dt = GetTransaction(OrderNumber);

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
        public DataTable GetTransaction(string OrderNumber)
        {
            var list = cyberPOSService.GetAllSalesDetails().Where(x => x.OrderNumber == OrderNumber);

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
