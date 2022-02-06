using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Data.Models
{
    public partial class Invoice
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public Guid OrderId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
