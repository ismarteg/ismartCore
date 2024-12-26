using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Emails.contract
{
    public interface IEmailServices
    {
        Task<EmailResponse> Send(EmailMessage emailMessage);
    }
}
