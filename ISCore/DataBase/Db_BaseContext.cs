using ISCore.Entities.Places;
using ISCore.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ISCore.DataBase
{
    /// <summary>
    /// Represents the base database context for ASP.NET Core Identity.
    /// Supports custom user, role, and user-role entities,
    /// and includes additional application-specific tables.
    /// </summary>
    /// <typeparam name="TUser">The type of the user entity.</typeparam>
    /// <typeparam name="TRole">The type of the role entity.</typeparam>
    /// <typeparam name="TUserRole">The type of the user-role join entity.</typeparam>
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
        /// <summary>
        /// Initializes a new instance of the <see cref="Db_BaseContext{TUser, TRole, TUserRole}"/> class
        /// with the specified options.
        /// </summary>
        /// <param name="options">The options to configure the DbContext.</param>
        public Db_BaseContext(DbContextOptions options) : base(options)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Db_BaseContext{TUser, TRole, TUserRole}"/> class
        /// with the specified typed options.
        /// </summary>
        /// <param name="options">The options to configure the DbContext.</param>
        public Db_BaseContext(DbContextOptions<Db_BaseContext<TUser, TRole, TUserRole>> options) : base(options)
        {
        }

        /// <summary>
        /// Configures the model for the context.
        /// This includes Identity mappings and custom relationships.
        /// </summary>
        /// <param name="builder">The model builder instance used to configure entities.</param>
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

        /// <summary>
        /// Stores one-time passwords used for authentication (e.g., 2FA).
        /// </summary>
        public DbSet<tbOTP> tbOTP { get; set; }
        
        /// <summary>
        /// Stores available cities.
        /// </summary>
        public DbSet<tbCity> tbCity { get; set; }

        /// <summary>
        /// Stores regions associated with cities.
        /// </summary>
        public DbSet<tbRegion> tbRegion { get; set; }

        /// <summary>
        /// Stores countries for the application.
        /// </summary>
        public DbSet<tbCountry> tbCountry { get; set; }

    }


    /// <summary>
    /// Concrete implementation of <see cref="Db_BaseContext{TUser, TRole, TUserRole}"/>
    /// using <see cref="AppUser"/>, <see cref="AppRole"/>, and <see cref="UserRole"/>.
    /// </summary>
    public class Db_BaseContext : Db_BaseContext<AppUser, AppRole, UserRole>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Db_BaseContext"/> class
        /// with the specified options.
        /// </summary>
        /// <param name="options">The options to configure the DbContext.</param>
        public Db_BaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}
