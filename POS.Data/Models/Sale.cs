using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Data.Models
{
    public partial class Sale
    {
        public Sale()
        {
            SalesDetails = new HashSet<SalesDetail>();
        }

        public Guid Id { get; set; }

        public Guid? CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        [StringLength(128)]
        public string CreatedBy { get; set; }

        public decimal TotalAmount { get; set; }

        [Required]
        public string OrderNumber { get; set; }

        public decimal Change { get; set; }

        public decimal CashPaid { get; set; }           

        public virtual Customer Customer { get; set; }
        public virtual ICollection<SalesDetail> SalesDetails { get; set; }
    }
}
