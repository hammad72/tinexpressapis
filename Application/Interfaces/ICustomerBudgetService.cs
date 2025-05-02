using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerBudgetService
    {
        Task<List<CustomerBudgetDto>> GetAllAsync();
        Task<CustomerBudgetDto> GetByIdAsync(int id);
        Task<List<CustomerBudgetDto>> GetByCIdAsync(int id);
        Task<bool> AddAsync(List<CreateCustomerBudgetDto> cDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateCustomerBudgetDto uDto);
    }
}
