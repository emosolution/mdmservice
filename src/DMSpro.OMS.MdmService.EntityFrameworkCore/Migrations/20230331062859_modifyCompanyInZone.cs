using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class modifyCompanyInZone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBase",
                table: "CompanyInZones");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemGroupId",
                table: "CompanyInZones",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInZones_ItemGroupId",
                table: "CompanyInZones",
                column: "ItemGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyInZones_ItemGroups_ItemGroupId",
                table: "CompanyInZones",
                column: "ItemGroupId",
                principalTable: "ItemGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyInZones_ItemGroups_ItemGroupId",
                table: "CompanyInZones");

            migrationBuilder.DropIndex(
                name: "IX_CompanyInZones_ItemGroupId",
                table: "CompanyInZones");

            migrationBuilder.DropColumn(
                name: "ItemGroupId",
                table: "CompanyInZones");

            migrationBuilder.AddColumn<bool>(
                name: "IsBase",
                table: "CompanyInZones",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
