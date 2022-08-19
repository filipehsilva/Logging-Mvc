using LoggingMvc.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoggingMvc.Data.Mappings
{
    public class LogMapping : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Date)
                .IsRequired();

            builder.Property(l => l.Type)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(l => l.Description)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.ToTable("Logs");
        }
    }
}
