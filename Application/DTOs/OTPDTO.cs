using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EmailOTPRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    // EmailOTPResponseDto.cs
    public class EmailOTPResponseDto
    {
        public int uid { get; set; }
        public string email { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string OTP { get; set; } // Optional: Only for debugging, remove in production
    }
    public class OtpChangePassRequestDTO
    {
        public int uid { get; set; }
        public string email { get; set; }
        public string otp { get; set; }
        public string newpass { get; set; }
        //public string confirmpass { get; set; }
        //public bool Success { get; set; }
        //public string Message { get; set; }

    }
    public class OtpChangePassDTO
    {
        public int uid { get; set; }
        public string email { get; set; }
        public string otp { get; set; }
        public string newpass { get; set; }
        //public string confirmpass { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    
    }
}
