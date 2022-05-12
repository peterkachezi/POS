using POS.Data.DTOs.ExpenseTypeModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS.Data.Services.ExpenseTypeModule
{
    public interface IExpenseTypeService
    {
        Task<ExpenseTypeDTO> Create(ExpenseTypeDTO expenseTypeDTO);
        Task<ExpenseTypeDTO> Update(ExpenseTypeDTO expenseTypeDTO);
        Task<List<ExpenseTypeDTO>> GetAll();
        Task<ExpenseTypeDTO> GetById(Guid Id);
        Task<bool> Delete(Guid Id);
    }
}