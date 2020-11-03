using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace API.Migrations
{
    public partial class CreateMedicamentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicaments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    ActiveSubstance = table.Column<string>(maxLength: 255, nullable: false),
                    BarCode = table.Column<string>(maxLength: 31, nullable: false),
                    RecipeRequired = table.Column<bool>(nullable: false),
                    IsReimbursed = table.Column<bool>(nullable: false),
                    Country = table.Column<string>(maxLength: 255, nullable: false),
                    Form = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicaments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PharmaceuticalForm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmaceuticalForm", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PharmaceuticalForm",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Tablets" },
                    { 2, "Syrup" },
                    { 3, "Suspension" },
                    { 4, "Lozenge" },
                    { 5, "Spray" },
                    { 6, "Drops" },
                    { 7, "Ointment" },
                    { 8, "Injection" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medicaments");

            migrationBuilder.DropTable(
                name: "PharmaceuticalForm");
        }
    }
}
