using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_CustomerAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EffDate",
                table: "CustomerAssignments");

            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "CustomerAssignments",
                newName: "CustomerId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveDate",
                table: "CustomerAssignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAssignments_CompanyId",
                table: "CustomerAssignments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAssignments_CustomerId",
                table: "CustomerAssignments",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAssignments_Companies_CompanyId",
                table: "CustomerAssignments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAssignments_CustomerProfiles_CustomerId",
                table: "CustomerAssignments",
                column: "CustomerId",
                principalTable: "CustomerProfiles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAssignments_Companies_CompanyId",
                table: "CustomerAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAssignments_CustomerProfiles_CustomerId",
                table: "CustomerAssignments");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAssignments_CompanyId",
                table: "CustomerAssignments");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAssignments_CustomerId",
                table: "CustomerAssignments");

            migrationBuilder.DropColumn(
                name: "EffectiveDate",
                table: "CustomerAssignments");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "CustomerAssignments",
                newName: "OutletId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EffDate",
                table: "CustomerAssignments",
                type: "datetime2",
                nullable: true);
        }
    }
}
