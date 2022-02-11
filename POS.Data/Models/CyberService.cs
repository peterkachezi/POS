﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Data.Models
{
   public partial class CyberService
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateDate { get; set; }
        [Required]
        [StringLength(128)]
        public string CreatedBy { get; set; }
    }
}
