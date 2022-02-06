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

                string code = GetProductCode.GenerateUniqueNumber();

                productDTO.ProductCode = "S" + "" + code;

                var s = new Product
                {
                    Id = Guid.NewGuid(),

                    Name = productDTO.Name,

                    ProductCode = productDTO.ProductCode,

                    CreateDate = DateTime.Now,

                    CreatedBy = productDTO.CreatedBy,

                    UpdateBy = productDTO.CreatedBy,

                    UpdatedDate = DateTime.Now,

                    SupplierId = productDTO.SupplierId,

                    BrandId = productDTO.BrandId

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

                var s = await context.Brands.FindAsync(Id);

                if (s != null)
                {
                    context.Brands.Remove(s);

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

                               select new ProductDTO
                               {
                                   Id = p.Id,

                                   Name = p.Name,

                                   ProductCode = p.ProductCode,

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

        public async Task<ProductDTO> GetById(Guid Id)
        {
            try
            {
                var product = await context.Products.FindAsync(Id);

                return new ProductDTO
                {
                    Id = product.Id,

                    Name = product.Name,

                    ProductCode = product.ProductCode,

                    CreateDate = product.CreateDate,

                    CreatedBy = product.CreatedBy,

                    UpdateBy = product.CreatedBy,

                    UpdatedDate = product.UpdatedDate,

                    SupplierId = product.SupplierId,

                    BrandId = product.BrandId,
                };
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

                        s.Name = productDTO.Name;

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
    }
}
