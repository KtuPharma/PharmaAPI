using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Models.Configurations
{
    public class TransactionMethodConfiguration : IEntityTypeConfiguration<TransactionMethod>
    {
        public void Configure(EntityTypeBuilder<TransactionMethod> builder)
        {
            builder.Property(e => e.Id).HasConversion<int>();
            builder.HasData(
                Enum.GetValues(typeof(TransactionMethodId))
                    .Cast<TransactionMethodId>().Select(
                        tm => new TransactionMethod()
                        {
                            Id = tm,
                            Name = tm.ToString()
                        })
            );
        }
    }
}
