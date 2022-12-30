using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Updated_Route_22122314304545 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SellingCateId",
                table: "Routes",
                newName: "SalesOrgHierarchyId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_SalesOrgHierarchyId",
                table: "Routes",
                column: "SalesOrgHierarchyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_SalesOrgHierarchies_SalesOrgHierarchyId",
                table: "Routes",
                column: "SalesOrgHierarchyId",
                principalTable: "SalesOrgHierarchies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_SalesOrgHierarchies_SalesOrgHierarchyId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_SalesOrgHierarchyId",
                table: "Routes");

            migrationBuilder.RenameColumn(
                name: "SalesOrgHierarchyId",
                table: "Routes",
                newName: "SellingCateId");
        }
    }
}
