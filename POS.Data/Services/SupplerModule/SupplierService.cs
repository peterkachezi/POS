using POS.Data.DTOs.SupplerModule;
using POS.Data.Models;
using POS.Data.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace POS.Data.Services.SupplerModule
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext context;

        public SupplierService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<SupplerDTO> Create(SupplerDTO supplerDTO)
        {
            try
            {
                string code = SupplierNumber.GenerateUniqueNumber();

                supplerDTO.SupplierCode = "S" + "" + code;

                var s = new Supplier
                {
                    Id = Guid.NewGuid(),

                    FirstName = supplerDTO.FirstName.Substring(0, 1).ToUpper() + supplerDTO.FirstName.Substring(1).ToLower().Trim(),

                    MiddleName = supplerDTO.MiddleName.Substring(0, 1).ToUpper() + supplerDTO.MiddleName.Substring(1).ToLower().Trim(),

                    LastName = supplerDTO.LastName.Substring(0, 1).ToUpper() + supplerDTO.LastName.Substring(1).ToLower().Trim(),

                    Email = supplerDTO.Email,

                    PhoneNumber = supplerDTO.PhoneNumber,

                    CountyId = supplerDTO.CountyId,

                    CreateDate = DateTime.Now,

                    CreatedBy = supplerDTO.CreatedBy,

                    SupplierCode = supplerDTO.SupplierCode,

                    Town = supplerDTO.Town,
                };

                context.Suppliers.Add(s);

                await context.SaveChangesAsync();

                return supplerDTO;
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

                var s = await context.Suppliers.FindAsync(Id);

                if (s != null)
                {
                    context.Suppliers.Remove(s);

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

        public async Task<List<SupplerDTO>> GetAll()
        {
            try
            {
                var suppliers = (from s in context.Suppliers

                                 join u in context.AppUsers on s.CreatedBy equals u.Id

                                 join c in context.Counties on s.CountyId equals c.Id

                                 select new SupplerDTO
                                 {
                                     Id = s.Id,

                                     FirstName = s.FirstName,

                                     MiddleName = s.MiddleName,

                                     LastName = s.LastName,

                                     Email = s.Email,

                                     PhoneNumber = s.PhoneNumber,

                                     CountyId = s.CountyId,

                                     CountyName = c.Name,

                                     CreateDate = DateTime.Now,

                                     CreatedBy = s.CreatedBy,

                                     SupplierCode = s.SupplierCode,

                                     Town = s.Town,

                                     CreatedByName = u.FirstName + " " + u.LastName,

                                 }).OrderByDescending(x => x.CreateDate).ToListAsync();

                return await suppliers;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<SupplerDTO> GetById(Guid Id)
        {
            try
            {
                var supplier = await context.Suppliers.FindAsync(Id);

                return new SupplerDTO
                {
                    Id = supplier.Id,

                    FirstName = supplier.FirstName,

                    MiddleName = supplier.MiddleName,

                    LastName = supplier.LastName,

                    Email = supplier.Email,

                    PhoneNumber = supplier.PhoneNumber,

                    CountyId = supplier.CountyId,

                    CreateDate = DateTime.Now,

                    CreatedBy = supplier.CreatedBy,

                    SupplierCode = supplier.SupplierCode,

                    Town = supplier.Town,
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<SupplerDTO> Update(SupplerDTO supplerDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.Suppliers.FindAsync(supplerDTO.Id);
                    {
                        s.FirstName = supplerDTO.FirstName.Substring(0, 1).ToUpper() + supplerDTO.FirstName.Substring(1).ToLower().Trim();

                        s.MiddleName = supplerDTO.MiddleName.Substring(0, 1).ToUpper() + supplerDTO.MiddleName.Substring(1).ToLower().Trim();

                        s.LastName = supplerDTO.LastName.Substring(0, 1).ToUpper() + supplerDTO.LastName.Substring(1).ToLower().Trim();

                        s.Email = supplerDTO.Email;

                        s.PhoneNumber = supplerDTO.PhoneNumber;

                        s.CountyId = supplerDTO.CountyId;

                        s.Town = supplerDTO.Town;
                    };

                    transaction.Commit();

                    await context.SaveChangesAsync();
                }

                return supplerDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
