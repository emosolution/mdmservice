using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Phone1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    erpCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    License = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TaxCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    vatName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    vatAddress = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreditLimit = table.Column<int>(type: "int", nullable: true),
                    IsCompany = table.Column<bool>(type: "bit", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SFACustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastOrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentTermId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LinkedCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PriceListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeoMaster0Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GeoMaster1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GeoMaster2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GeoMaster3Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GeoMaster4Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute0Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute3Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute4Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute5Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute6Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute7Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute8Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute9Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute10Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute11Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute12Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute13Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute14Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute15Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute16Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute1I7d = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute18Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attribute19Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Companies_LinkedCompanyId",
                        column: x => x.LinkedCompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute0Id",
                        column: x => x.Attribute0Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute10Id",
                        column: x => x.Attribute10Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute11Id",
                        column: x => x.Attribute11Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute12Id",
                        column: x => x.Attribute12Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute13Id",
                        column: x => x.Attribute13Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute14Id",
                        column: x => x.Attribute14Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute15Id",
                        column: x => x.Attribute15Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute16Id",
                        column: x => x.Attribute16Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute18Id",
                        column: x => x.Attribute18Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute19Id",
                        column: x => x.Attribute19Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute1I7d",
                        column: x => x.Attribute1I7d,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute1Id",
                        column: x => x.Attribute1Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute2Id",
                        column: x => x.Attribute2Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute3Id",
                        column: x => x.Attribute3Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute4Id",
                        column: x => x.Attribute4Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute5Id",
                        column: x => x.Attribute5Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute6Id",
                        column: x => x.Attribute6Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute7Id",
                        column: x => x.Attribute7Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute8Id",
                        column: x => x.Attribute8Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_CusAttributeValues_Attribute9Id",
                        column: x => x.Attribute9Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_GeoMasters_GeoMaster0Id",
                        column: x => x.GeoMaster0Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_GeoMasters_GeoMaster1Id",
                        column: x => x.GeoMaster1Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_GeoMasters_GeoMaster2Id",
                        column: x => x.GeoMaster2Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_GeoMasters_GeoMaster3Id",
                        column: x => x.GeoMaster3Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_GeoMasters_GeoMaster4Id",
                        column: x => x.GeoMaster4Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_PriceLists_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "PriceLists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_SystemDatas_PaymentTermId",
                        column: x => x.PaymentTermId,
                        principalTable: "SystemDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute0Id",
                table: "Customers",
                column: "Attribute0Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute10Id",
                table: "Customers",
                column: "Attribute10Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute11Id",
                table: "Customers",
                column: "Attribute11Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute12Id",
                table: "Customers",
                column: "Attribute12Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute13Id",
                table: "Customers",
                column: "Attribute13Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute14Id",
                table: "Customers",
                column: "Attribute14Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute15Id",
                table: "Customers",
                column: "Attribute15Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute16Id",
                table: "Customers",
                column: "Attribute16Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute18Id",
                table: "Customers",
                column: "Attribute18Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute19Id",
                table: "Customers",
                column: "Attribute19Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute1I7d",
                table: "Customers",
                column: "Attribute1I7d");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute1Id",
                table: "Customers",
                column: "Attribute1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute2Id",
                table: "Customers",
                column: "Attribute2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute3Id",
                table: "Customers",
                column: "Attribute3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute4Id",
                table: "Customers",
                column: "Attribute4Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute5Id",
                table: "Customers",
                column: "Attribute5Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute6Id",
                table: "Customers",
                column: "Attribute6Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute7Id",
                table: "Customers",
                column: "Attribute7Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute8Id",
                table: "Customers",
                column: "Attribute8Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Attribute9Id",
                table: "Customers",
                column: "Attribute9Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_GeoMaster0Id",
                table: "Customers",
                column: "GeoMaster0Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_GeoMaster1Id",
                table: "Customers",
                column: "GeoMaster1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_GeoMaster2Id",
                table: "Customers",
                column: "GeoMaster2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_GeoMaster3Id",
                table: "Customers",
                column: "GeoMaster3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_GeoMaster4Id",
                table: "Customers",
                column: "GeoMaster4Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_LinkedCompanyId",
                table: "Customers",
                column: "LinkedCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PaymentTermId",
                table: "Customers",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PriceListId",
                table: "Customers",
                column: "PriceListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
