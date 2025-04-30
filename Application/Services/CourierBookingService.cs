using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CourierBookingService : ICourierBookingService
    {
        private readonly ICourierBookingRepository _cbRepository;
        private readonly IMapper _mapper;
        public CourierBookingService(ICourierBookingRepository cbRepository, IMapper mapper)
        {
            _mapper = mapper;
            _cbRepository = cbRepository;
        }
        public async Task<string> OrderBookingCP(object data)
        {
            return _mapper.Map<string>(await _cbRepository.OrderBookingCP(data));
        }

        public async Task<string> OrderBookingZU(object data)
        {
            return _mapper.Map<string>(await _cbRepository.OrderBookingZU(data));
        }
    }
}
