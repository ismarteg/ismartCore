using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace DbCore.Entities.Clinic
{
    [Table(nameof(tblFiles), Schema = "Clinic")]
    public class tblFiles:BaseEntity
    {
        public string Title { get; set; }

        [ForeignKey("tblServicesCategory")]
        public int? ServiceCategoryId { get; set; }
        public tblServicesCategory ServicesCategory { get; set; }
    }
}
