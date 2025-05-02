using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class NotifcationDBContext:DbContext
    {
        public DbSet<userotp> userotp { get; set; }
        public NotifcationDBContext(DbContextOptions<NotifcationDBContext> options) : base(options)
        {
        }
    }
}
