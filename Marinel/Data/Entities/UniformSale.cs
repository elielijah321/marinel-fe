using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolDraftWebsite.Data.Entities
{
    public class UniformSale
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime StockDate { get; set; }
        public int Quantity { get; set; }
        public long Revenue { get; set; }
        public bool PaidInFull { get; set; }
        public bool Received { get; set; }
        public string Notes { get; set; }

        public int InventoryItemId { get; set; }
        public int StudentId { get; set; }

        public virtual InventoryItem InventoryItem { get; set; }
        public virtual Student Student { get; set; }
    }
}


