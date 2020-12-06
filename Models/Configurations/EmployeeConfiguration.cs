using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Models.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .HasMany(e => e.Messages)
                .WithOne(m => m.Author);
            builder
                .HasMany(e => e.Orders)
                .WithOne(o => o.Employee);
            builder
                .HasMany(e => e.Reports)
                .WithOne(r => r.Employee);
            builder
                .HasOne(e => e.Pharmacy)
                .WithMany(p => p.Employees);
            builder
                .HasOne(e => e.Warehouse)
                .WithMany(w => w.Employees);

            builder.HasIndex(e => e.Username).IsUnique();
            builder.HasIndex(e => e.Email).IsUnique();            
        }
    }
}
