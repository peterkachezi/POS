using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Data.DTOs.ProductModule
{
   public class ProductDTO
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }  
        public string CreatedByName { get; set; }  
        public string UpdateBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Status { get; set; }
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; }
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
 
    }
}
