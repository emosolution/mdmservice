using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class ModifyEmployeeProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProfiles_SystemDatas_EmployeeTypeId",
                table: "EmployeeProfiles");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeProfiles_EmployeeTypeId",
                table: "EmployeeProfiles");

            migrationBuilder.DropColumn(
                name: "EmployeeTypeId",
                table: "EmployeeProfiles");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeType",
                table: "EmployeeProfiles",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeType",
                table: "EmployeeProfiles");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeTypeId",
                table: "EmployeeProfiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfiles_EmployeeTypeId",
                table: "EmployeeProfiles",
                column: "EmployeeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProfiles_SystemDatas_EmployeeTypeId",
                table: "EmployeeProfiles",
                column: "EmployeeTypeId",
                principalTable: "SystemDatas",
                principalColumn: "Id");
        }
    }
}
