using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_NumberingConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "NumberingConfigs");

            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "NumberingConfigs");

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "NumberingConfigs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "StartNumber",
                table: "NumberingConfigs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SystemDataId",
                table: "NumberingConfigs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NumberingConfigs_CompanyId",
                table: "NumberingConfigs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_NumberingConfigs_SystemDataId",
                table: "NumberingConfigs",
                column: "SystemDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_NumberingConfigs_Companies_CompanyId",
                table: "NumberingConfigs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NumberingConfigs_SystemDatas_SystemDataId",
                table: "NumberingConfigs",
                column: "SystemDataId",
                principalTable: "SystemDatas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NumberingConfigs_Companies_CompanyId",
                table: "NumberingConfigs");

            migrationBuilder.DropForeignKey(
                name: "FK_NumberingConfigs_SystemDatas_SystemDataId",
                table: "NumberingConfigs");

            migrationBuilder.DropIndex(
                name: "IX_NumberingConfigs_CompanyId",
                table: "NumberingConfigs");

            migrationBuilder.DropIndex(
                name: "IX_NumberingConfigs_SystemDataId",
                table: "NumberingConfigs");

            migrationBuilder.DropColumn(
                name: "StartNumber",
                table: "NumberingConfigs");

            migrationBuilder.DropColumn(
                name: "SystemDataId",
                table: "NumberingConfigs");

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "NumberingConfigs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Number",
                table: "NumberingConfigs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<Guid>(
                name: "ObjectId",
                table: "NumberingConfigs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
