using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class logindetail
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public int UType { get; set; }
        public int UStatus { get; set; }

        public DateTime LastActiveTime { get; set; }

        public string Token { get; set; }
    }
}
