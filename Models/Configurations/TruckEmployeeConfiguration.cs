using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Models.Configurations
{
    public class TruckEmployeeConfiguration : IEntityTypeConfiguration<TruckEmployee>
    {
        public void Configure(EntityTypeBuilder<TruckEmployee> builder)
        {
            builder.HasKey(te => new {te.TruckId, te.EmployeeId});
            builder
                .HasOne(te => te.Truck)
                .WithMany(t => t.TruckEmployees)
                .HasForeignKey(te => te.TruckId);
            builder
                .HasOne(te => te.Employee)
                .WithMany(e => e.TruckEmployees)
                .HasForeignKey(te => te.EmployeeId);
        }
    }
}
