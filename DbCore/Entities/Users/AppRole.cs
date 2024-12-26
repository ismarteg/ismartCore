using Microsoft.AspNetCore.Identity;

namespace DbCore.Entities.Users
{
    public class AppRole : IdentityRole
    {
        public List<UserRole> UserRoles { get; set; }
    }

}
