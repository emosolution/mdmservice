using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPriceUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateStatusDate",
                table: "PriceUpdates",
                newName: "ReleasedDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EffectiveDate",
                table: "PriceUpdates",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledDate",
                table: "PriceUpdates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompleteDate",
                table: "PriceUpdates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "PriceUpdates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsScheduled",
                table: "PriceUpdates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelledDate",
                table: "PriceUpdates");

            migrationBuilder.DropColumn(
                name: "CompleteDate",
                table: "PriceUpdates");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "PriceUpdates");

            migrationBuilder.DropColumn(
                name: "IsScheduled",
                table: "PriceUpdates");

            migrationBuilder.RenameColumn(
                name: "ReleasedDate",
                table: "PriceUpdates",
                newName: "UpdateStatusDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EffectiveDate",
                table: "PriceUpdates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
