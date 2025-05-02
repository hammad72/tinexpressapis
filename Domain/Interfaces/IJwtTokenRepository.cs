using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IJwtTokenRepository
    {
        string GenerateToken(string userId, string userEmail, IList<string> roles);
        string GenerateRefreshToken();
    }
}
