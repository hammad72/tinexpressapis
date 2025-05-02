using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record CustomerPriorityDto(int id, int customer_id, int courier_id, string priority);
    public record CreateCustomerPriorityDto(int customer_id, int courier_id, string priority);
    public record UpdateCustomerPriorityDto(int id, int customer_id, int courier_id, string priority);
    public record CreateCustomerPriorityDtoArr(List<CreateCustomerPriorityDto> customer_priorities);
}