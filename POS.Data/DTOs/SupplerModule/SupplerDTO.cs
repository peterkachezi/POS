using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Data.DTOs.SupplerModule
{
    public class SupplerDTO
    {
        public Guid Id { get; set; }  
        public string FirstName { get; set; }     
        public string MiddleName { get; set; }     
        public string LastName { get; set; }
        public string FullName => FirstName + " " + MiddleName + " " + LastName;
        public string Email { get; set; }     
        public string PhoneNumber { get; set; }        
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public string SupplierCode { get; set; }
        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public DateTime CreateDate { get; set; }

        public string Town { get; set; }
    }
}
