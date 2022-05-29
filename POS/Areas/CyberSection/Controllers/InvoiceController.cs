﻿using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POS.Data.DTOs.CyberPOSModule;
using POS.Data.DTOs.InvoiceModule;
using POS.Data.Models;
using POS.Data.Services.CustomerModule;
using POS.Data.Services.CyberPOSModule;
using POS.Data.Services.InvoiceModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Areas.CyberSection.Controllers
{
    [Area("CyberSection")]
    public class InvoiceController : Controller
    {
        private readonly ICyberPOSService cyberPOSService;

        private readonly UserManager<AppUser> userManager;

        private readonly IWebHostEnvironment env;

        private readonly ICustomersService customerService;

        private readonly IInvoiceService invoiceService;
        public InvoiceController(IInvoiceService invoiceService, ICustomersService customerService, ICyberPOSService cyberPOSService, UserManager<AppUser> userManager, IWebHostEnvironment env)
        {
            this.cyberPOSService = cyberPOSService;

            this.userManager = userManager;

            this.customerService = customerService;

            this.invoiceService = invoiceService;

            this.env = env;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> DownloadInvoice(Guid Id, string InvoiceNumber)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                var servedBy = "Invoice Generated By" + " " + user.FirstName + " " + user.LastName +" "+ "on";

                var preparedBy =  user.FirstName + " " + user.LastName;

                var sale = cyberPOSService.GetSalesById(Id);

                var customer = await customerService.GetById(sale.CustomerId.Value);

                var s = new InvoiceDTO
                {
                    OrderId = Id,

                    CustomerId = sale.CustomerId.Value,

                    CreatedBy = user.Id,
                };

                var customerName = customer.FirstName + " " + customer.LastName;

                var customerNo = customer.CustomerNumber;

                var phoneNumber = customer.PhoneNumber;

                var invoiceDate = sale.OrderDate;

                var orderNumber = sale.OrderNumber;

                var saveInvoice = await invoiceService.Create(s);

                var dt = new DataTable();


                var data = GetTransaction(Id);

                var list = new List<CyberSaleDetailsDTO>();

                list = (from DataRow dr in data.Rows

                        select new CyberSaleDetailsDTO()
                        {
                            Total = Convert.ToDecimal(dr["Total"]),

                            Discount = Convert.ToDecimal(dr["Discount"]),

                        }).ToList();

                decimal subtotal = list.Sum(item => item.Total);

                decimal discount = list.Sum(item => item.Discount);

                decimal totalAmount = subtotal - discount;


                dt = data;

                string mimetype = "";

                int extension = 1;

                var path = $"{env.WebRootPath}\\Reports\\Invoice.rdlc";

                Dictionary<string, string> parameters = new Dictionary<string, string>();

                parameters.Add("CustomerName", customerName);
                parameters.Add("CustomerNo", customerNo);
                parameters.Add("PhoneNumber", phoneNumber);
                parameters.Add("ServedBy", servedBy);
                parameters.Add("preparedBy", preparedBy);
                parameters.Add("InvoiceNo", InvoiceNumber);
                parameters.Add("InvoiceDate", invoiceDate.ToShortDateString());
                parameters.Add("OrderNumber", orderNumber.ToString());
                parameters.Add("Subtotal", subtotal.ToString());
                parameters.Add("Discount", discount.ToString());
                parameters.Add("TotalAmount", totalAmount.ToString());

                LocalReport localReport = new LocalReport(path);

                localReport.AddDataSource("Invoices", dt);

                var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);

                return File(result.MainStream, "application/pdf");

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                return null;
            }

        }
        public DataTable GetTransaction(Guid Id)
        {
            var list = cyberPOSService.GetAllSalesDetails().Where(x => x.OrderId == Id);



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

            dt.Columns.Add("ServiceName");

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

                row["ProductId"] = item.ServiceId.ToString();

                row["Quantity"] = item.Quantity.ToString();

                row["SellingPrice"] = item.SellingPrice.ToString();

                row["Discount"] = item.Discount.ToString();

                row["Total"] = item.Total.ToString();

                row["OrderNumber"] = item.OrderNumber.ToString();

                row["CreateDate"] = item.CreateDate.ToString();

                row["CreatedBy"] = item.CreatedBy.ToString();

                row["ServiceName"] = item.ServiceName.ToString();

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