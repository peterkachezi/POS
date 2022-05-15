using POS.Data.DTOs.CyberPOSModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS.Data.Services.CyberPOSModule
{
    public interface ICyberPOSService
    {
        Task<CyberSaleDTO> AddSalesOrder(CyberSaleDTO  cyberSaleDTO);
        Task<List<CyberSaleDetailsDTO>> GetAllSalesDetails();
    }
}