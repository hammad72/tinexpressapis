using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record PaymentMethodDto(int id, string title, int status);
    public record CreatePaymentMethodDto(string title, int status);
    public record UpdatePaymentMethodDto(int id, string title, int status);
}
