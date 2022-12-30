using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_VisitPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "VisitPlans");

            migrationBuilder.DropColumn(
                name: "OutletId",
                table: "VisitPlans");

            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "VisitPlans",
                newName: "MCPDetailId");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "VisitPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VisitPlans_MCPDetailId",
                table: "VisitPlans",
                column: "MCPDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitPlans_MCPDetails_MCPDetailId",
                table: "VisitPlans",
                column: "MCPDetailId",
                principalTable: "MCPDetails",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitPlans_MCPDetails_MCPDetailId",
                table: "VisitPlans");

            migrationBuilder.DropIndex(
                name: "IX_VisitPlans_MCPDetailId",
                table: "VisitPlans");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "VisitPlans");

            migrationBuilder.RenameColumn(
                name: "MCPDetailId",
                table: "VisitPlans",
                newName: "RouteId");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "VisitPlans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OutletId",
                table: "VisitPlans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
