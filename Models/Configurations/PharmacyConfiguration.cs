using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Models.Configurations
{
    public class PharmacyConfiguration : IEntityTypeConfiguration<Pharmacy>
    {
        public void Configure(EntityTypeBuilder<Pharmacy> builder)
        {
            builder
                .HasMany(p => p.Registers)
                .WithOne(r => r.Pharmacy);
            builder
                .HasMany(p => p.Employees)
                .WithOne(e => e.Pharmacy);
            builder
                .HasMany(p => p.Reports)
                .WithOne(r => r.Pharmacy);
            builder
                .HasMany(p => p.Products)
                .WithOne(p => p.Pharmacy);
        }
    }
}
