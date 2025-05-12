using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record FavAddressesDto(int id, string type, string address, string suburb, string postcode, string city, string state, string country, string latlong, int customer_id, int status);
    public record CreateFavAddressesDto(string type, string address, string suburb, string postcode, string city, string state, string country, string latlong, int customer_id, int status);
    public record UpdateFavAddressesDto(int id, string type, string address, string suburb, string postcode, string city, string state, string country, string latlong, int customer_id, int status);
}
