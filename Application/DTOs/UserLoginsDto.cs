
namespace Application.DTOs
{
    public record UserLoginsDto(int id, string username, string password, int user_type, bool first_login, string? other,
        DateTime created_at, int created_by, DateTime? updated_at, int? updated_by);
    public record CreateUserLoginsDto(string username, string password, int user_type, bool first_login, string? other, int created_by);
    public record UpdateUserLoginsDto(int id, string username, string password, int user_type, bool first_login, string? other, int? updated_by);
    public record SocialInfoDTO(string email, string name);
}
