using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISCore.Services.DTO;

namespace ISCore.Services.DTO.Users
{
    public class DtoOTP : DTOBase<Guid>
    {
        public string UserId { get; set; }
        public string OTPNumper { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsSMS { get; set; }
        public bool IsOtpVerified { get; set; } = false;
    }
}
