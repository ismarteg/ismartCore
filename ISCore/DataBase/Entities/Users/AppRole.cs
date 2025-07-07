using Microsoft.AspNetCore.Identity;

namespace ISCore.Entities.Users
{
    public class AppRole : IdentityRole
    {
        public List<UserRole> UserRoles { get; set; }
    }

}
