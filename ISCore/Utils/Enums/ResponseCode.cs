using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISCore.Utils.Enums
{
    public enum ResponseCode
    {
        OK = 200,
        NotFound = 404,
        BadRequest = 400,
        Unauthorized = 401,
        InternalServerError = 500,
        ServiceUnavailable = 503
    }
}
