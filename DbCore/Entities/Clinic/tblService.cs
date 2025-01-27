using System.ComponentModel.DataAnnotations.Schema;

namespace DbCore.Entities.Clinic
{
    [Table(nameof(tblService), Schema = "Clinic")]
    public class tblService:BaseEntity
    {
        [MaxLength(200)]
        public string Title { get; set; }
       
        [MaxLength(250)]
        public string? Description { get; set; } = "";
        [MaxLength(50)]
        public string? Code { get; set; } = "";
        [Column(TypeName = "decimal(18,2)")]
        public decimal DefaultPrice { get; set; } = 0;

        public bool IsUnitService { get; set; } = false;

        public int VisitsOrUnitsCount { get; set; }

        [ForeignKey(nameof(tblServicesCategory))]
        public Guid ServicesCategoryID { get; set; }
        public tblServicesCategory ServicesCategory {  get; set; }

      
        public virtual ICollection<tblInsuranceCoverage> InsuranceCoverages { get; set; }


    }
}
