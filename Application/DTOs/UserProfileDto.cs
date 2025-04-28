
namespace Application.DTOs
{
    public record UserProfilePaginatedDto(int PageIndex, int PageSize, int TotalCount, int TotalPages, List<UserProfileDto> dataObj);
    public record UserProfileDto(int id, string first_name, string? last_name, string? emp_num, string email, string? dob, 
        DateTime? enrollment_date, DateTime? joining_date, string? address, string? postal_code, int? city_id, string? phone_number, 
        string? other, DateTime created_at, int created_by, DateTime? updated_at, int? updated_by, int status);
    public record CreateUserProfileDto_Ex(string first_name, string? last_name, string? emp_num, string email, string? dob,
        DateTime? enrollment_date, DateTime? joining_date, string? address, string? postal_code, int? city_id, string? phone_number,
        string? other, int created_by, int user_role_id, int status);
    public record CreateUserProfileDto(int id, string first_name, string? last_name, string? emp_num, string email, string? dob,
        DateTime? enrollment_date, DateTime? joining_date, string? address, string? postal_code, int? city_id, string? phone_number,
        string? other, int created_by, int status);

    public record UpdateUserProfileDto(int id, string first_name, string? last_name, string? emp_num, string email, string? dob,
        DateTime? enrollment_date, DateTime? joining_date, string? address, string? postal_code, int? city_id, string? phone_number,
        string? other, int? updated_by, int status);
}
