
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public record CouriersDto(int id, string name, string? legal_name, string? primary_location, int payment_method_id,
        string? restricted_item, string email, string? phone, DateTime? enrollment_date, string? far_away_distance,
        string? postal_code, int? city_id, int? state_id, int? country_id, string? web_site, string? logo, string? other,
        DateTime created_at, int created_by, DateTime? updated_at, int? updated_by, int status);
    public record CreateCouriersDto(int id, string name, string? legal_name, string? primary_location, int payment_method_id,
        string? restricted_item, string email, string? phone, DateTime? enrollment_date, string? far_away_distance,
        string? postal_code, int? city_id, int? state_id, int? country_id, string? web_site, string? logo, string? other,
        int created_by, int status);
    public record UpdateCouriersDto(int id, string name, string? legal_name, string? primary_location, int payment_method_id,
        string? restricted_item, string email, string? phone, DateTime? enrollment_date, string? far_away_distance,
        string? postal_code, int? city_id, int? state_id, int? country_id, string? web_site, string? logo, string? other,
        int? updated_by, int status);
}