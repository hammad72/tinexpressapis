using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CourierStatusesService : ICourierStatusesService
    {
        private readonly ICourierStatusesRepository _repository;
        private readonly IMapper _mapper;
        public CourierStatusesService(ICourierStatusesRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<int> AddAsync(CreateCourierStatusesDto cDto)
        {
            int id = await _repository.AddAsync(_mapper.Map<courierstatuses>(cDto));
            return id;
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<CourierStatusesDto>> GetAllAsync()
        {
            return _mapper.Map<List<CourierStatusesDto>>(await _repository.GetAllAsync());
        }

        public async Task<CourierStatusesDto> GetByIdAsync(int id)
        {
            return _mapper.Map<CourierStatusesDto>(await _repository.GetAsync(id));
        }

        public async Task<List<CourierStatusesDto>> GetByCIdAsync(int cid)
        {
            return _mapper.Map<List<CourierStatusesDto>>(await _repository.GetByCIdAsync(cid));
        }

        public async Task UpdateAsync(UpdateCourierStatusesDto uDto)
        {
            await _repository.UpdateAsync(_mapper.Map<courierstatuses>(uDto));
        }
    }
}
