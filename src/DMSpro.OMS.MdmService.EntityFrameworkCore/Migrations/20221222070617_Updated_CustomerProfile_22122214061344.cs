using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Updated_CustomerProfile_22122214061344 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "CustomerProfiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_PaymentId",
                table: "CustomerProfiles",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProfiles_CustomerProfiles_PaymentId",
                table: "CustomerProfiles",
                column: "PaymentId",
                principalTable: "CustomerProfiles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProfiles_CustomerProfiles_PaymentId",
                table: "CustomerProfiles");

            migrationBuilder.DropIndex(
                name: "IX_CustomerProfiles_PaymentId",
                table: "CustomerProfiles");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "CustomerProfiles");
        }
    }
}
