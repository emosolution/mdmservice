using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Updated_VisitPlan_23010610555152 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "VisitPlans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "VisitPlans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ItemGroupId",
                table: "VisitPlans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RouteId",
                table: "VisitPlans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_VisitPlans_CompanyId",
                table: "VisitPlans",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitPlans_CustomerId",
                table: "VisitPlans",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitPlans_ItemGroupId",
                table: "VisitPlans",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitPlans_RouteId",
                table: "VisitPlans",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitPlans_Companies_CompanyId",
                table: "VisitPlans",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitPlans_Customers_CustomerId",
                table: "VisitPlans",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitPlans_ItemGroups_ItemGroupId",
                table: "VisitPlans",
                column: "ItemGroupId",
                principalTable: "ItemGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitPlans_SalesOrgHierarchies_RouteId",
                table: "VisitPlans",
                column: "RouteId",
                principalTable: "SalesOrgHierarchies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitPlans_Companies_CompanyId",
                table: "VisitPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitPlans_Customers_CustomerId",
                table: "VisitPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitPlans_ItemGroups_ItemGroupId",
                table: "VisitPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitPlans_SalesOrgHierarchies_RouteId",
                table: "VisitPlans");

            migrationBuilder.DropIndex(
                name: "IX_VisitPlans_CompanyId",
                table: "VisitPlans");

            migrationBuilder.DropIndex(
                name: "IX_VisitPlans_CustomerId",
                table: "VisitPlans");

            migrationBuilder.DropIndex(
                name: "IX_VisitPlans_ItemGroupId",
                table: "VisitPlans");

            migrationBuilder.DropIndex(
                name: "IX_VisitPlans_RouteId",
                table: "VisitPlans");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "VisitPlans");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "VisitPlans");

            migrationBuilder.DropColumn(
                name: "ItemGroupId",
                table: "VisitPlans");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "VisitPlans");
        }
    }
}
