using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Configurations
{
    public class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            builder.Property(m => m.Id).HasMaxLength(85);
            builder.Property(m => m.NormalizedEmail).HasMaxLength(85);
            builder.Property(m => m.NormalizedUserName).HasMaxLength(85);
        }
    }
}
