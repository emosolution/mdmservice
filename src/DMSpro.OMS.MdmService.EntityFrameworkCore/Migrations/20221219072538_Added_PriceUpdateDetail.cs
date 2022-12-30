using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_PriceUpdateDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceUpdateDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PriceBeforeUpdate = table.Column<int>(type: "int", nullable: false),
                    NewPrice = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PriceUpdateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceListDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceUpdateDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceUpdateDetails_PriceListDetails_PriceListDetailId",
                        column: x => x.PriceListDetailId,
                        principalTable: "PriceListDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PriceUpdateDetails_PriceUpdates_PriceUpdateId",
                        column: x => x.PriceUpdateId,
                        principalTable: "PriceUpdates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceUpdateDetails_PriceListDetailId",
                table: "PriceUpdateDetails",
                column: "PriceListDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceUpdateDetails_PriceUpdateId",
                table: "PriceUpdateDetails",
                column: "PriceUpdateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceUpdateDetails");
        }
    }
}
