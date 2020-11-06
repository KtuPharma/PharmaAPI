﻿// <auto-generated />
using System;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(ApiContext))]
    [Migration("20201106233905_CreateWarehouseTable")]
    partial class CreateWarehouseTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("API.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Department");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Pharmacy"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Warehouse"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Transportation"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime");

                    b.Property<int>("Department")
                        .HasColumnType("int");

                    b.Property<DateTime>("FireDate")
                        .HasColumnType("datetime");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("PersonalCode")
                        .IsRequired()
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(15);

                    b.Property<int?>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("API.Models.EmployeeStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EmployeeStatus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Employed"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Vacationing"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Fired"
                        });
                });

            modelBuilder.Entity("API.Models.Medicament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ActiveSubstance")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("BarCode")
                        .IsRequired()
                        .HasColumnType("varchar(31)")
                        .HasMaxLength(31);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("Form")
                        .HasColumnType("int");

                    b.Property<bool>("IsReimbursed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("RecipeRequired")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Medicaments");
                });

            modelBuilder.Entity("API.Models.MedicineProvider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("MedicineProvider");
                });

            modelBuilder.Entity("API.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AuthorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("API.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AddressFrom")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("AddressTo")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime>("DeliveryTime")
                        .HasColumnType("datetime");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("datetime");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("API.Models.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OrderStatus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Waiting"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Preparing"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Prepared"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Delivering"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Delivered"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Canceled"
                        });
                });

            modelBuilder.Entity("API.Models.PharmaceuticalForm", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PharmaceuticalForm");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Tablets"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Syrup"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Suspension"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Lozenge"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Spray"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Drops"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Ointment"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Injection"
                        });
                });

            modelBuilder.Entity("API.Models.Pharmacy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<decimal>("Cash")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<decimal>("Revenue")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.ToTable("Pharmacy");
                });

            modelBuilder.Entity("API.Models.PharmacyWarehouse", b =>
                {
                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("PharmacyId", "WarehouseId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("PharmacyWarehouse");
                });

            modelBuilder.Entity("API.Models.ProductBalance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime");

                    b.Property<int>("MedicamentId")
                        .HasColumnType("int");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int?>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("ProviderId")
                        .HasColumnType("int");

                    b.Property<int?>("TransactionId")
                        .HasColumnType("int");

                    b.Property<int?>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicamentId");

                    b.HasIndex("OrderId");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("ProviderId");

                    b.HasIndex("TransactionId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("ProductBalance");
                });

            modelBuilder.Entity("API.Models.ProviderWarehouse", b =>
                {
                    b.Property<int>("ProviderId")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("ProviderId", "WarehouseId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("ProviderWarehouse");
                });

            modelBuilder.Entity("API.Models.Register", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Cash")
                        .HasColumnType("decimal(18, 2)")
                        .HasMaxLength(255);

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PharmacyId");

                    b.ToTable("Register");
                });

            modelBuilder.Entity("API.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("OrderAmount")
                        .HasColumnType("int");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PharmacyId");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("API.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<int>("Method")
                        .HasColumnType("int");

                    b.Property<int>("PharmacistId")
                        .HasColumnType("int");

                    b.Property<decimal>("Sum")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("PharmacistId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("API.Models.TransactionMethod", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TransactionMethod");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Cash"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Card"
                        });
                });

            modelBuilder.Entity("API.Models.Truck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<float>("Capacity")
                        .HasColumnType("float");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("PlateNumber")
                        .IsRequired()
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.ToTable("Truck");
                });

            modelBuilder.Entity("API.Models.TruckEmployee", b =>
                {
                    b.Property<int>("TruckId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.HasKey("TruckId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("TruckEmployees");
                });

            modelBuilder.Entity("API.Models.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Warehouse");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.HasOne("API.Models.Pharmacy", "Pharmacy")
                        .WithMany("Employees")
                        .HasForeignKey("PharmacyId");

                    b.HasOne("API.Models.Warehouse", "Warehouse")
                        .WithMany("Employees")
                        .HasForeignKey("WarehouseId");
                });

            modelBuilder.Entity("API.Models.Message", b =>
                {
                    b.HasOne("API.Models.Employee", "Author")
                        .WithMany("Messages")
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("API.Models.Order", b =>
                {
                    b.HasOne("API.Models.Employee", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API.Models.PharmacyWarehouse", b =>
                {
                    b.HasOne("API.Models.Pharmacy", "Pharmacy")
                        .WithMany("PharmacyWarehouses")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Warehouse", "Warehouse")
                        .WithMany("PharmacyWarehouses")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API.Models.ProductBalance", b =>
                {
                    b.HasOne("API.Models.Medicament", "Medicament")
                        .WithMany("Balances")
                        .HasForeignKey("MedicamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Order", "Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderId");

                    b.HasOne("API.Models.Pharmacy", "Pharmacy")
                        .WithMany("Products")
                        .HasForeignKey("PharmacyId");

                    b.HasOne("API.Models.MedicineProvider", "Provider")
                        .WithMany("Products")
                        .HasForeignKey("ProviderId");

                    b.HasOne("API.Models.Transaction", "Transaction")
                        .WithMany("Products")
                        .HasForeignKey("TransactionId");

                    b.HasOne("API.Models.Warehouse", "Warehouse")
                        .WithMany("Products")
                        .HasForeignKey("WarehouseId");
                });

            modelBuilder.Entity("API.Models.ProviderWarehouse", b =>
                {
                    b.HasOne("API.Models.MedicineProvider", "Provider")
                        .WithMany("ProviderWarehouses")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Warehouse", "Warehouse")
                        .WithMany("ProviderWarehouses")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API.Models.Register", b =>
                {
                    b.HasOne("API.Models.Pharmacy", "Pharmacy")
                        .WithMany("Registers")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API.Models.Report", b =>
                {
                    b.HasOne("API.Models.Employee", "Employee")
                        .WithMany("Reports")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Pharmacy", "Pharmacy")
                        .WithMany("Reports")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API.Models.Transaction", b =>
                {
                    b.HasOne("API.Models.Employee", "Pharmacist")
                        .WithMany("Transactions")
                        .HasForeignKey("PharmacistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API.Models.TruckEmployee", b =>
                {
                    b.HasOne("API.Models.Employee", "Employee")
                        .WithMany("TruckEmployees")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Truck", "Truck")
                        .WithMany("TruckEmployees")
                        .HasForeignKey("TruckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
