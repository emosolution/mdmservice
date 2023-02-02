using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCustomer23013023202013 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute1I7d",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Attribute1I7d",
                table: "Customers",
                newName: "Attribute17Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute1I7d",
                table: "Customers",
                newName: "IX_Customers_Attribute17Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute17Id",
                table: "Customers",
                column: "Attribute17Id",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute17Id",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Attribute17Id",
                table: "Customers",
                newName: "Attribute1I7d");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Attribute17Id",
                table: "Customers",
                newName: "IX_Customers_Attribute1I7d");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CusAttributeValues_Attribute1I7d",
                table: "Customers",
                column: "Attribute1I7d",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");
        }
    }
}
