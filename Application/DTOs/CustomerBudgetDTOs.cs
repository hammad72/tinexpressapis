using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record CustomerBudgetDto(int id, int customer_id, string destination, float budget);
    public record CreateCustomerBudgetDto(int customer_id, string destination, float budget);
    public record UpdateCustomerBudgetDto(int id, int customer_id, string destination, float budget);
    public record CreateCustomerBudgetDtoArr(List<CreateCustomerBudgetDto> customer_budgets);
}
