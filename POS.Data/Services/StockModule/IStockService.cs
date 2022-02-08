using POS.Data.DTOs.StockModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS.Data.Services.StockModule
{
    public interface IStockService
    {
        Task<StockDTO> Create(StockDTO stockDTO);
        Task<StockDTO> Update(StockDTO stockDTO);
        Task<bool> Delete(Guid Id);
        Task<StockDTO> GetById(Guid Id);
        Task<List<StockDTO>> GetAll();
    }
}