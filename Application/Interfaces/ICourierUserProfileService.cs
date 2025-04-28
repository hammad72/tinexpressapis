using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICourierUserProfileService
    {
        Task<List<CourierUserProfileDto>> GetAllAsync();
        Task<CourierUserProfileDto> GetByIdAsync(int id);
        Task<int> AddAsync(CreateCourierUserProfileDto_Ex cCourierUserProfileDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateCourierUserProfileDto uCourierUserProfileDto);
    }
}
