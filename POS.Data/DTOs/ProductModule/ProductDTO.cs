using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Data.DTOs.ProductModule
{
   public class ProductDTO
    {
        public Guid Id { get; set; }
        public Guid ProductNameId { get; set; }
        public string ProductName { get; set; }
        public string FullProductName => BrandName + " " + ProductName + " " + UOMName; 
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal ExpectedProfit { get; set; }
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
        public Guid UOMId { get; set; }
        public string UOMName { get; set; }   
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
 
    }
}
