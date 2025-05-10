using System.ComponentModel.DataAnnotations.Schema;

namespace DbCore.Entities.Clinic
{
   [Table(nameof(tblPatientInsurance), Schema = "Clinic")]
    public class tblPatientInsurance : BaseEntity
    {
        public Guid PatientId { get; set; }

        public Guid InsuranceId { get; set; }

        [ForeignKey("InsuranceId")]
        public virtual tblInsurance Insurance { get; set; }

        [Required, MaxLength(50)]
        public string PolicyNumber { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
