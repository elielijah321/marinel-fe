using SchoolDraftWebsite.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolDraftWebsite.Data.Entities
{
    public class FeedingInfoItem
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalCollected { get; set; }
        public int NumberOfStudents { get; set; }
        public decimal Revenue { get; set; }

        [ForeignKey("FeedingInfoItemId")]
        public ICollection<FeedingExpense> FeedingExpenses { get; set; }

        [NotMapped]
        public virtual Term Term { get; set; }
    }
}
