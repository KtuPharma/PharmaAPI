using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Models.Configurations
{
    public class PharmaceuticalFormConfigurator : IEntityTypeConfiguration<PharmaceuticalForm>
    {
        public void Configure(EntityTypeBuilder<PharmaceuticalForm> builder)
        {
            builder.Property(e => e.Id).HasConversion<int>();
            builder.HasData(
                Enum.GetValues(typeof(PharmaceuticalFormId))
                    .Cast<PharmaceuticalFormId>().Select(
                        pf => new PharmaceuticalForm()
                        {
                            Id = pf,
                            Name = pf.ToString()
                        })
            );
        }
    }
}
