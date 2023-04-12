using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOldCustomerGroupEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerGroupByAtts");

            migrationBuilder.DropTable(
                name: "CustomerGroupByGeos");

            migrationBuilder.DropTable(
                name: "CustomerGroupByLists");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerGroupByAtts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CusAttributeValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValueCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ValueName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerGroupByAtts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerGroupByAtts_CusAttributeValues_CusAttributeValueId",
                        column: x => x.CusAttributeValueId,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupByAtts_CustomerGroups_CustomerGroupId",
                        column: x => x.CustomerGroupId,
                        principalTable: "CustomerGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerGroupByGeos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeoMaster0Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GeoMaster1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GeoMaster2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GeoMaster3Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GeoMaster4Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerGroupByGeos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerGroupByGeos_CustomerGroups_CustomerGroupId",
                        column: x => x.CustomerGroupId,
                        principalTable: "CustomerGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupByGeos_GeoMasters_GeoMaster0Id",
                        column: x => x.GeoMaster0Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupByGeos_GeoMasters_GeoMaster1Id",
                        column: x => x.GeoMaster1Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupByGeos_GeoMasters_GeoMaster2Id",
                        column: x => x.GeoMaster2Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupByGeos_GeoMasters_GeoMaster3Id",
                        column: x => x.GeoMaster3Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupByGeos_GeoMasters_GeoMaster4Id",
                        column: x => x.GeoMaster4Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerGroupByLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerGroupByLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerGroupByLists_CustomerGroups_CustomerGroupId",
                        column: x => x.CustomerGroupId,
                        principalTable: "CustomerGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerGroupByLists_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByAtts_CusAttributeValueId",
                table: "CustomerGroupByAtts",
                column: "CusAttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByAtts_CustomerGroupId",
                table: "CustomerGroupByAtts",
                column: "CustomerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByGeos_CustomerGroupId",
                table: "CustomerGroupByGeos",
                column: "CustomerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByGeos_GeoMaster0Id",
                table: "CustomerGroupByGeos",
                column: "GeoMaster0Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByGeos_GeoMaster1Id",
                table: "CustomerGroupByGeos",
                column: "GeoMaster1Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByGeos_GeoMaster2Id",
                table: "CustomerGroupByGeos",
                column: "GeoMaster2Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByGeos_GeoMaster3Id",
                table: "CustomerGroupByGeos",
                column: "GeoMaster3Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByGeos_GeoMaster4Id",
                table: "CustomerGroupByGeos",
                column: "GeoMaster4Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByLists_CustomerGroupId",
                table: "CustomerGroupByLists",
                column: "CustomerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByLists_CustomerId",
                table: "CustomerGroupByLists",
                column: "CustomerId");
        }
    }
}
