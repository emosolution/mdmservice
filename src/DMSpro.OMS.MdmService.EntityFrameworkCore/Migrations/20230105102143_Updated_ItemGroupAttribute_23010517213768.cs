using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Updated_ItemGroupAttribute_23010517213768 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Attr5Id",
                table: "ItemGroupAttributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttributes_Attr5Id",
                table: "ItemGroupAttributes",
                column: "Attr5Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr5Id",
                table: "ItemGroupAttributes",
                column: "Attr5Id",
                principalTable: "ItemAttributeValues",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemGroupAttributes_ItemAttributeValues_Attr5Id",
                table: "ItemGroupAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ItemGroupAttributes_Attr5Id",
                table: "ItemGroupAttributes");

            migrationBuilder.DropColumn(
                name: "Attr5Id",
                table: "ItemGroupAttributes");
        }
    }
}
