using System.ComponentModel.DataAnnotations.Schema;

namespace DbCore.Entities.Clinic
{
    [Table(nameof(tblInsuranceCoverage), Schema = "Clinic")]
    public class tblInsuranceCoverage : BaseEntity
    {
       

        [ForeignKey(nameof(tblInsurance))]
      
        public Guid InsuranceId { get; set; }
        public virtual tblInsurance Insurance { get; set; }
 
        [ForeignKey("ServiceId")]
        public Guid ServiceId { get; set; }

        public virtual tblService Service { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CoverageAmount { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? CoveragePercentage { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MaxCoverageAmount { get; set; }

        public bool IsIncrease { get; set; }

        public CoverageType CoverageType { get; set; }
    }
}
