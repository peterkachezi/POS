using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Data.DTOs.UnitOfMeasureModule
{
    public class UOMDTO
    {
        public Guid Id { get; set; }   
        public string Name { get; set; }    
        public string Unit { get; set; }
        public string FullName => Name + " " + Unit;
        public DateTime CreateDate { get; set; }     
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
    }
}
