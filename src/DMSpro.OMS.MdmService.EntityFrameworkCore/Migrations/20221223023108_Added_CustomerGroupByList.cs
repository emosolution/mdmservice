using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_CustomerGroupByList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EffDate",
                table: "CustomerGroupByLists");

            migrationBuilder.RenameColumn(
                name: "BPId",
                table: "CustomerGroupByLists",
                newName: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByLists_CustomerGroupId",
                table: "CustomerGroupByLists",
                column: "CustomerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByLists_CustomerId",
                table: "CustomerGroupByLists",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGroupByLists_CustomerGroups_CustomerGroupId",
                table: "CustomerGroupByLists",
                column: "CustomerGroupId",
                principalTable: "CustomerGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGroupByLists_CustomerProfiles_CustomerId",
                table: "CustomerGroupByLists",
                column: "CustomerId",
                principalTable: "CustomerProfiles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGroupByLists_CustomerGroups_CustomerGroupId",
                table: "CustomerGroupByLists");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGroupByLists_CustomerProfiles_CustomerId",
                table: "CustomerGroupByLists");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGroupByLists_CustomerGroupId",
                table: "CustomerGroupByLists");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGroupByLists_CustomerId",
                table: "CustomerGroupByLists");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "CustomerGroupByLists",
                newName: "BPId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EffDate",
                table: "CustomerGroupByLists",
                type: "datetime2",
                nullable: true);
        }
    }
}
