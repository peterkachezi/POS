using POS.Data.DTOs.CyberPOSModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS.Data.Services.CyberPOSModule
{
    public interface ICyberPOSService
    {
        Task<CyberSaleDTO> AddCreditServices(CyberSaleDTO  cyberSaleDTO);
        Task<CyberSaleDTO> AddPaidServices(CyberSaleDTO  cyberSaleDTO);
        List<CyberSaleDetailsDTO> GetAllSalesDetails();
    }
}