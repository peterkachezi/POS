using POS.Data.DTOs.ExpenseTypeModule;
using POS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace POS.Data.Services.ExpenseTypeModule
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly ApplicationDbContext context;

        public ExpenseTypeService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<ExpenseTypeDTO> Create(ExpenseTypeDTO expenseTypeDTO)
        {
            try
            {
                var expense = new ExpenseType
                {
                    Id = Guid.NewGuid(),

                    Name = expenseTypeDTO.Name,

                    CreateDate = DateTime.Now,

                    CreatedBy = expenseTypeDTO.CreatedBy,

                };

                context.ExpenseTypes.Add(expense);

                await context.SaveChangesAsync();

                return expenseTypeDTO;
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
                var result = false;

                var expense = await context.ExpenseTypes.FindAsync(Id);

                if (expense != null)
                {
                    context.ExpenseTypes.Remove(expense);

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

        public Task<List<ExpenseTypeDTO>> GetAll()
        {
            try
            {
                var expense = (from e in context.ExpenseTypes

                               join u in context.AppUsers on e.CreatedBy equals u.Id

                               select new ExpenseTypeDTO
                               {
                                   Id = e.Id,

                                   Name = e.Name,

                                   CreateDate = e.CreateDate,

                                   CreatedBy = e.CreatedBy,

                                   CreatedByName = u.FirstName + " " + u.LastName,


                               }).ToListAsync();

                return expense;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }

        public Task<ExpenseTypeDTO> GetById(Guid Id)
        {
            try
            {
                var expense = (from e in context.ExpenseTypes

                               join u in context.AppUsers on e.CreatedBy equals u.Id

                               where Id == e.Id

                               select new ExpenseTypeDTO
                               {
                                   Id = e.Id,

                                   Name = e.Name,

                                   CreateDate = e.CreateDate,

                                   CreatedBy = e.CreatedBy,

                                   CreatedByName = u.FirstName + " " + u.LastName,


                               }).FirstOrDefaultAsync();

                return expense;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ExpenseTypeDTO> Update(ExpenseTypeDTO expenseTypeDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var expense = await context.ExpenseTypes.FindAsync(expenseTypeDTO.Id);

                    if (expense != null)
                    {
                        expense.Name = expenseTypeDTO.Name;

                        transaction.Commit();

                        await context.SaveChangesAsync();
                    }
                    return expenseTypeDTO;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }
    }
}
