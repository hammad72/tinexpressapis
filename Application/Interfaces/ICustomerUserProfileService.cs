using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerUserProfileService
    {
        Task<List<CustomerUserProfileDto>> GetAllAsync();
        Task<PaginatedList<CustomerUserProfileDto>> GetAllAsync(int pageIndex, int pageSize);
        Task<CustomerUserProfileDto> GetByIdAsync(int id);
        Task<int> AddAsync(CreateCustomerUserProfileDto_Ex cCustomerUserProfileDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateCustomerUserProfileDto uCustomerUserProfileDto);
    }
}
