using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_CustomerGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerType",
                table: "CustomerGroups");

            migrationBuilder.DropColumn(
                name: "GroupByMode",
                table: "CustomerGroups");

            migrationBuilder.RenameColumn(
                name: "EffDate",
                table: "CustomerGroups",
                newName: "EffectiveDate");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CustomerGroups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<int>(
                name: "GroupBy",
                table: "CustomerGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupBy",
                table: "CustomerGroups");

            migrationBuilder.RenameColumn(
                name: "EffectiveDate",
                table: "CustomerGroups",
                newName: "EffDate");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CustomerGroups",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<short>(
                name: "CustomerType",
                table: "CustomerGroups",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "GroupByMode",
                table: "CustomerGroups",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
