using System.ComponentModel.DataAnnotations.Schema;

namespace DbCore.Entities.Clinic
{
    [Table(nameof(tblInventoryItemPriceHistory), Schema = "Clinic")]
    public class tblInventoryItemPriceHistory : BaseEntity
    {
        public Guid InventoryItemId { get; set; }

        [ForeignKey("InventoryItemId")]
        public virtual tblInventoryItem InventoryItem { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SellingPrice { get; set; }

        public string? ChangeReason { get; set; }
    }
}
