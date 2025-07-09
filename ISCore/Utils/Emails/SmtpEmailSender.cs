using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ISCore.Utils.Emails
{
    public class SmtpEmailSender : IEmailServices
    {
        public SmtpSettings _SmtpSettings { get; set; }
        public SmtpEmailSender(IOptions<SmtpSettings> smtpOptions)
        {
            _SmtpSettings = smtpOptions.Value;
        }
        public async Task<EmailResponse> Send(EmailMessage emailMessage)
        {
            var response = new EmailResponse();

            try
            {
                using (var smtpClient = new SmtpClient(_SmtpSettings.Host, _SmtpSettings.Port)) // حط هنا الدومين والبورت بتوعك
                {
                    smtpClient.Credentials = new NetworkCredential(_SmtpSettings.Username, _SmtpSettings.Password);
                    smtpClient.EnableSsl = _SmtpSettings.UseSsl;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_SmtpSettings.FromEmail),
                        Subject = emailMessage.Subject,
                        Body = emailMessage.EmailBody,
                        IsBodyHtml = emailMessage.IsBodyHtml
                    };

                    mailMessage.To.Add(emailMessage.ToEmail);

                    await smtpClient.SendMailAsync(mailMessage);

                    response.ResponseCode = 1; // Assuming 1 means success
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode= 0;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
