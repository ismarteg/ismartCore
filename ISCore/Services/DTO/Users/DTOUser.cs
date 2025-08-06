using ISCore.Entities.Users;
using System.ComponentModel.DataAnnotations;


namespace ISCore.Services.DTO.Users
{
    public class DtoUser : IDTOUser
    {
        public string Id { get; set; } = "";
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string National_ID { get; set; } = "";
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? FilePath { get; set; }
        public string? Role { get; set; }
        public List<string> UserRolesString { get; set; } = new List<string>();
        public string PhoneNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool LockoutEnabled { get; set; }
        public bool IsActive { get; set; }

        public virtual List<UserRole> UserRoles { get; set; }
    }
}
