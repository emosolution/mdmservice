using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Updated_CusAttributeValue_22122211331636 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentCusAttributeValueId",
                table: "CusAttributeValues",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CusAttributeValues_ParentCusAttributeValueId",
                table: "CusAttributeValues",
                column: "ParentCusAttributeValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_CusAttributeValues_CusAttributeValues_ParentCusAttributeValueId",
                table: "CusAttributeValues",
                column: "ParentCusAttributeValueId",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CusAttributeValues_CusAttributeValues_ParentCusAttributeValueId",
                table: "CusAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_CusAttributeValues_ParentCusAttributeValueId",
                table: "CusAttributeValues");

            migrationBuilder.DropColumn(
                name: "ParentCusAttributeValueId",
                table: "CusAttributeValues");
        }
    }
}
