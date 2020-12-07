using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Models.Configurations
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder
                .HasMany(w => w.Products)
                .WithOne(b => b.Warehouse);
            builder
                .HasMany(w => w.Employees)
                .WithOne(e => e.Warehouse);
            builder
                .HasMany(w => w.Orders)
                .WithOne(o => o.Warehouse);
        }
    }
}
