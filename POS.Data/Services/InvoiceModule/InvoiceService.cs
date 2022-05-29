using POS.Data.DTOs.InvoiceModule;
using POS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data.Services.InvoiceModule
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext context;
        public InvoiceService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<InvoiceDTO> Create(InvoiceDTO invoiceDTO)
        {
            try
            {
                var s = new Invoice
                {
                    Id = Guid.NewGuid(),

                    OrderId = invoiceDTO.OrderId,

                    CustomerId = invoiceDTO.CustomerId,

                    CreatedBy = invoiceDTO.CreatedBy,

                    CreateDate = DateTime.Now,
                };

                context.Invoices.Add(s);

                await context.SaveChangesAsync();

                return invoiceDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
