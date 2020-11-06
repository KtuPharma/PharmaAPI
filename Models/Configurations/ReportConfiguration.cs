using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Models.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder
                .HasOne(r => r.Employee)
                .WithMany(e => e.Reports);
            builder
                .HasOne(r => r.Pharmacy)
                .WithMany(p => p.Reports);
        }
    }
}
