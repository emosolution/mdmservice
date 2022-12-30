using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Updated_Customer_22122817010154 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PaymentId",
                table: "Customers",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Customers_PaymentId",
                table: "Customers",
                column: "PaymentId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Customers_PaymentId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_PaymentId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Customers");
        }
    }
}
