using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_SalesOrgEmpAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValueId",
                table: "SalesOrgEmpAssignments",
                newName: "SalesOrgHierarchyId");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "SalesOrgEmpAssignments",
                newName: "EmployeeProfileId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "SalesOrgEmpAssignments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrgEmpAssignments_EmployeeProfileId",
                table: "SalesOrgEmpAssignments",
                column: "EmployeeProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrgEmpAssignments_SalesOrgHierarchyId",
                table: "SalesOrgEmpAssignments",
                column: "SalesOrgHierarchyId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrgEmpAssignments_EmployeeProfiles_EmployeeProfileId",
                table: "SalesOrgEmpAssignments",
                column: "EmployeeProfileId",
                principalTable: "EmployeeProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrgEmpAssignments_SalesOrgHierarchies_SalesOrgHierarchyId",
                table: "SalesOrgEmpAssignments",
                column: "SalesOrgHierarchyId",
                principalTable: "SalesOrgHierarchies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrgEmpAssignments_EmployeeProfiles_EmployeeProfileId",
                table: "SalesOrgEmpAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrgEmpAssignments_SalesOrgHierarchies_SalesOrgHierarchyId",
                table: "SalesOrgEmpAssignments");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrgEmpAssignments_EmployeeProfileId",
                table: "SalesOrgEmpAssignments");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrgEmpAssignments_SalesOrgHierarchyId",
                table: "SalesOrgEmpAssignments");

            migrationBuilder.RenameColumn(
                name: "SalesOrgHierarchyId",
                table: "SalesOrgEmpAssignments",
                newName: "ValueId");

            migrationBuilder.RenameColumn(
                name: "EmployeeProfileId",
                table: "SalesOrgEmpAssignments",
                newName: "EmployeeId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "SalesOrgEmpAssignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
