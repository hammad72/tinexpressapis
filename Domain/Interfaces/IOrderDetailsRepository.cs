using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderDetailsRepository
    {
        Task<List<order>> GetAllAsync();
        Task<order> GetAsync(int id);
        //Task DeleteAsync(int id);
        Task<string> AddAsync(orderdetails od, List<orderitems> oi);
        //Task UpdateAsync(couriers c);
    }
}
