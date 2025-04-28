using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICustomerProfileService
    {
        Task<List<CustomerProfileDto>> GetAllAsync();
        Task<CustomerProfileDto> GetByIdAsync(int id);
        Task<int> AddAsync(CreateCustomerProfileDto_Ex cCustomerProfileDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateCustomerProfileDto uCustomerProfileDto);
    }
}
