using LoggingMvc.Business.Models;
using LoggingMvc.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace LoggingMvc.Data.Context
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogMapping());
        }
    }
}
