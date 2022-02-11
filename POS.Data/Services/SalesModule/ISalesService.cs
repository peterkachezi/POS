using POS.Data.DTOs.SalesModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS.Data.Services.SalesModule
{
    public interface ISalesService
    {
        Task<SalesDTO> AddSalesOrder(SalesDTO salesOrederDTO);
        Task<List<SalesDetailsDTO>> GetAllSalesDetails();

    }
}