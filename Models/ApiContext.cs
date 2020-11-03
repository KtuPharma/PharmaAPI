using System;
using System.Linq;
using API.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class ApiContext : DbContext
    {
        public DbSet<Medicament> Medicaments { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicament>().Property(e => e.Form).HasConversion<int>();
            modelBuilder.Entity<PharmaceuticalForm>().Property(e => e.Id).HasConversion<int>();
            modelBuilder.Entity<PharmaceuticalForm>().HasData(
                Enum.GetValues(typeof(PharmaceuticalFormId))
                    .Cast<PharmaceuticalFormId>().Select(
                        e => new PharmaceuticalForm()
                        {
                            Id = e,
                            Name = e.ToString()
                        })
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
