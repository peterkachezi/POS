using POS.Data.DTOs.ProductModule;
using POS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using POS.Data.Utils;

namespace POS.Data.Services.ProductModule
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<ProductDTO> Create(ProductDTO productDTO)
        {
            try
            {
                string code = SupplierNumber.GenerateUniqueNumber();

                productDTO.ProductCode = "P" + "" + code;

                var profit = productDTO.SellingPrice - productDTO.CostPrice;

                var s = new Product
                {
                    Id = Guid.NewGuid(),

                    ProductNameId = productDTO.ProductNameId,

                    CostPrice = productDTO.CostPrice,

                    SellingPrice = productDTO.SellingPrice,

                    ExpectedProfit = profit,

                    ProductCode = productDTO.ProductCode,

                    CreateDate = DateTime.Now,

                    CreatedBy = productDTO.CreatedBy,

                    UpdateBy = productDTO.CreatedBy,

                    UpdatedDate = DateTime.Now,

                    SupplierId = productDTO.SupplierId,

                    BrandId = productDTO.BrandId,

                    UOMId = productDTO.UOMId

                };

                context.Products.Add(s);

                await context.SaveChangesAsync();

                return productDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                bool result = false;

                var s = await context.Products.FindAsync(Id);

                if (s != null)
                {
                    context.Products.Remove(s);

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
        public async Task<List<ProductDTO>> GetAll()
        {
            try
            {
                var product = (from p in context.Products

                               join s in context.Suppliers on p.SupplierId equals s.Id

                               join u in context.AppUsers on p.CreatedBy equals u.Id

                               join b in context.Brands on p.BrandId equals b.Id

                               join prodname in context.ProductNames on p.ProductNameId equals prodname.Id

                               join uom in context.UOMs on p.UOMId equals uom.Id

                               select new ProductDTO
                               {
                                   Id = p.Id,

                                   ProductNameId = p.ProductNameId,

                                   ProductName = prodname.Name,

                                   CostPrice = p.CostPrice,

                                   SellingPrice = p.SellingPrice,

                                   ExpectedProfit = p.ExpectedProfit,

                                   ProductCode = p.ProductCode,

                                   UOMId = p.UOMId,

                                   UOMName = uom.Name + " " + uom.Unit,

                                   CreateDate = p.CreateDate,

                                   CreatedBy = p.CreatedBy,

                                   UpdateBy = p.CreatedBy,

                                   UpdatedDate = p.UpdatedDate,

                                   SupplierId = p.SupplierId,

                                   BrandId = p.BrandId,

                                   CreatedByName = u.FirstName + " " + u.LastName,

                                   SupplierName = s.FirstName + " " + s.LastName,

                                   BrandName = b.Name,

                               }).OrderByDescending(x => x.CreateDate).ToListAsync();

                return await product;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ProductDTO> GetByProductCode(string productCode)
        {
            try
            {
                var product = (from p in context.Products

                               join s in context.Suppliers on p.SupplierId equals s.Id

                               join u in context.AppUsers on p.CreatedBy equals u.Id

                               join b in context.Brands on p.BrandId equals b.Id

                               join prodname in context.ProductNames on p.ProductNameId equals prodname.Id

                               join uom in context.UOMs on p.UOMId equals uom.Id

                               where p.ProductCode == productCode

                               select new ProductDTO
                               {
                                   Id = p.Id,

                                   ProductNameId = p.ProductNameId,

                                   ProductName = prodname.Name,

                                   CostPrice = p.CostPrice,

                                   SellingPrice = p.SellingPrice,

                                   ExpectedProfit = p.ExpectedProfit,

                                   ProductCode = p.ProductCode,

                                   UOMId = p.UOMId,

                                   UOMName = uom.Name + " " + uom.Unit,

                                   CreateDate = p.CreateDate,

                                   CreatedBy = p.CreatedBy,

                                   UpdateBy = p.CreatedBy,

                                   UpdatedDate = p.UpdatedDate,

                                   SupplierId = p.SupplierId,

                                   BrandId = p.BrandId,

                                   CreatedByName = u.FirstName + " " + u.LastName,

                                   SupplierName = s.FirstName + " " + s.LastName,

                                   BrandName = b.Name,

                               }).FirstOrDefaultAsync();

                return await product;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ProductDTO> GetById(Guid Id)
        {
            try
            {
                var product = (from p in context.Products

                               join s in context.Suppliers on p.SupplierId equals s.Id

                               join u in context.AppUsers on p.CreatedBy equals u.Id

                               join b in context.Brands on p.BrandId equals b.Id

                               join prodname in context.ProductNames on p.ProductNameId equals prodname.Id

                               join uom in context.UOMs on p.UOMId equals uom.Id

                               where p.Id == Id

                               select new ProductDTO
                               {
                                   Id = p.Id,

                                   ProductNameId = p.ProductNameId,

                                   ProductName = prodname.Name,

                                   CostPrice = p.CostPrice,

                                   SellingPrice = p.SellingPrice,

                                   ExpectedProfit = p.ExpectedProfit,

                                   ProductCode = p.ProductCode,

                                   UOMId = p.UOMId,

                                   UOMName = uom.Name + " " + uom.Unit,

                                   CreateDate = p.CreateDate,

                                   CreatedBy = p.CreatedBy,

                                   UpdateBy = p.CreatedBy,

                                   UpdatedDate = p.UpdatedDate,

                                   SupplierId = p.SupplierId,

                                   BrandId = p.BrandId,

                                   CreatedByName = u.FirstName + " " + u.LastName,

                                   SupplierName = s.FirstName + " " + s.LastName,

                                   BrandName = b.Name,

                               }).FirstOrDefaultAsync();

                return await product;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ProductDTO> Update(ProductDTO productDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.Products.FindAsync(productDTO.Id);
                    {

                        s.ProductNameId = productDTO.ProductNameId;

                        s.CostPrice = productDTO.CostPrice;

                        s.SellingPrice = productDTO.SellingPrice;

                        s.ExpectedProfit = productDTO.ExpectedProfit;

                        s.UOMId = productDTO.UOMId;

                        s.UpdatedDate = DateTime.Now;

                        s.SupplierId = productDTO.SupplierId;

                        s.BrandId = productDTO.BrandId;

                        s.UpdateBy = productDTO.UpdateBy;

                    };

                    transaction.Commit();

                    await context.SaveChangesAsync();
                }

                return productDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ProductNameDTO> CreateProductName(ProductNameDTO productNameDTO)
        {
            try
            {
                var s = new ProductName
                {
                    Id = Guid.NewGuid(),

                    Name = productNameDTO.Name.Trim(),

                    CreatedBy = productNameDTO.CreatedBy,

                    CreateDate = DateTime.Now,
                };

                context.ProductNames.Add(s);

                await context.SaveChangesAsync();

                return productNameDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<List<ProductNameDTO>> GetAllProductName()
        {
            try
            {
                var productNames = (from p in context.ProductNames


                                    join u in context.AppUsers on p.CreatedBy equals u.Id

                                    select new ProductNameDTO
                                    {
                                        Id = p.Id,

                                        Name = p.Name,

                                        CreatedBy = p.CreatedBy,

                                        CreateDate = p.CreateDate,

                                        CreatedByName = u.FirstName + " " + u.LastName,

                                    }).OrderByDescending(x => x.CreateDate).ToListAsync();

                return await productNames;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ProductNameDTO> GetProductNamesById(Guid Id)
        {
            var brand = await context.ProductNames.FindAsync(Id);

            return new ProductNameDTO
            {
                Id = brand.Id,

                Name = brand.Name,

                CreatedBy = brand.CreatedBy,

                CreateDate = brand.CreateDate,
            };
        }
        public async Task<ProductNameDTO> UpdateProductName(ProductNameDTO productNameDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.ProductNames.FindAsync(productNameDTO.Id);
                    {
                        s.Name = productNameDTO.Name.Trim();

                    };

                    transaction.Commit();

                    await context.SaveChangesAsync();
                }

                return productNameDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<bool> DeleteProductName(Guid Id)
        {
            try
            {
                bool result = false;

                var s = await context.ProductNames.FindAsync(Id);

                if (s != null)
                {
                    context.ProductNames.Remove(s);

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

    }
}
