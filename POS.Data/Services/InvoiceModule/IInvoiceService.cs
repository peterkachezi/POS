using POS.Data.DTOs.InvoiceModule;
using System.Threading.Tasks;

namespace POS.Data.Services.InvoiceModule
{
    public interface IInvoiceService
    {
        Task<InvoiceDTO> Create(InvoiceDTO invoiceDTO);
    }
}