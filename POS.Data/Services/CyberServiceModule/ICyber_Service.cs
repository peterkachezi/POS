using POS.Data.DTOs.CyberServiceModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS.Data.Services.CyberServiceModule
{
    public interface ICyber_Service
    {
        Task<CyberServiceDTO> Create(CyberServiceDTO cyberServiceDTO);
        Task<CyberServiceDTO> Update(CyberServiceDTO cyberServiceDTO);
        Task<bool> Delete(Guid Id);
        Task<CyberServiceDTO> GetById(Guid Id);
        Task<List<CyberServiceDTO>> GetAll();
    }
}