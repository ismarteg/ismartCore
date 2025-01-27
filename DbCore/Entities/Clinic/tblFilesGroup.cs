global using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbCore.Entities.Clinic
{
    [Table(nameof(tblFilesGroup), Schema = "Clinic")]
    public class tblFilesGroup:BaseEntity
    {
        [MaxLength(200)]
        public string Tilte { get; set; }
        public int Order { get; set; } 
    }
}
