using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddInSaleAndQuantityForBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InSale",
                table: "ProductBalance",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProductBalance",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InSale",
                table: "ProductBalance");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductBalance");
        }
    }
}
