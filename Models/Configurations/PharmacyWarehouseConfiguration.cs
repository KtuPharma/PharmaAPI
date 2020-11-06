using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Models.Configurations
{
    public class PharmacyWarehouseConfiguration : IEntityTypeConfiguration<PharmacyWarehouse>
    {
        public void Configure(EntityTypeBuilder<PharmacyWarehouse> builder)
        {
            builder.HasKey(pw => new {pw.PharmacyId, pw.WarehouseId});
            builder
                .HasOne(pw => pw.Pharmacy)
                .WithMany(p => p.PharmacyWarehouses)
                .HasForeignKey(pw => pw.PharmacyId);
            builder
                .HasOne(pw => pw.Warehouse)
                .WithMany(w => w.PharmacyWarehouses)
                .HasForeignKey(pw => pw.WarehouseId);
        }
    }
}
