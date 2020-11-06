using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace API.Migrations
{
    public partial class CreateTransactionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "ProductBalance",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Sum = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Method = table.Column<int>(nullable: false),
                    PharmacistId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Employee_PharmacistId",
                        column: x => x.PharmacistId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductBalance_TransactionId",
                table: "ProductBalance",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PharmacistId",
                table: "Transaction",
                column: "PharmacistId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBalance_Transaction_TransactionId",
                table: "ProductBalance",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBalance_Transaction_TransactionId",
                table: "ProductBalance");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_ProductBalance_TransactionId",
                table: "ProductBalance");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "ProductBalance");
        }
    }
}
