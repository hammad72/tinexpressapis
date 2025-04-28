using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _pmRepository;
        private readonly IMapper _mapper;
        public PaymentMethodService(IPaymentMethodRepository pmRepository, IMapper mapper)
        {
            _mapper = mapper;
            _pmRepository = pmRepository;
        }
        public async Task<int> AddAsync(CreatePaymentMethodDto cpm)
        {
            CreatePaymentMethodDto cPaymentMethodDto = new CreatePaymentMethodDto(cpm.title,cpm.status);
            int pmid = await _pmRepository.AddAsync(_mapper.Map<paymentmethod>(cPaymentMethodDto));
            return pmid;
        }

        public async Task DeleteAsync(int id)
        {
            await _pmRepository.DeleteAsync(id);
        }

        public async Task<List<PaymentMethodDto>> GetAllAsync()
        {
            return _mapper.Map<List<PaymentMethodDto>>(await _pmRepository.GetAllAsync());
        }

        public async Task<PaymentMethodDto> GetByIdAsync(int id)
        {
            return _mapper.Map<PaymentMethodDto>(await _pmRepository.GetAsync(id));
        }

        public async Task UpdateAsync(UpdatePaymentMethodDto uPaymentMethodDto)
        {
            await _pmRepository.UpdateAsync(_mapper.Map<paymentmethod>(uPaymentMethodDto));
        }
    }
}
