using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCore.Email
{
    public class SmtpEmailSender:IEmailSender
    {
        public SmtpSettings _settings {  get; set; }

        public SmtpEmailSender()
        {
           
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient(_settings.Host)
            {
                Port = _settings.Port,
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                EnableSsl = _settings.UseSsl,
            };

            var mailMessage = new MailMessage(_settings.FromEmail, to, subject, body)
            {
                IsBodyHtml = true,
            };

            await client.SendMailAsync(mailMessage);
        }
    }
}
