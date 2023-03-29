using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPriceList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFirstPriceList",
                table: "PriceLists",
                newName: "IsReleased");

            migrationBuilder.AddColumn<bool>(
                name: "IsBase",
                table: "PriceLists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "PriceLists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleasedDate",
                table: "PriceLists",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBase",
                table: "PriceLists");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "PriceLists");

            migrationBuilder.DropColumn(
                name: "ReleasedDate",
                table: "PriceLists");

            migrationBuilder.RenameColumn(
                name: "IsReleased",
                table: "PriceLists",
                newName: "IsFirstPriceList");
        }
    }
}
