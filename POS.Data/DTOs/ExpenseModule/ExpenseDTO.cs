using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Data.DTOs.ExpenseModule
{
    public class ExpenseDTO
    {
        public Guid Id { get; set; }
        public Guid ExpenseTypeId { get; set; }
        public string ExpenseTypeName { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string FileAttchmentName { get; set; }
        public DateTime ExpenseDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public string NewExpenseDate { get { return ExpenseDate.ToShortDateString(); } }
    }
}
