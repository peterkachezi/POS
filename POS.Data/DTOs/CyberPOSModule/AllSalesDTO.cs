using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Data.DTOs.CyberPOSModule
{
   public class AllSalesDTO
    {      
        public List<CyberSaleDetailsDTO>  cyberSaleDetailsDTOs { get; set; }
        public CyberSaleDTO  cyberSaleDTO { get; set; }
    }
}
