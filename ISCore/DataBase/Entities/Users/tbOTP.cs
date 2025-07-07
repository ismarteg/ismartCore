using System.ComponentModel.DataAnnotations.Schema;

namespace ISCore.Entities.Users
{
    [Table(nameof(tbOTP), Schema = "Site")]
    public class tbOTP : BaseEntity
    {
        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; }
        public string OTPNumper { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsSMS { get; set; }
        public bool IsOtpVerified { get; set; } = false;

    }
}
