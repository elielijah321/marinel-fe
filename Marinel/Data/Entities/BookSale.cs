using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolDraftWebsite.Data.Entities
{
    public class BookSale
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int NoOfBooksSold { get; set; }
        public decimal Revenue { get; set; }
        public int InventoryItemId { get; set; }

        public virtual InventoryItem InventoryItem { get; set; }
    }
}


