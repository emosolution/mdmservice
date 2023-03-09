using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class ModifyNumberingConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumberingConfigs_Companies_CompanyId",
                table: "NumberingConfigs");

            migrationBuilder.DropIndex(
                name: "IX_NumberingConfigs_CompanyId",
                table: "NumberingConfigs");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "NumberingConfigs");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "NumberingConfigs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "NumberingConfigs");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "NumberingConfigs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NumberingConfigs_CompanyId",
                table: "NumberingConfigs",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_NumberingConfigs_Companies_CompanyId",
                table: "NumberingConfigs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
