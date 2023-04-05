using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class AddedCustomerGroupGeo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerGroupGeos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CustomerGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeoMaster0Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeoMaster1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeoMaster2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeoMaster3Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeoMaster4Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_CustomerGroupGeos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerGroupGeos_CustomerGroups_CustomerGroupId",
                        column: x => x.CustomerGroupId,
                        principalTable: "CustomerGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupGeos_GeoMasters_GeoMaster0Id",
                        column: x => x.GeoMaster0Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupGeos_GeoMasters_GeoMaster1Id",
                        column: x => x.GeoMaster1Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupGeos_GeoMasters_GeoMaster2Id",
                        column: x => x.GeoMaster2Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupGeos_GeoMasters_GeoMaster3Id",
                        column: x => x.GeoMaster3Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupGeos_GeoMasters_GeoMaster4Id",
                        column: x => x.GeoMaster4Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupGeos_CustomerGroupId",
                table: "CustomerGroupGeos",
                column: "CustomerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupGeos_GeoMaster0Id",
                table: "CustomerGroupGeos",
                column: "GeoMaster0Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupGeos_GeoMaster1Id",
                table: "CustomerGroupGeos",
                column: "GeoMaster1Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupGeos_GeoMaster2Id",
                table: "CustomerGroupGeos",
                column: "GeoMaster2Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupGeos_GeoMaster3Id",
                table: "CustomerGroupGeos",
                column: "GeoMaster3Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupGeos_GeoMaster4Id",
                table: "CustomerGroupGeos",
                column: "GeoMaster4Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerGroupGeos");
        }
    }
}
