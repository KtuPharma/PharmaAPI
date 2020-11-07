using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Models.Configurations
{
    public class ProviderWarehouseConfiguration : IEntityTypeConfiguration<ProviderWarehouse>
    {
        public void Configure(EntityTypeBuilder<ProviderWarehouse> builder)
        {
            builder.HasKey(pw => new {pw.ProviderId, pw.WarehouseId});
            builder
                .HasOne(pw => pw.Provider)
                .WithMany(p => p.ProviderWarehouses)
                .HasForeignKey(pw => pw.ProviderId);
            builder
                .HasOne(pw => pw.Warehouse)
                .WithMany(w => w.ProviderWarehouses)
                .HasForeignKey(pw => pw.WarehouseId);
        }
    }
}
