using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Models.Configurations
{
    public class MedicineProviderConfiguration : IEntityTypeConfiguration<MedicineProvider>
    {
        public void Configure(EntityTypeBuilder<MedicineProvider> builder)
        {
            builder
                .HasMany(p => p.Products)
                .WithOne(p => p.Provider);
        }
    }
}
