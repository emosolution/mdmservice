using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCustomerAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HierarchyLevel",
                table: "CustomerAttributes");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "CustomerAttributes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "CustomerAttributes");

            migrationBuilder.AddColumn<int>(
                name: "HierarchyLevel",
                table: "CustomerAttributes",
                type: "int",
                maxLength: 19,
                nullable: true);
        }
    }
}
