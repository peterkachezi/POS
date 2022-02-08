using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Data.Models
{
  public partial  class Stock
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        [Required]
        [StringLength(128)]
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public static implicit operator bool(Stock v)
        {
            throw new NotImplementedException();
        }
    }
}
