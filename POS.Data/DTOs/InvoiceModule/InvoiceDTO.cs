using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Data.DTOs.InvoiceModule
{
   public class InvoiceDTO
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
