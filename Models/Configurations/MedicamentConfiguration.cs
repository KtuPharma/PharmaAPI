using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Models.Configurations
{
    public class MedicamentConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.Property(m => m.Form).HasConversion<int>();
            builder
                .HasMany(m => m.Balances)
                .WithOne(b => b.Medicament);
        }
    }
}
