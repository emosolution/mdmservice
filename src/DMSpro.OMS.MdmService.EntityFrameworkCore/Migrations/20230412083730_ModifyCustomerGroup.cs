using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCustomerGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EffectiveDate",
                table: "CustomerGroups");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "CustomerGroups",
                newName: "Selectable");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CustomerGroups",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CustomerGroups",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CustomerGroups");

            migrationBuilder.RenameColumn(
                name: "Selectable",
                table: "CustomerGroups",
                newName: "Active");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CustomerGroups",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveDate",
                table: "CustomerGroups",
                type: "datetime2",
                nullable: true);
        }
    }
}
