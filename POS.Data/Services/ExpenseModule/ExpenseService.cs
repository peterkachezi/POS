using POS.Data.DTOs.ExpenseModule;
using POS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace POS.Data.Services.ExpenseModule
{
    public class ExpenseService : IExpenseService
    {
        private readonly ApplicationDbContext context;

        public ExpenseService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<ExpenseDTO> Create(ExpenseDTO expenseDTO)
        {
            try
            {
                var expense = new Expense
                {
                    Id = Guid.NewGuid(),

                    Description = expenseDTO.Description,

                    Amount = expenseDTO.Amount,

                    ExpenseDate = expenseDTO.ExpenseDate,

                    CreateDate = DateTime.Now,

                    CreatedBy = expenseDTO.CreatedBy,

                    FileAttchmentName = expenseDTO.FileAttchmentName,

                };

                context.Expenses.Add(expense);

                await context.SaveChangesAsync();

                return expenseDTO;
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

                var expense = await context.Expenses.FindAsync(Id);

                if (expense != null)
                {
                    context.Expenses.Remove(expense);

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

        public Task<List<ExpenseDTO>> GetAll()
        {
            try
            {
                var expense = (from e in context.Expenses

                               join u in context.AppUsers on e.CreatedBy equals u.Id

                               join et in context.ExpenseTypes on e.ExpenseTypeId equals et.Id

                               select new ExpenseDTO
                               {
                                   Id = e.Id,

                                   Amount = e.Amount,

                                   ExpenseTypeId = e.ExpenseTypeId,

                                   ExpenseTypeName = et.Name,

                                   Description = e.Description,

                                   FileAttchmentName = e.FileAttchmentName,

                                   CreateDate = e.CreateDate,

                                   ExpenseDate = e.ExpenseDate,

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

        public Task<ExpenseDTO> GetById(Guid Id)
        {
            try
            {
                var expense = (from e in context.Expenses

                               join u in context.AppUsers on e.CreatedBy equals u.Id

                               join et in context.ExpenseTypes on e.ExpenseTypeId equals et.Id

                               where Id == e.Id

                               select new ExpenseDTO
                               {
                                   Id = e.Id,

                                   Amount = e.Amount,

                                   ExpenseTypeId = e.ExpenseTypeId,

                                   ExpenseTypeName = et.Name,

                                   Description = e.Description,

                                   FileAttchmentName = e.FileAttchmentName,

                                   CreateDate = e.CreateDate,

                                   ExpenseDate = e.ExpenseDate,

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

        public async Task<ExpenseDTO> Update(ExpenseDTO expenseDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var expense = await context.Expenses.FindAsync(expenseDTO.Id);

                    if (expense != null)
                    {
                        expense.Amount = expenseDTO.Amount;

                        expense.Description = expenseDTO.Description;

                        transaction.Commit();

                        await context.SaveChangesAsync();
                    }
                    return expenseDTO;
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
