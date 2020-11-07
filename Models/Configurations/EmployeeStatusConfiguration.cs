using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Models.Configurations
{
    public class EmployeeStatusConfiguration : IEntityTypeConfiguration<EmployeeStatus>
    {
        public void Configure(EntityTypeBuilder<EmployeeStatus> builder)
        {
            builder.Property(e => e.Id).HasConversion<int>();
            builder.HasData(
                Enum.GetValues(typeof(EmployeeStatusId))
                    .Cast<EmployeeStatusId>().Select(
                        es => new EmployeeStatus()
                        {
                            Id = es,
                            Name = es.ToString()
                        })
            );
        }
    }
}
