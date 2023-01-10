using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Updated_ItemAttributeValue_23010516190027 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "ItemAttributeValues",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttributeValues_ParentId",
                table: "ItemAttributeValues",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAttributeValues_ItemAttributeValues_ParentId",
                table: "ItemAttributeValues",
                column: "ParentId",
                principalTable: "ItemAttributeValues",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemAttributeValues_ItemAttributeValues_ParentId",
                table: "ItemAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_ItemAttributeValues_ParentId",
                table: "ItemAttributeValues");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ItemAttributeValues");
        }
    }
}
