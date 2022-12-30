using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_SSHistoryInZone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SellingZoneId",
                table: "SSHistoryInZones",
                newName: "SalesOrgHierarchyId");

            migrationBuilder.CreateIndex(
                name: "IX_SSHistoryInZones_EmployeeId",
                table: "SSHistoryInZones",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SSHistoryInZones_SalesOrgHierarchyId",
                table: "SSHistoryInZones",
                column: "SalesOrgHierarchyId");

            migrationBuilder.AddForeignKey(
                name: "FK_SSHistoryInZones_EmployeeProfiles_EmployeeId",
                table: "SSHistoryInZones",
                column: "EmployeeId",
                principalTable: "EmployeeProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SSHistoryInZones_SalesOrgHierarchies_SalesOrgHierarchyId",
                table: "SSHistoryInZones",
                column: "SalesOrgHierarchyId",
                principalTable: "SalesOrgHierarchies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SSHistoryInZones_EmployeeProfiles_EmployeeId",
                table: "SSHistoryInZones");

            migrationBuilder.DropForeignKey(
                name: "FK_SSHistoryInZones_SalesOrgHierarchies_SalesOrgHierarchyId",
                table: "SSHistoryInZones");

            migrationBuilder.DropIndex(
                name: "IX_SSHistoryInZones_EmployeeId",
                table: "SSHistoryInZones");

            migrationBuilder.DropIndex(
                name: "IX_SSHistoryInZones_SalesOrgHierarchyId",
                table: "SSHistoryInZones");

            migrationBuilder.RenameColumn(
                name: "SalesOrgHierarchyId",
                table: "SSHistoryInZones",
                newName: "SellingZoneId");
        }
    }
}
