using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class Added_Item : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ERPCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsPurchasable = table.Column<bool>(type: "bit", nullable: false),
                    IsSaleable = table.Column<bool>(type: "bit", nullable: false),
                    IsInventoriable = table.Column<bool>(type: "bit", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ManageItemBy = table.Column<int>(type: "int", nullable: false),
                    ExpiredType = table.Column<int>(type: "int", nullable: true),
                    ExpiredValue = table.Column<int>(type: "int", nullable: true),
                    IssueMethod = table.Column<int>(type: "int", nullable: true),
                    CanUpdate = table.Column<bool>(type: "bit", nullable: false),
                    ItemTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UomGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryUOMId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurUOMId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesUOMId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Attr0Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr3Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr4Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr5Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr6Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr7Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr8Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr9Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr0Id",
                        column: x => x.Attr0Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr10Id",
                        column: x => x.Attr10Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr11Id",
                        column: x => x.Attr11Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr12Id",
                        column: x => x.Attr12Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr13Id",
                        column: x => x.Attr13Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr14Id",
                        column: x => x.Attr14Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr15Id",
                        column: x => x.Attr15Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr16Id",
                        column: x => x.Attr16Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr17Id",
                        column: x => x.Attr17Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr18Id",
                        column: x => x.Attr18Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr19Id",
                        column: x => x.Attr19Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr1Id",
                        column: x => x.Attr1Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr2Id",
                        column: x => x.Attr2Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr3Id",
                        column: x => x.Attr3Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr4Id",
                        column: x => x.Attr4Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr5Id",
                        column: x => x.Attr5Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr6Id",
                        column: x => x.Attr6Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr7Id",
                        column: x => x.Attr7Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr8Id",
                        column: x => x.Attr8Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_ItemAttributeValues_Attr9Id",
                        column: x => x.Attr9Id,
                        principalTable: "ItemAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_SystemDatas_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "SystemDatas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_UOMGroupDetails_InventoryUOMId",
                        column: x => x.InventoryUOMId,
                        principalTable: "UOMGroupDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_UOMGroupDetails_PurUOMId",
                        column: x => x.PurUOMId,
                        principalTable: "UOMGroupDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_UOMGroupDetails_SalesUOMId",
                        column: x => x.SalesUOMId,
                        principalTable: "UOMGroupDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_UOMGroups_UomGroupId",
                        column: x => x.UomGroupId,
                        principalTable: "UOMGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_VATs_VatId",
                        column: x => x.VatId,
                        principalTable: "VATs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr0Id",
                table: "Items",
                column: "Attr0Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr10Id",
                table: "Items",
                column: "Attr10Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr11Id",
                table: "Items",
                column: "Attr11Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr12Id",
                table: "Items",
                column: "Attr12Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr13Id",
                table: "Items",
                column: "Attr13Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr14Id",
                table: "Items",
                column: "Attr14Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr15Id",
                table: "Items",
                column: "Attr15Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr16Id",
                table: "Items",
                column: "Attr16Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr17Id",
                table: "Items",
                column: "Attr17Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr18Id",
                table: "Items",
                column: "Attr18Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr19Id",
                table: "Items",
                column: "Attr19Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr1Id",
                table: "Items",
                column: "Attr1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr2Id",
                table: "Items",
                column: "Attr2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr3Id",
                table: "Items",
                column: "Attr3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr4Id",
                table: "Items",
                column: "Attr4Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr5Id",
                table: "Items",
                column: "Attr5Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr6Id",
                table: "Items",
                column: "Attr6Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr7Id",
                table: "Items",
                column: "Attr7Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr8Id",
                table: "Items",
                column: "Attr8Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Attr9Id",
                table: "Items",
                column: "Attr9Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_InventoryUOMId",
                table: "Items",
                column: "InventoryUOMId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemTypeId",
                table: "Items",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_PurUOMId",
                table: "Items",
                column: "PurUOMId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SalesUOMId",
                table: "Items",
                column: "SalesUOMId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UomGroupId",
                table: "Items",
                column: "UomGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_VatId",
                table: "Items",
                column: "VatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
