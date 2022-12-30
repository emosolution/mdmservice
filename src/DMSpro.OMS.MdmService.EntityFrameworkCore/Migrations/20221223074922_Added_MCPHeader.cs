using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_MCPHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "MCPHeaders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_MCPHeaders_CompanyId",
                table: "MCPHeaders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MCPHeaders_RouteId",
                table: "MCPHeaders",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_MCPHeaders_Companies_CompanyId",
                table: "MCPHeaders",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MCPHeaders_SalesOrgHierarchies_RouteId",
                table: "MCPHeaders",
                column: "RouteId",
                principalTable: "SalesOrgHierarchies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MCPHeaders_Companies_CompanyId",
                table: "MCPHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_MCPHeaders_SalesOrgHierarchies_RouteId",
                table: "MCPHeaders");

            migrationBuilder.DropIndex(
                name: "IX_MCPHeaders_CompanyId",
                table: "MCPHeaders");

            migrationBuilder.DropIndex(
                name: "IX_MCPHeaders_RouteId",
                table: "MCPHeaders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "MCPHeaders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
