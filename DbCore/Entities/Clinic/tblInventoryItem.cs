using System.ComponentModel.DataAnnotations.Schema;

namespace DbCore.Entities.Clinic
{
    [Table(nameof(tblInventoryItem), Schema = "Clinic")]
    public class tblInventoryItem : BaseEntity
    {
        [Required, MaxLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int CurrentStock { get; set; }

        public virtual ICollection<tblInventoryTransaction> Transactions { get; set; }
        public virtual ICollection<tblInventoryItemPriceHistory> PriceHistory { get; set; }
    }

}
