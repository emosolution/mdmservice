using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class AddAllGeoMasterLevelToCustomerGroupByGeo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGroupByGeos_GeoMasters_GeoMasterId",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGroupByGeos_GeoMasterId",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropColumn(
                name: "GeoMasterId",
                table: "CustomerGroupByGeos");

            migrationBuilder.AddColumn<Guid>(
                name: "GeoMaster0Id",
                table: "CustomerGroupByGeos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GeoMaster1Id",
                table: "CustomerGroupByGeos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GeoMaster2Id",
                table: "CustomerGroupByGeos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GeoMaster3Id",
                table: "CustomerGroupByGeos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GeoMaster4Id",
                table: "CustomerGroupByGeos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByGeos_GeoMaster0Id",
                table: "CustomerGroupByGeos",
                column: "GeoMaster0Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByGeos_GeoMaster1Id",
                table: "CustomerGroupByGeos",
                column: "GeoMaster1Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByGeos_GeoMaster2Id",
                table: "CustomerGroupByGeos",
                column: "GeoMaster2Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByGeos_GeoMaster3Id",
                table: "CustomerGroupByGeos",
                column: "GeoMaster3Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByGeos_GeoMaster4Id",
                table: "CustomerGroupByGeos",
                column: "GeoMaster4Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGroupByGeos_GeoMasters_GeoMaster0Id",
                table: "CustomerGroupByGeos",
                column: "GeoMaster0Id",
                principalTable: "GeoMasters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGroupByGeos_GeoMasters_GeoMaster1Id",
                table: "CustomerGroupByGeos",
                column: "GeoMaster1Id",
                principalTable: "GeoMasters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGroupByGeos_GeoMasters_GeoMaster2Id",
                table: "CustomerGroupByGeos",
                column: "GeoMaster2Id",
                principalTable: "GeoMasters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGroupByGeos_GeoMasters_GeoMaster3Id",
                table: "CustomerGroupByGeos",
                column: "GeoMaster3Id",
                principalTable: "GeoMasters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGroupByGeos_GeoMasters_GeoMaster4Id",
                table: "CustomerGroupByGeos",
                column: "GeoMaster4Id",
                principalTable: "GeoMasters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGroupByGeos_GeoMasters_GeoMaster0Id",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGroupByGeos_GeoMasters_GeoMaster1Id",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGroupByGeos_GeoMasters_GeoMaster2Id",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGroupByGeos_GeoMasters_GeoMaster3Id",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGroupByGeos_GeoMasters_GeoMaster4Id",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGroupByGeos_GeoMaster0Id",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGroupByGeos_GeoMaster1Id",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGroupByGeos_GeoMaster2Id",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGroupByGeos_GeoMaster3Id",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGroupByGeos_GeoMaster4Id",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropColumn(
                name: "GeoMaster0Id",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropColumn(
                name: "GeoMaster1Id",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropColumn(
                name: "GeoMaster2Id",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropColumn(
                name: "GeoMaster3Id",
                table: "CustomerGroupByGeos");

            migrationBuilder.DropColumn(
                name: "GeoMaster4Id",
                table: "CustomerGroupByGeos");

            migrationBuilder.AddColumn<Guid>(
                name: "GeoMasterId",
                table: "CustomerGroupByGeos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByGeos_GeoMasterId",
                table: "CustomerGroupByGeos",
                column: "GeoMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGroupByGeos_GeoMasters_GeoMasterId",
                table: "CustomerGroupByGeos",
                column: "GeoMasterId",
                principalTable: "GeoMasters",
                principalColumn: "Id");
        }
    }
}
