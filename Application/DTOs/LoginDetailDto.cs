using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record LoginDetailDto(int Id, int UserId, string UserName, string Name, string Password, int UType, int UStatus, DateTime LastActiveTime, string Token);
    public record CreateLoginDetailDto(int Id, int UserId, string UserName, string Name, string Password, int UType, int UStatus, DateTime LastActiveTime, string Token);
    //public record CreateCouriersDto(int id, string name, string? legal_name, string? primary_location, int payment_method_id,
    //    string? restricted_item, string email, string? phone, DateTime? enrollment_date, string? far_away_distance,
    //    string? postal_code, int? city_id, int? state_id, int? country_id, string? web_site, string? logo, string? other,
    //    int created_by, int status);
    //public record UpdateCouriersDto(int id, string name, string? legal_name, string? primary_location, int payment_method_id,
    //    string? restricted_item, string email, string? phone, DateTime? enrollment_date, string? far_away_distance,
    //    string? postal_code, int? city_id, int? state_id, int? country_id, string? web_site, string? logo, string? other,
    //    int? updated_by, int status);
}
