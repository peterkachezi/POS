using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Data.Models
{
    public partial class ExpenseType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
