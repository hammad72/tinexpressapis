using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Application.Services
{
    public class GetQuoteService : IGetQuoteService
    {
        private readonly IGetQuoteRepository _gqRepository;
        private readonly IMapper _mapper;
        public GetQuoteService(IGetQuoteRepository gqRepository, IMapper mapper)
        {
            _mapper = mapper;
            _gqRepository = gqRepository;
        }

        public async Task<string> getQuoteCourierPlease(object data)
        {
            return _mapper.Map<string>(await _gqRepository.getQuoteCourierPlease(data));
        }

        public async Task<string> getQuoteZoom2u(object data)
        {
            return _mapper.Map<string>(await _gqRepository.getQuoteZoom2u(data));
        }
    }
}
