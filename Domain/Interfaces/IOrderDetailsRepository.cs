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
        Task<orderdetails?> GetAsync(string cn);
        //Task DeleteAsync(int id);
        Task<string> AddAsync(orderdetails od, List<orderitems> oi);
        //Task UpdateAsync(couriers c);
        Task<orderdetails> getOrderByConsignmentAsync(string consignment);
        Task<orderdetails> UpdateOrderDetail(orderdetails o);
    }
}
