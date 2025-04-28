
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IPackageContentService
    {
        Task<List<PackageContentDto>> GetAllAsync();
        Task<PackageContentDto> GetByIdAsync(int id);
        Task<int> AddAsync(CreatePackageContentDto cPackageContentDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdatePackageContentDto uPackageContentDto);
    }
}
