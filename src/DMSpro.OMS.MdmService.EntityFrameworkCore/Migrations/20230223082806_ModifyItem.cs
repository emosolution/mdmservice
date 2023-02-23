using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class ModifyItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_UOMGroupDetails_InventoryUOMId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_UOMGroupDetails_PurUOMId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_UOMGroupDetails_SalesUOMId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "ERPCode",
                table: "Items",
                newName: "erpCode");

            migrationBuilder.AddColumn<decimal>(
                name: "PurUnitRate",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SalesUnitRate",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_UOMs_InventoryUOMId",
                table: "Items",
                column: "InventoryUOMId",
                principalTable: "UOMs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_UOMs_PurUOMId",
                table: "Items",
                column: "PurUOMId",
                principalTable: "UOMs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_UOMs_SalesUOMId",
                table: "Items",
                column: "SalesUOMId",
                principalTable: "UOMs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_UOMs_InventoryUOMId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_UOMs_PurUOMId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_UOMs_SalesUOMId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "PurUnitRate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SalesUnitRate",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "erpCode",
                table: "Items",
                newName: "ERPCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_UOMGroupDetails_InventoryUOMId",
                table: "Items",
                column: "InventoryUOMId",
                principalTable: "UOMGroupDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_UOMGroupDetails_PurUOMId",
                table: "Items",
                column: "PurUOMId",
                principalTable: "UOMGroupDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_UOMGroupDetails_SalesUOMId",
                table: "Items",
                column: "SalesUOMId",
                principalTable: "UOMGroupDetails",
                principalColumn: "Id");
        }
    }
}
