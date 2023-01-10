using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class RemoveItemMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemMasters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemMasters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Attr0Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr10Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr11Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr12Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr13Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr14Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr15Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr16Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr17Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr18Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr19Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr3Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr4Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr5Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr6Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr7Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr8Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr9Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BasePrice = table.Column<int>(type: "int", nullable: false),
                    CanUpdate = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ERPCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ExpiredType = table.Column<int>(type: "int", nullable: false),
                    ExpiredValue = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inventoriable = table.Column<bool>(type: "bit", nullable: false),
                    InventoryUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IssueMethod = table.Column<int>(type: "int", nullable: false),
                    ItemTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ManageType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PurUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Purchasble = table.Column<bool>(type: "bit", nullable: false),
                    Saleable = table.Column<bool>(type: "bit", nullable: false),
                    SalesUnit = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UOMGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VATId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr0Id",
                        column: x => x.Attr0Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr10Id",
                        column: x => x.Attr10Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr11Id",
                        column: x => x.Attr11Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr12Id",
                        column: x => x.Attr12Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr13Id",
                        column: x => x.Attr13Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr14Id",
                        column: x => x.Attr14Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr15Id",
                        column: x => x.Attr15Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr16Id",
                        column: x => x.Attr16Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr17Id",
                        column: x => x.Attr17Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr18Id",
                        column: x => x.Attr18Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr19Id",
                        column: x => x.Attr19Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr1Id",
                        column: x => x.Attr1Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr2Id",
                        column: x => x.Attr2Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr3Id",
                        column: x => x.Attr3Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr4Id",
                        column: x => x.Attr4Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr5Id",
                        column: x => x.Attr5Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr6Id",
                        column: x => x.Attr6Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr7Id",
                        column: x => x.Attr7Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr8Id",
                        column: x => x.Attr8Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_ProdAttributeValues_Attr9Id",
                        column: x => x.Attr9Id,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_SystemDatas_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "SystemDatas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_UOMGroups_UOMGroupId",
                        column: x => x.UOMGroupId,
                        principalTable: "UOMGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_UOMs_InventoryUnitId",
                        column: x => x.InventoryUnitId,
                        principalTable: "UOMs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_UOMs_PurUnitId",
                        column: x => x.PurUnitId,
                        principalTable: "UOMs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_UOMs_SalesUnit",
                        column: x => x.SalesUnit,
                        principalTable: "UOMs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemMasters_VATs_VATId",
                        column: x => x.VATId,
                        principalTable: "VATs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr0Id",
                table: "ItemMasters",
                column: "Attr0Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr10Id",
                table: "ItemMasters",
                column: "Attr10Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr11Id",
                table: "ItemMasters",
                column: "Attr11Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr12Id",
                table: "ItemMasters",
                column: "Attr12Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr13Id",
                table: "ItemMasters",
                column: "Attr13Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr14Id",
                table: "ItemMasters",
                column: "Attr14Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr15Id",
                table: "ItemMasters",
                column: "Attr15Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr16Id",
                table: "ItemMasters",
                column: "Attr16Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr17Id",
                table: "ItemMasters",
                column: "Attr17Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr18Id",
                table: "ItemMasters",
                column: "Attr18Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr19Id",
                table: "ItemMasters",
                column: "Attr19Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr1Id",
                table: "ItemMasters",
                column: "Attr1Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr2Id",
                table: "ItemMasters",
                column: "Attr2Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr3Id",
                table: "ItemMasters",
                column: "Attr3Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr4Id",
                table: "ItemMasters",
                column: "Attr4Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr5Id",
                table: "ItemMasters",
                column: "Attr5Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr6Id",
                table: "ItemMasters",
                column: "Attr6Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr7Id",
                table: "ItemMasters",
                column: "Attr7Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr8Id",
                table: "ItemMasters",
                column: "Attr8Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_Attr9Id",
                table: "ItemMasters",
                column: "Attr9Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_InventoryUnitId",
                table: "ItemMasters",
                column: "InventoryUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_ItemTypeId",
                table: "ItemMasters",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_PurUnitId",
                table: "ItemMasters",
                column: "PurUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_SalesUnit",
                table: "ItemMasters",
                column: "SalesUnit");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_UOMGroupId",
                table: "ItemMasters",
                column: "UOMGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMasters_VATId",
                table: "ItemMasters",
                column: "VATId");
        }
    }
}
