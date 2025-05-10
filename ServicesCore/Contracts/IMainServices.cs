using ServicesCore.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCore.Contracts
{
    public interface IMainServices
    {
         Srv_Users _Srv_Users { get; }
    }
}
