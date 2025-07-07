using ISCore.Entities.Places;
using ISCore.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ISCore.DataBase
{
    public class Db_BaseContext : IdentityDbContext<AppUser, AppRole, string,
      IdentityUserClaim<string>,
      UserRole,
      IdentityUserLogin<string>,
      IdentityRoleClaim<string>,
      IdentityUserToken<string>>
    {
        public Db_BaseContext(DbContextOptions options) : base(options)
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
        public DbSet<tbOTP> tbOTP { get; set; }
        public DbSet<tbCity> tbCity { get; set; }
        public DbSet<tbRegion> tbRegion { get; set; }
        public DbSet<tbCountry> tbCountry { get; set; }




    }
}
