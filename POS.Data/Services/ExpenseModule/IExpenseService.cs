using POS.Data.DTOs.ExpenseModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data.Services.ExpenseModule
{
    public interface IExpenseService
    {
        Task<ExpenseDTO> Create(ExpenseDTO expenseDTO);
        Task<ExpenseDTO> Update(ExpenseDTO expenseDTO);
        Task<List<ExpenseDTO>> GetAll();
        Task<ExpenseDTO> GetById(Guid Id);
        Task<bool> Delete(Guid Id);
    }
}
