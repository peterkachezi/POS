using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using POS.Data.Services.CyberPOSModule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Areas.CyberSection.Controllers
{
    [Area("CyberSection")]
    public class SalesReportController : Controller
    {
        private readonly ICyberPOSService cyberPOSService;

        public SalesReportController(ICyberPOSService cyberPOSService)
        {
            this.cyberPOSService = cyberPOSService;
        }
        public IActionResult Index()
        {
            return View();
        }
             

        public IActionResult DownloadAllSalesReport()
        {

            var sales = cyberPOSService.GetAllSalesDetails();

            if (sales != null)
            {
                var stream = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var xlPackage = new ExcelPackage(stream))
                {
                    var worksheet = xlPackage.Workbook.Worksheets.Add("Users");

                    var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");

                    namedStyle.Style.Font.UnderLine = true;

                    namedStyle.Style.Font.Color.SetColor(Color.Blue);

                    const int startRow = 5;

                    var row = startRow;

                    //Create Headers and format them
                    worksheet.Cells["A1,B1,C1"].Value = "Sales Report";
                    using (var r = worksheet.Cells["A1:G1"])
                    {
                        r.Merge = true;
                        r.Style.Font.Color.SetColor(Color.White);
                        r.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                        r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                    }

                    worksheet.Cells["A2"].Value = "TransactionNumber";
                    worksheet.Cells["B2"].Value = "ServiceName";
                    worksheet.Cells["C2"].Value = "Quantity";
                    worksheet.Cells["D2"].Value = "SellingPrice";
                    worksheet.Cells["E2"].Value = "Discount";
                    worksheet.Cells["F2"].Value = "CreateByName";
                    worksheet.Cells["G2"].Value = "CreateDate";

                    worksheet.Cells["A2:G2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells["A2:G2"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                    worksheet.Cells["A2:G2"].Style.Font.Bold = true;

                    row = 3;
                    foreach (var user in sales)
                    {
                        worksheet.Cells[row, 1].Value = user.OrderNumber.ToString();
                        worksheet.Cells[row, 2].Value = user.ServiceName.ToString();
                        worksheet.Cells[row, 3].Value = user.Quantity.ToString();
                        worksheet.Cells[row, 4].Value = user.SellingPrice.ToString();
                        worksheet.Cells[row, 5].Value = user.Discount.ToString();
                        worksheet.Cells[row, 6].Value = user.CreateByName.ToString();
                        worksheet.Cells[row, 7].Value = user.CreateDate.ToShortDateString();

                        row++;
                    }

                    // set some core property values
                    xlPackage.Workbook.Properties.Title = "HK";
                    xlPackage.Workbook.Properties.Author = "HK";
                    xlPackage.Workbook.Properties.Subject = "HK";
                    // save the new spreadsheet
                    xlPackage.Save();
                    // Response.Clear();
                }
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LeadsReport.xlsx");
            }
            else
            {
                return null;
            }

        }
        public IActionResult DownloadSalesReportByDateRange(DateTime DateFrom, DateTime DateTo)
        {

            var endDate = DateTo.AddHours(23).AddMinutes(59).AddSeconds(59);

            if (DateFrom == DateTime.MinValue)
            {
                TempData["ErrorDate"] = "Please select Date From ";

                return RedirectToAction("Index", new { area = "Admin" });

            }

            if (DateTo == DateTime.MinValue)
            {
                TempData["ErrorDate"] = "Please select Date To ";

                return RedirectToAction("Index");

            }

            var sales = cyberPOSService.GetAllSalesDetails().Where(x => x.CreateDate >= DateFrom && x.CreateDate <= endDate).ToList();

            if (sales != null)
            {
                var stream = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var xlPackage = new ExcelPackage(stream))
                {
                    var worksheet = xlPackage.Workbook.Worksheets.Add("Users");

                    var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");

                    namedStyle.Style.Font.UnderLine = true;

                    namedStyle.Style.Font.Color.SetColor(Color.Blue);

                    const int startRow = 5;

                    var row = startRow;

                    //Create Headers and format them
                    worksheet.Cells["A1,B1,C1"].Value = "Sales Report";
                    using (var r = worksheet.Cells["A1:G1"])
                    {
                        r.Merge = true;
                        r.Style.Font.Color.SetColor(Color.White);
                        r.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                        r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                    }

                    worksheet.Cells["A2"].Value = "TransactionNumber";
                    worksheet.Cells["B2"].Value = "ServiceName";
                    worksheet.Cells["C2"].Value = "Quantity";
                    worksheet.Cells["D2"].Value = "SellingPrice";
                    worksheet.Cells["E2"].Value = "Discount";
                    worksheet.Cells["F2"].Value = "CreateByName";
                    worksheet.Cells["G2"].Value = "CreateDate";

                    worksheet.Cells["A2:G2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells["A2:G2"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                    worksheet.Cells["A2:G2"].Style.Font.Bold = true;

                    row = 3;
                    foreach (var user in sales)
                    {
                        worksheet.Cells[row, 1].Value = user.OrderNumber.ToString();
                        worksheet.Cells[row, 2].Value = user.ServiceName.ToString();
                        worksheet.Cells[row, 3].Value = user.Quantity.ToString();
                        worksheet.Cells[row, 4].Value = user.SellingPrice.ToString();
                        worksheet.Cells[row, 5].Value = user.Discount.ToString();
                        worksheet.Cells[row, 6].Value = user.CreateByName.ToString();
                        worksheet.Cells[row, 7].Value = user.CreateDate.ToShortDateString();

                        row++;
                    }

                    // set some core property values
                    xlPackage.Workbook.Properties.Title = "HK";
                    xlPackage.Workbook.Properties.Author = "HK";
                    xlPackage.Workbook.Properties.Subject = "HK";
                    // save the new spreadsheet
                    xlPackage.Save();
                    // Response.Clear();
                }
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LeadsReport.xlsx");
            }
            else
            {
                return null;
            }

        }
        public IActionResult TodaysSalesReport()
        {

            DateTime startDateTime = DateTime.Today; //Today at 00:00:00

            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59

            var sales = cyberPOSService.GetAllSalesDetails().Where(x => x.CreateDate >= startDateTime && x.CreateDate <= endDateTime);

            if (sales != null)
            {
                var stream = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var xlPackage = new ExcelPackage(stream))
                {
                    var worksheet = xlPackage.Workbook.Worksheets.Add("Users");

                    var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");

                    namedStyle.Style.Font.UnderLine = true;

                    namedStyle.Style.Font.Color.SetColor(Color.Blue);

                    const int startRow = 5;

                    var row = startRow;

                    //Create Headers and format them
                    worksheet.Cells["A1,B1,C1"].Value = "Sales Report";
                    using (var r = worksheet.Cells["A1:G1"])
                    {
                        r.Merge = true;
                        r.Style.Font.Color.SetColor(Color.White);
                        r.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                        r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                    }

                    worksheet.Cells["A2"].Value = "TransactionNumber";
                    worksheet.Cells["B2"].Value = "ServiceName";
                    worksheet.Cells["C2"].Value = "Quantity";
                    worksheet.Cells["D2"].Value = "SellingPrice";
                    worksheet.Cells["E2"].Value = "Discount";
                    worksheet.Cells["F2"].Value = "CreateByName";
                    worksheet.Cells["G2"].Value = "CreateDate";

                    worksheet.Cells["A2:G2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells["A2:G2"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                    worksheet.Cells["A2:G2"].Style.Font.Bold = true;

                    row = 3;
                    foreach (var user in sales)
                    {
                        worksheet.Cells[row, 1].Value = user.OrderNumber.ToString();
                        worksheet.Cells[row, 2].Value = user.ServiceName.ToString();
                        worksheet.Cells[row, 3].Value = user.Quantity.ToString();
                        worksheet.Cells[row, 4].Value = user.SellingPrice.ToString();
                        worksheet.Cells[row, 5].Value = user.Discount.ToString();
                        worksheet.Cells[row, 6].Value = user.CreateByName.ToString();
                        worksheet.Cells[row, 7].Value = user.CreateDate.ToShortDateString();

                        row++;
                    }

                    // set some core property values
                    xlPackage.Workbook.Properties.Title = "HK";
                    xlPackage.Workbook.Properties.Author = "HK";
                    xlPackage.Workbook.Properties.Subject = "HK";
                    // save the new spreadsheet
                    xlPackage.Save();
                    // Response.Clear();
                }
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LeadsReport.xlsx");
            }
            else
            {
                return null;
            }

        }

    }
}
