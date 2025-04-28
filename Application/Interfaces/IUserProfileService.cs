using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserProfileService
    {
        Task<List<UserProfileDto>> GetAllAsync(); 
        Task<PaginatedList<UserProfileDto>> GetAllAsync(int pageIndex, int pageSize);

        Task<UserProfileDto> GetByIdAsync(int id);
        Task<int> AddAsync(CreateUserProfileDto_Ex cUserProfileDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateUserProfileDto uUserProfileDto);
    }
}
