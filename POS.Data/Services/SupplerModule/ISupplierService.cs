using POS.Data.DTOs.SupplerModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS.Data.Services.SupplerModule
{
    public interface ISupplierService
    {
        Task<SupplerDTO> Create(SupplerDTO supplerDTO);
        Task<SupplerDTO> Update(SupplerDTO supplerDTO);
        Task<bool> Delete(Guid Id);
        Task<List<SupplerDTO>> GetAll();
        Task<SupplerDTO> GetById(Guid Id);
    }
}