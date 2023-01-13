﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_HolidayDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "HolidayDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HolidayDetails_HolidayId",
                table: "HolidayDetails",
                column: "HolidayId");

            migrationBuilder.AddForeignKey(
                name: "FK_HolidayDetails_Holidays_HolidayId",
                table: "HolidayDetails",
                column: "HolidayId",
                principalTable: "Holidays",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HolidayDetails_Holidays_HolidayId",
                table: "HolidayDetails");

            migrationBuilder.DropIndex(
                name: "IX_HolidayDetails_HolidayId",
                table: "HolidayDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "HolidayDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}