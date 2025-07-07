
using ISCore.Entities.Contracts;
using ISCore.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISCore.Entities
{
    public class BaseEntity: iSoftEntity<int>
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("AppUser")]
        public string? CreatorId { get; set; }
        public AppUser? Creator { get; set; }

        [ForeignKey("AppUser")]
        public string? LastEditorId { get; set; }
        public AppUser? LastEditor { get; set; }

        public DateTime? CreationDate { get; set; } = DateTime.Now;
        public DateTime? LastEditDate { get; set; } = DateTime.Now;

        
 
    }
}
