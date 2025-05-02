using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISendOTPRepository
    {
        string GenerateOTP();
        Task<bool> SendOTPEmailAsync(int uid,string email, string otp);
        Task<bool> VerifyOTPAsync(int uid, string otp);
    }
}
