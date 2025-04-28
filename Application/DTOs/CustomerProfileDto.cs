
namespace Application.DTOs
{
    public record CustomerProfileDto(int id, string name, string? legal_name, string email, string? phone, int payment_method_id,
        string? referral_name, string? fav_pickup_address, string? fav_dropoff_address, string? address, DateTime? enrollment_date,
        int? business_type_id, string? invoice_frequency, string? sales_tax_num, bool? proof_of_delivery, int? desired_attempts,
        string? other, DateTime created_at, int created_by, DateTime? updated_at, int? updated_by, int status);
    public record CreateCustomerProfileDto_Ex(string name, string? legal_name, string email, string? phone, int payment_method_id,
        string? referral_name, string? fav_pickup_address, string? fav_dropoff_address, string? address, DateTime? enrollment_date,
        int? business_type_id, string? invoice_frequency, string? sales_tax_num, bool? proof_of_delivery, int? desired_attempts,
        string? other, int created_by, int user_role_id,int status);
    public record CreateCustomerProfileDto(int id, string name, string? legal_name, string email, string? phone, int payment_method_id,
        string? referral_name, string? fav_pickup_address, string? fav_dropoff_address, string? address, DateTime? enrollment_date,
        int? business_type_id, string? invoice_frequency, string? sales_tax_num, bool? proof_of_delivery, int? desired_attempts,
        string? other, int created_by, int status);
    public record UpdateCustomerProfileDto(int id, string name, string? legal_name, string email, string? phone, int payment_method_id,
        string? referral_name, string? fav_pickup_address, string? fav_dropoff_address, string? address, DateTime? enrollment_date,
        int? business_type_id, string? invoice_frequency, string? sales_tax_num, bool? proof_of_delivery, int? desired_attempts,
        string? other, DateTime created_at, int created_by, DateTime? updated_at, int? updated_by, int status);
}