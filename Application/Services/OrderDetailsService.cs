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
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly IOrderDetailsRepository _oRepository;
        private readonly IMapper _mapper;

        public OrderDetailsService(IOrderDetailsRepository oRepository, IMapper mapper)
        {
            _mapper = mapper;
            _oRepository = oRepository;
        }

        public async Task<string> AddAsync(OrderDetailsDto cOrderDetailDto, List<OrderItemsDto> cOrderItems)
        {
            try
            {
                //string cn = await _oRepository.AddAsync(_mapper.Map<order>(cOrderDto));
                string cn = await _oRepository.AddAsync(_mapper.Map<orderdetails>(cOrderDetailDto), _mapper.Map<List<orderitems>>(cOrderItems));
                //string cn = await _oRepository.AddAsync(cOrderDetailDto, cOrderItems);
                return cn;
            }
            catch (Exception ex) { throw ex; }
        }

        //public Task DeleteAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}        //public Task DeleteAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<List<OrderDetailsDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDetailsDto?> GetByIdAsync(string cn)
        {
            return _mapper.Map<OrderDetailsDto?>(await _oRepository.GetAsync(cn));
        }
    }
}
