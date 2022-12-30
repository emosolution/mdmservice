using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_PricelistAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerGroupId",
                table: "PricelistAssignments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PricelistAssignments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PricelistAssignments_CustomerGroupId",
                table: "PricelistAssignments",
                column: "CustomerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PricelistAssignments_PriceListId",
                table: "PricelistAssignments",
                column: "PriceListId");

            migrationBuilder.AddForeignKey(
                name: "FK_PricelistAssignments_CustomerGroups_CustomerGroupId",
                table: "PricelistAssignments",
                column: "CustomerGroupId",
                principalTable: "CustomerGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PricelistAssignments_PriceLists_PriceListId",
                table: "PricelistAssignments",
                column: "PriceListId",
                principalTable: "PriceLists",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PricelistAssignments_CustomerGroups_CustomerGroupId",
                table: "PricelistAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_PricelistAssignments_PriceLists_PriceListId",
                table: "PricelistAssignments");

            migrationBuilder.DropIndex(
                name: "IX_PricelistAssignments_CustomerGroupId",
                table: "PricelistAssignments");

            migrationBuilder.DropIndex(
                name: "IX_PricelistAssignments_PriceListId",
                table: "PricelistAssignments");

            migrationBuilder.DropColumn(
                name: "CustomerGroupId",
                table: "PricelistAssignments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PricelistAssignments");
        }
    }
}
