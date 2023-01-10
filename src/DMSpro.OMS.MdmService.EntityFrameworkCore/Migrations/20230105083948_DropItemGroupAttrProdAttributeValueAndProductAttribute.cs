using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMSpro.OMS.MdmService.Migrations
{
    public partial class DropItemGroupAttrProdAttributeValueAndProductAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemGroupAttrs");

            migrationBuilder.DropTable(
                name: "ProdAttributeValues");

            migrationBuilder.DropTable(
                name: "ProductAttributes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    AttrName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AttrNo = table.Column<int>(type: "int", maxLength: 19, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HierarchyLevel = table.Column<int>(type: "int", maxLength: 19, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsProductCategory = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProdAttributeValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttrValName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ParentProdAttributeValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProdAttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdAttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdAttributeValues_ProdAttributeValues_ParentProdAttributeValueId",
                        column: x => x.ParentProdAttributeValueId,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProdAttributeValues_ProductAttributes_ProdAttributeId",
                        column: x => x.ProdAttributeId,
                        principalTable: "ProductAttributes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemGroupAttrs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Attr0 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr10 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr11 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr12 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr13 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr14 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr15 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr16 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr17 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr18 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr19 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr3 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr4 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr5 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr6 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr7 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr8 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attr9 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Dummy = table.Column<bool>(type: "bit", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ItemGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemGroupAttrs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ItemGroups_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "ItemGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr0",
                        column: x => x.Attr0,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr1",
                        column: x => x.Attr1,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr10",
                        column: x => x.Attr10,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr11",
                        column: x => x.Attr11,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr12",
                        column: x => x.Attr12,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr13",
                        column: x => x.Attr13,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr14",
                        column: x => x.Attr14,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr15",
                        column: x => x.Attr15,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr16",
                        column: x => x.Attr16,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr17",
                        column: x => x.Attr17,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr18",
                        column: x => x.Attr18,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr19",
                        column: x => x.Attr19,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr2",
                        column: x => x.Attr2,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr3",
                        column: x => x.Attr3,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr4",
                        column: x => x.Attr4,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr5",
                        column: x => x.Attr5,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr6",
                        column: x => x.Attr6,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr7",
                        column: x => x.Attr7,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr8",
                        column: x => x.Attr8,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemGroupAttrs_ProdAttributeValues_Attr9",
                        column: x => x.Attr9,
                        principalTable: "ProdAttributeValues",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr0",
                table: "ItemGroupAttrs",
                column: "Attr0");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr1",
                table: "ItemGroupAttrs",
                column: "Attr1");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr10",
                table: "ItemGroupAttrs",
                column: "Attr10");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr11",
                table: "ItemGroupAttrs",
                column: "Attr11");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr12",
                table: "ItemGroupAttrs",
                column: "Attr12");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr13",
                table: "ItemGroupAttrs",
                column: "Attr13");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr14",
                table: "ItemGroupAttrs",
                column: "Attr14");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr15",
                table: "ItemGroupAttrs",
                column: "Attr15");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr16",
                table: "ItemGroupAttrs",
                column: "Attr16");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr17",
                table: "ItemGroupAttrs",
                column: "Attr17");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr18",
                table: "ItemGroupAttrs",
                column: "Attr18");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr19",
                table: "ItemGroupAttrs",
                column: "Attr19");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr2",
                table: "ItemGroupAttrs",
                column: "Attr2");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr3",
                table: "ItemGroupAttrs",
                column: "Attr3");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr4",
                table: "ItemGroupAttrs",
                column: "Attr4");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr5",
                table: "ItemGroupAttrs",
                column: "Attr5");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr6",
                table: "ItemGroupAttrs",
                column: "Attr6");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr7",
                table: "ItemGroupAttrs",
                column: "Attr7");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr8",
                table: "ItemGroupAttrs",
                column: "Attr8");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_Attr9",
                table: "ItemGroupAttrs",
                column: "Attr9");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroupAttrs_ItemGroupId",
                table: "ItemGroupAttrs",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdAttributeValues_ParentProdAttributeValueId",
                table: "ProdAttributeValues",
                column: "ParentProdAttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdAttributeValues_ProdAttributeId",
                table: "ProdAttributeValues",
                column: "ProdAttributeId");
        }
    }
}
