using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Updated_ItemAttribute_23010515483556 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsItemCategory",
                table: "ItemAttributes",
                newName: "IsSellingCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSellingCategory",
                table: "ItemAttributes",
                newName: "IsItemCategory");
        }
    }
}
