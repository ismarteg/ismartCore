using System.ComponentModel.DataAnnotations.Schema;

namespace DbCore.Entities.Clinic
{
    [Table(nameof(tblInsurance), Schema = "Clinic")]
    public class tblInsurance : BaseEntity
    {
        [Required, MaxLength(200)]
        public string InsuranceName { get; set; }

        public string Description { get; set; }

        public virtual ICollection<tblPatientInsurance> PatientInsurances { get; set; }

        public virtual ICollection<tblInsuranceCoverage> InsuranceCoverages { get; set; }
    }
}
