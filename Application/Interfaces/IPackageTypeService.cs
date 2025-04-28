using Application.DTOs;

namespace Application.Interfaces
{
    public interface IPackageTypeService
    {
        Task<List<PackageTypeDto>> GetAllAsync();
        Task<PackageTypeDto> GetByIdAsync(int id);
        Task<int> AddAsync(CreatePackageTypeDto cPackageTypeDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdatePackageTypeDto uPackageTypeDto);
    }
}
