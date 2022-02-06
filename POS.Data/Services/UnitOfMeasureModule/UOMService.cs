using Microsoft.EntityFrameworkCore;
using POS.Data.DTOs.UnitOfMeasureModule;
using POS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace POS.Data.Services.UnitOfMeasureModule
{
    public class UOMService : IUOMService
    {
        private readonly ApplicationDbContext context;

        public UOMService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<UOMDTO> Create(UOMDTO uOMDTO)
        {
            try
            {
                var s = new UOM
                {
                    Id = Guid.NewGuid(),

                    Name = uOMDTO.Name,

                    Unit = uOMDTO.Unit,

                    CreateDate = DateTime.Now,

                    CreatedBy = uOMDTO.CreatedBy,
                };

                context.UOMs.Add(s);

                await context.SaveChangesAsync();

                return uOMDTO;
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

                var s = await context.UOMs.FindAsync(Id);

                if (s != null)
                {
                    context.UOMs.Remove(s);

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

        public async Task<List<UOMDTO>> GetAll()
        {
            try
            {
                var uom = (from ufm in context.UOMs

                           join u in context.AppUsers on ufm.CreatedBy equals u.Id

                           select new UOMDTO
                           {
                               Id = ufm.Id,

                               Name = ufm.Name,

                               Unit = ufm.Unit,

                               CreateDate = ufm.CreateDate,

                               CreatedBy = ufm.CreatedBy,

                               CreatedByName = u.FirstName + " " + u.LastName,

                           }).OrderByDescending(x => x.CreateDate).ToListAsync();

                 return await uom;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<UOMDTO> GetById(Guid Id)
        {
            try
            {
                var packing = await context.UOMs.FindAsync(Id);

                return new UOMDTO
                {
                    Id = packing.Id,

                    Name = packing.Name,

                    Unit = packing.Unit,

                    CreateDate = packing.CreateDate,

                    CreatedBy = packing.CreatedBy
                };


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<UOMDTO> Update(UOMDTO uOMDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.UOMs.FindAsync(uOMDTO.Id);

                    if (s == null)
                    {
                        return null;
                    }
                    else
                    {
                        s.Name = uOMDTO.Name;

                        s.Unit = uOMDTO.Unit;                      

                        transaction.Commit();

                        await context.SaveChangesAsync();
                    }

                }
                return uOMDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
