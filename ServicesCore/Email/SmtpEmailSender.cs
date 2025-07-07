using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ServicesCore.Email
{
    public class SmtpEmailSender:IEmailSender
    {
        public SmtpSettings _settings {  get; set; }

       

        public SmtpEmailSender(IOptions<SmtpSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var smtp = new SmtpClient(_settings.Host, _settings.Port)
            {
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                EnableSsl = _settings.UseSsl
            };

            var message = new MailMessage(_settings.FromEmail, to, subject, body)
            {
                IsBodyHtml = true
            };

            await smtp.SendMailAsync(message);
        }
    }
}
