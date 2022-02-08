using POS.Data.DTOs.StockModule;
using POS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace POS.Data.Services.StockModule
{
    public class StockService : IStockService
    {
        private readonly ApplicationDbContext context;

        public StockService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<StockDTO> Create(StockDTO stockDTO)
        {
           
            bool isExist = context.Stocks.Any(x => x.ProductId == stockDTO.ProductId);

            if (isExist == false)
            {
                var s = new Stock
                {
                    Id = Guid.NewGuid(),

                    ProductId = stockDTO.ProductId,

                    ProductCode = stockDTO.ProductCode,

                    Quantity = stockDTO.Quantity,

                    CreateDate = DateTime.Now,

                    UpdatedDate = DateTime.Now,

                    CreatedBy = stockDTO.CreatedBy,

                    UpdatedBy = stockDTO.CreatedBy,
                };

                context.Stocks.Add(s);

                await context.SaveChangesAsync();
            }
            else if(isExist==true)
            {
                var findStockId = await context.Stocks.Where(x => x.ProductId == stockDTO.ProductId).FirstOrDefaultAsync();

                using (var transaction = context.Database.BeginTransaction())
                {
                    var u = await context.Stocks.FindAsync(findStockId.Id);

                    u.Quantity = stockDTO.Quantity + findStockId.Quantity;

                    transaction.Commit();

                    await context.SaveChangesAsync();
                }
            }


            return stockDTO;
        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                bool result = false;

                var s = await context.Stocks.FindAsync(Id);

                if (s != null)
                {
                    context.Stocks.Remove(s);

                    await context.SaveChangesAsync();

                    return true;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<List<StockDTO>> GetAll()
        {
            try
            {
                var stock = (from s in context.Stocks

                             join u in context.AppUsers on s.CreatedBy equals u.Id

                             join p in context.Products on s.ProductId equals p.Id

                             join pn in context.ProductNames on p.ProductNameId equals pn.Id

                             select new StockDTO
                             {
                                 Id = s.Id,

                                 ProductId = s.ProductId,

                                 ProductCode = s.ProductCode,

                                 Quantity = s.Quantity,

                                 CreateDate = s.CreateDate,

                                 UpdatedDate = s.UpdatedDate,

                                 CreatedBy = s.CreatedBy,

                                 CreatedByName = u.FirstName + " " + u.LastName,

                                 UpdatedBy = s.CreatedBy,

                                 SellingPrice = p.SellingPrice,

                                 CostPrice = p.CostPrice,

                                 ExpectedProfit = p.ExpectedProfit,

                                 ProductNames = pn.Name,

                             }).OrderByDescending(x => x.CreateDate).ToListAsync();

                return await stock;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }




        public async Task<StockDTO> GetById(Guid Id)
        {
            try
            {
                var stock = await context.Stocks.FindAsync(Id);

                return new StockDTO
                {
                    Id = stock.Id,

                    ProductId = stock.ProductId,

                    ProductCode = stock.ProductCode,

                    Quantity = stock.Quantity,

                    CreateDate = stock.CreateDate,

                    UpdatedDate = stock.UpdatedDate,

                    CreatedBy = stock.CreatedBy,

                    UpdatedBy = stock.CreatedBy,
                };
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public Task<StockDTO> Update(StockDTO stockDTO)
        {
            throw new NotImplementedException();
        }
    }
}
