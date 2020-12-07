using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Models.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasOne(o => o.Employee)
                .WithMany(e => e.Orders);
            builder
                .HasMany(o => o.Products)
                .WithOne(b => b.Order);
            builder
                .HasOne(o => o.Warehouse)
                .WithMany(w => w.Orders);
        }
    }
}
