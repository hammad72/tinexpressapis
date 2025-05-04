using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record CourierStatusMappingDto(int id, int courier_id, int courier_status_id, string courier_status_title,
        string description, int tin_status_id, int status);
    public record CreateCourierStatusMappingDto(int courier_id, int courier_status_id, string courier_status_title,
        string description, int tin_status_id, int status);
    public record UpdateCourierStatusMappingDto(int id, int courier_id, int courier_status_id, string courier_status_title,
        string description, int tin_status_id, int status);
}
