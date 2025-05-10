using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DbCore.Entities.Clinic
{
    /// <summary>
    /// Main info is Recoded in Master
    /// this matser table for Visits And Servies 
    /// If services Has More than One Viste Like (dentist and cosmetology Leaser etc...)
    /// Payments is Separated from visits
    /// tblServicesReservation has child list of Visits and child list of Payments
    /// </summary>
    [Table(nameof(tblServicesReservation), Schema = "Clinic")]
    public class tblServicesReservation:BaseEntity
    {

        [ForeignKey(nameof(tblPatient))]
        public Guid PatientId { get; set; }

        // Can not edit if Reservation has Visits
        [ForeignKey(nameof(tblService))]
        public Guid ServiceId { get; set; }

        [ForeignKey(nameof(tblClinicData))]
        public Guid BranchId { get; set; }

        public int VisitsOrUnitsCount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalDiscount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal NetPrice { get; set; }
        public string Notes { get; set; }

        [NotNull, DefaultValue(false)]
        public bool IsClosed { get; set; }
        public virtual List<tblVisits> Visits { get; set; }
    }
}
