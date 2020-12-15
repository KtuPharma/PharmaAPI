using Microsoft.EntityFrameworkCore;
using API.Models.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using API.Models;

namespace API.Models
{
    public class ApiContext : DbContext
    {
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<TruckEmployee> TruckEmployees { get; set; }
        public DbSet<Truck> Truck { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<ProductBalance> ProductBalances { get; set; }
        public DbSet<MedicineProvider> MedicineProvider { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Pharmacy> Pharmacy { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<ProviderWarehouse> ProviderWarehouse { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<EmployeeStatus> EmployeeStatus { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Report> Report { get; set; }

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
        }
    }
}
