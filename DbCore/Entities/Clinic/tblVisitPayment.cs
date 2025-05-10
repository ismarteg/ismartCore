using System.ComponentModel.DataAnnotations.Schema;

namespace DbCore.Entities.Clinic
{
    [Table(nameof(tblVisitPayment), Schema = "Clinic")]
    public class tblVisitPayment : BaseEntity
    {
        public Guid VisitId { get; set; }

        [ForeignKey("VisitId")]
        public virtual tblVisits Visit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PatientPayment { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal InsurancePayment { get; set; }

        public Guid? InsuranceId { get; set; }

        [ForeignKey("InsuranceId")]
        public virtual tblInsurance Insurance { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
