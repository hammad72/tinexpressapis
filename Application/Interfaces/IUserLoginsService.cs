using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserLoginsService
    {
        Task<List<UserLoginsDto>> GetAllAsync();
        Task<UserLoginsDto> GetByIdAsync(int id);
        Task<int> AddAsync(CreateUserLoginsDto cUserLoginsDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateUserLoginsDto uUserLoginsDto);
        Task<UserLoginsDto> GetByEmailandPassword(string email, string password);
        Task<UserLoginsDto> GetByEmaila(string email);
        Task AddLoginDetailAsync(CreateLoginDetailDto ld);
        Task AddAndUpdateLoginModel(CreateAndUpdateLoginModelDto lm);
        Task<LoginModelDto> GetLoginModel(string refreshToken);
        Task<SocialInfoDTO> socialGoogle(string requestToken);
        Task<SocialInfoDTO> socialFb(string requestToken);
    }
}
