using ISCore.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISCore.DataBase.Entities.Contracts
{
    public interface IAppUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
       
        public bool IsActive { get; set; }
      

        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public DateTime LastLoginDate { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}
