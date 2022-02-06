using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Data.Models
{
    public partial class Product
    {
 
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int ReOrderLevel { get; set; }
        public decimal ExpectedProfit { get; set; }
        public DateTime CreateDate { get; set; }
        [Required]
        [StringLength(128)]
        public string CreatedBy { get; set; }
        [Required]
        [StringLength(128)]
        public string UpdateBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Status { get; set; }
        public Guid PackagingId { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }

        public virtual ICollection<SalesDetail> SalesDetails { get; set; }
    }
}
