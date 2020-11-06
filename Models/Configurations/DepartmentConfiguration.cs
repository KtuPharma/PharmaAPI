using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Models.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(e => e.Id).HasConversion<int>();
            builder.HasData(
                Enum.GetValues(typeof(DepartmentId))
                    .Cast<DepartmentId>().Select(
                        d => new Department()
                        {
                            Id = d,
                            Name = d.ToString()
                        })
            );
        }
    }
}
