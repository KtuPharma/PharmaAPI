using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace API.Migrations
{
    public partial class CreateOrdersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ProductBalance",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    OrderTime = table.Column<DateTime>(nullable: false),
                    DeliveryTime = table.Column<DateTime>(nullable: false),
                    AddressFrom = table.Column<string>(maxLength: 255, nullable: false),
                    AddressTo = table.Column<string>(maxLength: 255, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductBalance_OrderId",
                table: "ProductBalance",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_EmployeeId",
                table: "Order",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBalance_Order_OrderId",
                table: "ProductBalance",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBalance_Order_OrderId",
                table: "ProductBalance");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropIndex(
                name: "IX_ProductBalance_OrderId",
                table: "ProductBalance");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ProductBalance");
        }
    }
}
