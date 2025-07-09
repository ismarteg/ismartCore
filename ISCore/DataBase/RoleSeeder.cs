using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ISCore.DataBase
{

    public static class RoleSeeder<TRole> where TRole : IdentityRole, new()
    {
        /// <summary>
        /// Seed roles from static class of constants.
        /// </summary>
        /// <param name="roleManager">RoleManager instance</param>
        /// <param name="staticClassType">The static class that contains public const string role names</param>
        public static async Task SeedAsync(RoleManager<TRole> roleManager, Type staticClassType)
        {
            var fields = staticClassType.GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (var field in fields)
            {
                if (field.IsLiteral && !field.IsInitOnly) // const field
                {
                    var roleName = field.GetValue(null)?.ToString();
                    if (!string.IsNullOrWhiteSpace(roleName) && !await roleManager.RoleExistsAsync(roleName))
                    {
                        var role = new TRole
                        {
                            Name = roleName
                        };

                        await roleManager.CreateAsync(role);
                    }
                }
            }
        }
    }
}
