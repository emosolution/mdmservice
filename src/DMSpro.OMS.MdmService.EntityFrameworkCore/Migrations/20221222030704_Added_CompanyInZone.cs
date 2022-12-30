using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_CompanyInZone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SellingZoneId",
                table: "CompanyInZones",
                newName: "SalesOrgHierarchyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInZones_CompanyId",
                table: "CompanyInZones",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInZones_SalesOrgHierarchyId",
                table: "CompanyInZones",
                column: "SalesOrgHierarchyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyInZones_Companies_CompanyId",
                table: "CompanyInZones",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyInZones_SalesOrgHierarchies_SalesOrgHierarchyId",
                table: "CompanyInZones",
                column: "SalesOrgHierarchyId",
                principalTable: "SalesOrgHierarchies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyInZones_Companies_CompanyId",
                table: "CompanyInZones");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyInZones_SalesOrgHierarchies_SalesOrgHierarchyId",
                table: "CompanyInZones");

            migrationBuilder.DropIndex(
                name: "IX_CompanyInZones_CompanyId",
                table: "CompanyInZones");

            migrationBuilder.DropIndex(
                name: "IX_CompanyInZones_SalesOrgHierarchyId",
                table: "CompanyInZones");

            migrationBuilder.RenameColumn(
                name: "SalesOrgHierarchyId",
                table: "CompanyInZones",
                newName: "SellingZoneId");
        }
    }
}
