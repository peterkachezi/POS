using Microsoft.EntityFrameworkCore;
using POS.Data.DTOs.SalesModule;
using POS.Data.Models;
using POS.Data.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data.Services.SalesModule
{
    public class SalesService : ISalesService
    {
        private ApplicationDbContext context;

        public SalesService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<SalesDTO> AddSalesOrder(SalesDTO salesOrederDTO)
        {
            try
            {
                string order_number = SalesNumber.GenerateUniqueNumber();

                var orderNumber = ("SLN" + order_number);

                salesOrederDTO.OrderNumber = orderNumber;

                salesOrederDTO.Id = Guid.NewGuid();

                salesOrederDTO.OrderDate = DateTime.Now;


                var s = new Sale
                {
                    Id = salesOrederDTO.Id,

                    CustomerId = salesOrederDTO.CustomerId,

                    OrderDate = salesOrederDTO.OrderDate,

                    CreatedBy = salesOrederDTO.CreatedBy,

                    TotalAmount = salesOrederDTO.TotalAmount,

                    OrderNumber = salesOrederDTO.OrderNumber,

                    CashPaid = salesOrederDTO.CashPaid,

                    Change = salesOrederDTO.Change,
                };

                context.Sales.Add(s);

                var SalesOrderDetailList = new List<SalesDetail>();


                foreach (var item in salesOrederDTO.ListOfSalesDetails)
                {
                    var orderlist = new SalesDetail();
                    {
                        orderlist.Id = Guid.NewGuid();

                        orderlist.OrderId = salesOrederDTO.Id;

                        orderlist.ProductId = item.ProductId;

                        orderlist.Quantity = item.Quantity;

                        orderlist.SellingPrice = item.SellingPrice;

                        orderlist.Discount = item.Discount;

                        orderlist.Total = item.Total;

                        orderlist.OrderNumber = salesOrederDTO.OrderNumber;

                        orderlist.CreateDate = salesOrederDTO.OrderDate;

                        orderlist.CreatedBy = salesOrederDTO.CreatedBy;

                        orderlist.PaymentStatus = 1;

                    };

                    context.SalesDetails.Add(orderlist);

                    SalesOrderDetailList.Add(orderlist);
                }

                var result = await context.SaveChangesAsync() > 0;

                if (result == true)
                {
                    foreach (var item in SalesOrderDetailList)
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            var k = context.Stocks.Where(x => x.Id == item.ProductId).FirstOrDefault();
                            {
                                k.Quantity = k.Quantity - item.Quantity;

                                context.SaveChanges();

                                transaction.Commit();
                            }
                        }

                    }
                }

                return salesOrederDTO;




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }

        public async Task<List<SalesDetailsDTO>> GetAllSalesDetails()
        {
            var salesdetails = (from sd in context.SalesDetails

                                join u in context.AppUsers on sd.CreatedBy equals u.Id

                                //join p in context.Products on sd.ProductId equals p.Id

                                //join pn in context.ProductNames on p.ProductNameId equals pn.Id

                                //join b in context.Brands on p.BrandId equals b.Id

                                //join uom in context.UOMs on p.UOMId equals uom.Id

                                select new SalesDetailsDTO
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

                                    //ProductName=b.Name +" " + pn.Name +" " + uom.Name +" " + uom.Unit

                                }).OrderByDescending(x => x.CreateDate).ToListAsync();

            return await salesdetails;
        }
    }
}
