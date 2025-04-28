
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class PackageTypeService : IPackageTypeService
    {
        private readonly IPackageTypeRepository _pRepository;
        private readonly IMapper _mapper;

        public PackageTypeService(IPackageTypeRepository pRepository, IMapper mapper)
        {
            _mapper = mapper;
            _pRepository = pRepository;
        }

        public async Task<int> AddAsync(CreatePackageTypeDto cPackageTypeDto)
        {
            int ptid = await _pRepository.AddAsync(_mapper.Map<packagetype>(cPackageTypeDto));
            return ptid;
        }

        public async Task DeleteAsync(int id)
        {
            await _pRepository.DeleteAsync(id);
        }

        public async Task<List<PackageTypeDto>> GetAllAsync()
        {
            return _mapper.Map<List<PackageTypeDto>>(await _pRepository.GetAllAsync());
        }

        public async Task<PackageTypeDto> GetByIdAsync(int id)
        {
            return _mapper.Map<PackageTypeDto>(await _pRepository.GetAsync(id));
        }

        public async Task UpdateAsync(UpdatePackageTypeDto uPackageTypeDto)
        {
            await _pRepository.UpdateAsync(_mapper.Map<packagetype>(uPackageTypeDto));
        }
    }
}
