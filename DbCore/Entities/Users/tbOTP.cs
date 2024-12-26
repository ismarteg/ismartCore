using System.ComponentModel.DataAnnotations.Schema;

namespace DbCore.Entities.Users
{
    [Table(nameof(tbOTP), Schema = "Site")]
    public class tbOTP : BaseEntity
    {
        public int OTPNumper { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsSMS { get; set; }

    }
}
