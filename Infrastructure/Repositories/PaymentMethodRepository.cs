using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly OrderDbContext _orderDbContext;
        public PaymentMethodRepository(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<int> AddAsync(paymentmethod pm)
        {
            await _orderDbContext.AddAsync(pm);
            await _orderDbContext.SaveChangesAsync();
            int pmid = pm.id;
            return pmid;
        }

        public async Task DeleteAsync(int id)
        {
            var pm = await _orderDbContext.paymentmethod.FindAsync(id);
            if (pm != null)
            {
                _orderDbContext.paymentmethod.Remove(pm);
                await _orderDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<paymentmethod>> GetAllAsync() => await _orderDbContext.paymentmethod.ToListAsync();
        public async Task<paymentmethod> GetAsync(int id) => await _orderDbContext.paymentmethod.FindAsync(id);

        public async Task UpdateAsync(paymentmethod pm)
        {
            _orderDbContext.paymentmethod.Update(pm);
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
