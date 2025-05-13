using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    internal class MarginsDTOs
    {
    }
    public record MarginsDto(int id, int courier_id, string? courier_title, double margin);
    public record CreateMarginsDto(int courier_id, string? courier_title, double margin);
    public record UpdateMarginsDto(int id, int courier_id, string? courier_title, double margin);
    public record CreateMarginsDtoArr(List<CreateMarginsDto> margins);
}
