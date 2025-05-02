using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISendOTPService
    {
        string GenerateOTP();
        //Task<bool> SendOTPEmailAsync(string email, string otp);
    }
}
