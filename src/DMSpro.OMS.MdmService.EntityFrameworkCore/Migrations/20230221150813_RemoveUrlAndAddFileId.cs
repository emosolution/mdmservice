using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUrlAndAddFileId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "ItemImages");

            migrationBuilder.DropColumn(
                name: "url",
                table: "EmployeeImages");

            migrationBuilder.DropColumn(
                name: "url",
                table: "EmployeeAttachments");

            migrationBuilder.DropColumn(
                name: "url",
                table: "CustomerAttachments");

            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "ItemImages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "EmployeeImages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "EmployeeAttachments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                table: "CustomerAttachments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileId",
                table: "ItemImages");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "EmployeeImages");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "EmployeeAttachments");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "CustomerAttachments");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "ItemImages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "url",
                table: "EmployeeImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "url",
                table: "EmployeeAttachments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "url",
                table: "CustomerAttachments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
