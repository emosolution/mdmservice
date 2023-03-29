using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPriceListAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "PricelistAssignments",
                newName: "ReleasedDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsReleased",
                table: "PricelistAssignments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReleased",
                table: "PricelistAssignments");

            migrationBuilder.RenameColumn(
                name: "ReleasedDate",
                table: "PricelistAssignments",
                newName: "ReleaseDate");
        }
    }
}
