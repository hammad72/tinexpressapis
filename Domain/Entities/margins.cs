using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class margins
    {
        [Key]
        public int id { get; set; }
        public int courier_id { get; set; }
        public string? courier_title { get; set; }
        public double margin { get; set; }
    }
}
