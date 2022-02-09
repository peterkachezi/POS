using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Data.DTOs.StockModule
{
   public  class StockDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string UpdatedBy { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal ExpectedProfit { get; set; }
        public string ProductName { get; set; }
        public string UOMName { get; set; }
        public string BrandName { get; set; }
        public string FullProductName => BrandName + " " + ProductName + " " + UOMName;
    }
}
