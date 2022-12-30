using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class DropCustomerProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAssignments_CustomerProfiles_CustomerId",
                table: "CustomerAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAttachments_CustomerProfiles_CustomerId",
                table: "CustomerAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContacts_CustomerProfiles_CustomerId",
                table: "CustomerContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGroupByLists_CustomerProfiles_CustomerId",
                table: "CustomerGroupByLists");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerInZones_CustomerProfiles_CustomerId",
                table: "CustomerInZones");

            migrationBuilder.DropForeignKey(
                name: "FK_MCPDetails_CustomerProfiles_CustomerId",
                table: "MCPDetails");

            migrationBuilder.DropTable(
                name: "CustomerProfiles");

            migrationBuilder.DropIndex(
                name: "IX_MCPDetails_CustomerId",
                table: "MCPDetails");

            migrationBuilder.DropIndex(
                name: "IX_CustomerInZones_CustomerId",
                table: "CustomerInZones");

            migrationBuilder.DropIndex(
                name: "IX_CustomerGroupByLists_CustomerId",
                table: "CustomerGroupByLists");

            migrationBuilder.DropIndex(
                name: "IX_CustomerContacts_CustomerId",
                table: "CustomerContacts");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAttachments_CustomerId",
                table: "CustomerAttachments");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAssignments_CustomerId",
                table: "CustomerAssignments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreditLimit = table.Column<int>(type: "int", nullable: true),
                    CusAttributeValue0Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue10Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue11Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue12Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue13Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue14Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue15Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue16Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue17Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue18Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue19Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue3Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue4Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue5Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue6Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue7Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue8Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CusAttributeValue9Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ERPCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeoMaste3rId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GeoMaster0Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GeoMaster1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GeoMaster2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GeoMaster4Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsCompany = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    License = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkedCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentTermId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Phone1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SFACustomerCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VATAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VATName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_Companies_LinkedCompanyId",
                        column: x => x.LinkedCompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue0Id",
                        column: x => x.CusAttributeValue0Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue10Id",
                        column: x => x.CusAttributeValue10Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue11Id",
                        column: x => x.CusAttributeValue11Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue12Id",
                        column: x => x.CusAttributeValue12Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue13Id",
                        column: x => x.CusAttributeValue13Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue14Id",
                        column: x => x.CusAttributeValue14Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue15Id",
                        column: x => x.CusAttributeValue15Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue16Id",
                        column: x => x.CusAttributeValue16Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue17Id",
                        column: x => x.CusAttributeValue17Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue18Id",
                        column: x => x.CusAttributeValue18Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue19Id",
                        column: x => x.CusAttributeValue19Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue1Id",
                        column: x => x.CusAttributeValue1Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue2Id",
                        column: x => x.CusAttributeValue2Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue3Id",
                        column: x => x.CusAttributeValue3Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue4Id",
                        column: x => x.CusAttributeValue4Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue5Id",
                        column: x => x.CusAttributeValue5Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue6Id",
                        column: x => x.CusAttributeValue6Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue7Id",
                        column: x => x.CusAttributeValue7Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue8Id",
                        column: x => x.CusAttributeValue8Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CusAttributeValues_CusAttributeValue9Id",
                        column: x => x.CusAttributeValue9Id,
                        principalTable: "CusAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_CustomerProfiles_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "CustomerProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_GeoMasters_GeoMaste3rId",
                        column: x => x.GeoMaste3rId,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_GeoMasters_GeoMaster0Id",
                        column: x => x.GeoMaster0Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_GeoMasters_GeoMaster1Id",
                        column: x => x.GeoMaster1Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_GeoMasters_GeoMaster2Id",
                        column: x => x.GeoMaster2Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_GeoMasters_GeoMaster4Id",
                        column: x => x.GeoMaster4Id,
                        principalTable: "GeoMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_PriceLists_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "PriceLists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerProfiles_SystemDatas_PaymentTermId",
                        column: x => x.PaymentTermId,
                        principalTable: "SystemDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MCPDetails_CustomerId",
                table: "MCPDetails",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInZones_CustomerId",
                table: "CustomerInZones",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGroupByLists_CustomerId",
                table: "CustomerGroupByLists",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_CustomerId",
                table: "CustomerContacts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAttachments_CustomerId",
                table: "CustomerAttachments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAssignments_CustomerId",
                table: "CustomerAssignments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue0Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue0Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue10Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue10Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue11Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue11Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue12Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue12Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue13Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue13Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue14Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue14Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue15Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue15Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue16Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue16Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue17Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue17Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue18Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue18Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue19Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue19Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue1Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue1Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue2Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue2Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue3Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue3Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue4Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue4Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue5Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue5Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue6Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue6Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue7Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue7Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue8Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue8Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_CusAttributeValue9Id",
                table: "CustomerProfiles",
                column: "CusAttributeValue9Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_GeoMaste3rId",
                table: "CustomerProfiles",
                column: "GeoMaste3rId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_GeoMaster0Id",
                table: "CustomerProfiles",
                column: "GeoMaster0Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_GeoMaster1Id",
                table: "CustomerProfiles",
                column: "GeoMaster1Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_GeoMaster2Id",
                table: "CustomerProfiles",
                column: "GeoMaster2Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_GeoMaster4Id",
                table: "CustomerProfiles",
                column: "GeoMaster4Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_LinkedCompanyId",
                table: "CustomerProfiles",
                column: "LinkedCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_PaymentId",
                table: "CustomerProfiles",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_PaymentTermId",
                table: "CustomerProfiles",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfiles_PriceListId",
                table: "CustomerProfiles",
                column: "PriceListId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAssignments_CustomerProfiles_CustomerId",
                table: "CustomerAssignments",
                column: "CustomerId",
                principalTable: "CustomerProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAttachments_CustomerProfiles_CustomerId",
                table: "CustomerAttachments",
                column: "CustomerId",
                principalTable: "CustomerProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContacts_CustomerProfiles_CustomerId",
                table: "CustomerContacts",
                column: "CustomerId",
                principalTable: "CustomerProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGroupByLists_CustomerProfiles_CustomerId",
                table: "CustomerGroupByLists",
                column: "CustomerId",
                principalTable: "CustomerProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerInZones_CustomerProfiles_CustomerId",
                table: "CustomerInZones",
                column: "CustomerId",
                principalTable: "CustomerProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MCPDetails_CustomerProfiles_CustomerId",
                table: "MCPDetails",
                column: "CustomerId",
                principalTable: "CustomerProfiles",
                principalColumn: "Id");
        }
    }
}
