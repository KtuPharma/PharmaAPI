using Microsoft.EntityFrameworkCore;
using API.Models.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace API.Models
{
    public class ApiContext : IdentityDbContext<User>
    {
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<TruckEmployee> TruckEmployees { get; set; }
        public DbSet<User> User { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionMethodConfiguration());
            modelBuilder.ApplyConfiguration(new PharmaceuticalFormConfigurator());
            modelBuilder.ApplyConfiguration(new EmployeeStatusConfiguration());

            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new TruckEmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductBalanceConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new MedicamentConfiguration());

            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            modelBuilder.ApplyConfiguration(new PharmacyConfiguration());
            modelBuilder.ApplyConfiguration(new RegisterConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
            modelBuilder.ApplyConfiguration(new MedicineProviderConfiguration());
            modelBuilder.ApplyConfiguration(new PharmacyWarehouseConfiguration());
            modelBuilder.ApplyConfiguration(new ProviderWarehouseConfiguration());

            modelBuilder.ApplyConfiguration(new IdentityUserConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityRoleConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityUserLoginConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityUserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityUserTokenConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityUserClaimConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityRoleClaimConfiguration());
        }
    }
}
