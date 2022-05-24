using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Data.Models
{
    public partial class PaymentType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public string CreatedBy { get; set; }
    }
}
