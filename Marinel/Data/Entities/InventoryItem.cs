using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolDraftWebsite.Data.Entities
{
    public class InventoryItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int InventoryTypeId { get; set; }
        public int Quantity { get; set; }
        public long CostPerUnit { get; set; }
        public long SellingPrice { get; set; }
        public virtual InventoryType InventoryType { get; set; }
    }
}
