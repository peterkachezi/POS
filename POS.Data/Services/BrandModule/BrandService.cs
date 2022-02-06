using POS.Data.DTOs.BrandModule;
using POS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace POS.Data.Services.BrandModule
{
    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext context;

        public BrandService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<BrandDTO> Create(BrandDTO brandDTO)
        {
            try
            {
                var s = new Brand
                {
                    Id = Guid.NewGuid(),

                    Name = brandDTO.Name.Substring(0, 1).ToUpper() + brandDTO.Name.Substring(1).ToLower().Trim(),

                    CreatedBy = brandDTO.CreatedBy,

                    CreateDate = DateTime.Now,
                };

                context.Brands.Add(s);

                await context.SaveChangesAsync();

                return brandDTO;
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

        public async Task<List<BrandDTO>> GetAll()
        {
            try
            {
                var brands = (from b in context.Brands


                              join u in context.AppUsers on b.CreatedBy equals u.Id

                              select new BrandDTO
                              {
                                  Id = b.Id,

                                  Name = b.Name,

                                  CreatedBy = b.CreatedBy,

                                  CreateDate = b.CreateDate,

                                  CreatedByName = u.FirstName + " " + u.LastName,

                              }).OrderByDescending(x => x.CreateDate).ToListAsync();

                return await brands;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<BrandDTO> GetById(Guid Id)
        {
            var brand = await context.Brands.FindAsync(Id);

            return new BrandDTO
            {
                Id = brand.Id,

                Name = brand.Name,

                CreatedBy = brand.CreatedBy,

                CreateDate = brand.CreateDate,
            };
        }

        public async Task<BrandDTO> Update(BrandDTO brandDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.Brands.FindAsync(brandDTO.Id);
                    {
                        s.Name = brandDTO.Name.Substring(0, 1).ToUpper() + brandDTO.Name.Substring(1).ToLower().Trim();

                    };

                    transaction.Commit();

                    await context.SaveChangesAsync();
                }

                return brandDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
