using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class ChangeToDateTimeInEmployeeInZone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "EmployeeInZones");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "EmployeeInZones",
                type: "datetime",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "EmployeeInZones");

            migrationBuilder.AddColumn<Guid>(
                name: "EndDate",
                table: "EmployeeInZones",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
