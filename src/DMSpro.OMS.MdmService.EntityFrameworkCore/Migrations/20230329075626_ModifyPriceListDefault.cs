using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPriceListDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDefault",
                table: "PriceLists",
                newName: "IsDefaultForVendor");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultForCustomer",
                table: "PriceLists",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefaultForCustomer",
                table: "PriceLists");

            migrationBuilder.RenameColumn(
                name: "IsDefaultForVendor",
                table: "PriceLists",
                newName: "IsDefault");
        }
    }
}
