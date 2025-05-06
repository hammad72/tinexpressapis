using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CourierStatusMappingService : ICourierStatusMappingService
    {
        private readonly ICourierStatusMappingRepository _repository;
        private readonly IMapper _mapper;
        public CourierStatusMappingService(ICourierStatusMappingRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<bool> AddAsync(List<CreateCourierStatusMappingDto> cDto)
        {
            bool res = await _repository.AddAsync(_mapper.Map<List<courierstatusmapping>>(cDto));
            return res;
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<CourierStatusMappingDto>> GetAllAsync()
        {
            return _mapper.Map<List<CourierStatusMappingDto>>(await _repository.GetAllAsync());
        }

        public async Task<CourierStatusMappingDto> GetByIdAsync(int id)
        {
            return _mapper.Map<CourierStatusMappingDto>(await _repository.GetAsync(id));
        }

        public async Task<List<CourierStatusMappingDto>> GetByCIdAsync(int cid)
        {
            return _mapper.Map<List<CourierStatusMappingDto>>(await _repository.GetByCIdAsync(cid));
        }

        public async Task UpdateAsync(UpdateCourierStatusMappingDto uDto)
        {
            await _repository.UpdateAsync(_mapper.Map<courierstatusmapping>(uDto));
        }
    }
}
