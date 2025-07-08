using ISCore.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISCore.DTO.Users
{
    public interface IDTOUser
    {
        public string Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string National_ID { get; set; }
        public string Password { get; set; }
        public string? FilePath { get; set; }
        public string? Role { get; set; }
        public List<string> UserRolesString { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool LockoutEnabled { get; set; }
        public bool IsActive { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}
