using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record UserTypesDto(int id, string title, string? description, int? plateform_id, string? other, int status);
    public record CreateUserTypesDto(string title, string? description, int? plateform_id, string? other, int status);
    public record UpdateUserTypesDto(int id, string title, string? description, int? plateform_id, string? other, int status);
}
