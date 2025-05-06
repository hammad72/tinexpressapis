using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IJwtTokenRepository _jwtTokenRepository;

        public JwtTokenService(IJwtTokenRepository jwtTokenRepository)
        {
            _jwtTokenRepository = jwtTokenRepository;
        }

        public string GenerateRefreshToken()
        {
           return _jwtTokenRepository.GenerateRefreshToken();
        }

        public  string GenerateToken(string userId, string userEmail, IList<string> roles)
        {
            return _jwtTokenRepository.GenerateToken(userId, userEmail, roles);
        }

        //string GenerateToken(string userId, string userEmail, IList<string> roles)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
