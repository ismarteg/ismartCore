using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Emails;

namespace ISCore.Interfaces
{
    public interface IEmailServices
    {
        Task<EmailResponse> Send(EmailMessage emailMessage);
    }
}
