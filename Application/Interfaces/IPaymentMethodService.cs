using Application.DTOs;

namespace Application.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<List<PaymentMethodDto>> GetAllAsync();
        Task<PaymentMethodDto> GetByIdAsync(int id);
        Task<int> AddAsync(CreatePaymentMethodDto cPaymentMethodDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdatePaymentMethodDto uPaymentMethodDto);
    }
}
