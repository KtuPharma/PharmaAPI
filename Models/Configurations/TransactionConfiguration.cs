using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Models.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
                .HasOne(t => t.Pharmacist)
                .WithMany(p => p.Transactions);
            builder
                .HasMany(t => t.Products)
                .WithOne(b => b.Transaction);
        }
    }
}
