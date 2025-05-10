using System.ComponentModel.DataAnnotations.Schema;

namespace DbCore.Entities.Clinic
{
    [Table(nameof(tblInventoryTransaction), Schema = "Clinic")]
    public class tblInventoryTransaction : BaseEntity
    {
        public Guid InventoryItemId { get; set; }

        [ForeignKey("InventoryItemId")]
        public virtual tblInventoryItem InventoryItem { get; set; }

        public DateTime TransactionDate { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public InventoryTransactionType TransactionType { get; set; }

        public Guid? RelatedVisitId { get; set; }

        [ForeignKey("RelatedVisitId")]
        public virtual tblVisits RelatedVisit { get; set; }

        public Guid? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public virtual Vendor Vendor { get; set; }
        public string Notes { get; set; }
    }
}
