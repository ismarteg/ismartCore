using ISCore.DataBase.Entities.Contracts;
using ISCore.Entities.Users.Const;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ISCore.DataBase
{
    public class Seed<Tdbcontext,TUser,TRole>
        where Tdbcontext : DbContext
       where TUser : IdentityUser, IAppUser, new()
      where TRole : IdentityRole
    {
        public static void EnsureBasicDataPopulated(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<Tdbcontext>();
                context.Database.Migrate();
                var appliedMigrations = context.Database.GetAppliedMigrations().LastOrDefault();
            }
        }
        public static async Task EnsureAdminPopulated(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<Tdbcontext>();
                var usrMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<TUser>>();
                var roleMgr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<TRole>>();



                // Create SupperAdmin
                if (!usrMgr.Users.Any())
                {
                    TUser SuperAdmin = new TUser
                    {
                        UserName = "Admin",
                        Email = "Admin@System.com",
                        FirstName = "Admin1",
                        LastName = "",
                        EmailConfirmed = true
                    };
                    var result = await usrMgr.CreateAsync(SuperAdmin, "P@ssW0rd");
                    if (result.Succeeded)
                    {
                        var roleResult = await usrMgr.AddToRoleAsync(SuperAdmin, Cnst_Roles.SuperAdmin);
                    }
                    else
                    {
                        Console.WriteLine(result.Errors);
                    }
                }

                // Create System Admin
                var SystemAdmins = await usrMgr.GetUsersInRoleAsync(Cnst_Roles.Admin);

                if (SystemAdmins.Count == 0)
                {
                    TUser SystemAdmin = new TUser
                    {
                        UserName = "SysAdmin",
                        Email = "SysAdmin@System.com",
                        FirstName = "Admin2",
                        LastName = "",

                        EmailConfirmed = true
                    };
                    var result = await usrMgr.CreateAsync(SystemAdmin, "P@ssW0rd");
                    if (result.Succeeded)
                    {
                        var rolecheck = await usrMgr.IsInRoleAsync(SystemAdmin, Cnst_Roles.Admin);
                        if (!rolecheck)
                        {
                            await usrMgr.AddToRoleAsync(SystemAdmin, Cnst_Roles.Admin);
                        }
                    }
                    else
                    {
                        Console.WriteLine(result.Errors);
                    }
                }
            }
        }
        public static async Task EnsureAppRolePopulated(IApplicationBuilder app)
        {

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<Tdbcontext>();
                var roleMgr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<TRole>>();


                // Get the type of the static class

                Type staticClassTypeMain = typeof(Cnst_Roles);
                // Get all public static fields

                FieldInfo[] fields_main = staticClassTypeMain.GetFields(BindingFlags.Public | BindingFlags.Static);

                foreach (var field in fields_main)
                {
                    if (field.IsLiteral && !field.IsInitOnly)
                    {
                        object value = field.GetValue(null);
                        string roleName = value.ToString();

                        if (!await roleMgr.RoleExistsAsync(roleName))
                        {
                            TRole newRole = Activator.CreateInstance<TRole>();
                            if (newRole is IdentityRole role)
                            {
                                role.Name = roleName;
                                await roleMgr.CreateAsync((TRole)(object)role);
                            }
                        }
                    }
                }
            }
        }


    }
}
