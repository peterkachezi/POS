using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Data.Models
{
    public partial class CustomerOrder
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public string OrderNotes { get; set; }

        public Guid CustomerId { get; set; }

        public int Quantity { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
