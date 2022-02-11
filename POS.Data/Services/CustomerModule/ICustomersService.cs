using POS.Data.DTOs.CustomerModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data.Services.CustomerModule
{
    public interface ICustomersService
    {
        Task<CustomerDTO> Create(CustomerDTO customerDTO);
        Task<CustomerDTO> Update(CustomerDTO customerDTO);
        Task<bool> Delete(Guid Id);
        Task<List<CustomerDTO>> GetAll();
        Task<CustomerDTO> GetById(Guid Id);
    }
}
