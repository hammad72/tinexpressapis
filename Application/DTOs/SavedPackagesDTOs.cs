using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record SavedPackagesDto(int? id, string? sp_code, int? package_type_id, string? package_type,
        int? package_content_id, string? package_content, int? qty, int? weight, int? length, int? width,
        int? height, string? unit, int? customer_id);
    public record CreateSavedPackagesDto(string? sp_code, int? package_type_id, string? package_type,
        int? package_content_id, string? package_content, int? qty, int? weight, int? length, int? width,
        int? height, string? unit, int? customer_id);
    public record UpdateSavedPackagesDto(int? id, string? sp_code, int? package_type_id, string? package_type,
        int? package_content_id, string? package_content, int? qty, int? weight, int? length, int? width,
        int? height, string? unit, int? customer_id);
}
