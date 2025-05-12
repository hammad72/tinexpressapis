using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class OrderDbContext : DbContext
    {
        public DbSet<options> options { get; set; }
        public DbSet<ordersource> ordersource { get; set; }

        public DbSet<packagetype> packagetype { get; set; }
        public DbSet<packagecontent> packagecontent { get; set; }
        public DbSet<paymentmethod> paymentmethod { get; set; }
        public DbSet<orderdetails> orderdetails { get; set; }
        public DbSet<orderitems> orderitems { get; set; }
        public DbSet<courierapisetting> courierapisetting { get; set; }
        public DbSet<customerpriority> customerpriority { get; set; }
        public DbSet<customerbudget> customerbudget { get; set; }
        public DbSet<orderstatuses> orderstatuses { get; set; }
        public DbSet<courierstatuses> courierstatuses { get; set; }
        public DbSet<courierstatusmapping> courierstatusmapping { get; set; }
        public DbSet<favaddresses> favaddresses { get; set; }
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
            //Test 3
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
