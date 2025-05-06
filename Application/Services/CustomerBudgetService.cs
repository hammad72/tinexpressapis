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
    public class CustomerBudgetService : ICustomerBudgetService
    {
        private readonly ICustomerBudgetRepository _repository;
        private readonly IMapper _mapper; 
        public CustomerBudgetService(ICustomerBudgetRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<bool> AddAsync(List<CreateCustomerBudgetDto> cDto)
        {
            bool res = await _repository.AddAsync(_mapper.Map<List<customerbudget>>(cDto));
            return res;
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<CustomerBudgetDto>> GetAllAsync()
        {
            return _mapper.Map<List<CustomerBudgetDto>>(await _repository.GetAllAsync());
        }

        public async Task<CustomerBudgetDto> GetByIdAsync(int id)
        {
            return _mapper.Map<CustomerBudgetDto>(await _repository.GetAsync(id));
        }

        public async Task<List<CustomerBudgetDto>> GetByCIdAsync(int id)
        {
            return _mapper.Map< List<CustomerBudgetDto>>(await _repository.GetByCIdAsync(id));
        }

        public async Task UpdateAsync(UpdateCustomerBudgetDto uDto)
        {
            await _repository.UpdateAsync(_mapper.Map<customerbudget>(uDto));
        }
    }
}
