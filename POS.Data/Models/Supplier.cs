using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Data.Models
{
    public partial class Supplier
    {
        public Guid Id { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(100)]
        public string SupplierCode { get; set; }
        public DateTime CreateDate { get; set; }
        public int CountyId { get; set; }
        public string Town { get; set; }
    }
}
