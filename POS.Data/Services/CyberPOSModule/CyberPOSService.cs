using POS.Data.DTOs.CyberPOSModule;
using POS.Data.Models;
using POS.Data.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace POS.Data.Services.CyberPOSModule
{
    public class CyberPOSService : ICyberPOSService
    {
        private readonly ApplicationDbContext context;

        public CyberPOSService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<CyberSaleDTO> AddPaidServices(CyberSaleDTO cyberSaleDTO)
        {
            try
            {
                string order_number = SalesNumber.GenerateUniqueNumber();

                var orderNumber = ("SLN" + order_number);

                cyberSaleDTO.OrderNumber = orderNumber;

                cyberSaleDTO.Id = Guid.NewGuid();

                cyberSaleDTO.OrderDate = DateTime.Now;

                var s = new CyberSale
                {
                    Id = cyberSaleDTO.Id,

                    CustomerId = cyberSaleDTO.CustomerId,

                    OrderDate = cyberSaleDTO.OrderDate,

                    CreatedBy = cyberSaleDTO.CreatedBy,

                    TotalAmount = cyberSaleDTO.TotalAmount,

                    OrderNumber = cyberSaleDTO.OrderNumber,

                    CashPaid = cyberSaleDTO.CashPaid,

                    Change = cyberSaleDTO.Change,

                    PaymentStatus = 1,
                };

                context.CyberSales.Add(s);

                var SalesOrderDetailList = new List<CyberSaleDetail>();


                foreach (var item in cyberSaleDTO.ListOfCyberSaleDetails)
                {
                    var orderlist = new CyberSaleDetail();
                    {
                        orderlist.Id = Guid.NewGuid();

                        orderlist.OrderId = cyberSaleDTO.Id;

                        orderlist.ProductId = item.ProductId;

                        orderlist.Quantity = item.Quantity;

                        orderlist.SellingPrice = item.SellingPrice;

                        orderlist.Discount = item.Discount;

                        orderlist.Total = item.Total;

                        orderlist.OrderNumber = cyberSaleDTO.OrderNumber;

                        orderlist.CreateDate = cyberSaleDTO.OrderDate;

                        orderlist.CreatedBy = cyberSaleDTO.CreatedBy;

                        orderlist.PaymentStatus = 1;

                    };

                    context.CyberSaleDetails.Add(orderlist);

                    SalesOrderDetailList.Add(orderlist);
                }

                var result = await context.SaveChangesAsync() > 0;


                return cyberSaleDTO;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }

        public async Task<CyberSaleDTO> AddCreditServices(CyberSaleDTO cyberSaleDTO)
        {
            try
            {
                string order_number = SalesNumber.GenerateUniqueNumber();

                var orderNumber = ("SLN" + order_number);

                cyberSaleDTO.OrderNumber = orderNumber;

                cyberSaleDTO.Id = Guid.NewGuid();

                cyberSaleDTO.OrderDate = DateTime.Now;

                var s = new CyberSale
                {
                    Id = cyberSaleDTO.Id,

                    CustomerId = cyberSaleDTO.CustomerId,

                    OrderDate = cyberSaleDTO.OrderDate,

                    CreatedBy = cyberSaleDTO.CreatedBy,

                    TotalAmount = cyberSaleDTO.TotalAmount,

                    OrderNumber = cyberSaleDTO.OrderNumber,

                    CashPaid = cyberSaleDTO.CashPaid,

                    Change = cyberSaleDTO.Change,

                    PaymentStatus = 0,


                };

                context.CyberSales.Add(s);

                var SalesOrderDetailList = new List<CyberSaleDetail>();

                foreach (var item in cyberSaleDTO.ListOfCyberSaleDetails)
                {
                    var orderlist = new CyberSaleDetail();
                    {
                        orderlist.Id = Guid.NewGuid();

                        orderlist.OrderId = cyberSaleDTO.Id;

                        orderlist.ProductId = item.ProductId;

                        orderlist.Quantity = item.Quantity;

                        orderlist.SellingPrice = item.SellingPrice;

                        orderlist.Discount = item.Discount;

                        orderlist.Total = item.Total;

                        orderlist.OrderNumber = cyberSaleDTO.OrderNumber;

                        orderlist.CreateDate = cyberSaleDTO.OrderDate;

                        orderlist.CreatedBy = cyberSaleDTO.CreatedBy;

                        orderlist.CustomerId = cyberSaleDTO.CustomerId;

                        orderlist.PaymentStatus = 0;

                    };

                    context.CyberSaleDetails.Add(orderlist);

                    SalesOrderDetailList.Add(orderlist);
                }

                var result = await context.SaveChangesAsync() > 0;

                return cyberSaleDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }

        public List<CyberSaleDTO> GetAllPendingPayments()
        {
            try
            {
                var salesdetails = (from sales in context.CyberSales

                                    join u in context.AppUsers on sales.CreatedBy equals u.Id

                                    join c in context.Customers on sales.CustomerId equals c.Id

                                    where sales.PaymentStatus == 0

                                    select new CyberSaleDTO
                                    {
                                        Id = sales.Id,

                                        CustomerId = sales.CustomerId,

                                        PaymentStatus = sales.PaymentStatus,

                                        CreatedByName = u.FirstName + " " + u.LastName,

                                        CustomerName = c.FirstName + " " + c.LastName,

                                        OrderDate = sales.OrderDate,

                                        TotalAmount = sales.TotalAmount,

                                        OrderNumber = sales.OrderNumber,

                                        CashPaid = sales.CashPaid,

                                        Change = sales.Change,

                                    }).ToList();

                return salesdetails;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public List<CyberSaleDetailsDTO> GetAllSalesDetails()
        {
            var salesdetails = (from sd in context.CyberSaleDetails

                                join u in context.AppUsers on sd.CreatedBy equals u.Id

                                join s in context.CyberServices on sd.ProductId equals s.Id

                                select new CyberSaleDetailsDTO
                                {

                                    Id = sd.Id,

                                    OrderId = sd.OrderId,

                                    ProductId = sd.ProductId,

                                    Quantity = sd.Quantity,

                                    SellingPrice = sd.SellingPrice,

                                    Discount = sd.Discount,

                                    Total = sd.Total,

                                    OrderNumber = sd.OrderNumber,

                                    CreateDate = sd.CreateDate,

                                    CreatedBy = sd.CreatedBy,

                                    TaxAmount = sd.TaxAmount,

                                    PaymentStatus = sd.PaymentStatus,

                                    CreateByName = u.FirstName + " " + u.LastName,

                                    ServiceName = s.Name,



                                }).OrderByDescending(x => x.CreateDate).ToList();

            return salesdetails;
        }


        public List<CyberSaleDetailsDTO> GetAllSalesDetailsByOrderId(Guid Id)
        {
            var salesdetails = (from sd in context.CyberSaleDetails

                                join u in context.AppUsers on sd.CreatedBy equals u.Id

                                join s in context.CyberServices on sd.ProductId equals s.Id

                                where sd.OrderId==Id

                                select new CyberSaleDetailsDTO
                                {

                                    Id = sd.Id,

                                    OrderId = sd.OrderId,

                                    ProductId = sd.ProductId,

                                    Quantity = sd.Quantity,

                                    SellingPrice = sd.SellingPrice,

                                    Discount = sd.Discount,

                                    Total = sd.Total,

                                    OrderNumber = sd.OrderNumber,

                                    CreateDate = sd.CreateDate,

                                    CreatedBy = sd.CreatedBy,

                                    TaxAmount = sd.TaxAmount,

                                    PaymentStatus = sd.PaymentStatus,

                                    CreateByName = u.FirstName + " " + u.LastName,

                                    ServiceName = s.Name,

                                }).OrderByDescending(x => x.CreateDate).ToList();

            return salesdetails;
        }
    }
}

