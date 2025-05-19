using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace Domain.Entities
{
    public class supportcomplains
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string request_type { get; set; }
        // Partial Delivery fields
        public int? total_packages { get; set; }
        public int? receivedpackages { get; set; }
        public string? courier_reference { get; set; }

        // Lost Package fields
        public string? weight_dimensions { get; set; }

        // Common fields
        public string? package_description { get; set; }
        public string? feedback { get; set; }

        [Required]
        public string? reference_number { get; set; }
        public int? customer_id { get; set; }
        public string? customer_name { get; set; }

        public string? file_paths { get; set; } // Comma-separated file paths
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public int? created_by { get; set; }
        public DateTime? updated_at { get; set; } 
        public int? updated_by { get; set; }
        public int? status { get; set; }
    }
    public class SupportEnityRef
    {
        public string request_type { get; set; }
        public int? total_packages { get; set; }
        public int? received_packages { get; set; }
        public string courier_reference { get; set; }
        public string weight_dimensions { get; set; }
        public string package_description { get; set; }
        public string feedback { get; set; }
        public string reference_number { get; set; }
        public int customer_id { get; set; }
        public List<IFormFile> files { get; set; }
    }

    //public class SupportResponseDto
    //{
    //    public int id { get; set; }
    //    public string request_type { get; set; }
    //    public string reference_number { get; set; }
    //    public DateTime created_at { get; set; }
    //}
}