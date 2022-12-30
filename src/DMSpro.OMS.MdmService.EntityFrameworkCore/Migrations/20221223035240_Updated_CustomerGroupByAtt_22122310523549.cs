using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Updated_CustomerGroupByAtt_22122310523549 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttributeCode",
                table: "CustomerGroupByAtts");

            migrationBuilder.AddColumn<Guid>(
                name: "CusAttributeValueId",
                table: "CustomerGroupByAtts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByAtts_CusAttributeValueId",
                table: "CustomerGroupByAtts",
                column: "CusAttributeValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGroupByAtts_CusAttributeValues_CusAttributeValueId",
                table: "CustomerGroupByAtts",
                column: "CusAttributeValueId",
                principalTable: "CusAttributeValues",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGroupByAtts_CusAttributeValues_CusAttributeValueId",
                table: "CustomerGroupByAtts");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGroupByAtts_CusAttributeValueId",
                table: "CustomerGroupByAtts");

            migrationBuilder.DropColumn(
                name: "CusAttributeValueId",
                table: "CustomerGroupByAtts");

            migrationBuilder.AddColumn<string>(
                name: "AttributeCode",
                table: "CustomerGroupByAtts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
