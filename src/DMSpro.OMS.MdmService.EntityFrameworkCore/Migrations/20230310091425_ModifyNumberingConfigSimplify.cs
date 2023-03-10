using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class ModifyNumberingConfigSimplify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "NumberingConfigs");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "NumberingConfigs");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "NumberingConfigs");

            migrationBuilder.RenameColumn(
                name: "StartNumber",
                table: "NumberingConfigs",
                newName: "PaddingZeroNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaddingZeroNumber",
                table: "NumberingConfigs",
                newName: "StartNumber");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "NumberingConfigs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "NumberingConfigs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "NumberingConfigs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
