using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCore
{
    public class OtpService
    {
        private readonly AppIdentityDbContext _context;

        public async Task<string> GenerateOtpAsync(string userId)
        {
            var code = new Random().Next(100000, 999999).ToString();
            var otp = new UserOtp
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Code = code,
                Expiration = DateTime.UtcNow.AddMinutes(5)
            };
            _context.UserOtps.Add(otp);
            await _context.SaveChangesAsync();
            return code;
        }

        public async Task<bool> VerifyOtpAsync(string userId, string code)
        {
            var otp = await _context.UserOtps
                .Where(x => x.UserId == userId && x.Code == code && x.Expiration > DateTime.UtcNow)
                .OrderByDescending(x => x.Expiration)
                .FirstOrDefaultAsync();

            return otp != null;
        }
    }
}
