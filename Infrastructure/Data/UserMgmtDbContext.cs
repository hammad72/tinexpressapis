using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class UserMgmtDbContext : DbContext
    {
        public DbSet<loginmodel> loginmodel { get; set; }
        public DbSet<logindetail> logindetail { get; set; }
        public DbSet<userlogins> userlogins { get; set; }
        public DbSet<userprofile> userprofile { get; set; }
        public DbSet<customerprofile> customerprofile { get; set; }
        public DbSet<customeruserprofile> customeruserprofile { get; set; }
        public DbSet<couriers> couriers { get; set; }
        public DbSet<courieruserprofile> courieruserprofile { get; set; }
        public DbSet<usertypes> usertypes { get; set; }
        public UserMgmtDbContext(DbContextOptions<UserMgmtDbContext> options) : base(options)
        {
        }
    }
}
