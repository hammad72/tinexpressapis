using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICourierBookingService
    {
        Task<string> OrderBookingZU(object data);
        Task<string> OrderBookingCP(object data);
    }
}
