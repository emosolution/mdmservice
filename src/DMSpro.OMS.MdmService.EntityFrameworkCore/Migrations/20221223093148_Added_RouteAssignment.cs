using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_RouteAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "RouteAssignments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_RouteAssignments_EmployeeId",
                table: "RouteAssignments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteAssignments_RouteId",
                table: "RouteAssignments",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteAssignments_EmployeeProfiles_EmployeeId",
                table: "RouteAssignments",
                column: "EmployeeId",
                principalTable: "EmployeeProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteAssignments_SalesOrgHierarchies_RouteId",
                table: "RouteAssignments",
                column: "RouteId",
                principalTable: "SalesOrgHierarchies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteAssignments_EmployeeProfiles_EmployeeId",
                table: "RouteAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteAssignments_SalesOrgHierarchies_RouteId",
                table: "RouteAssignments");

            migrationBuilder.DropIndex(
                name: "IX_RouteAssignments_EmployeeId",
                table: "RouteAssignments");

            migrationBuilder.DropIndex(
                name: "IX_RouteAssignments_RouteId",
                table: "RouteAssignments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "RouteAssignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
