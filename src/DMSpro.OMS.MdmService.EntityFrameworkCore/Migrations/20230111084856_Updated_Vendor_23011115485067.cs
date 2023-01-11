using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Updated_Vendor_23011115485067 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Companies_LinkedCompanyId",
                table: "Vendors");

            migrationBuilder.RenameColumn(
                name: "LinkedCompanyId",
                table: "Vendors",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Vendors_LinkedCompanyId",
                table: "Vendors",
                newName: "IX_Vendors_CompanyId");

            migrationBuilder.AddColumn<string>(
                name: "LinkedCompany",
                table: "Vendors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Companies_CompanyId",
                table: "Vendors",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Companies_CompanyId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "LinkedCompany",
                table: "Vendors");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Vendors",
                newName: "LinkedCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Vendors_CompanyId",
                table: "Vendors",
                newName: "IX_Vendors_LinkedCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Companies_LinkedCompanyId",
                table: "Vendors",
                column: "LinkedCompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
