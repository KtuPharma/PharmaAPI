using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Configurations
{
    public class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
        {
            builder.Property(m => m.UserId).HasMaxLength(85);
            builder.Property(m => m.LoginProvider).HasMaxLength(85);
            builder.Property(m => m.Name).HasMaxLength(85);
        } 
    }
}
