using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolDraftWebsite.Data.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
        public DateTime DOB { get; set; }

        public virtual Class Class { get; set; }
    }
}
