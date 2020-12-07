using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddedWarehouseToOrdersAndOdersToWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Order",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_WarehouseId",
                table: "Order",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Warehouse_WarehouseId",
                table: "Order",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Warehouse_WarehouseId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_WarehouseId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Order");
        }
    }
}
