using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_CustomerGroupByAtt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByAtts_CustomerGroupId",
                table: "CustomerGroupByAtts",
                column: "CustomerGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGroupByAtts_CustomerGroups_CustomerGroupId",
                table: "CustomerGroupByAtts",
                column: "CustomerGroupId",
                principalTable: "CustomerGroups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGroupByAtts_CustomerGroups_CustomerGroupId",
                table: "CustomerGroupByAtts");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGroupByAtts_CustomerGroupId",
                table: "CustomerGroupByAtts");
        }
    }
}
