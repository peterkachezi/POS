using POS.Data.DTOs.UnitOfMeasureModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data.Services.UnitOfMeasureModule
{
    public interface IUOMService
    {
        Task<UOMDTO> Create(UOMDTO uOMDTO);
        Task<bool> Delete(Guid Id);
        Task<UOMDTO> Update(UOMDTO uOMDTO);
        Task<UOMDTO> GetById(Guid Id);
        Task<List<UOMDTO>> GetAll();
    }
}
