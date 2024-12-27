using DbCore.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DbCore
{
    public class Db_BaseContext: IdentityDbContext<AppUser, AppRole, string,
      IdentityUserClaim<string>,
      UserRole,
      IdentityUserLogin<string>,
      IdentityRoleClaim<string>,
      IdentityUserToken<string>>
    {
        public Db_BaseContext(DbContextOptions options) : base(options)
        {

        }
        public Db_BaseContext(DbContextOptions<Db_BaseContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

           

        }


    }
}
