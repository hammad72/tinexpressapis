using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record CourierUserProfileDto(int id, string first_name, string? last_name, string? emp_num, string email, string? dob,
        DateTime? enrollment_date, DateTime? joining_date, string? address, string? postal_code, int? city_id, string? phone_number,
        string? other, int courier_id, DateTime created_at, int created_by, DateTime? updated_at, int? updated_by, int status);
    public record CreateCourierUserProfileDto_Ex(string first_name, string? last_name, string? emp_num, string email, string? dob,
        DateTime? enrollment_date, DateTime? joining_date, string? address, string? postal_code, int? city_id, string? phone_number,
        string? other, int courier_id, int created_by, int user_role_id, int status);
    public record CreateCourierUserProfileDto(int id, string first_name, string? last_name, string? emp_num, string email, string? dob,
        DateTime? enrollment_date, DateTime? joining_date, string? address, string? postal_code, int? city_id, string? phone_number,
        string? other, int courier_id, int created_by, int status);
    public record UpdateCourierUserProfileDto(int id, string first_name, string? last_name, string? emp_num, string email, string? dob,
        DateTime? enrollment_date, DateTime? joining_date, string? address, string? postal_code, int? city_id, string? phone_number,
        string? other, int courier_id, int? updated_by, int status);
}

