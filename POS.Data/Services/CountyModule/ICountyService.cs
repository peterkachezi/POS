using POS.Data.DTOs.CountyModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS.Data.Services.CountyModule
{
    public interface ICountyService
    {
        Task<List<CountyDTO>> GetAll();
    }
}