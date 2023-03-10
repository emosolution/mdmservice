using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class ModifyNumberingConfigDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentNumber",
                table: "NumberingConfigDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaddingZeroNumber",
                table: "NumberingConfigDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Prefix",
                table: "NumberingConfigDetails",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Suffix",
                table: "NumberingConfigDetails",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentNumber",
                table: "NumberingConfigDetails");

            migrationBuilder.DropColumn(
                name: "PaddingZeroNumber",
                table: "NumberingConfigDetails");

            migrationBuilder.DropColumn(
                name: "Prefix",
                table: "NumberingConfigDetails");

            migrationBuilder.DropColumn(
                name: "Suffix",
                table: "NumberingConfigDetails");
        }
    }
}
