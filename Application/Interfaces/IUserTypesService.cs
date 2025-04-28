using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserTypesService
    {
        Task<List<UserTypesDto>> GetAllAsync();
        Task<List<UserTypesDto>> GetUserTypesAllByPlatform(int pid);
        Task<UserTypesDto> GetByIdAsync(int id);
        Task<int> AddAsync(CreateUserTypesDto cUserTypesDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateUserTypesDto uUserTypesDto);
    }
}
