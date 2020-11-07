using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Models.Configurations
{
    public class ProductBalanceConfiguration : IEntityTypeConfiguration<ProductBalance>
    {
        public void Configure(EntityTypeBuilder<ProductBalance> builder)
        {
            builder
                .HasOne(b => b.Medicament)
                .WithMany(m => m.Balances);
            builder
                .HasOne(b => b.Order)
                .WithMany(o => o.Products);
            builder
                .HasOne(b => b.Transaction)
                .WithMany(t => t.Products);
            builder
                .HasOne(b => b.Pharmacy)
                .WithMany(p => p.Products);
            builder
                .HasOne(b => b.Warehouse)
                .WithMany(w => w.Products);
            builder
                .HasOne(b => b.Provider)
                .WithMany(p => p.Products);
        }
    }
}
