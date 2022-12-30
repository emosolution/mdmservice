using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_CustomerInZone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SellingZoneId",
                table: "CustomerInZones",
                newName: "SalesOrgHierarchyId");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "CustomerInZones",
                newName: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInZones_CustomerId",
                table: "CustomerInZones",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInZones_SalesOrgHierarchyId",
                table: "CustomerInZones",
                column: "SalesOrgHierarchyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerInZones_CustomerProfiles_CustomerId",
                table: "CustomerInZones",
                column: "CustomerId",
                principalTable: "CustomerProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerInZones_SalesOrgHierarchies_SalesOrgHierarchyId",
                table: "CustomerInZones",
                column: "SalesOrgHierarchyId",
                principalTable: "SalesOrgHierarchies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerInZones_CustomerProfiles_CustomerId",
                table: "CustomerInZones");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerInZones_SalesOrgHierarchies_SalesOrgHierarchyId",
                table: "CustomerInZones");

            migrationBuilder.DropIndex(
                name: "IX_CustomerInZones_CustomerId",
                table: "CustomerInZones");

            migrationBuilder.DropIndex(
                name: "IX_CustomerInZones_SalesOrgHierarchyId",
                table: "CustomerInZones");

            migrationBuilder.RenameColumn(
                name: "SalesOrgHierarchyId",
                table: "CustomerInZones",
                newName: "SellingZoneId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "CustomerInZones",
                newName: "OutletId");
        }
    }
}
