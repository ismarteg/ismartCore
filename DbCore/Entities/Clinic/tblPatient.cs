

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using Shared.Enums;

namespace DbCore.Entities.Clinic
{
    [Table(nameof(tblPatient), Schema = "Clinic")]
    public class tblPatient:BaseEntity
    {
        [NotNull, DefaultValue(0)]
        public long PatientCode { get; set; }

        [MaxLength(50)]
        public string MainMobile { get; set; }

        [MaxLength(250)]
        public string PatientName { get; set; }
        [MaxLength(20), AllowNull]
        public string? PatientTitle { get; set; }

        [NotNull]
        public DateTime BirthDate { get; set; } = DateTime.Now.AddYears(-5);

        //public Gender Gender { get; set; }
        public Gender Gender { get; set; }

        [MaxLength(20)]
        public string? NationalId { get; set; }

        [MaxLength(50)]
        public string? Mobiles { get; set; }
        [MaxLength(50)]
        public string? Phone { get; set; }
        [MaxLength(250)]
        public string? Address { get; set; }

        public string? StartSummary { get; set; }

        public int? WhereYouknowUs { get; set; }
        public string? WhereYouknowUsOther { get; set; }


        //[ForeignKey(nameof(TbRegion))]
        public Guid? RegionId { get; set; }
       // public TbRegion? TbRegion { get; set; }

        [ForeignKey(nameof(AppUser))]
        public string? UserId { get; set; }

        [ForeignKey(nameof(tblClinicData))]
        public Guid ClinicId { get; set; }
    }
}
