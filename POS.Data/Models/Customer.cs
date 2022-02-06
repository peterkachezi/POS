using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Data.Models
{
    public partial class Customer
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
        public string CustomerNumber { get; set; }

        [StringLength(100)]
        public string DeliveryLocation { get; set; }

        [StringLength(100)]
        public string Apartment { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
