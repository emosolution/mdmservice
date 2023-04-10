using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCustomerAttributeValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAttributeValues_CustomerAttributeValues_ParentId",
                table: "CustomerAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAttributeValues_ParentId",
                table: "CustomerAttributeValues");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "CustomerAttributeValues");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "CustomerAttributeValues",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAttributeValues_ParentId",
                table: "CustomerAttributeValues",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAttributeValues_CustomerAttributeValues_ParentId",
                table: "CustomerAttributeValues",
                column: "ParentId",
                principalTable: "CustomerAttributeValues",
                principalColumn: "Id");
        }
    }
}
