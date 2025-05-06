using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICourierBookingRepository
    {
        Task<object> OrderBookingZU(object data);
        Task<string> OrderBookingCP(object data);
    }
}
