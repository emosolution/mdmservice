using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Updated_MCPDetail_22122315301461 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MCPHeaderId",
                table: "MCPDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MCPDetails_MCPHeaderId",
                table: "MCPDetails",
                column: "MCPHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_MCPDetails_MCPHeaders_MCPHeaderId",
                table: "MCPDetails",
                column: "MCPHeaderId",
                principalTable: "MCPHeaders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MCPDetails_MCPHeaders_MCPHeaderId",
                table: "MCPDetails");

            migrationBuilder.DropIndex(
                name: "IX_MCPDetails_MCPHeaderId",
                table: "MCPDetails");

            migrationBuilder.DropColumn(
                name: "MCPHeaderId",
                table: "MCPDetails");
        }
    }
}
