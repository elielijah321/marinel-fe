using System;

namespace SchoolDraftWebsite.Data.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public ExpenseType Type { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal Total { get; set; }
    }
}
