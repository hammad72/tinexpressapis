using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DownloadLabelRepository : IDownloadLabelRepository
    {
        private readonly OrderDbContext _orderDbContext;
        public DownloadLabelRepository(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }
        public async Task<orderdetails?> GetByIdAsync(string refNum)
        {
            try
            {
                var data = await _orderDbContext.orderdetails.Where(x => x.consignment_number == refNum).FirstOrDefaultAsync();
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
