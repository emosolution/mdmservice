using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_Route : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "SalesOrgId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "SellingZoneId",
                table: "Routes");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_ItemGroupId",
                table: "Routes",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_RouteTypeId",
                table: "Routes",
                column: "RouteTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_ItemGroups_ItemGroupId",
                table: "Routes",
                column: "ItemGroupId",
                principalTable: "ItemGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_SystemDatas_RouteTypeId",
                table: "Routes",
                column: "RouteTypeId",
                principalTable: "SystemDatas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_ItemGroups_ItemGroupId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_SystemDatas_RouteTypeId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_ItemGroupId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_RouteTypeId",
                table: "Routes");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Routes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Routes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Routes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Routes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SalesOrgId",
                table: "Routes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SellingZoneId",
                table: "Routes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
