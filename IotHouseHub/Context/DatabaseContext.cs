using IOTDevice.Models;
using Microsoft.EntityFrameworkCore;

namespace IotHouseHub.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Device>(entity =>
            {
                entity.HasKey(p => p.Id);
            });
        }
    }
}
