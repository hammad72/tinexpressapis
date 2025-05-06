using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record LoginModelDto( int UserId, string UserName, string Password, string RefreshToken, DateTime? RefreshTokenExpiryTime);
    public record CreateAndUpdateLoginModelDto(int UserId, string UserName, string Password, string RefreshToken, DateTime? RefreshTokenExpiryTime);

    public record RefreshRequest(string RefreshToken);

    public record LoginRequest(string Email, string Password);
    public record SocialLoginRequest(string Provider,string Token);
}
