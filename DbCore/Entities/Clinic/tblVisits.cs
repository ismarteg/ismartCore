
using Shared.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DbCore.Entities.Clinic
{
    [Table(nameof(tblVisits), Schema = "Clinic")]
    public class tblVisits:BaseEntity
    {


        //date and time
        public DateTime VisitDate { get; set; } = DateTime.Now;
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }


        [ForeignKey(nameof(tblPatient))]
        public Guid PatientId { get; set; }

        //value Come from tblServicesReservation
        [ForeignKey(nameof(tblService))]
        public Guid ServiceId { get; set; }

        [ForeignKey(nameof(tblClinicData))]
        public Guid BranchId { get; set; }

        [ForeignKey(nameof(tblServicesReservation))]
        public Guid ServicesReservationId { get; set; }
       
        public int UnitCount { get; set; }

        public int ExtraUnitCount { get; set; }

        public VisitStatus VisitStatus { get; set; }

        public string? Note { get; set; }

        [NotNull, DefaultValue(false)]
        public bool IsOnlineBooking { get; set; }

        public string? VedioCallLink { get; set; }

        public virtual ICollection<tblVisitPayment> Payments { get; set; }

        public virtual ICollection<tblPricingLog> PricingLogs { get; set; }
    }
}
