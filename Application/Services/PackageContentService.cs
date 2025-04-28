
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class PackageContentService : IPackageContentService
    {
        private readonly IPackageContentRepository _pRepository;
        private readonly IMapper _mapper;

        public PackageContentService(IPackageContentRepository pRepository, IMapper mapper)
        {
            _mapper = mapper;
            _pRepository = pRepository;
        }

        public async Task<int> AddAsync(CreatePackageContentDto cPackageContentDto)
        {
            int ptid = await _pRepository.AddAsync(_mapper.Map<packagecontent>(cPackageContentDto));
            return ptid;
        }

        public async Task DeleteAsync(int id)
        {
            await _pRepository.DeleteAsync(id);
        }

        public async Task<List<PackageContentDto>> GetAllAsync()
        {
            return _mapper.Map<List<PackageContentDto>>(await _pRepository.GetAllAsync());
        }

        public async Task<PackageContentDto> GetByIdAsync(int id)
        {
            return _mapper.Map<PackageContentDto>(await _pRepository.GetAsync(id));
        }

        public async Task UpdateAsync(UpdatePackageContentDto uPackageContentDto)
        {
            await _pRepository.UpdateAsync(_mapper.Map<packagecontent>(uPackageContentDto));
        }
    }
}
