using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Data.Models
{
    public partial class Customer
    {

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CreatedBy { get; set; }
        public string CustomerNumber { get; set; }
        public string DeliveryLocation { get; set; }

        public string Apartment { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
