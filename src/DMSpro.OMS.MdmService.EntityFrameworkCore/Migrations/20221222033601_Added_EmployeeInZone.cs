using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_EmployeeInZone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SellingZoneId",
                table: "EmployeeInZones",
                newName: "SalesOrgHierarchyId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EffectiveDate",
                table: "EmployeeInZones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInZones_EmployeeId",
                table: "EmployeeInZones",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInZones_SalesOrgHierarchyId",
                table: "EmployeeInZones",
                column: "SalesOrgHierarchyId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeInZones_EmployeeProfiles_EmployeeId",
                table: "EmployeeInZones",
                column: "EmployeeId",
                principalTable: "EmployeeProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeInZones_SalesOrgHierarchies_SalesOrgHierarchyId",
                table: "EmployeeInZones",
                column: "SalesOrgHierarchyId",
                principalTable: "SalesOrgHierarchies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeInZones_EmployeeProfiles_EmployeeId",
                table: "EmployeeInZones");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeInZones_SalesOrgHierarchies_SalesOrgHierarchyId",
                table: "EmployeeInZones");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeInZones_EmployeeId",
                table: "EmployeeInZones");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeInZones_SalesOrgHierarchyId",
                table: "EmployeeInZones");

            migrationBuilder.RenameColumn(
                name: "SalesOrgHierarchyId",
                table: "EmployeeInZones",
                newName: "SellingZoneId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EffectiveDate",
                table: "EmployeeInZones",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
