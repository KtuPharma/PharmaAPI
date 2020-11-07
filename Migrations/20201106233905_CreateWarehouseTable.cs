using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace API.Migrations
{
    public partial class CreateWarehouseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProviderId",
                table: "ProductBalance",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "ProductBalance",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Employee",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MedicineProvider",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Country = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineProvider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 255, nullable: false),
                    City = table.Column<string>(maxLength: 255, nullable: false),
                    Country = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PharmacyWarehouse",
                columns: table => new
                {
                    PharmacyId = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacyWarehouse", x => new { x.PharmacyId, x.WarehouseId });
                    table.ForeignKey(
                        name: "FK_PharmacyWarehouse_Pharmacy_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PharmacyWarehouse_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProviderWarehouse",
                columns: table => new
                {
                    ProviderId = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderWarehouse", x => new { x.ProviderId, x.WarehouseId });
                    table.ForeignKey(
                        name: "FK_ProviderWarehouse_MedicineProvider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "MedicineProvider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProviderWarehouse_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductBalance_ProviderId",
                table: "ProductBalance",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBalance_WarehouseId",
                table: "ProductBalance",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_WarehouseId",
                table: "Employee",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyWarehouse_WarehouseId",
                table: "PharmacyWarehouse",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderWarehouse_WarehouseId",
                table: "ProviderWarehouse",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Warehouse_WarehouseId",
                table: "Employee",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBalance_MedicineProvider_ProviderId",
                table: "ProductBalance",
                column: "ProviderId",
                principalTable: "MedicineProvider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBalance_Warehouse_WarehouseId",
                table: "ProductBalance",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Warehouse_WarehouseId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBalance_MedicineProvider_ProviderId",
                table: "ProductBalance");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBalance_Warehouse_WarehouseId",
                table: "ProductBalance");

            migrationBuilder.DropTable(
                name: "PharmacyWarehouse");

            migrationBuilder.DropTable(
                name: "ProviderWarehouse");

            migrationBuilder.DropTable(
                name: "MedicineProvider");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_ProductBalance_ProviderId",
                table: "ProductBalance");

            migrationBuilder.DropIndex(
                name: "IX_ProductBalance_WarehouseId",
                table: "ProductBalance");

            migrationBuilder.DropIndex(
                name: "IX_Employee_WarehouseId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "ProductBalance");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "ProductBalance");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Employee");
        }
    }
}
