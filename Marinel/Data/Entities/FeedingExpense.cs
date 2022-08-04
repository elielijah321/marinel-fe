using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDraftWebsite.Data.Entities
{
    public class FeedingExpense
    {
        [Key]
        public int Id { get; set; }
        public long ExpenseAmount { get; set; }
        public string ExpenseReason { get; set; }

        public int FeedingInfoItemId { get; set; }
    }
}
