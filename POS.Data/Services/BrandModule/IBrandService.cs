using POS.Data.DTOs.BrandModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS.Data.Services.BrandModule
{
    public interface IBrandService
    {
        Task<BrandDTO> Create(BrandDTO brandDTO);
        Task<BrandDTO> Update(BrandDTO brandDTO);
        Task<bool> Delete(Guid Id);
        Task<BrandDTO> GetById(Guid Id);
        Task<List<BrandDTO>> GetAll();
    }
}