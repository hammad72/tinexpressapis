using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPaymentMethodRepository
    {
        Task<List<paymentmethod>> GetAllAsync();
        Task<paymentmethod> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<int> AddAsync(paymentmethod pm);
        Task UpdateAsync(paymentmethod pm);
    }
}
