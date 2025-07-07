
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ISCore.Entities.Users
{
    public class AppUser : IdentityUser
    {
        // should be corrected
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(20)]
        public string National_ID { get; set; } = "";
        public bool IsActive { get; set; } = true;
        [MaxLength(100)]
        public string FaceBookId { get; set; } = "";
        [MaxLength(200)]
        public string GoogleId { get; set; } = "";

        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public DateTime LastLoginDate { get; set; }

        public virtual List<UserRole> UserRoles { get; set; }

    }
}
