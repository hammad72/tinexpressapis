using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOTPControlService
    {
        Task<EmailOTPResponseDto> SendOTPAsync(string email);
        Task<OtpChangePassDTO> verifyOTpControl(OtpChangePassRequestDTO param);
    }
}
