using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCustomerImage23030400532771 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "POSMItemId",
                table: "CustomerImages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerImages_POSMItemId",
                table: "CustomerImages",
                column: "POSMItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerImages_Items_POSMItemId",
                table: "CustomerImages",
                column: "POSMItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerImages_Items_POSMItemId",
                table: "CustomerImages");

            migrationBuilder.DropIndex(
                name: "IX_CustomerImages_POSMItemId",
                table: "CustomerImages");

            migrationBuilder.DropColumn(
                name: "POSMItemId",
                table: "CustomerImages");
        }
    }
}
