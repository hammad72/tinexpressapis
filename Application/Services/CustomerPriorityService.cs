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
    public class CustomerPriorityService : ICustomerPriorityService
    {
        private readonly ICustomerPriorityRepository _repository;
        private readonly IMapper _mapper;
        public CustomerPriorityService(ICustomerPriorityRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<bool> AddAsync(List<CreateCustomerPriorityDto> cDto)
        {
            bool res = await _repository.AddAsync(_mapper.Map<List<customerpriority>>(cDto));
            return res;
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<CustomerPriorityDto>> GetAllAsync()
        {
            return _mapper.Map<List<CustomerPriorityDto>>(await _repository.GetAllAsync());
        }

        public async Task<CustomerPriorityDto> GetByIdAsync(int id)
        {
            return _mapper.Map<CustomerPriorityDto>(await _repository.GetAsync(id));
        }
        public async Task<List<CustomerPriorityDto>> GetByCIdAsync(int id)
        {
            return _mapper.Map<List<CustomerPriorityDto>>(await _repository.GetByCIdAsync(id));
        }

        public async Task UpdateAsync(UpdateCustomerPriorityDto uDto)
        {
            await _repository.UpdateAsync(_mapper.Map<customerpriority>(uDto));
        }
    }
}
