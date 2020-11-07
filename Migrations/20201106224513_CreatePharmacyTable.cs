using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace API.Migrations
{
    public partial class CreatePharmacyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PharmacyId",
                table: "ProductBalance",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PharmacyId",
                table: "Employee",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pharmacy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 255, nullable: false),
                    City = table.Column<string>(maxLength: 255, nullable: false),
                    Cash = table.Column<decimal>(nullable: false),
                    Revenue = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Register",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Model = table.Column<string>(maxLength: 255, nullable: false),
                    Cash = table.Column<decimal>(maxLength: 255, nullable: false),
                    PharmacyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Register_Pharmacy_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    OrderAmount = table.Column<int>(nullable: false),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    PharmacyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Report_Pharmacy_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductBalance_PharmacyId",
                table: "ProductBalance",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PharmacyId",
                table: "Employee",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Register_PharmacyId",
                table: "Register",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_EmployeeId",
                table: "Report",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_PharmacyId",
                table: "Report",
                column: "PharmacyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Pharmacy_PharmacyId",
                table: "Employee",
                column: "PharmacyId",
                principalTable: "Pharmacy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBalance_Pharmacy_PharmacyId",
                table: "ProductBalance",
                column: "PharmacyId",
                principalTable: "Pharmacy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Pharmacy_PharmacyId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBalance_Pharmacy_PharmacyId",
                table: "ProductBalance");

            migrationBuilder.DropTable(
                name: "Register");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Pharmacy");

            migrationBuilder.DropIndex(
                name: "IX_ProductBalance_PharmacyId",
                table: "ProductBalance");

            migrationBuilder.DropIndex(
                name: "IX_Employee_PharmacyId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "PharmacyId",
                table: "ProductBalance");

            migrationBuilder.DropColumn(
                name: "PharmacyId",
                table: "Employee");
        }
    }
}
