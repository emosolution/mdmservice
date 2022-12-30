using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Updated_Vendor_22122216463074 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GeoMaster0Id",
                table: "Vendors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GeoMaster1Id",
                table: "Vendors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GeoMaster2Id",
                table: "Vendors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GeoMaster3Id",
                table: "Vendors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GeoMaster4Id",
                table: "Vendors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_GeoMaster0Id",
                table: "Vendors",
                column: "GeoMaster0Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_GeoMaster1Id",
                table: "Vendors",
                column: "GeoMaster1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_GeoMaster2Id",
                table: "Vendors",
                column: "GeoMaster2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_GeoMaster3Id",
                table: "Vendors",
                column: "GeoMaster3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_GeoMaster4Id",
                table: "Vendors",
                column: "GeoMaster4Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_GeoMasters_GeoMaster0Id",
                table: "Vendors",
                column: "GeoMaster0Id",
                principalTable: "GeoMasters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_GeoMasters_GeoMaster1Id",
                table: "Vendors",
                column: "GeoMaster1Id",
                principalTable: "GeoMasters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_GeoMasters_GeoMaster2Id",
                table: "Vendors",
                column: "GeoMaster2Id",
                principalTable: "GeoMasters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_GeoMasters_GeoMaster3Id",
                table: "Vendors",
                column: "GeoMaster3Id",
                principalTable: "GeoMasters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_GeoMasters_GeoMaster4Id",
                table: "Vendors",
                column: "GeoMaster4Id",
                principalTable: "GeoMasters",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_GeoMasters_GeoMaster0Id",
                table: "Vendors");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_GeoMasters_GeoMaster1Id",
                table: "Vendors");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_GeoMasters_GeoMaster2Id",
                table: "Vendors");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_GeoMasters_GeoMaster3Id",
                table: "Vendors");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_GeoMasters_GeoMaster4Id",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_GeoMaster0Id",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_GeoMaster1Id",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_GeoMaster2Id",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_GeoMaster3Id",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_GeoMaster4Id",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "GeoMaster0Id",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "GeoMaster1Id",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "GeoMaster2Id",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "GeoMaster3Id",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "GeoMaster4Id",
                table: "Vendors");
        }
    }
}
