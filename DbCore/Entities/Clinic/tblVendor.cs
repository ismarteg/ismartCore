using System.ComponentModel.DataAnnotations.Schema;

namespace DbCore.Entities.Clinic
{
    [Table(nameof(tblVendor), Schema = "Clinic")]
    public class tblVendor : BaseEntity
    {
        [Required, MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? ContactPerson { get; set; }

        [MaxLength(50)]
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        public virtual ICollection<tblInventoryTransaction> Transactions { get; set; }
    }
}
