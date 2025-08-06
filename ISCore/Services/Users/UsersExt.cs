using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISCore.Services.Users
{
    public static class UsersExt
    {
        public static string getError(this IdentityResult result)
        {
            return result != null && result.Errors != null
            ? string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)): "";
        }
    }
}
