using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISCore.Utils.Emails
{
    public class EmailMessage
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string EmailBody { get; set; }
        public string CcEmail { get; set; }
        public string BcEmail { get; set; }
        public bool IsBodyHtml { get; set; } = true; // Default to true, can be set to false if needed
    }
}
