using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_CustomerGroupByGeo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeoType",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "CustomerGroupByGeos");

            migrationBuilder.RenameColumn(
                name: "EffDate",
                table: "CustomerGroupByGeos",
                newName: "EffectiveDate");

            migrationBuilder.AddColumn<Guid>(
                name: "GeoMasterId",
                table: "CustomerGroupByGeos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByGeos_CustomerGroupId",
                table: "CustomerGroupByGeos",
                column: "CustomerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByGeos_GeoMasterId",
                table: "CustomerGroupByGeos",
                column: "GeoMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGroupByGeos_CustomerGroups_CustomerGroupId",
                table: "CustomerGroupByGeos",
                column: "CustomerGroupId",
                principalTable: "CustomerGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGroupByGeos_GeoMasters_GeoMasterId",
                table: "CustomerGroupByGeos",
                column: "GeoMasterId",
                principalTable: "GeoMasters",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGroupByGeos_CustomerGroups_CustomerGroupId",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGroupByGeos_GeoMasters_GeoMasterId",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGroupByGeos_CustomerGroupId",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGroupByGeos_GeoMasterId",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropColumn(
                name: "GeoMasterId",
                table: "CustomerGroupByGeos");

            migrationBuilder.RenameColumn(
                name: "EffectiveDate",
                table: "CustomerGroupByGeos",
                newName: "EffDate");

            migrationBuilder.AddColumn<short>(
                name: "GeoType",
                table: "CustomerGroupByGeos",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "CustomerGroupByGeos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
