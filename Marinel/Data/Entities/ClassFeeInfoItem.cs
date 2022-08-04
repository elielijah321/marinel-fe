using SchoolDraftWebsite.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolDraftWebsite.Data.Entities
{
    public class ClassFeeInfoItem
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int NumberEnrolled { get; set; }
        public int NumberPaid { get; set; }
        public long AmountReceived { get; set; }

        [NotMapped]
        public virtual Term Term { get; set; }
    }
}
