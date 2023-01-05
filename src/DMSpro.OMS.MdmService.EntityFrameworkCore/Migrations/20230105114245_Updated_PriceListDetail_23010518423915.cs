using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Updated_PriceListDetail_23010518423915 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                table: "PriceListDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PriceListDetails_ItemId",
                table: "PriceListDetails",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListDetails_Items_ItemId",
                table: "PriceListDetails",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceListDetails_Items_ItemId",
                table: "PriceListDetails");

            migrationBuilder.DropIndex(
                name: "IX_PriceListDetails_ItemId",
                table: "PriceListDetails");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "PriceListDetails");
        }
    }
}
