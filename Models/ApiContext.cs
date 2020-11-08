using Microsoft.EntityFrameworkCore;
using API.Models.Configurations;

namespace API.Models
{
    public class ApiContext : DbContext
    {
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<TruckEmployee> TruckEmployees { get; set; }
        public DbSet<User> Users { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
