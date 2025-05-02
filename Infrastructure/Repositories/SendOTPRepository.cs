using Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SendOTPRepository:ISendOTPRepository
    {
        private readonly IEmailRepository _emailService;
        private readonly IMemoryCache _memoryCache;

        public SendOTPRepository(IEmailRepository emailService, IMemoryCache memoryCache)
        {
            _emailService = emailService;
            _memoryCache = memoryCache;
        }

        public string GenerateOTP()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString(); // 6-digit OTP
        }

        public async Task<bool> SendOTPEmailAsync(int uid,string email, string otp)
        {
            try
            {
                // Cache OTP with UID as key (5 minute expiration)
                _memoryCache.Set(uid.ToString(), otp, TimeSpan.FromMinutes(5));

                var subject = "Your TIN Express Verification Code";
                var body = $"Your OTP code is: {otp}. This code expires in 5 minutes.";

                return await _emailService.SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                // Log error (implement proper logging)
                Console.WriteLine($"Error in SendOTPEmailAsync: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> VerifyOTPAsync(int uid, string otp)
        {
            try
            {
                // Retrieve OTP using UID as string key
                var storedOtp = _memoryCache.Get<string>(uid.ToString());

                // Case-sensitive comparison
                return storedOtp != null && string.Equals(storedOtp, otp, StringComparison.Ordinal);
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine($"Error in VerifyOTPAsync: {ex.Message}");
                return false;
            }
        }
    }
}
