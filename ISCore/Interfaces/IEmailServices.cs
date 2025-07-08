

using ISCore.Emails;

namespace ISCore.Interfaces
{
    public interface IEmailServices
    {
        Task<EmailResponse> Send(EmailMessage emailMessage);
    }
}
