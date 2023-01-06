using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Updated_MCPHeader_23010610435059 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ItemGroupId",
                table: "MCPHeaders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MCPHeaders_ItemGroupId",
                table: "MCPHeaders",
                column: "ItemGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_MCPHeaders_ItemGroups_ItemGroupId",
                table: "MCPHeaders",
                column: "ItemGroupId",
                principalTable: "ItemGroups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MCPHeaders_ItemGroups_ItemGroupId",
                table: "MCPHeaders");

            migrationBuilder.DropIndex(
                name: "IX_MCPHeaders_ItemGroupId",
                table: "MCPHeaders");

            migrationBuilder.DropColumn(
                name: "ItemGroupId",
                table: "MCPHeaders");
        }
    }
}
