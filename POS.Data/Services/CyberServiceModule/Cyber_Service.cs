using POS.Data.DTOs.CyberServiceModule;
using POS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace POS.Data.Services.CyberServiceModule
{
    public class Cyber_Service : ICyber_Service
    {
        private ApplicationDbContext context;

        public Cyber_Service(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<CyberServiceDTO> Create(CyberServiceDTO cyberServiceDTO)
        {
            try
            {
                var s = new CyberService
                {
                    Id = Guid.NewGuid(),

                    Name = cyberServiceDTO.Name,

                    Amount = cyberServiceDTO.Amount,

                    CreateDate = DateTime.Now,

                    CreatedBy = cyberServiceDTO.CreatedBy,
                };

                context.CyberServices.Add(s);

                await context.SaveChangesAsync();

                return cyberServiceDTO;
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

                var s = await context.CyberServices.FindAsync(Id);

                if (s != null)
                {
                    context.CyberServices.Remove(s);

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

        public async Task<List<CyberServiceDTO>> GetAll()
        {
            var services = (from s in context.CyberServices

                            join u in context.AppUsers on s.CreatedBy equals u.Id

                            select new CyberServiceDTO
                            {
                                Id = s.Id,

                                Name = s.Name,

                                Amount = s.Amount,

                                CreateDate = s.CreateDate,

                                CreatedBy = s.CreatedBy,

                                CreatedByName = u.FirstName + " " + u.LastName,

                            }).OrderByDescending(x => x.CreateDate).ToListAsync();

            return await services;
        }

        public async Task<CyberServiceDTO> GetById(Guid Id)
        {
            var services = (from s in context.CyberServices

                            join u in context.AppUsers on s.CreatedBy equals u.Id

                            where s.Id == Id

                            select new CyberServiceDTO
                            {
                                Id = s.Id,

                                Name = s.Name,

                                Amount = s.Amount,

                                CreateDate = s.CreateDate,

                                CreatedBy = s.CreatedBy,

                                CreatedByName = u.FirstName + " " + u.LastName,

                            }).FirstOrDefaultAsync();

            return await services;
        }

        public async Task<CyberServiceDTO> Update(CyberServiceDTO cyberServiceDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.CyberServices.FindAsync(cyberServiceDTO.Id);

                    if (s != null)
                    {

                        s.Name = cyberServiceDTO.Name;

                        s.Amount = cyberServiceDTO.Amount;

                        transaction.Commit();

                        await context.SaveChangesAsync();

                    }
                    else
                    {
                        return null;
                    }
                }

                return cyberServiceDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
