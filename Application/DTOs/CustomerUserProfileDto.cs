using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record CustomerUserProfileDto(int id, string first_name, string? last_name, string? emp_num, string email, string? dob,
        DateTime? enrollment_date, DateTime? joining_date, string? address, string? postal_code, int? city_id, string? phone_number,
        string? other, int customer_id, DateTime created_at, int created_by, DateTime? updated_at, int? updated_by, int status);
    public record CreateCustomerUserProfileDto_Ex(string first_name, string? last_name, string? emp_num, string email, string? dob,
        DateTime? enrollment_date, DateTime? joining_date, string? address, string? postal_code, int? city_id, string? phone_number,
        string? other, int customer_id, int created_by, int user_role_id, int status);
    public record CreateCustomerUserProfileDto(int id, string first_name, string? last_name, string? emp_num, string email, string? dob,
        DateTime? enrollment_date, DateTime? joining_date, string? address, string? postal_code, int? city_id, string? phone_number,
        string? other, int customer_id, int created_by, int status);
    public record UpdateCustomerUserProfileDto(int id, string first_name, string? last_name, string? emp_num, string email, string? dob,
        DateTime? enrollment_date, DateTime? joining_date, string? address, string? postal_code, int? city_id, string? phone_number,
        string? other, int customer_id, int? updated_by, int status);
}
