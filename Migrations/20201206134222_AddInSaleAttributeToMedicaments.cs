using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddInSaleAttributeToMedicaments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InSale",
                table: "Medicaments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InSale",
                table: "Medicaments");
        }
    }
}
