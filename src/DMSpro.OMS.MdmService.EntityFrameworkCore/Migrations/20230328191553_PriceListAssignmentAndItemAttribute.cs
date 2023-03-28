using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class PriceListAssignmentAndItemAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSellingCategory",
                table: "ItemAttributes");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "PricelistAssignments",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "PricelistAssignments");

            migrationBuilder.AddColumn<bool>(
                name: "IsSellingCategory",
                table: "ItemAttributes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
