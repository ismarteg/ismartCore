namespace ISCore.Utils.Emails
{
    public interface IEmailServices
    {
        public SmtpSettings _SmtpSettings { get; set; }
        Task<EmailResponse> Send(EmailMessage emailMessage);
    }
}
