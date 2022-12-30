using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_MCPDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OutletId",
                table: "MCPDetails",
                newName: "CustomerId");

            migrationBuilder.AlterColumn<int>(
                name: "VisitOrder",
                table: "MCPDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "MCPDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "Distance",
                table: "MCPDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_MCPDetails_CustomerId",
                table: "MCPDetails",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MCPDetails_CustomerProfiles_CustomerId",
                table: "MCPDetails",
                column: "CustomerId",
                principalTable: "CustomerProfiles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MCPDetails_CustomerProfiles_CustomerId",
                table: "MCPDetails");

            migrationBuilder.DropIndex(
                name: "IX_MCPDetails_CustomerId",
                table: "MCPDetails");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "MCPDetails",
                newName: "OutletId");

            migrationBuilder.AlterColumn<long>(
                name: "VisitOrder",
                table: "MCPDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "MCPDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Distance",
                table: "MCPDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
