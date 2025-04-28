using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class paymentmethod
    {
        [Required]
        public int id { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public int status { get; set; }
    }
}
