using POS.Data.DTOs.PaymentTypeModule;
using POS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace POS.Data.Services.PaymentTypeModule
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly ApplicationDbContext context;

        public PaymentTypeService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<PaymentTypeDTO> Create(PaymentTypeDTO paymentTypeDTO)
        {
            try
            {
                var s = new PaymentType
                {
                    Id = Guid.NewGuid(),

                    Name = paymentTypeDTO.Name.Substring(0, 1).ToUpper() + paymentTypeDTO.Name.Substring(1).ToLower().Trim(),

                    CreatedBy = paymentTypeDTO.CreatedBy,

                    CreateDate = DateTime.Now,
                };

                context.PaymentTypes.Add(s);

                await context.SaveChangesAsync();

                return paymentTypeDTO;
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

                var s = await context.PaymentTypes.FindAsync(Id);

                if (s != null)
                {
                    context.PaymentTypes.Remove(s);

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

        public async Task<List<PaymentTypeDTO>> GetAll()
        {
            try
            {
                var brands = (from b in context.PaymentTypes

                              join u in context.AppUsers on b.CreatedBy equals u.Id

                              select new PaymentTypeDTO
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

        public async Task<PaymentTypeDTO> GetById(Guid Id)
        {
            try
            {
                var brands = (from b in context.PaymentTypes

                              join u in context.AppUsers on b.CreatedBy equals u.Id

                              where b.Id == Id

                              select new PaymentTypeDTO
                              {
                                  Id = b.Id,

                                  Name = b.Name,

                                  CreatedBy = b.CreatedBy,

                                  CreateDate = b.CreateDate,

                                  CreatedByName = u.FirstName + " " + u.LastName,

                              }).FirstOrDefaultAsync();

                return await brands;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<PaymentTypeDTO> Update(PaymentTypeDTO paymentTypeDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.PaymentTypes.FindAsync(paymentTypeDTO.Id);
                    {
                        s.Name = paymentTypeDTO.Name.Substring(0, 1).ToUpper() + paymentTypeDTO.Name.Substring(1).ToLower().Trim();

                    };

                    transaction.Commit();

                    await context.SaveChangesAsync();
                }

                return paymentTypeDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
