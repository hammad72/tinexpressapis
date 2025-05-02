using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class OrderDbContext : DbContext
    {
        public DbSet<packagetype> packagetype { get; set; }
        public DbSet<packagecontent> packagecontent { get; set; }
        public DbSet<paymentmethod> paymentmethod { get; set; }
        public DbSet<orderdetails> orderdetails { get; set; }
        public DbSet<orderitems> orderitems { get; set; }
        public DbSet<courierapisetting> courierapisetting { get; set; }
        public DbSet<customerpriority> customerpriority { get; set; }
        public DbSet<customerbudget> customerbudget { get; set; }
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<orderitems>()
                .Property(b => b.consignment_number)
                .IsRequired(false);

            modelBuilder.Entity<orderitems>()
                .Property(b => b.order_number)
                .IsRequired(false);
        }

    }
}
