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
    public class ShipmentService:IShipmentService
    {
        private readonly IShipmentRepository _repository;
        private readonly IMapper _mapper;
        public ShipmentService(IShipmentRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<List<OrderSourceDto>> GetAllOrderSourceAsync()
        {
            return _mapper.Map<List<OrderSourceDto>>(await _repository.GetAllOrderSourceAsync());
        }
        public async Task<List<OptionsDto>> GetAllOptionsAsync()
        {
            return _mapper.Map<List<OptionsDto>>(await _repository.GetAllOptionsAsync());
        }
        public async Task<PaginatedList<shipmentDto>> GetShipmentAsync(int pageIndex, int pageSize)
        {
            //var pagedOrderDetails = await _repository.GetOrderDetailsAsync(filterDto.OrderSource, filterDto.Option, filterDto.Search, filterDto.PageNumber, filterDto.PageSize);           
            var paginatedList = await _repository.GetShipmentAsync(pageIndex, pageSize);

            var shipm = _mapper.Map<PaginatedList<shipmentDto>>(paginatedList);
            return shipm;
     
        }
     
    }
}
