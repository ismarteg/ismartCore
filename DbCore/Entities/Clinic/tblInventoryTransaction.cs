using Shared.Enums.clinics;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbCore.Entities.Clinic
{
    [Table(nameof(tblInventoryTransaction), Schema = "Clinic")]
    public class tblInventoryTransaction : BaseEntity
    {
        [ForeignKey(nameof(tblInventoryItem))]
        public Guid InventoryItemId { get; set; }
        public virtual tblInventoryItem InventoryItem { get; set; }

        public DateTime TransactionDate { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public InventoryTransactionType TransactionType { get; set; }

        [ForeignKey(nameof(tblVisits))]
        public Guid? RelatedVisitId { get; set; }
        public virtual tblVisits RelatedVisit { get; set; }


        [ForeignKey(nameof(tblVendor))]
        public Guid? VendorId { get; set; }
        public virtual tblVendor Vendor { get; set; }
        public string Notes { get; set; }
    }
}
