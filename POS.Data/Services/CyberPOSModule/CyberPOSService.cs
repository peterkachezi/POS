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
        private ApplicationDbContext context;

        public CyberPOSService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<CyberSaleDTO> AddSalesOrder(CyberSaleDTO cyberSaleDTO)
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

        public async Task<List<CyberSaleDetailsDTO>> GetAllSalesDetails()
        {
            var salesdetails = (from sd in context.CyberSaleDetails

                                join u in context.AppUsers on sd.CreatedBy equals u.Id

                                //join p in context.Products on sd.ProductId equals p.Id

                                //join pn in context.ProductNames on p.ProductNameId equals pn.Id

                                //join b in context.Brands on p.BrandId equals b.Id

                                //join uom in context.UOMs on p.UOMId equals uom.Id

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

                                    //ProductName=b.Name +" " + pn.Name +" " + uom.Name +" " + uom.Unit

                                }).OrderByDescending(x => x.CreateDate).ToListAsync();

            return await salesdetails;
        }
    }
}

