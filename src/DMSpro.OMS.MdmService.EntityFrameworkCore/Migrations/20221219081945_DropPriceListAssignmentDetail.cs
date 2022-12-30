using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class DropPriceListAssignmentDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PricelistAssignmentDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
