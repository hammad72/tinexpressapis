
namespace Application.DTOs
{
    public record PackageContentDto(int id, string title, string? short_code, string? other, int status);
    public record CreatePackageContentDto(string title, string? short_code, string? other, int status);
    public record UpdatePackageContentDto(int id, string title, string? short_code, string? other, int status);
}
