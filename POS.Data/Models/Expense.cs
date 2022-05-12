using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Data.Models
{
    public partial class Expense
    {
        public Guid Id { get; set; }
        public Guid ExpenseTypeId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string FileAttchmentName { get; set; }
        public DateTime ExpenseDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
