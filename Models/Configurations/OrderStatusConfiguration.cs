using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Models.Configurations
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.Property(e => e.Id).HasConversion<int>();
            builder.HasData(
                Enum.GetValues(typeof(OrderStatusId))
                    .Cast<OrderStatusId>().Select(
                        os => new OrderStatus()
                        {
                            Id = os,
                            Name = os.ToString()
                        })
            );
        }
    }
}
