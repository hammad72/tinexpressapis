using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class userlogins
    {
        public int id { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public int user_type { get; set; }

        public bool first_login { get; set; }

        public string? other { get; set; }

        [Required]
        [Column(TypeName = "TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime created_at { get; set; }

        public int created_by { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? updated_at { get; set; }

        public int? updated_by { get; set; }

        public int status { get; set; }
    }
}
