using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_CustomerAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "CustomerAttributes");

            migrationBuilder.DropColumn(
                name: "CustomerAttributeTree",
                table: "CustomerAttributes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CustomerAttributes");

            migrationBuilder.AddColumn<string>(
                name: "AttrName",
                table: "CustomerAttributes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AttrNo",
                table: "CustomerAttributes",
                type: "int",
                maxLength: 19,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HierarchyLevel",
                table: "CustomerAttributes",
                type: "int",
                maxLength: 19,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttrName",
                table: "CustomerAttributes");

            migrationBuilder.DropColumn(
                name: "AttrNo",
                table: "CustomerAttributes");

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

            migrationBuilder.AddColumn<string>(
                name: "CustomerAttributeTree",
                table: "CustomerAttributes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CustomerAttributes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
