using System.ComponentModel.DataAnnotations.Schema;

namespace DbCore.Entities.Clinic
{
    [Table(nameof(tblPricingLog), Schema = "Clinic")]
    public class tblPricingLog : BaseEntity
    {
        public Guid VisitId { get; set; }

        [ForeignKey("VisitId")]
        public virtual tblVisits Visit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OldValue { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal NewValue { get; set; }
    }
}
