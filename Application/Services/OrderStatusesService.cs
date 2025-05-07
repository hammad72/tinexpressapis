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
    public class OrderStatusesService : IOrderStatusesService
    {
        private readonly IOrderStatusesRepository _repository;
        private readonly IMapper _mapper; public OrderStatusesService(IOrderStatusesRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<int> AddAsync(CreateOrderStatusesDto cDto)
        {
            int id = await _repository.AddAsync(_mapper.Map<orderstatuses>(cDto));
            return id;
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<OrderStatusesDto>> GetAllAsync()
        {
            return _mapper.Map<List<OrderStatusesDto>>(await _repository.GetAllAsync());
        }

        public async Task<OrderStatusesDto> GetByIdAsync(int id)
        {
            return _mapper.Map<OrderStatusesDto>(await _repository.GetAsync(id));
        }

        public async Task UpdateAsync(UpdateOrderStatusesDto uDto)
        {
            await _repository.UpdateAsync(_mapper.Map<orderstatuses>(uDto));
        }
    }
}
