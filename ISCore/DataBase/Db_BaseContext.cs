using ISCore.Entities.Places;
using ISCore.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ISCore.DataBase
{
    public class Db_BaseContext<TUser, TRole, TUserRole> :
    IdentityDbContext<TUser, TRole, string,
                      IdentityUserClaim<string>,
                      TUserRole,
                      IdentityUserLogin<string>,
                      IdentityRoleClaim<string>,
                      IdentityUserToken<string>>
    where TUser : IdentityUser
    where TRole : IdentityRole
    where TUserRole  : IdentityUserRole<string>
    {
        public Db_BaseContext(DbContextOptions options) : base(options)
        {
        }

        public Db_BaseContext(DbContextOptions<Db_BaseContext<TUser, TRole, TUserRole>> options) : base(options)
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

    public class Db_BaseContext : Db_BaseContext<AppUser, AppRole, UserRole>
    {
        public Db_BaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}
