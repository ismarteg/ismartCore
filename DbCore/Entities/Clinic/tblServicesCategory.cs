using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DbCore.Entities.Clinic
{
    [Table(nameof(tblServicesCategory), Schema = "Clinic")]
    public class tblServicesCategory:BaseEntity
    {
        [Required, MaxLength(200)]
        public string Title { get; set; }

        public string? Description { get; set; }
        public bool IsFinal { get; set; } = false;

        [MaxLength(200)]
        public string? CategoryCode { get; set; }


        [ForeignKey(nameof(tblServicesCategory))]
        [AllowNull]
        public Guid? ParentId { get; set; }
        public tblServicesCategory Parent { get; set; }
        public virtual List<tblServicesCategory> ChildrenCategories { get; }

       
    }
}
