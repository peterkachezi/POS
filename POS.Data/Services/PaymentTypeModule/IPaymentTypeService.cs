using POS.Data.DTOs.PaymentTypeModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS.Data.Services.PaymentTypeModule
{
    public interface IPaymentTypeService
    {
        Task<PaymentTypeDTO> Create(PaymentTypeDTO paymentTypeDTO);
        Task<PaymentTypeDTO> Update(PaymentTypeDTO paymentTypeDTO);
        Task<bool> Delete(Guid Id);
        Task<PaymentTypeDTO> GetById(Guid Id);
        Task<List<PaymentTypeDTO>> GetAll();
    }
}