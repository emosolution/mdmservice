using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_PriceListDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceListDetails_ItemMasters_ItemMasterId",
                table: "PriceListDetails");

            migrationBuilder.DropIndex(
                name: "IX_PriceListDetails_ItemMasterId",
                table: "PriceListDetails");

            migrationBuilder.DropColumn(
                name: "ItemMasterId",
                table: "PriceListDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ItemMasterId",
                table: "PriceListDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PriceListDetails_ItemMasterId",
                table: "PriceListDetails",
                column: "ItemMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListDetails_ItemMasters_ItemMasterId",
                table: "PriceListDetails",
                column: "ItemMasterId",
                principalTable: "ItemMasters",
                principalColumn: "Id");
        }
    }
}
