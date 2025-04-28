
namespace Application.DTOs
{
    public record PackageTypeDto(int id, string title, string? short_code, string? other, int status);
    public record CreatePackageTypeDto(string title, string? short_code, string? other, int status);
    public record UpdatePackageTypeDto(int id, string title, string? short_code, string? other, int status);
}



