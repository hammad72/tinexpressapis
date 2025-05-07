using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record CourierStatusesDto(int id, int courier_id, int courier_status_id, string courier_status_title,
        string description, int status);
    public record CreateCourierStatusesDto(int courier_id, int courier_status_id, string courier_status_title,
        string description, int status);
    public record UpdateCourierStatusesDto(int id, int courier_id, int courier_status_id, string courier_status_title,
        string description, int status);
}
